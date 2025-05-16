using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private MushroomEnemy mushroom;

    public ChaseState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.animator.SetBool("isWalking", true);
    }


    public override void UpdateState()
    {
        mushroom.lastKnownPlayerPosition = mushroom.GetPlayerTransform().position;

        if (mushroom.IsPlayerInRange(mushroom.explosionRange))
        {
            mushroom.SwitchState(new ExplodeState(mushroom));
        }
        else if (!mushroom.IsPlayerInRange(mushroom.detectionRange))
        {
            mushroom.SwitchState(new PatrolState(mushroom));
        }
        else
        {
            mushroom.MoveToward(mushroom.GetPlayerTransform().position);
        }
        if (mushroom.currentHealth <= 0)
        {
            mushroom.SwitchState(new MushroomDeathState(mushroom));
        }
    }
}

