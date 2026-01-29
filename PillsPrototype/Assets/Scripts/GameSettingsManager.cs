using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSettingsManager : MonoBehaviour
{
    [Header("Reference")]
    public TextMeshProUGUI dayDisplayer;

    [Header("Parameters")]
    public int maxDay;
    public static int currentDay { get; set; }
    public SceneTransitioner sceneTransitioner;

    public void Awake()
    {
        currentDay += 1;
        dayDisplayer.text = currentDay.ToString();
        if (currentDay >= maxDay + 1)
        {
            sceneTransitioner.SwitchScenes("GameOverScene");
        }
    }
}
