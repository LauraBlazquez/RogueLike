using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : EnemyState
{
    private TurretEnemy turret;

    public TurretIdleState(TurretEnemy turret) : base(turret)
    {
        this.turret = turret;
    }

    public override void EnterState() 
    {
        
    }

    public override void UpdateState()
    {
        if (turret.IsPlayerInRange())
        {
            turret.SwitchState(new TurretAttackState(turret));
        }
        if (turret.health <= 0)
        {
            turret.SwitchState(new TurretDeathState(turret));
        }
    }
}
