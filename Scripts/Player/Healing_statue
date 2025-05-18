using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Statue : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 3f; // Radius of the statue

    public void StartPrayer(System.Action onPrayerComplete)
    {
        Debug.Log("Player interacted with the statue.");
        StartCoroutine(PrayerRoutine(onPrayerComplete));
    }

    private System.Collections.IEnumerator PrayerRoutine(System.Action onPrayerComplete)
    {
        Debug.Log("Statue is performing prayer...");
        yield return new WaitForSeconds(2f); // Animation delay

       
        onPrayerComplete?.Invoke();
    }

    // To debug the radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public float GetInteractionRadius()
    {
        return interactionRadius;
    }
}
