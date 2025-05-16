using UnityEngine;
using System.Collections;

public class Grenade : Bullet
{
    public float explosionRadius = 1.5f;
    public float explosionForce = 150f;
    private Rigidbody2D rb;
    private bool hasExploded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 target, float damage, string targetTag, string poolID, float launchSpeed)
    {
        Vector3 dir = (target - transform.position);
        float time = dir.magnitude / launchSpeed;
        Vector2 velocity = CalculateArcVelocity(transform.position, target, time);
        rb.velocity = velocity;
        this.damage = damage;
        this.poolID = poolID;
        this.targetTag = targetTag;
        hasExploded = false;

        StartCoroutine(AutoExplodeAfterTime(lifetime));
    }

    private Vector2 CalculateArcVelocity(Vector3 start, Vector3 end, float time)
    {
        Vector2 distance = end - start;
        float vx = distance.x / time;
        float vy = (distance.y - 0.5f * Physics2D.gravity.y * time * time) / time;
        return new Vector2(vx, vy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
            Explode();
    }

    private IEnumerator AutoExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!hasExploded)
            Explode();
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            var dmg = hit.GetComponent<IDamageable>();
            if (dmg != null)
                dmg.TakeDamage(damage);

            Rigidbody2D body = hit.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                Vector2 forceDir = (hit.transform.position - transform.position).normalized;
                body.AddForce(forceDir * explosionForce);
            }
        }
        hasExploded = true;
        ReturnToPool();
    }
}
