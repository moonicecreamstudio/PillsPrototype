using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public GameObject taskText;
    public Transform clipboardPanel;

    public float timer;
    public float timeToSpawnText;
    public List<string> taskTexts = new List<string>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToSpawnText)
        {
            GameObject newTextGameObject = Instantiate(taskText, clipboardPanel);
            TextMeshProUGUI tmpText = newTextGameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = taskTexts[Random.Range(0, taskTexts.Count)];
            timer = 0;
        }

    }
}
