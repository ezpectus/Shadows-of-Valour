using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawner : MonoBehaviour
{
    public GameObject item;

    void Start()
    {
        if (Random.value > 0.5f) // 50% шанс
        {
            item.SetActive(true);
        }
        else
        {
            item.SetActive(false);
        }
    }
}
