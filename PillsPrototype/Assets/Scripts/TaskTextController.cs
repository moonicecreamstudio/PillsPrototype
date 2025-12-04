using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskTextController : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public TextMeshProUGUI taskText;
    public Image taskSliderImageColor;
    public float duration;
    public float randomizedDespawnTime;
    public Slider slider;
    public TaskManager taskManager;

    void Start()
    {
        duration += Random.Range(-randomizedDespawnTime, randomizedDespawnTime); // Different spawning time
        taskManager = transform.parent.GetComponent<TaskManager>();
        slider.maxValue = duration;
        slider.value = duration; 
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {
        float timer = 0f;

        while (timer < duration)
        {
            float t = timer / duration;
            Color currentColor = Color.Lerp(startColor, endColor, t);
            if (taskSliderImageColor != null)
            {
                taskSliderImageColor.color = currentColor;
            }
            slider.value -= Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        taskManager.tasks.Remove(gameObject);
        Destroy(gameObject);
    }
}
