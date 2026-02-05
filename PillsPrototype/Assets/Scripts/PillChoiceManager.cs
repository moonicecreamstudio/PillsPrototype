using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PillChoiceManager : MonoBehaviour
{
    [Header("References")]
    public GameObject pillChoice;
    public PillBottleManager pillBottleManager;
    public CameraController cameraController;
    public TextMeshProUGUI pillText;
    public DialogueSystemManager dialogueSystemManager;
    public PlayerStatusManager playerStatusManager;

    [Header("Parameters")]
    public bool isPillSelected;

    void Start()
    {
        pillChoice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPillSelected == false)
        {
            pillChoice.SetActive(false);
        }

        // Change the text to match the pill's gameObject's name
        if  (isPillSelected == true)
        {
            pillText.text = "Consume " + pillBottleManager.pillBottle.name.ToString() + "?";
        }
    }

    public void YesButton()
    {
        pillBottleManager.isPillConsumed = true;
        isPillSelected = false;
        pillChoice.SetActive(false);
        cameraController.isCameraDisabled = false;
        Debug.Log(pillBottleManager.pillBottle.name.ToString());
        //dialogueSystemManager.PlayConsumeText(pillBottleManager.pillBottle.name.ToString()); // Used for multiple pills
        playerStatusManager.currentIntake += playerStatusManager.pillDosageAmount;
        //dialogueSystemManager.StartCoroutine(dialogueSystemManager.ConsumeText(pillBottleManager.pillBottle.name.ToString()));

        // Barks
        float randomBark = Random.Range(0, 3);
        if (randomBark == 0) dialogueSystemManager.PlayConsumeText("replenishfocus_01");
        if (randomBark == 1) dialogueSystemManager.PlayConsumeText("replenishfocus_02");
        if (randomBark == 2) dialogueSystemManager.PlayConsumeText("replenishfocus_03");
    }

    public void NoButton()
    {
        pillBottleManager.isPillCancelled = true;
        isPillSelected = false;
        pillChoice.SetActive(false);
        cameraController.isCameraDisabled = false;
    }
}
