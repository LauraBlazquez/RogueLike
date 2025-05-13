using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float health = 100f;

    [Header("State Machine")]
    protected EnemyState currentState;

    [Header("Player Reference")]
    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(EnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy took {damage} damage, health left: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
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
