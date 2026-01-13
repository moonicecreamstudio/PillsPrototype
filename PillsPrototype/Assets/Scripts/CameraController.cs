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

    [Header("References")]
    public float sensX;
    public float sensY;
    public float xRotation;
    public float yRotation;

    private void Start()
    {
        // At the start, lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
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


        // Handle what the player clicks on, and what do to
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // Check if what has been clicked

            if (Physics.Raycast(raycast, out RaycastHit hit, 100f))
            {
                Debug.Log("Clicked object: " + hit.collider.name);

                if (hit.collider.CompareTag("Focus Pill"))
                {
                    Debug.Log("Consumed focus pills.");
                    focusSlider.timer = 20;
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
        }
    }
}
