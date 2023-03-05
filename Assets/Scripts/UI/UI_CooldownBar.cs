using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CooldownBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxJuice(int Juice)
    {
        slider.maxValue = Juice;
        slider.value = Juice;
    }


    public void SetDreamJuice(int Juice)
    {
        slider.value = Juice;
    }

}
