using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenLetterDisplayer : MonoBehaviour
{
    [Header("References")]
    public TextMeshPro textMeshProText = null;
    public TextMeshPro counterDisplay;
    public TaskManager taskManager;

    [Header("Parameters")]
    private string remainingWord = string.Empty;
    private string currentWord;
    public float difficultyLevel;
    public int emailSentCounter;

    [Header("Word Banks")]
    public List<string> wordBankEasy;
    public List<string> wordBankMedium;
    public List<string> wordBankHard;

    void Start()
    {
        difficultyLevel = 0;
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {

        // Set difficulty

        if (emailSentCounter == 7)
        {
            difficultyLevel = 1;
        }

        if (emailSentCounter == 14)
        {
            difficultyLevel = 2;
        }

        // Get bank word

        if (difficultyLevel == 0) 
        {
            currentWord = wordBankEasy[Random.Range(0, wordBankEasy.Count)];
            SetRemainingWord(currentWord);
        }

        if (difficultyLevel == 1)
        {
            currentWord = wordBankMedium[Random.Range(0, wordBankMedium.Count)];
            SetRemainingWord(currentWord);
        }

        if (difficultyLevel == 2)
        {
            currentWord = wordBankHard[Random.Range(0, wordBankHard.Count)];
            SetRemainingWord(currentWord);
        }
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        textMeshProText.text = remainingWord;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                emailSentCounter += 1;
                TaskComplete();
                counterDisplay.text = emailSentCounter.ToString();
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void TaskComplete()
    {
        if (taskManager.tasks.Count > 0)
        {
            GameObject first = taskManager.tasks[0];
            taskManager.tasks.RemoveAt(0);
            Destroy(first);
        }
    }

}
