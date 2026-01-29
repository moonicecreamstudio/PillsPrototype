using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystemManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI subtitleText; // Used for dialogue text
    public TextMeshProUGUI subtitleText2; // Used for when items are consumed
    public GameObject subtitleBackdrop;

    [Header("Parameters")]
    public float timer;
    public float timeToNext; // Time it takes to change and remove the text.
    private Coroutine currentConsumeRoutine;

    [System.Serializable]
    public class TextData
    {
        public string textID;
        public float displayTime; // In seconds
        [SerializeField]
        [TextArea(3, 10)]
        public string textContents;
    }
    public TextData[] textData;

    void Start()
    {
        subtitleText.text = "";
        subtitleText2.text = "";
        StartCoroutine(IntroText());
    }
    void Update()
    {
        
    }

    IEnumerator IntroText()
    {
        subtitleBackdrop.SetActive(true);
        // Go through the texts for the introduction
        for (int i = 0; i < textData.Length; i++)
        {
            if (textData[i].textID.Contains("intro"))
            {
                subtitleText.text = textData[i].textContents;
                yield return new WaitForSeconds(textData[i].displayTime);
                subtitleText.text = null;
            }
        }
        subtitleBackdrop.SetActive(false);
    }

    public void PlayConsumeText(string pillName) // Overrides the previous text if message has not finished
    {
        if (currentConsumeRoutine != null)
        {
            StopCoroutine(currentConsumeRoutine);
            currentConsumeRoutine = null;
        }
        currentConsumeRoutine = StartCoroutine(ConsumeText(pillName));
    }
    public IEnumerator ConsumeText(string pillName) // Uses a secondary text so that the story stuff doesn't get overwritten
    {
        for (int i = 0; i < textData.Length; i++)
        {
            if (textData[i].textID.Contains(pillName))
            {
                subtitleText2.text = textData[i].textContents;
                yield return new WaitForSeconds(textData[i].displayTime);
                subtitleText2.text = null;
                yield break;
            }
        }
    }

}
