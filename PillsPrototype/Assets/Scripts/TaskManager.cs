using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("References")]
    public GameObject taskText;
    public Transform clipboardPanel;

    [Header("Parameters")]
    public float timer;
    public float timeToSpawnText;
    public List<string> taskTexts = new List<string>();
    public List<GameObject> tasks = new List<GameObject>(); // Stores the task order

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToSpawnText)
        {
            var taskName = taskTexts[Random.Range(0, taskTexts.Count)];
            GameObject newTextGameObject = Instantiate(taskText, clipboardPanel);
            newTextGameObject.name = taskName;
            tasks.Add(newTextGameObject);
            TextMeshProUGUI tmpText = newTextGameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = taskName;
            timer = 0;
        }
    }
}
