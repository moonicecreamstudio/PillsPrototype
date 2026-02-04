using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClipBoardScript : MonoBehaviour
{
    float EmailNum;
    float ResumeNum;
    float PaperNum;
    public float EmailReq;
    public float ResumeReq;
    public float PaperReq;
    //public TextMeshProUGUI emailText;
    public GameObject emailtext;
    public GameObject resumeText;
    public GameObject paperText;
    TextMeshProUGUI currentEmailText;
    TextMeshProUGUI currentResumeText;
    TextMeshProUGUI currentPaperText;
    public GameObject EmailCheck;
    public GameObject ResumeCheck;
    public GameObject PaperCheck;

    // Start is called before the first frame update
    void Start()
    {
        emailtext.GetComponent<TextMeshProUGUI>().text = $"Send {EmailReq} emails today";


        currentEmailText = emailtext.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        resumeText.GetComponent<TextMeshProUGUI>().text = $"Hire {ResumeReq} People";
        currentResumeText = resumeText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        paperText.GetComponent<TextMeshProUGUI>().text = $"Sort {PaperReq} Papers today";
        currentPaperText = paperText.transform.GetChild(0).GetComponent<TextMeshProUGUI>() ;


        EmailNum = 0;
        ResumeNum = 0;
        PaperNum = 0;

        EmailCheck.SetActive(false);
        ResumeCheck.SetActive(false);
        PaperCheck.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        

        
    }

    public void AddEmail()
    {
        EmailNum += 1;

        if (EmailNum >= EmailReq)
        {
            //set active check
            EmailCheck.SetActive(true);

        }

        currentEmailText.text = EmailNum.ToString();
    }

    public void AddResume()
    {
        ResumeNum += 1;

        if (ResumeNum >= ResumeReq)
        {
            //set active check
            ResumeCheck.SetActive(true);

        }


        currentResumeText.text = ResumeNum.ToString();
    }

    public void AddPaper()
    {
        PaperNum += 1;

        if (PaperNum >= PaperReq)
        {
            //set active check
            PaperCheck.SetActive(true);

        }


        currentPaperText.text = PaperNum.ToString();
    }




}
