using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDayEarly : MonoBehaviour
{
    public bool isclickedOn;
    public DayNightTransitioner daynightscript;

    public Slider dayClock;
    public SliderManager dayClock2;

    // Start is called before the first frame update
    void Start()
    {
        isclickedOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isclickedOn)
        {
            dayClock2.isActive = false;
            dayClock.value = 0f;

            //daynightscript.gameObject.SetActive(true);

            //daynightscript.EndDayEarly();
            //Debug.Log("enditall");
            //isclickedOn = false;
        }



    }
}
