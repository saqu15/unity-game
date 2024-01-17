using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Text healthPotionText;

    public int healthPotionCount = 0;
    
    public static int healthPotionValue = 30;

    private void Awake()
    {
        Instance = this;
        healthPotionText = GameObject.Find("HealthPotionCounter").GetComponent<Text>();
    }

    public void AddItem(CollectibleItem item)
    {
        if (item is HealthPotion)
        {
            healthPotionCount++;
            UpdateUI();
        }
    }

    public void UseHealthPotion()
    {
        if (HasHealthPotions() && (HealthBar.Instance.GetHealth() < HealthBar.Instance.GetMaxHealth()))
        {
            healthPotionCount--;
            HealthBar.Instance.SetHealth(HealthBar.Instance.GetHealth()+healthPotionValue);
            UpdateUI(); 
        }
    }
    public bool HasHealthPotions()
    {
        if (healthPotionCount > 0) 
        {
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        healthPotionText.text = healthPotionCount.ToString();
    }
}