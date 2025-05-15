using UnityEngine;
using System.Collections;

public class Grenade : Bullet
{
    public float explosionRadius = 1.5f;
    public float explosionForce = 300f;
    private Rigidbody2D rb;
    private bool hasExploded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FireParabola(Vector3 target, float damage, string targetTag, string poolID, float launchSpeed)
    {
        this.poolID = poolID;
        this.targetTag = targetTag;
        this.damage = damage;
        hasExploded = false;

        Vector2 dir = (target - transform.position);
        float time = dir.magnitude / launchSpeed;

        Vector2 velocity = CalculateArcVelocity(transform.position, target, time);
        rb.velocity = velocity;

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
        hasExploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            // aplicar daño
            var dmg = hit.GetComponent<IDamageable>();
            if (dmg != null)
                dmg.TakeDamage(damage);

            // empuje físico
            Rigidbody2D body = hit.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                Vector2 forceDir = (hit.transform.position - transform.position).normalized;
                body.AddForce(forceDir * explosionForce);
            }
        }
        //Debug.Log("Granada explotó...");
        ReturnToPool();
    }
}
