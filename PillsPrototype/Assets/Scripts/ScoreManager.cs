using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public ClipBoardScript clipBoardScript;
    public GameSettingsManager gameSettingsManager;
    public Slider clockSlider;

    [Header("Day Score")]
    public float percentageEmail;
    public float percentageResume;
    public float percentagePaper;
    public float sumScore;

    public float goodScore;
    public float poorScore;
    public bool isScoreCalculated;
    public bool isDayOver;

    [System.Serializable]
    public class OverallScore
    {
        public float day;
        public float score;
    }
    public List<OverallScore> overallScore = new List<OverallScore>();

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (clockSlider.value <= 0)
        {
            isDayOver = true;
        }

        if (isDayOver == true && isScoreCalculated == false)
        {
            StartCoroutine(CalculateScore());
            foreach (var s in overallScore) // Not working as intended, but a quick and dirty way to spit out a score
            {
                Debug.Log("Day " + s.day + " Score: " + s.score);
            }
            isScoreCalculated = true;
        }
    }

    IEnumerator CalculateScore()
    {
        // At the end of the day, calculate the percentage
        percentageEmail = clipBoardScript.EmailNum / clipBoardScript.EmailReq;
        percentageResume = clipBoardScript.ResumeNum / clipBoardScript.ResumeReq;
        percentagePaper = clipBoardScript.PaperNum / clipBoardScript.PaperReq;

        sumScore = percentageEmail + percentageResume + percentagePaper;
        overallScore.Add(new OverallScore {day = GameSettingsManager.currentDay, score = sumScore});

        if (sumScore < poorScore)
        {
            Debug.Log("Poor");
        }
        if (sumScore >= poorScore && sumScore < goodScore)
        {
            Debug.Log("Fair");
        }
        if (sumScore >= goodScore)
        {
            Debug.Log("Good");
        }

        yield break;
    }
}
