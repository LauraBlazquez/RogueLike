using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Sniper", menuName = "Weapons/Sniper")]
public class Sniper : WeaponData
{
    public float bulletSpeed;
    public float spawnOffset;

    public override void UseWeapon(GameObject owner)
    {
        if (poolSO == null) return;

        var pool = GenericPool.GetPool(poolSO.poolID);
        if (pool == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPosition.z = 0f;

        Vector3 direction = (mouseWorldPosition - owner.transform.position).normalized;
        Vector3 spawnPosition = owner.transform.position + direction * spawnOffset;

        GameObject bulletGO = pool.GetBullet();
        if (bulletGO != null)
        {
            bulletGO.transform.position = spawnPosition;
            bulletGO.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Fire(direction, damage, "Enemy", "Sniper", bulletSpeed);
        }
    }
}
