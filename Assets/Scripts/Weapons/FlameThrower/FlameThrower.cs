using UnityEngine;

[CreateAssetMenu(fileName = "FlameThrower", menuName = "Weapons/FlameThrower")]
public class FlameThrower : WeaponData
{
    public override void UseWeapon(GameObject user)
    {
        Debug.Log("Burning all");
    }
}
