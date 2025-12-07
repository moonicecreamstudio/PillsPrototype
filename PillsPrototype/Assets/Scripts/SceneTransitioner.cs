using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public string sceneName;

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
