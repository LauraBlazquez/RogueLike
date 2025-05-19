using UnityEngine;

public class BuyItems : MonoBehaviour
{
    [SerializeField] private StoreItem item;
    private CoinManager coinManager;

    private void Start()
    {
        coinManager = CoinManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && coinManager.currentCoins >= item.price)
        { 
            Player player = other.GetComponent<Player>();
            coinManager.currentCoins -= item.price;
            coinManager.UpdateUI();
            item.Heal(player);
            Destroy(gameObject);
        }
    }
}
