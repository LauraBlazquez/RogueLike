using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : EnemyState
{
    private TurretEnemy turret;

    public TurretAttackState(TurretEnemy turret) : base(turret)
    {
        this.turret = turret;
    }

    public override void EnterState()
    {
        turret.animator.SetTrigger("Shoot");
    }

    public override void UpdateState()
    {
        if (!turret.IsPlayerInRange() || Player.IsDead)
        {
            turret.SwitchState(new TurretIdleState(turret));
            turret.animator.ResetTrigger("Shoot");
            return;
        }
        if (turret.health <= 0)
        {
            turret.SwitchState(new TurretDeathState(turret));
        }
        turret.LookAtPlayer();
        turret.Shoot();
    }
}

