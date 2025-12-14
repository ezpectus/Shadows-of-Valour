using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BackgroundFollow : MonoBehaviour
{
    public Transform player;  
    public float followSpeed = 2f; 

    private Vector3 startOffset;

    void Start()
    {
        startOffset = transform.position - player.position;
    }

    void Update()
    {
        
        Vector3 targetPosition = new Vector3(player.position.x + startOffset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
