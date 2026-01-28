using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCycle : MonoBehaviour
{
    public GameObject easyResumes;
    public GameObject requirments;
    public GameObject[] resumeList;
    public GameObject[] requirmentList;
    public int currentResume;
    string firstquality;
    string secondquality;
    string thirdquality;

    // Start is called before the first frame update
    void Start()
    {
        resumeList = new GameObject[easyResumes.transform.childCount];
            
            

        for (int i = 0; i < resumeList.Length; i++)
        {
            resumeList[i] = easyResumes.transform.GetChild(i).gameObject;
        }


        requirmentList = new GameObject[requirments.transform.childCount];

        for (int i = 0; i < requirmentList.Length; i++)
        {
            requirmentList[i] = requirments.transform.GetChild(i).gameObject;
        }
        /*for (int i = 0; i < resumeList.Length; i++) // have the first of the list get setactived
        {
            if (i > 0)
            {
                resumeList[i].SetActive(false);
            }
            else
            {
                resumeList[i].SetActive(true);
            }
        }*/
        currentResume = Random.Range(0, resumeList.Length);
        resumeList[(currentResume)].gameObject.SetActive(true); //random resume set active first

        EasyResumeRequirements();

        //Debug.Log(resumeList.Length);

        
    }

    void EasyResumeRequirements()
    {



        int option1 = 0;
        int option2 = 0;
        int option3 = 0;
        //bool chooseran = false;
        for (int i = 0; i < requirmentList.Length; i++)
        {
            requirmentList[i].gameObject.SetActive(false);
        }

        while ((option1 == option2) || (option1 == option3) || (option2 == option3))
        {
            option1 = Random.Range(0, requirmentList.Length);
            option2 = Random.Range(0, requirmentList.Length);
            option3 = Random.Range(0, requirmentList.Length);
            //chooseran = true;
            /*Debug.Log(option3);
            Debug.Log(option1);
            Debug.Log(option2);*/
        }


        

        requirmentList[option1].SetActive(true);
        firstquality = requirmentList[option1].name;
        requirmentList[option1].GetComponent<RectTransform>().localPosition = new Vector3(0.07899928f, 0.282f, -0.5550001f);

        requirmentList[option2].SetActive(true);
        requirmentList[option2].GetComponent<RectTransform>().localPosition = new Vector3(0.07899928f, -0.022f, -0.5550001f);
        secondquality = requirmentList[option2].name;


        requirmentList[option3].SetActive(true);
        requirmentList[option3].GetComponent<RectTransform>().localPosition = new Vector3(0.07899928f, -0.347f, -0.5550001f);
        thirdquality = requirmentList[option3].name;

    }




    // Update is called once per frame
    void Update()
    {
        
        //temp controls for buttons 
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextResumeVoid();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PrevResumeVoid();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeAccept();
        }



    }

    void Listloop()
    {
        if (currentResume < 0)
        {
            currentResume = resumeList.Length;
        }
        if (currentResume > resumeList.Length)
        {
            currentResume = 0;
        }
    }

    public void nextResumeVoid()
    {

        //Debug.Log("why");

        resumeList[(currentResume)].gameObject.SetActive(false);
        currentResume += 1;
        if (currentResume > resumeList.Length - 1)
        {
            currentResume = 0;
        }
        resumeList[(currentResume)].gameObject.SetActive(true);
    }
    public void PrevResumeVoid()
    {
        resumeList[(currentResume)].gameObject.SetActive(false);
        currentResume -= 1;
        if (currentResume < 0)
        {
            currentResume = resumeList.Length - 1;
        }
        resumeList[(currentResume)].gameObject.SetActive(true);
    }


    public void ResumeAccept()
    {
        bool ChoseCorrect = false;
        GameObject chosenResume = null;
        

        for (int i = 0; i < resumeList.Length; i++)
        {
            if (resumeList[i].activeInHierarchy)
            {
                chosenResume = resumeList[i];
            }
            
        }


        GameObject[] resumeQualities = new GameObject[chosenResume.transform.childCount];
        for (int i = 0; i < resumeQualities.Length; i++)
        {
            resumeQualities[i] = chosenResume.transform.GetChild(i).gameObject;
            Debug.Log(resumeQualities[i]);
            if ((resumeQualities[i].name == firstquality) || (resumeQualities[i].name == secondquality) || resumeQualities[i].name == thirdquality)
            {
                PlusPoint();
                ChoseCorrect = true;
                break;
            }
        }

        if (ChoseCorrect == false)
        {
            Debug.Log("you lose something or other");
        }
        

        //EasyResumeRequirements();

    }

    public void PlusPoint()
    {
        Debug.Log("you did it");

        EasyResumeRequirements();

    }


}
