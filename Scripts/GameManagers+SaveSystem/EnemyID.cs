using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[DisallowMultipleComponent]
public class EnemyID : MonoBehaviour
{
    [Tooltip("Unique enemy ID. It can be set manually, or it will be generated automatically.")]
    public string id;

    private void Awake()
    {
        // ID generation only if it is not set yet
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this); // save the ID in the inspector
#endif
        }
    }

    private void Start()
    {
        // Check: if the enemy has already been killed - destroy it
        if (GameManager.Instance != null && GameManager.Instance.IsEnemyAlreadyKilled(id))
        {
            Debug.Log($"[EnemyID] The enemy with ID {id} has already been killed. Deleting the object.");
            Destroy(gameObject);
        }
    }
}
