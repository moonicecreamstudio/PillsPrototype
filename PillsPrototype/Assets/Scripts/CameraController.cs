using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public SliderManager focusSlider;
    public TaskManager taskManager;
    public ButtonAnimator sortButton;
    public ButtonAnimator hireButton;
    public Transform orientation;
    public PillBottleManager focusBottle;
    public PillBottleManager energyBottle;
    public PillBottleManager calmnessBottle;
    public Camera cameraCamera;
    public GameObject cameraObject;
    public GameObject cameraHolderObject;

    [Header("Variables")]
    public float sensX;
    public float sensY;
    public float xRotation;
    public float yRotation;
    public bool isCameraDisabled;

    private void Start()
    {
        // At the start, lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isCameraDisabled == false)
        {
            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * -sensY;

            yRotation += mouseX;
            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        // Exit out of focus, return camera to original position
        if (Input.GetKeyDown("`"))
        {
            isCameraDisabled = false;
            cameraHolderObject.transform.position = new Vector3(0, 4.98f, -1.9f);
        }

        // Handle what the player clicks on, and what do to
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // Check if what has been clicked, middle of the screen

            if (Physics.Raycast(raycast, out RaycastHit hit, 100f) && isCameraDisabled == false)
            {
                Debug.Log("Clicked object: " + hit.collider.name);

                if (hit.collider.CompareTag("Focus Pill"))
                {
                    focusBottle.isClickedOn = true;
                    isCameraDisabled = true;
                }

                //if (hit.collider.CompareTag("Energy Pill"))
                //{
                //    energyBottle.isClickedOn = true;
                //    isCameraDisabled = true;
                //}

                //if (hit.collider.CompareTag("Calmness Pill"))
                //{
                //    calmnessBottle.isClickedOn = true;
                //    isCameraDisabled = true;
                //}

                // Instead of hard coding variables, might be better to have GameObjects, or different cameras?

                if (hit.collider.CompareTag("Typing Minigame"))
                {
                    Debug.Log("Zoom in typing game.");
                    isCameraDisabled = true;
                    //cameraCamera.fieldOfView = 80;
                    cameraHolderObject.transform.position = new Vector3(0, 4.98f, -1.9f);
                    cameraHolderObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
                }

                if (hit.collider.CompareTag("Resume Minigame"))
                {
                    Debug.Log("Zoom in resume game.");
                    isCameraDisabled = true;
                    //cameraCamera.fieldOfView = 80;
                    cameraHolderObject.transform.position = new Vector3(1.1f, 4.98f, -1.85f);
                    cameraHolderObject.transform.localEulerAngles = new Vector3(-15f, 30.9f, 0);
                }

                if (hit.collider.CompareTag("Sorting Minigame"))
                {
                    Debug.Log("Zoom in sorting game.");
                    isCameraDisabled = true;
                    //cameraCamera.fieldOfView = 80;
                    cameraHolderObject.transform.position = new Vector3(0.75f, 5.9f, -3.29f);
                    cameraHolderObject.transform.localEulerAngles = new Vector3(28, 90, 0);
                }

                if (hit.collider.CompareTag("Sort Complete Button"))
                {
                    Debug.Log("Sort complete.");
                    sortButton.StartCoroutine(sortButton.ButtonPressed());
                    if (taskManager.tasks.Count > 0)
                    {
                        GameObject first = taskManager.tasks[0];
                        taskManager.tasks.RemoveAt(0);
                        Destroy(first);
                    }
                }

                if (hit.collider.CompareTag("Hire Complete Button"))
                {
                    Debug.Log("Hire complete.");
                    hireButton.StartCoroutine(hireButton.ButtonPressed());
                    if (taskManager.tasks.Count > 0)
                    {
                        GameObject first = taskManager.tasks[0];
                        taskManager.tasks.RemoveAt(0);
                        Destroy(first);
                    }
                }
            }

            // Secondary Clicking

            Ray raycast2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;

            if (Physics.Raycast(raycast2, out hit2) && isCameraDisabled == true)
            {
                // Name of clicked object
                Debug.Log("Hit object: " + hit2.collider.gameObject.name);
            }
        }
    }
}
