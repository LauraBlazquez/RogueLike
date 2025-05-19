using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int currentCoins = 0;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateUI();
    }

    public void SetCoins(int amount)
    {
        currentCoins = amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinText.text = currentCoins.ToString();
    }

    public int GetCoins()
    {
        return currentCoins;
    }    
}
