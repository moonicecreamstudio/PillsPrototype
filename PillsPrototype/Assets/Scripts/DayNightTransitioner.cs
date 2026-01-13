using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayNightTransitioner : MonoBehaviour
{
    [Header("References")]
    public Slider dayClock;


    void Update()
    {
        if (dayClock.value <= 0)
        {
            SceneManager.LoadScene("ShopScene");
        }
    }
}
