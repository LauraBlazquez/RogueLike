using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "FlameThrower", menuName = "Weapons/FlameThrower")]
public class FlameThrower : WeaponData
{
    public override void UseWeapon(GameObject user)
    {
        var behaviour = user.GetComponentInChildren<FlameThrowerBehaviour>();
        if (behaviour != null)
        {
            behaviour.Initialize(this);
            behaviour?.StartFiring();
        }
    }

    public override void StopUse(GameObject user)
    {
        var behaviour = user.GetComponentInChildren<FlameThrowerBehaviour>();
        behaviour?.StopFiring();

    }
}
