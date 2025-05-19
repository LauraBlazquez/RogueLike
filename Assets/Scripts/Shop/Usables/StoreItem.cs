using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StoreItem", menuName = "Objects/StoreItem")]
public class StoreItem : ScriptableObject
{
    public float value;
    public int price;

    public void Heal(Player user)
    {
        user.currentHealth += value;
        if (user.currentHealth > user.maxHealth)
        {
            user.currentHealth = user.maxHealth;
        }
        user.healthBar.UpdateHealthBar(user.currentHealth);
    }
}
