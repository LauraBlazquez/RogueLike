using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamouflagedState : EnemyState
{
    private MushroomEnemy mushroom;

    public CamouflagedState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if (mushroom.IsPlayerInRange(mushroom.detectionRange))
        {
            mushroom.SwitchState(new TransformState((MushroomEnemy)mushroom));
        }
        if (mushroom.currentHealth <= 0)
        {
            mushroom.SwitchState(new MushroomDeathState(mushroom));
        }
    }
}