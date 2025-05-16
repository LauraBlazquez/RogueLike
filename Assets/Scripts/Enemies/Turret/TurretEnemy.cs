using UnityEngine;

public class TurretEnemy : Enemy
{
    [Header("Turret Settings")]
    public float detectionRange = 8f;
    public float fireRate = 1f;
    public float damage = 10f;
    public Transform firePoint;
    public Transform turretHead;
    public GenericPool turretPool;

    private float fireCooldown;
    [HideInInspector] public Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        SwitchState(new TurretIdleState(this));
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        currentState?.UpdateState();
    }

    public void LookAtPlayer()
    {
        if (player == null) return;
        Vector2 dir = player.position - turretHead.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        turretHead.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public bool IsPlayerInRange()
    {
        if(player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    public void Shoot()
    {
        animator.SetFloat("X", player.position.x);
        animator.SetFloat("Y", player.position.y);
        spriteRenderer.flipX = player.position.x < 0;
        if (fireCooldown <= 0f)
        {
            if (turretPool == null)
            {
                Debug.LogWarning("No Enemy Bullet Pool found!");
                return;
            }

            GameObject bulletGO = turretPool.GetBullet();
            if (bulletGO != null)
            {
                bulletGO.transform.position = firePoint.position;
                bulletGO.transform.rotation = firePoint.rotation;

                Vector2 direction = (player.position - firePoint.position).normalized;
                bulletGO.GetComponent<Bullet>().Fire(direction, damage, "Player", "Turret");
            }
            animator?.SetTrigger("Shoot");
            fireCooldown = 1f / fireRate;
        }
    }

    private void LateUpdate()
    {
        fireCooldown -= Time.deltaTime;
    }
}
