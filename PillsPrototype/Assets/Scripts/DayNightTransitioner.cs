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

    [Header("Variables")]
    public string sceneName;

    void Update()
    {
        if (dayClock.value <= 0)
        {
            StartCoroutine(NextDay());
        }
    }

    IEnumerator NextDay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
        yield break;
    }
}
