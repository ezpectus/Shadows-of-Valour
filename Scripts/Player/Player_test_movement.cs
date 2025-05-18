using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class b : MonoBehaviour
{


    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Receipt of direction of movement 
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Setting the Speed ??parameter to control walking animations 
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        // Attack 
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }
}


