using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDayChanger : MonoBehaviour
{
    public TextMeshProUGUI dayDisplayer;

    public void NextDaySwitch()
    {
        if (dayDisplayer.text == "1")
        {
            SceneManager.LoadScene("IntroScene2");
        }
        else
        {
            SceneManager.LoadScene("NightScene");
        }
    }
}
