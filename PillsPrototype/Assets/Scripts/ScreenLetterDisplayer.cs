using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenLetterDisplayer : MonoBehaviour
{
    // You need a word bank.
    public TextMeshPro textMeshProText = null;

    private string remainingWord = string.Empty;
    private string currentWord;
    public List<string> wordBank;


    void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        // Get bank word.
        currentWord = wordBank[Random.Range(0, wordBank.Count)];
        SetRemainingWord(currentWord);
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

}
