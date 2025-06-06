using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float moveSpeed = 3.5f;
    public HealthBar healthBar;
    private PlayerControlls controls;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float idleTimer = 0;
    private float sleepThreshold = 3;
    private bool isSleeping = false;
    private bool canMove = true;
    private bool isDead = false;
    public static bool IsDead { get; set;} = false;
    public static bool IsFlipped { get; private set;} = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControlls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => movementInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.Gameplay.Movement.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled -= ctx => movementInput = Vector2.zero;
        controls.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = movementInput * moveSpeed;

        bool isWalking = movementInput != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            animator.SetFloat("X", movementInput.x);
            animator.SetFloat("Y", movementInput.y);
            Vector3 localScale = transform.localScale;
            if (movementInput.x < 0)
            {
                localScale.x = -Mathf.Abs(localScale.x);
                IsFlipped = true;
            }
            else if (movementInput.x > 0)
            {
                localScale.x = Mathf.Abs(localScale.x);
                IsFlipped = false;
            }
            transform.localScale = localScale;

            idleTimer = 0;
            isSleeping = false;
        }
        else
        {
            idleTimer += Time.fixedDeltaTime;

            if(!isSleeping && idleTimer >= sleepThreshold)
            {
                animator.SetTrigger("Sleep");
                isSleeping = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth);
        if (currentHealth  > 0) animator.SetTrigger("Hurt");
        else Die();      
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        IsDead = true;
        animator.SetTrigger("Dead");
        rb.velocity = Vector2.zero;
        canMove = false;

        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GameOver();
    }
}

