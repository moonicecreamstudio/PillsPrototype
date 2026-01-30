using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool looking;

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
        paperList[currentpaper].SetActive(true);




    }

    public void redButton()
    {
        if (paperList[currentpaper].name == "Red")
        {
            Debug.Log("you did it");
            clipboard.AddPaper();
            spawnNewPaper();
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
        }
        else
        {
            Debug.Log("try again");
        }

        


    }

}
