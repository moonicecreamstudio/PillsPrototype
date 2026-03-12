using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("References")]
    public KeyCode pauseButton;
    public SliderManager focusSlider;
    public SliderManager dayClock;
    public PlayerStatusManager playerStatusManager;
    public UIWindowMover pauseMenuUI;
    public CameraController cameraController;
    public GameObject subMenu1;
    public GameObject quitGameConfirm;
    public GameObject mainMenuConfirm;
    public GameObject optionsMenu;

    [Header("Variables")]
    public static bool isGamePaused;
    public bool isOriginallyLocked;
    public bool isOriginallyVisible;
    public bool isOriginallyCameraDisabled;

    public void Start()
    {

        subMenu1.SetActive(true);
        mainMenuConfirm.SetActive(false);
        quitGameConfirm.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseButton) && playerStatusManager.isGameStarted == true)
        {
            StartCoroutine(PauseGame());
        }
    }

    // Return to Main Menu
    public void ReturnMainMenu()
    {
        subMenu1.SetActive(false);
        mainMenuConfirm.SetActive(true);
    }

    public void DenyMainMenu()
    {
        subMenu1.SetActive(true);
        mainMenuConfirm.SetActive(false);
    }
    public void ConfirmMainMenu()
    {
        isGamePaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    // Options

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        subMenu1.SetActive(false);
    }
    public void ExitOptions()
    {
        optionsMenu.SetActive(false);
        subMenu1.SetActive(true);
    }

    // Quit Game
    public void QuitGame()
    {
        quitGameConfirm.SetActive(true);
        subMenu1.SetActive(false);
    }

    public void DenyQuit()
    {
        quitGameConfirm.SetActive(false);
        subMenu1.SetActive(true);
    }

    public void ConfirmQuit()
    {
        Application.Quit();
    }

    IEnumerator PauseGame()
    {
        if(isGamePaused == false)
        {
            Debug.Log("PAUSED");
            isGamePaused = true;
            pauseMenuUI.windowOn();
            focusSlider.isActive = false;
            dayClock.isActive = false;

            //Remember Cursor Lock State

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                isOriginallyLocked = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                isOriginallyLocked = false;
            }

            // Cursor visibility
            if (Cursor.visible == false)
            {
                isOriginallyVisible = false;
                Cursor.visible = true;
            }
            else if (Cursor.visible == true)
            {
                isOriginallyVisible = true;
            }

            //Disable Camera Controls
            if (cameraController.isCameraDisabled == false)
            {
                isOriginallyCameraDisabled = false;
                cameraController.isCameraDisabled = true;
            }
            else if (cameraController.isCameraDisabled == true)
            {
                isOriginallyCameraDisabled = true;
            }


            yield break;
        }

        if (isGamePaused == true)
        {
            UnpauseButton();
            yield break;
        }
    }

    public void UnpauseButton()
    {
        Debug.Log("NOT PAUSED");
        subMenu1.SetActive(true);
        mainMenuConfirm.SetActive(false);
        quitGameConfirm.SetActive(false);
        optionsMenu.SetActive(false);

        if (isOriginallyLocked == true) Cursor.lockState = CursorLockMode.Locked;
        else if (isOriginallyLocked == false) Cursor.lockState = CursorLockMode.None;
        if (isOriginallyVisible == true) Cursor.visible = true;
        else if (isOriginallyVisible == false) Cursor.visible = false;
        if (isOriginallyCameraDisabled == true) cameraController.isCameraDisabled = true;
        else if (isOriginallyCameraDisabled == false) cameraController.isCameraDisabled = false;
        isGamePaused = false;
        pauseMenuUI.windowOff();
        focusSlider.isActive = true;
        dayClock.isActive = true;
    }
}
