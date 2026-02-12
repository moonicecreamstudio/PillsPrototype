using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSettingsManager : MonoBehaviour
{
    [Header("Reference")]
    public TextMeshProUGUI dayDisplayer;
    public ClipBoardScript clipBoardScript;
    public SliderManager dayTimer;
    public DialogueSystemManager dialogueSystemManager;

    [Header("Parameters")]
    public int maxDay;
    public static int currentDay { get; set; }
    public SceneTransitioner sceneTransitioner;

    [System.Serializable]
    public class LevelMetrics
    {
        public float secondsInLevel;
        public float emailReq;
        public float resumeReq;
        public float sortReq;
    }
    public LevelMetrics[] levelMetrics;

    public void Awake()
    {
        currentDay += 1;
        dayDisplayer.text = currentDay.ToString();
        if (currentDay > 1)
        {
            dialogueSystemManager.hasDayOnePassed = true;
        }

        if (currentDay >= maxDay + 1)
        {
            sceneTransitioner.SwitchScenes("GameOverScene");
        }

        for (int i = 0; i < maxDay; i++)
        {
            if (currentDay == i)
            {
                dayTimer.secondsInLevel = levelMetrics[i].secondsInLevel;
                clipBoardScript.EmailReq = levelMetrics[i].emailReq;
                clipBoardScript.ResumeReq = levelMetrics[i].resumeReq;
                clipBoardScript.PaperReq = levelMetrics[i].sortReq;
            }
        }

    }
}
