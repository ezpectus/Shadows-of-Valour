using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerPrayer : MonoBehaviour
{
    [SerializeField] private KeyCode prayerKey = KeyCode.X; 
    [SerializeField] private float prayerCooldown = 5f; 
    private bool canPray = true;

    private PlayerHealth playerHealth; // Ссылка на компонент здоровья игрока
    private Animator animator; // Ссылка на анимацию

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(prayerKey) && canPray)
        {
            TryPray();
        }
    }

    private void TryPray()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f); // Радиус проверки
        foreach (var hit in hitColliders)
        {
            Statue statue = hit.GetComponent<Statue>();
            if (statue != null)
            {
                float distance = Vector3.Distance(transform.position, statue.transform.position);
                if (distance <= statue.GetInteractionRadius())
                {
                    canPray = false;
                    animator.SetTrigger("Pray"); // Анимация молитвы
                    statue.StartPrayer(OnPrayerComplete); // Запускаем молитву у статуи
                    return;
                }
            }
        }
        Debug.Log("No statue found in range.");
    }

    private void OnPrayerComplete()
    {
        playerHealth.FullRestore(); // Полностью восстанавливаем игрока
        Debug.Log("Prayer completed: Player fully restored.");
        Invoke(nameof(ResetPrayerCooldown), prayerCooldown); // Устанавливаем кулдаун
    }

    private void ResetPrayerCooldown()
    {
        canPray = true;
    }

    public void SetPrayerKey(KeyCode newKey)
    {
        prayerKey = newKey;
        Debug.Log($"Prayer key changed to: {newKey}");
    }
}
