using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PillBottleManager : MonoBehaviour
{
    [Header("References")]
    public GameObject pillBottle;
    public SliderManager sliderStat;
    public Transform cameraPosition;
    public PillChoiceManager pillChoiceManager;

    [Header("Parameters")]
    public Transform originPosition;
    public float cameraRadius;
    public bool isClickedOn;
    public float speed;
    public bool isPillConsumed;
    public bool isPillCancelled;

    void Update()
    {
        if (isClickedOn == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pillChoiceManager.isPillSelected = true;
            pillChoiceManager.pillChoice.SetActive(true);
            pillChoiceManager.pillBottleManager = this;

            // Move the pill towards the camera
            float step = speed * Time.deltaTime;
            float dist = Vector3.Distance(transform.position, cameraPosition.position);
            while (dist > cameraRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, cameraPosition.position, step);
                return;
            }
            isClickedOn = false;
        }

        if (isPillConsumed == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Debug.Log("Consumed pills.");
            sliderStat.timer += 20;

            transform.position = originPosition.position; // Return the pill to the original position

            isPillConsumed = false;
        }

        if (isPillCancelled == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            transform.position = originPosition.position; // Return the pill to the original position

            isPillCancelled = false;
        }
    }
}
