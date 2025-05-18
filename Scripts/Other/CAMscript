using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMscript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;

        transform.position = temp;
        
    }
}
