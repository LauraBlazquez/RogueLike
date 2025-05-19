using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    public void UpdateHealthBar(float currentHealth)
    {
        healthBar.fillAmount = currentHealth / 100f; 
    }
}
