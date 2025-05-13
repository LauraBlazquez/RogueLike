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
        // Posiblemente iniciar animación de alerta
    }

    public override void UpdateState()
    {
        if (!turret.IsPlayerInRange())
        {
            turret.SwitchState(new TurretIdleState(turret));
            return;
        }

        turret.LookAtPlayer();
        turret.Shoot();
    }

    public override void ExitState() { }
}

