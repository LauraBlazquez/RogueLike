using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GrenadeLauncher", menuName = "Weapons/GrenadeLauncher")]
public class GrenadeLauncher : WeaponData
{
    public float launchSpeed;
    public float maxRange;

    public override void UseWeapon(GameObject owner)
    {
        if (poolSO == null) return; 

        var pool = GenericPool.GetPool(poolSO.poolID);
        if (pool == null) return; 

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        Vector3 spawnPos = owner.transform.position;
        float distance = Vector3.Distance(spawnPos, mouseWorldPos);

        if (distance > maxRange) distance = maxRange;

        GameObject grenadeGO = pool.GetBullet();
        if (grenadeGO != null)
        {
            grenadeGO.transform.position = spawnPos;
            Grenade grenade = grenadeGO.GetComponent<Grenade>();
            grenade.Fire(mouseWorldPos, damage, "Enemy", "Grenade", launchSpeed);
        }
    }
}
