using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;  
    public float moveDistance = 3f;  
    public float waitTime = 1f; 

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = true;
    private bool isWaiting = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!isWaiting)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPos) < 0.01f)
                {
                    isWaiting = true;
                    yield return new WaitForSeconds(waitTime);
                    movingUp = !movingUp;
                    targetPos = movingUp ? startPos + Vector3.up * moveDistance : startPos;
                    isWaiting = false;
                }
            }
            yield return null;
        }
    }
}
