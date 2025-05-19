using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeState : EnemyState
{
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
        mushroom.Explode();
    }
}

