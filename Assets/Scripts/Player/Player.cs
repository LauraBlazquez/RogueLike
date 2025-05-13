using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float moveSpeed = 3.5f;
    private PlayerControlls controls;
    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControlls();
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
        rb.velocity = movementInput * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log($"Player took {damage} damage. Health left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Player died!");
        Destroy(gameObject);
    }
}

