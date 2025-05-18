
using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Range(0f, 1f)]
    [SerializeField] private float spawnChance = 0.6f;

    private void Start()
    {
        SpawnEnemiesProcedurally();
    }

    private void SpawnEnemiesProcedurally()
    {
        foreach (Transform point in spawnPoints)
        {
            if (Random.value < spawnChance)
            {
                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                GameObject instance = Instantiate(prefab, point.position, Quaternion.identity);

                // Optionally assign an ID if needed
                var idComponent = instance.GetComponent<EnemyID>();
                if (idComponent != null)
                    Debug.Log($"Spawned enemy with ID: {idComponent.id}");
            }
        }
    }
}



