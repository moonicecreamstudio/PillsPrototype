using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTaskRelation : MonoBehaviour
{
    PlayerStatusManager statusManager;
    public float difficultyLevel;
    // Start is called before the first frame update
    void Start()
    {
        statusManager = GetComponent<PlayerStatusManager>();
        difficultyLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (statusManager.previousFocusValue >= (2 * statusManager.focusSlider.maxValue) / 3 && statusManager.focusSlider.value <= (2 * statusManager.focusSlider.maxValue) / 3) //thank you calvin. this is when slider is below 2/3 of the focuslider max value (aka stage 1)
        {
            Debug.Log("stage 1");
            difficultyLevel = 1;

        }
        else if (statusManager.previousFocusValue >= (statusManager.focusSlider.maxValue) / 3 && statusManager.focusSlider.value <= (statusManager.focusSlider.maxValue) / 3)
        {
            Debug.Log("stage 2");
            difficultyLevel = 2;
        }
        else if (statusManager.focusSlider.value <= statusManager.unfocusThreshold)
        {
            Debug.Log("stage 3");
            difficultyLevel = 3;
        }
        else if (statusManager.focusSlider.value > (2 * statusManager.focusSlider.maxValue)/3)
        {
            if (difficultyLevel != 0)
            {
                difficultyLevel = 0;
                Debug.Log("stage 0");
            }
        }




    }


}
