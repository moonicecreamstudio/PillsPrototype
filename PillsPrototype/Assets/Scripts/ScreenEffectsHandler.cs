using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectsHandler : MonoBehaviour
{
    [Header("Misc. References")]
    public Slider focusSlider;
    public Slider clockSlider;

    [Header("Screen Effect References")]
    public Image headInTheCloudsVignette;

    //colors for each screen effects
    Color headInTheCloudsAlphaChange;

    //Slider Manager
    SliderManager focusSliderManager;

    //misc floats
    float maxFocus;
    float vignetteAlpha;

    // Start is called before the first frame update
    void Start()
    {
        //set base colors to white and transparent
        headInTheCloudsAlphaChange = Color.white;
        headInTheCloudsAlphaChange.a = 0;

        //obtain slider manager
        focusSliderManager = focusSlider.GetComponent<SliderManager>();

        maxFocus = focusSliderManager.secondsInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (clockSlider.value > 0)
        {
            vignetteAlpha = maxFocus - (focusSlider.value * 2);
        }
        else if (clockSlider.value <= 0)
        {
            vignetteAlpha = 0;
        }

        //adjust alpha values based on current focus level
        headInTheCloudsAlphaChange.a = vignetteAlpha / maxFocus;
        headInTheCloudsVignette.color = headInTheCloudsAlphaChange;
    }
}
