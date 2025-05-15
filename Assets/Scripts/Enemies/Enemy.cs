using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float health = 100f;
    private float currentHealth;
    private HealthBar healthBar;

    [Header("State Machine")]
    protected EnemyState currentState;

    [Header("Player Reference")]
    protected Transform player;

    protected virtual void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        currentHealth = health;
        healthBar.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (player != null) currentState?.UpdateState();
    }

    public void SwitchState(EnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.gameObject.SetActive(true);
        healthBar.UpdateHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject, 1f);
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public virtual void MoveToward(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
