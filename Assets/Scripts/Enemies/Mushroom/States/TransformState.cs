using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TransformState : EnemyState
{
    private float transformDuration = 1f;
    private float timer = 0f;
    private MushroomEnemy mushroom;

    public TransformState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.hasTransformed = true;
        mushroom.animator.SetTrigger("Transform");
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= transformDuration)
        {
            mushroom.SwitchState(new ChaseState(mushroom));
        }
        if (mushroom.currentHealth <= 0)
        {
            mushroom.SwitchState(new MushroomDeathState(mushroom));
        }
    }
}