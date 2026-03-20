using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    void Start()
    {
        // Makes cursor visiable and allows the player to move the mouse when in the shop
        // code from calvin :p
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void sceneTransition()
    {
        SceneManager.LoadScene("MainScene");
    }
}
