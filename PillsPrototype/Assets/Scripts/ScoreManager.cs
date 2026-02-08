using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public ClipBoardScript clipBoardScript;
    public GameSettingsManager gameSettingsManager;
    public PlayerStatusManager playerStatusManager;
    public Slider clockSlider;
    public Slider focusSlider;
    public UIWindowMover uIWindowMover;
    public Radishmouse.UILinerRenderer uILinerRenderer;
    public TextMeshProUGUI emailsSent;
    public TextMeshProUGUI emailsReq;
    public TextMeshProUGUI peopleHired;
    public TextMeshProUGUI peopleReq;
    public TextMeshProUGUI papersSorted;
    public TextMeshProUGUI papersReq;
    public TextMeshProUGUI employerNote;
    public TextMeshProUGUI doctorNote;

    [Header("Day Score")]
    public float percentageEmail;
    public float percentageResume;
    public float percentagePaper;
    public float sumScore;

    public float goodScore;
    public float poorScore;
    public bool isScoreCalculated;
    public bool isDayOver;
    public Vector2[] focusToTimePoints;
    public float previousTimerValue;

    [System.Serializable]
    public class TextData
    {
        public string textID;
        [SerializeField]
        [TextArea(3, 10)]
        public string textContents;
    }
    public TextData[] textData;

    [System.Serializable]
    public class OverallScore
    {
        public float day;
        public float score;
    }
    public List<OverallScore> overallScore = new List<OverallScore>();

    void Update()
    {
        if (clockSlider.value <= 0)
        {
            isDayOver = true;
        }

        // Calculate score
        if (isDayOver == true && isScoreCalculated == false)
        {
            StartCoroutine(CalculateScore());
            foreach (var s in overallScore) // Not working as intended, but a quick and dirty way to spit out a score
            {
                Debug.Log("Day " + s.day + " Score: " + s.score);
            }
            isScoreCalculated = true;
        }

        // Display Score
        if (isDayOver == true) uIWindowMover.windowOn();

        // Graph
        for (int i = 0; i < uILinerRenderer.points.Length; i++)
        {
            if (previousTimerValue >= (clockSlider.maxValue / uILinerRenderer.points.Length) * (uILinerRenderer.points.Length - i) && 
                clockSlider.value < (clockSlider.maxValue / uILinerRenderer.points.Length) * (uILinerRenderer.points.Length - i))
                {
                    uILinerRenderer.points[i] = new Vector2((1 - (clockSlider.value / clockSlider.maxValue)) * 200, (focusSlider.value / focusSlider.maxValue) * 100);
                    uILinerRenderer.SetAllDirty();
                }
        }



        previousTimerValue = clockSlider.value;
    }

    IEnumerator CalculateScore()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Tasks done text displayer
        emailsSent.text = clipBoardScript.EmailNum.ToString();
        emailsReq.text = clipBoardScript.EmailReq.ToString();
        peopleHired.text = clipBoardScript.ResumeNum.ToString();
        peopleReq.text = clipBoardScript.ResumeReq.ToString();
        papersSorted.text = clipBoardScript.PaperNum.ToString();
        papersReq.text = clipBoardScript.PaperReq.ToString();

        // At the end of the day, calculate the percentage
        percentageEmail = clipBoardScript.EmailNum / clipBoardScript.EmailReq;
        percentageResume = clipBoardScript.ResumeNum / clipBoardScript.ResumeReq;
        percentagePaper = clipBoardScript.PaperNum / clipBoardScript.PaperReq;

        // Do not let it go over 1
        if (percentageEmail > 1) percentageEmail = 1;
        if (percentageResume > 1) percentageResume = 1;
        if (percentagePaper > 1) percentagePaper = 1;

        sumScore = percentageEmail + percentageResume + percentagePaper;
        overallScore.Add(new OverallScore {day = GameSettingsManager.currentDay, score = sumScore});

        // Employers' Note
        if (sumScore < poorScore)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("poor_01"))
                {
                    employerNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Poor");
        }
        if (sumScore >= poorScore && sumScore < goodScore)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("fair_01"))
                {
                    employerNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Fair");
        }
        if (sumScore >= goodScore)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("good_01"))
                {
                    employerNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Good");
        }

        // Doctor's Note
        if (playerStatusManager.currentIntake == 0)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("nostim_01"))
                {
                    doctorNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Player never took any stimulants.");
        }

        if (playerStatusManager.currentIntake > 0 && playerStatusManager.currentIntake <= playerStatusManager.intakeThreshold)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("lowstim_01"))
                {
                    doctorNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Player took adequate stimulants.");
        }

        if (playerStatusManager.currentIntake > playerStatusManager.intakeThreshold)
        {
            for (int i = 0; i < textData.Length; i++)
            {
                if (textData[i].textID.Contains("highstim_01"))
                {
                    doctorNote.text = textData[i].textContents;
                }
            }
            Debug.Log("Player overdosed.");
        }

        yield break;
    }
}
