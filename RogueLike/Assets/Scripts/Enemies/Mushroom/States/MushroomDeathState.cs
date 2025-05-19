using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MushroomDeathState : EnemyState
{
    private MushroomEnemy mushroom;

    public MushroomDeathState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.animator.SetTrigger("Dead");
        mushroom.Die();
    }
}

