using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{
    [Header("References")]
    public Slider focusSlider;
    public Slider energySlider;
    public SliderManager dayTimer;
    public SliderManager focusTimer;

    [HideInInspector] public bool _isTired; // Player will begin dozing off when true
    [HideInInspector] public bool _isUnfocused;
    [HideInInspector] public bool _isZoningOut;
    [HideInInspector] public float timer;
    [HideInInspector] public float timer2;

    [Header("Parameters")]
    public float tiredThreshold;
    public float timeDelayToDoze;
    public float unfocusThreshold;
    public float timeDelayToZoneOut;
    public bool isOnePillMode;
    public bool isDoingTyping;
    public bool isDoingResume;
    public bool isDoingSorting;

    [Header("Player Stats")]
    public float insomniaLevel;

    void Update()
    {
        if (isOnePillMode == false)
        {
            // When the energy slider is below the tiredThreshold, the player can get tired
            if (energySlider.value <= tiredThreshold)
            {
                timer += Time.deltaTime;
                if (timer >= timeDelayToDoze) // When timer exceeds timeDelayToDoze, player is tired
                {
                    timer = 0;
                    _isTired = true;

                }
            }
        }

        // When the focus slider is below the threshold, the player is unfocused
        if (focusSlider.value <= unfocusThreshold)
        {

            timer2 += Time.deltaTime;
            if (timer2 >= Random.Range(timeDelayToZoneOut - 5, timeDelayToZoneOut + 5)) // When timer exceeds a random range of +/-5
            {
                timer2 = 0;
                _isUnfocused = true;
                Debug.Log("I'm feeling unfocused...");
            }
        }
        else
        {
            _isUnfocused = false;
        }

        // If the player begins any minigame, the day timer starts
        if (isDoingTyping || isDoingResume || isDoingSorting)
        {
            dayTimer.isActive = true;
            focusTimer.isActive = true;
        } 
    }
}
