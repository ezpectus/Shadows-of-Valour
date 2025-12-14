using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Точка, куда игрок возвращается после смерти
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth.IsDead)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Перемещаем игрока в точку возрождения
        transform.position = respawnPoint.position;

        // Восстанавливаем состояние игрока
        playerHealth.Revive();
    }
}
