using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float health = 100f;
    [HideInInspector] public float currentHealth;
    public GameObject coinPrefab;
    private int coinAmount;
    private HealthBar healthBar;

    [Header("State Machine")]
    protected EnemyState currentState;

    [Header("Player Reference")]
    protected Transform player;

    protected virtual void Start()
    {
        coinAmount = Random.Range(1, 4);
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
    }

    public virtual void Die()
    {
        SpawnCoins();
        Destroy(gameObject, 1f);
    }

    private void SpawnCoins()
    {
        if (coinPrefab == null) return;
        for (int i = 0; i < coinAmount; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }
}
