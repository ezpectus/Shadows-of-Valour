using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// This class is responsible for controlling a trap with spikes, which deals damage to the player when entering the area of ​​effect.

public class SpikeTrap : MonoBehaviour
{
    public int damage = 25;
    public float attackInterval = 1.5f;
    public float detectionRange = 2f; // Attack range 
    public Color gizmoColor = Color.red; // Color for Gizmos

    private bool playerInZone = false;
    private Coroutine attackCoroutine;

    private void Start()
    {
        if (!GetComponent<Collider2D>().isTrigger)
        {
           Debug.LogError("The SpikeTrap object must have a collider configured as a Trigger.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.TryGetComponent(out PlayerHealth playerHealth))
    {
        playerInZone = true;
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackPlayer(playerHealth));
        }
            Debug.Log("The player entered the attack zone!");
        }
}
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out _))
        {
            playerInZone = false;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            Debug.Log("The player left the attack zone.");
        }
    }



    private IEnumerator AttackPlayer(PlayerHealth playerHealth)
    {
        while (playerInZone)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Player received {damage} damage from spikes!");
            }
            else
            {
                Debug.LogWarning("PlayerHealth became null!");
                yield break;
            }

            yield return new WaitForSeconds(attackInterval);
        }

        attackCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
