using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private CoinManager coinManager;

    private void Start()
    {
        coinManager = CoinManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && coinManager.currentCoins >= weaponData.price)
        {
            var weaponManager = other.GetComponentInChildren<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.UnlockWeapon(weaponData);
            }
            coinManager.currentCoins -= weaponData.price;
            coinManager.UpdateUI();
            Destroy(gameObject);
        }
    }
}
