using UnityEngine;

public class MushroomIdleState : EnemyState
{
    private float timer;
    private MushroomEnemy mushroom;

    public MushroomIdleState(MushroomEnemy mushroom) : base(mushroom)
    {
        this.mushroom = mushroom;
    }

    public override void EnterState()
    {
        mushroom.animator.SetBool("isWalking", false);
    }


    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= mushroom.idlePauseTime)
        {
            mushroom.SwitchState(new PatrolState((MushroomEnemy)mushroom));
        }

        if (mushroom.IsPlayerInRange(mushroom.detectionRange))
        {
            mushroom.SwitchState(new ChaseState(mushroom));
        }
        if (mushroom.currentHealth <= 0)
        {
            mushroom.SwitchState(new MushroomDeathState(mushroom));
        }
    }
}
