using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public Slider clockFace;
    public float timer;
    public float secondsInLevel;

    void Start()
    {
        clockFace.maxValue = secondsInLevel;
        timer = secondsInLevel;
    }

    void Update()
    {
        timer = timer - Time.deltaTime;
        clockFace.value = timer;
    }
}
