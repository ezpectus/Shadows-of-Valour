using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine.UI;



public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public float deathDelay = 3f; // Delay time before removing the enemy
    public Slider healthBar; // Enemy's health bar

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private Animator animator;
    private bool isDead = false;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeHealth(maxHealth);

        // Initialize the health bar, if it is assigned
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

        // Update the health bar, if it is assigned
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) return;

        currentHealth -= amount;

        // Update the health bar, if it is assigned
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            Die(sender);
        }
    }

    private void Die(GameObject sender)
    {
        var idComponent = GetComponent<EnemyID>();
        if (idComponent != null)
        {
            GameManager.Instance.MarkEnemyAsKilled(idComponent.id);
        }

        if (isDead) return;

        isDead = true;

        Debug.Log("Enemy is dying.");
        OnDeathWithReference?.Invoke(sender);

        // Notify GameManager that the enemy is destroyed
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        if (animator == null)
        {
            Debug.LogWarning("Animator not found. Destroying object immediately.");
            Destroy(gameObject);
            return;
        }

        // Check for repeated animation playback
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_death"))
        {
            Debug.LogWarning("Animation already playing. Aborting.");
            return;
        }

        animator.SetTrigger("skeleton_death");
        animator.SetBool("isDead", true);
        StartCoroutine(WaitForDeathAnimation());
    }

    private void OnDestroy()
    {
        if (!isDead && GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }
    }


    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_death") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return new WaitForSeconds(deathDelay); // Use the delay time setting

        Destroy(gameObject);
    }
   

    public bool IsDead()
    {
        return isDead;
    }
}
