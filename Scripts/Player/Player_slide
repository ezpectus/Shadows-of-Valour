using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;



public class SlideMechanic : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideDuration = 0.5f;
    public float slideDistance = 5f;
    public float slideCooldown = 2f;
    public int maxSlides = 2;
    public KeyCode slideKey = KeyCode.LeftShift;

    private int currentSlides;
    private bool isSliding;
    private bool isCooldown;
    private Vector2 slideDirection;
    private Rigidbody2D rb;
    private Animator animator; // Для управления анимациями

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSlides = maxSlides;
    }

    private void Update()
    {
        // Проверяем ввод и доступные слайды
        if (Input.GetKeyDown(slideKey) && !isSliding && currentSlides > 0)
        {
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        currentSlides--;

        // Рассчитываем направление слайда
        slideDirection = new Vector2(transform.localScale.x, 0).normalized;

        // Запускаем анимацию слайда
        animator.SetBool("isSliding", true);

        float slideSpeed = slideDistance / slideDuration;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            rb.velocity = slideDirection * slideSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isSliding = false;

        // Отключаем анимацию слайда
        animator.SetBool("isSliding", false);

        // Если слайды закончились, запускаем перезарядку
        if (currentSlides == 0 && !isCooldown)
        {
            StartCoroutine(SlideCooldown());
        }
    }

    private IEnumerator SlideCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(slideCooldown);
        currentSlides = maxSlides;
        isCooldown = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(transform.localScale.x * slideDistance, 0, 0));
    }
}
