using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using static UnityEngine.RuleTile.TilingRuleOutput;



public class Boss : MonoBehaviour
{
    public UnityEngine.Transform rayCast;
    public LayerMask raycastMask;
    public float walkSpeed = 3.5f;
    public float detectionRadius = 7f;
    public float attackDistance = 1.5f;
    public int attackDamage = 20;

    [Header("Attack Timing Settings")]
    public float attackPrepareTime = 0.3f;
    public float attackExecutionTime = 0.5f;
    public float attackCooldown = 1.2f;

    [Header("Initial Facing Direction")]
    public bool isFacingLeft = true;

    private Animator animator;
    private UnityEngine.Transform target;
    private bool isAttacking;
    private BossHealth bossHealth;
    private bool isTakingHit = false;
    private bool isDead = false;
    private Coroutine attackCoroutine;

    private enum BossState { Idle, Walking, Attacking, TakingHit, Dead }
    private BossState currentState = BossState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        FaceDirection(isFacingLeft);
    }

    private void Update()
    {
        if (isDead || (bossHealth != null && bossHealth.IsDead()))
        {
            SetState(BossState.Dead);
            return;
        }

        if (currentState == BossState.TakingHit || currentState == BossState.Dead || isAttacking)
            return;

        switch (currentState)
        {
            case BossState.Idle:
                if (CheckForPlayer())
                    StartCoroutine(StartWalkingAfterDelay());
                break;
            case BossState.Walking:
                WalkToPlayer();
                break;
        }
    }

    private void SetState(BossState newState)
    {
        if (currentState == newState) return;
        Debug.Log($"State Change: {currentState} -> {newState}");
        currentState = newState;

        ResetAnimatorBools(); // Перед сменой состояния очищаем анимации

        switch (newState)
        {
            case BossState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("Walk", false);
                break;
            case BossState.Walking:
                animator.SetBool("Walk", true);
                animator.SetBool("isIdle", false);
                break;
            case BossState.Attacking:
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
                animator.SetTrigger("Attack");
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(PerformAttack());
                break;
            case BossState.TakingHit:
                animator.SetTrigger("TakeHit");
                break;
            case BossState.Dead:
                animator.SetTrigger("Boss_Death");
                StartCoroutine(WaitForDeathAnimation());
                break;
        }
    }

    private void ResetAnimatorBools()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
        animator.SetBool("isIdle", false);
    }

    private IEnumerator StartWalkingAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        SetState(BossState.Walking);
    }

    private void WalkToPlayer()
    {
        if (target == null || Vector2.Distance(transform.position, target.position) > detectionRadius)
        {
            SetState(BossState.Idle);
            return;
        }

        FaceTarget(target.position.x);

        if (Vector2.Distance(transform.position, target.position) <= attackDistance)
        {
            StartAttack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), walkSpeed * Time.deltaTime);
        }
    }

    private void StartAttack()
    {
        if (isAttacking || isTakingHit || isDead || currentState == BossState.Attacking)
            return;

        isAttacking = true;
        SetState(BossState.Attacking);
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(attackPrepareTime);

        Collider2D hit = Physics2D.OverlapCircle(rayCast.position, attackDistance, raycastMask);
        if (hit != null && hit.TryGetComponent(out IHealth playerHealth))
        {
            yield return new WaitForSeconds(attackExecutionTime);
            if (!isDead && !isTakingHit && currentState == BossState.Attacking && Vector2.Distance(transform.position, target.position) <= attackDistance)
            {
                playerHealth.GetHit(attackDamage, gameObject);
                Debug.Log("Attack hit the player!");
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;

        if (!isDead)
            SetState(CheckForPlayer() ? BossState.Walking : BossState.Idle);

        attackCoroutine = null; 
    }

    public void TriggerHit()
    {
        if (isTakingHit || isDead)
            return;

        isTakingHit = true;
        SetState(BossState.TakingHit);
        StartCoroutine(ResetHitState());
    }

    private IEnumerator ResetHitState()
    {
        yield return new WaitForSeconds(0.5f);
        isTakingHit = false;

        if (!isAttacking && !isDead)
            SetState(CheckForPlayer() ? BossState.Walking : BossState.Idle);
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private bool CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, raycastMask);
        if (player != null)
        {
            target = player.transform;
            FaceTarget(target.position.x);
            return true;
        }
        target = null;
        return false;
    }

    private void FaceTarget(float targetX)
    {
        if ((targetX > transform.position.x && isFacingLeft) || (targetX < transform.position.x && !isFacingLeft))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FaceDirection(bool faceLeft)
    {
        if (faceLeft != isFacingLeft)
        {
            Flip();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
