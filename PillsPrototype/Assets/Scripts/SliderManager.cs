using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [Header("References")]
    public Slider slider;

    [Header("Parameters")]
    public float timer;
    public float secondsInLevel; // How many seconds it takes before the bar reaches 0

    void Start()
    {
        slider.maxValue = secondsInLevel;
        timer = secondsInLevel;
    }

    void Update()
    {
        timer = timer - Time.deltaTime;
        slider.value = timer;

        if (timer >= slider.maxValue)
        {
            timer = slider.maxValue;
        }

        if (timer <= 0)
        {
            timer = 0;
        }
    }
}
