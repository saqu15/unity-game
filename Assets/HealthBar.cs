using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public Slider slider;

    private void Awake()
    {
        Instance = this;
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public float GetMaxHealth()
    {
        return slider.maxValue;
    }
    
    public void SetHealth(float health)
    {
        if(health < GetMaxHealth())
        {
            slider.value = (float)Math.Ceiling(health);
        }
        else
        {
            slider.value = GetMaxHealth();
        }
    }
    public float GetHealth()
    {
        return slider.value;
    }
}
