using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTaskRelation : MonoBehaviour
{
    PlayerStatusManager statusManager;
    public float difficultyLevel;
    //public PaperSortingScript1 sortingScript1;
    //public ResumeCycle ResumeCycle;
    // Start is called before the first frame update
    void Start()
    {
        statusManager = GetComponent<PlayerStatusManager>();
        difficultyLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (statusManager.previousFocusValue > (statusManager.focusSlider.maxValue) / 3 && statusManager.focusSlider.value <= (2 * statusManager.focusSlider.maxValue) / 3) //thank you calvin. this is when slider is below 2/3 of the focuslider max value (aka stage 1)
        {
            //Debug.Log("stage 1");
            if (difficultyLevel != 1)
            {
                difficultyLevel = 1;
            }

        }
        if (statusManager.previousFocusValue > statusManager.unfocusThreshold && statusManager.focusSlider.value <= (statusManager.focusSlider.maxValue) / 3)
        {
            if (difficultyLevel != 2)
            {
                difficultyLevel = 2; 
            }

            //Debug.Log("stage 2");
            
        }
        if (statusManager.focusSlider.value <= statusManager.unfocusThreshold)
        {
            if (difficultyLevel != 3)
            {
                difficultyLevel = 3;
            } 
            //Debug.Log("stage 3");
            
        }
        if (statusManager.focusSlider.value > (2 * statusManager.focusSlider.maxValue)/3)
        {
            if (difficultyLevel != 0)
            {
                difficultyLevel = 0;
                //Debug.Log("stage 0");
            }
        }




    }


}
