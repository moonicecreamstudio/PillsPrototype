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

    Image currentPaperImage; 

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
            StartCoroutine(WaitAndActivateGame());





        }


        if (Input.GetKeyDown("`") && !looking)
        {
            HideSortingGame();
        }
    }

    public void HideSortingGame()
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

    public IEnumerator WaitAndActivateGame()
    {
        yield return new WaitForEndOfFrame();
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

    IEnumerator SwitchPages()
    {
        float transitionSeconds = 0.5f;
        yield return FadeCurrentPage(transitionSeconds, 0);

        // TODO: code to randomize which page is current


        // Ensure new page starts at 0 alpha so we don't see a visible pop.
        var color = currentPaperImage.color;
        color.a = 0;
        currentPaperImage.color = color;
        yield return FadeCurrentPage(transitionSeconds, 1, 0);
    }

    IEnumerator FadeCurrentPage(float durationSeconds, float targetAlpha, float startAlpha = -1) {
        Color oldColor = currentPaperImage.color;
        if (startAlpha >= 0) oldColor.a = startAlpha;

        Color newColor = currentPaperImage.color;
        newColor.a = targetAlpha;
        float speed = 1f / durationSeconds;
        
        for (float t = 0; t < 1; t += speed * Time.deltaTime)
        {          
            currentPaperImage.color = Color.Lerp(oldColor, newColor, t);
            yield return null;
        }

        currentPaperImage.color = newColor;
    }


    public void RandomizeButtonsHeight()
    {
        audiosource.Play();

        StartCoroutine(FadeCurrentPage(0.5f, 0));
        

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
