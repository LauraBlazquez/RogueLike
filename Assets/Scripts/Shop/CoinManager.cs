using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    [SerializeField] private TextMeshProUGUI coinText;
    private int currentCoins = 0;

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
