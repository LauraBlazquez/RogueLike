using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GrenadeLauncher", menuName = "Weapons/GrenadeLauncher")]
public class GrenadeLauncher : WeaponData
{
    public float launchSpeed = 10f;
    public float maxRange = 10f;

    public override void UseWeapon(GameObject owner)
    {
        if (poolSO == null)
        {
            Debug.LogWarning("No grenade pool assigned.");
            return;
        }
        
        var pool = GenericPool.GetPool(poolSO.poolID);
        if (pool == null)
        {
            Debug.LogWarning($"GrenadeLauncher: No active pool found with ID: {poolSO.poolID}");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        Vector3 spawnPos = owner.transform.position;
        float distance = Vector3.Distance(spawnPos, mouseWorldPos);

        if (distance > maxRange)
        {
            mouseWorldPos = spawnPos + (mouseWorldPos - spawnPos).normalized * maxRange;
        }

        GameObject grenadeGO = pool.GetBullet();
        if (grenadeGO != null)
        {
            grenadeGO.transform.position = spawnPos;

            Grenade grenade = grenadeGO.GetComponent<Grenade>();
            grenade.FireParabola(mouseWorldPos, damage, "Enemy", "Grenade", launchSpeed);
        }
    }
}
