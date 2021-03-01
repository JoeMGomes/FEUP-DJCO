using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public Color low, high;
    public void SetHealth(float health) 
    {
        slider.value = health;
        fill.color = Color.Lerp(low, high, slider.normalizedValue);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
       
    }
}
