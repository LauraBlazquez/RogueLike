using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeathState : EnemyState
{
    private TurretEnemy turret;

    public TurretDeathState(TurretEnemy turret) : base(turret)
    {
        this.turret = turret;
    }

    public override void EnterState()
    {
        turret.animator.SetTrigger("Dead");
        turret.Die();
    }
}

