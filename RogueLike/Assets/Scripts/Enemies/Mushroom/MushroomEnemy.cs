using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy
{
    [Header("Mushroom Settings")]
    public float detectionRange = 3f;
    public float explosionRange = 1.2f;
    public float speed = 2.5f;
    public float damage = 50f;
    public float patrolRadius = 3f;
    public float idlePauseTime = 1.5f;

    [Header("State Info")]
    public bool hasTransformed = false;
    public Vector2 lastKnownPlayerPosition;

    [HideInInspector] public Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        SwitchState(new CamouflagedState(this));
    }

    public bool IsPlayerInRange(float range)
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= range;
    }

    public void MoveToward(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        animator.SetFloat("X", transform.position.x);
        animator.SetFloat("Y", transform.position.y);
    }

    public void Explode()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, explosionRange, LayerMask.GetMask("Player"));
        if (hit)
        {
            hit.GetComponent<IDamageable>()?.TakeDamage(damage);
        }
        Destroy(gameObject, 1f);
        Destroy(this);
    }
}


