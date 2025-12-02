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

    void Start()
    {
        duration += Random.Range(-randomizedDespawnTime, randomizedDespawnTime); // Different spawning time
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
        Destroy(gameObject);
    }
}
