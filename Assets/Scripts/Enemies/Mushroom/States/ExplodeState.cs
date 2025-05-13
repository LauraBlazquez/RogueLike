using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeState : EnemyState
{
    private float explodeDelay = 0.3f;
    private float timer;
    private MushroomEnemy mushroom;

    public ExplodeState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.animator.SetTrigger("Explode");
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= explodeDelay)
        {
            mushroom.Explode();
        }
    }
}

