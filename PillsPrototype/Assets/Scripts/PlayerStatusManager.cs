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
    public SliderManager focusBar;
    public SliderManager energyBar;
    public DialogueSystemManager dialogueSystemManager;

    [Header("Parameters")]
    public float tiredThreshold;
    public float timeDelayToDoze;
    public float unfocusThreshold;
    public float timeDelayToZoneOut;
    public float timeDelayToTremor;
    public bool isOnePillMode;
    public bool isDoingTyping;
    public bool isDoingResume;
    public bool isDoingSorting;
    public float pillDosageAmount;
    public float chanceToTremors;

    [Header("Checks")]
    public float previousFocusValue; 
    public float previousIntakeValue;

    [Header("Player Stats")]
    public float insomniaLevel;
    public float currentIntake;
    public float intakeThreshold;

    [HideInInspector] public bool _isTired; // Player will begin dozing off when true
    [HideInInspector] public bool _isUnfocused;
    [HideInInspector] public bool _isTremoring;
    [HideInInspector] public bool _isZoningOut;
    [HideInInspector] public float timer;
    [HideInInspector] public float timer2;
    [HideInInspector] public float timer3;

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

        // Runs code once the bar depletes past below a threshold
        if (previousFocusValue >= focusSlider.maxValue / 2 && focusSlider.value <= focusSlider.maxValue / 2)
        {
            float randomBark = Random.Range(0, 2);
            if (randomBark == 0) dialogueSystemManager.PlayConsumeText("focusbarmedium_01");
            if (randomBark == 1) dialogueSystemManager.PlayConsumeText("focusbarmedium_02");
        }

        if (previousFocusValue >= unfocusThreshold && focusSlider.value <= unfocusThreshold)
        {
            float randomBark = Random.Range(0, 2);
            if (randomBark == 0) dialogueSystemManager.PlayConsumeText("focusbarlow_01");
            if (randomBark == 1) dialogueSystemManager.PlayConsumeText("focusbarlow_02");
        }

        // When the focus slider is below the threshold, the player is unfocused
        if (focusSlider.value <= unfocusThreshold)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= Random.Range(timeDelayToZoneOut - 5, timeDelayToZoneOut + 5)) // When timer exceeds a random range of +/-5
            {
                timer2 = 0;
                _isUnfocused = true;
                Debug.Log("Player's focus is below the unfocus threshold.");
            }
        }
        else
        {
            _isUnfocused = false;
        }

        // Tremor bark
        if (previousIntakeValue <= intakeThreshold && currentIntake > intakeThreshold)
        {
            dialogueSystemManager.PlayConsumeText("overdose_01");
        }

        // Begin chance for tremors
        if (currentIntake > intakeThreshold && _isTremoring == false)
        {
            timer3 += Time.deltaTime;
            if (timer3 >= Random.Range(timeDelayToTremor - 5, timeDelayToTremor + 5)) // When timer exceeds a random range of +/-5
            {
                if (Random.Range(0, 100) <= chanceToTremors) // Chance to experience tremors out of 100
                {
                    _isTremoring = true;
                    Debug.Log("Player is experincing tremors.");
                }
                timer3 = 0;
                Debug.Log(timer3);
            }
        }

        // If the player begins any minigame, the day timer starts
        if (isDoingTyping || isDoingResume || isDoingSorting)
        {
            dayTimer.isActive = true;
            focusBar.isActive = true;
            energyBar.isActive = true;
        }

        // Allows to run code once for barks
        previousFocusValue = focusSlider.value;
        previousIntakeValue = currentIntake;
    }
}
