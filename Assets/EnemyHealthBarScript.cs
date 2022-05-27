using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(int health)
    {
        slider.maxValue = health;
        slider.value = health; //Bu olmayabilir bakal�m sonras�nda.
    }
    public void SetHealth(int health)
    {
        slider.value = health;

    }
}
