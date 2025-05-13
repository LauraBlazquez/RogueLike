using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : EnemyState
{
    private float transformDuration = 1f;
    private float timer;
    private MushroomEnemy mushroom;

    public TransformState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.hasTransformed = true;
        mushroom.animator.SetTrigger("Transform");
        timer = 0f;
    }


    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= transformDuration)
        {
            mushroom.SwitchState(new ChaseState(mushroom));
        }
    }
}