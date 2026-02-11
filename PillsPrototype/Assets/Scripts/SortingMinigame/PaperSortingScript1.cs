using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperSortingScript1 : MonoBehaviour
{


    //public CameraController controller;
    public GameObject ButtonCanvas;
    public GameObject[] paperList;
    public GameObject paperObjects;
    int currentpaper;
    public PlayerStatusManager statusManager;
    public ClipBoardScript clipboard;
    public Canvas paperCanv;

    GameObject[] buttonlist;

    bool looking;

    AudioSource audiosource;

    //Image currentPaperImage; 

    // Start is called before the first frame update
    void Start()
    {
        looking = true;

        ButtonCanvas.SetActive(false);
        paperList = new GameObject[paperObjects.transform.childCount];

        for (int i = 0; i < paperList.Length; i++)
        {
            paperList[i] = paperObjects.transform.GetChild(i).gameObject;
        }

        paperCanv.sortingOrder = 0;


        buttonlist = new GameObject[ButtonCanvas.transform.childCount];

        for (int i = 0;i < buttonlist.Length; i++)
        {
            buttonlist[i] = ButtonCanvas.transform.GetChild(i).gameObject;
        }

        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            ButtonCanvas.SetActive(true);
            spawnNewPaper();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ButtonCanvas.SetActive(false);
            paperList[currentpaper].SetActive(false);
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            if (statusManager.isDoingSorting && looking)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ButtonCanvas.SetActive(true);
                spawnNewPaper();
                
                looking = false;
                paperCanv.sortingOrder = 2;
            }

            


        }


        if (Input.GetKeyDown("`") && !looking)
        {
            ButtonCanvas.SetActive(false);
            for (int i = 0; i < paperList.Length; i++)
            {
                paperList[i].SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            looking = true;
            paperCanv.sortingOrder = 0;
        }


    }


    public void spawnNewPaper()
    {
        for (int i = 0; i < paperList.Length; i++)
        {
            paperList[i].SetActive(false);
        }

        currentpaper = Random.Range(0, paperList.Length);

        float ranPaperZ = Random.Range(2.4f, 3f);
        float ranPaperX = Random.Range(1.802f, 2.04f);

        paperList[currentpaper].GetComponent<RectTransform>().localPosition = new Vector3(ranPaperX, paperList[currentpaper].GetComponent<RectTransform>().localPosition.y, ranPaperZ);

        paperList[currentpaper].SetActive(true);

        //currentPaperImage = paperList[currentpaper].gameObject.GetComponent<Image>();


    }

    public void redButton()
    {
        if (paperList[currentpaper].name == "Red")
        {
            Debug.Log("you did it");
            clipboard.AddPaper();
            spawnNewPaper();
            RandomizeButtonsHeight();
        }
        else
        {
            //Debug.Log("try again");
        }

        
    }

    public void greenButton()
    {
        if (paperList[currentpaper].name == "Green")
        {
            Debug.Log("you did it");
            clipboard.AddPaper();
            spawnNewPaper();
            RandomizeButtonsHeight();
        }
        else
        {
            Debug.Log("try again");
        }

        

    }

    public void blueButton()
    {
        if (paperList[currentpaper].name == "Blue")
        {
            Debug.Log("you did it");
            clipboard.AddPaper();
            spawnNewPaper();
            RandomizeButtonsHeight();
        }
        else
        {
            Debug.Log("try again");
        }

        


    }

    public void RandomizeButtonsLocal()
    {





    }

    public void RandomizeButtonsHeight()
    {
        audiosource.Play();

        /*while (currentPaperImage.color.a > 1)
        {
            Color OldColor = currentPaperImage.color;
            //currentPaperImage.color -= new Color (currentPaperImage.color.r, ;
            currentPaperImage.color.Lerp((OldColor), (new Color(1,1,1), 1));

        }*/
        

        float chance = Random.Range(1,10);

        if (chance < 5)
        {
            for (int i = 0; i < buttonlist.Length; i++)
            {
                float RanHeight = Random.Range(2.017f, 3.061f);
                buttonlist[i].GetComponent<RectTransform>().localPosition = new Vector3(buttonlist[i].GetComponent<RectTransform>().localPosition.x, buttonlist[i].GetComponent<RectTransform>().localPosition.y, RanHeight);
            }
        }

        

        //.GetComponent<RectTransform>().localPosition = new Vector3(0.07899928f, 0.282f, -0.5550001f);


    }

}
