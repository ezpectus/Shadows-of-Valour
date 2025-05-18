using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class ShieldedSkeletonHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int currentHealth = 100;
    [SerializeField]
    private int maxHealth = 100;
    public float deathDelay = 3f;

    public Slider healthBar; // Полоска здоровья врага

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference; // События для хита и смерти

    private Animator animator;
    private bool isDead = false;
    public bool isInvincible = false; // Флаг для щита

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeHealth(maxHealth);

        // Инициализируем полоску здоровья, если она назначена
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;

        // Обновляем полоску здоровья, если она назначена
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead || isInvincible) return;

        currentHealth -= amount;
        Debug.Log("Shielded skeleton took damage: " + amount);

        animator.SetTrigger("TakeHit");

        // Обновляем полоску здоровья, если она назначена
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender); // Вызов события хита
        }
        else
        {
            Die(sender);
        }
    }

    private void Die(GameObject sender)
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Shielded skeleton is dying.");
        OnDeathWithReference?.Invoke(sender); // Вызов события смерти

        if (animator == null)
        {
            Debug.LogWarning("Animator not found. Destroying object immediately.");
            Destroy(gameObject);
            return;
        }

        // Проверка на повторное воспроизведение анимации
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_death"))
        {
            Debug.LogWarning("Animation already playing. Aborting.");
            return;
        }

        animator.SetTrigger("skeleton_death"); // Используем триггер skeleton_death
        animator.SetBool("isDead", true);
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_death") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return new WaitForSeconds(deathDelay); // Задержка перед удалением

        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
