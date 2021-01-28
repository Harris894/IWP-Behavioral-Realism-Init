using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionUI : MonoBehaviour
{
    private Slider slider;
    private Image fill;
    public Gradient gradient;


    private void Start()
    {
        slider = GetComponent<Slider>();
        fill = transform.GetChild(1).GetComponent<Image>();
    }

    public void SetMaxSatisfaction(int satisfaction, int currentSatisfaction)
    {
        slider.maxValue = satisfaction;
        slider.value = currentSatisfaction;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetSatisfaction(float satisfaction)
    {
        slider.value = satisfaction;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
