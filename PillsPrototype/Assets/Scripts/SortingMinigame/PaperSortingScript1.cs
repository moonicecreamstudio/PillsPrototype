using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSortingScript1 : MonoBehaviour
{


    public CameraController controller;
    public GameObject ButtonCanvas;
    public GameObject[] paperList;
    public GameObject paperObjects;
    int currentpaper;

    // Start is called before the first frame update
    void Start()
    {
        ButtonCanvas.SetActive(false);
        paperList = new GameObject[paperObjects.transform.childCount];

        for (int i = 0; i < paperList.Length; i++)
        {
            paperList[i] = paperObjects.transform.GetChild(i).gameObject;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ButtonCanvas.SetActive(true);
            spawnNewPaper();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ButtonCanvas.SetActive(false);
            paperList[currentpaper].SetActive(false);
        }



    }


    public void spawnNewPaper()
    {
        currentpaper = Random.Range(0, paperList.Length);
        paperList[currentpaper].SetActive(true);




    }

    public void redButton()
    {
        if (paperList[currentpaper].name == "Red")
        {
            Debug.Log("you did it");

        }
        else
        {
            Debug.Log("try again");
        }

        spawnNewPaper();
    }

    public void greenButton()
    {
        if (paperList[currentpaper].name == "Green")
        {
            Debug.Log("you did it");

        }
        else
        {
            Debug.Log("try again");
        }

        spawnNewPaper();

    }

    public void blueButton()
    {
        if (paperList[currentpaper].name == "Blue")
        {
            Debug.Log("you did it");

        }
        else
        {
            Debug.Log("try again");
        }

        spawnNewPaper();


    }

}
