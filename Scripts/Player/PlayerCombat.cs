using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombat : MonoBehaviour
{
    [Header("Attack 1 Settings")]
    public int attackDamage = 20;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.5f;

    [Header("Attack 2 Settings")]
    public float attack2Duration = 0.5f; // Длительность атаки 2
    public float attack2Distance = 2f;   // Дистанция во время атаки 2
    public KeyCode attack2Key = KeyCode.Mouse1; // Клавиша для атаки 2 (правая кнопка мыши)
    public int attack2Damage = 30;       // Урон атаки 2

    private Animator animator;
    private bool isAttacking = false;
    private bool isAttacking2 = false; // Булеан для атаки 2
    private float lastAttackTime = 0;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Проверка на null, чтобы избежать NullReferenceException
        if (animator == null || rb == null)
        {
            Debug.LogError("Не все компоненты инициализированы правильно.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack1());
        }

        if (Input.GetKeyDown(attack2Key) && !isAttacking2)
        {
            StartCoroutine(Attack2());
        }
    }

    private IEnumerator Attack1()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        animator.SetBool("isAttacking", true);
        animator.SetTrigger("Attack");

        // Наносим урон врагам в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            IHealth enemyHealth = enemy.GetComponent<IHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetHit(attackDamage, gameObject);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private IEnumerator Attack2()
    {
        isAttacking2 = true;
        animator.SetBool("isAttacking2", true); // Устанавливаем анимацию для атаки 2
        animator.SetTrigger("Attack2");

        Vector2 attackDirection = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0).normalized;
        float attackSpeed = attack2Distance / attack2Duration;

        float elapsedTime = 0f;
        while (elapsedTime < attack2Duration)
        {
            rb.velocity = attackDirection * attackSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Останавливаем движение после атаки

        // Наносим урон врагам в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            IHealth enemyHealth = enemy.GetComponent<IHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetHit(attack2Damage, gameObject);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking2 = false; // Сбрасываем флаг атаки 2
        animator.SetBool("isAttacking2", false); // Останавливаем анимацию для атаки 2
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
