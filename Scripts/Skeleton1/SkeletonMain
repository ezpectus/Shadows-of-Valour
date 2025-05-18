using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;



public class Skeleton1 : MonoBehaviour
{
    #region Public Variables
    public Transform pointA;
    public Transform pointB;
    public Transform rayCast;
    public LayerMask raycastMask;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRadius = 5f;
    public float attackDistance = 1.5f;
    public float changeTargetDistance = 0.9f;
    public int attackDamage = 10;

    [Header("Attack Timing Settings")]
    public float attackPrepareTime = 0.3f;
    public float attackExecutionTime = 0.5f;
    public float attackCooldown = 1f;

    [Header("Initial Facing Direction")]
    public bool isFacingLeft = true;
    #endregion

    #region Private Variables
    private Animator anim;
    private Transform target;
    private Transform currentTarget;
    private bool isAttacking;
    private EnemyHealth enemyHealth;
    private bool isDead;

    private enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Dead
    }
    private State currentState = State.Patrolling;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentTarget = pointA;
        enemyHealth = GetComponent<EnemyHealth>();
        FaceDirection(isFacingLeft);
    }

    private void Update()
    {
        if (enemyHealth.IsDead())
        {
            if (currentState != State.Dead)
            {
                HandleDeath();
            }
            return;
        }

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Attacking:
                HandleAttack();
                break;
        }
    }

    private void Patrol()
    {
        anim.SetBool("Walk", true);
        anim.SetBool("Attack", false);

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget.position) <= changeTargetDistance)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            // Заменяем метод Flip() на FaceDirection(), чтобы корректно менять направление врага
            FaceDirection(currentTarget == pointA);
        }

        if (CheckForPlayer())
        {
            currentState = State.Chasing;
        }
    }

    private void ChasePlayer()
    {
        anim.SetBool("Walk", true);
        anim.SetBool("Attack", false);

        if (target == null || Vector2.Distance(transform.position, target.position) > detectionRadius)
        {
            target = null;
            currentState = State.Patrolling;
            return;
        }

        FaceTarget(target.position.x); // Корректируем направление врага перед атакой

        if (Vector2.Distance(transform.position, target.position) <= attackDistance)
        {
            currentState = State.Attacking;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        }
    }

    private void HandleAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        anim.SetBool("Walk", false);
        anim.SetTrigger("Attack");

        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(attackPrepareTime);

        Collider2D hit = Physics2D.OverlapCircle(rayCast.position, attackDistance, raycastMask);
        if (hit != null && hit.TryGetComponent(out IHealth playerHealth))
        {
            yield return new WaitForSeconds(attackExecutionTime);
            playerHealth.GetHit(attackDamage, gameObject);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;

        if (target != null && Vector2.Distance(transform.position, target.position) <= detectionRadius)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Patrolling;
        }
    }

    private void HandleDeath()
    {
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("skeleton_death"); // Используем триггер skeleton_death
        anim.SetBool("Dead", true);

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorStateInfo(0).IsName("skeleton_death") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return new WaitForSeconds(3f); // Задержка перед удалением
      
        Destroy(gameObject);
    }

    private bool CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, raycastMask);
        if (player != null)
        {
            target = player.transform;
            FaceTarget(target.position.x); // Добавляем вызов FaceTarget здесь, чтобы враг всегда смотрел на игрока
            return true;
        }

        target = null;
        return false;
    }

    private void FaceTarget(float targetX)
    {
        if ((targetX > transform.position.x && isFacingLeft) ||
            (targetX < transform.position.x && !isFacingLeft))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FaceDirection(bool faceLeft)
    {
        if (faceLeft != isFacingLeft)
        {
            isFacingLeft = faceLeft;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
