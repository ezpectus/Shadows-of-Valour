using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Jump Settings")]
    public int maxJumps = 2;
    public float jumpForce = 5f;
    public float jumpCooldown = 0.2f;
    public float jumpRecoveryTime = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    private int currentJumps;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool isRecovering = false;
    private bool canJump = true;

    void Start()
    {
        currentJumps = maxJumps;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jumps if grounded and not jumping
        if (isGrounded && !isJumping)
        {
            currentJumps = maxJumps;
        }

        // Horizontal movement
        float horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        // Animation control
        bool isMoving = Mathf.Abs(horizontalMove) > 0.1f;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isGrounded", isGrounded);

        // Flip the player
        if (horizontalMove > 0 && !isFacingRight)
            Flip();
        else if (horizontalMove < 0 && isFacingRight)
            Flip();

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && currentJumps > 0 && canJump)
        {
            StartCoroutine(JumpCooldown());
            Jump();
            currentJumps--;

            if (currentJumps == 0 && !isRecovering)
            {
                StartCoroutine(RestoreJumps());
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump()
    {
        if (!isJumping) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("isJumping", true);
            StartCoroutine(HandleLanding());
        }
    }


    // Reset jump animation after delay if grounded
    private IEnumerator HandleLanding()
    {
        yield return new WaitForSeconds(1f);
        if (isGrounded)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private IEnumerator RestoreJumps()
    {
        isRecovering = true;
        yield return new WaitForSeconds(jumpRecoveryTime);
        currentJumps = maxJumps;
        isRecovering = false;
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
