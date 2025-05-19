using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var weaponManager = other.GetComponentInChildren<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.UnlockWeapon(weaponData);
            }
            Destroy(gameObject);
        }
    }
}
