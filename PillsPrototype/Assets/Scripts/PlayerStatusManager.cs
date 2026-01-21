using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{
    [Header("References")]
    public Slider focusSlider;
    public Slider energySlider;

    [HideInInspector] public bool _isTired; // Player will begin dozing off when true
    [HideInInspector] public bool _isUnfocused;
    [HideInInspector] public float timer;

    [Header("Parameters")]
    public float tiredThreshold;
    public float timeDelayToDoze;
    public float unfocusThreshold;
    public bool isOnePillMode;

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
            _isUnfocused = true;
        }
        else
        {
            _isUnfocused = false;
        }
    }
}
