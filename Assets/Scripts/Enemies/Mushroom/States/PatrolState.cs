using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private Vector3 patrolTarget;
    private float aproxLastPlayerPosition = 0.2f;
    private MushroomEnemy mushroom;

    public PatrolState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        SetNewPatrolPoint();
        mushroom.animator.SetBool("isWalking", true);
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(mushroom.transform.position, patrolTarget) <= aproxLastPlayerPosition)
        {
            mushroom.SwitchState(new MushroomIdleState((MushroomEnemy)mushroom));
        }
        else
        {
            mushroom.MoveToward(patrolTarget);
        }
        if (mushroom.IsPlayerInRange(mushroom.detectionRange))
        {
            mushroom.SwitchState(new ChaseState(mushroom));
        }
        if (mushroom.health <= 0)
        {
            mushroom.SwitchState(new MushroomDeathState(mushroom));
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * mushroom.patrolRadius;
        patrolTarget = mushroom.lastKnownPlayerPosition + new Vector2(randomOffset.x, randomOffset.y);
    }
}

