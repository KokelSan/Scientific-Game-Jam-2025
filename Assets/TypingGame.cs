using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingGame : MonoBehaviour
{
    public TMP_Text consigneText; 
    public TMP_InputField inputField;
    public TMP_Text resultText;
    public Button enterButton;

    public string text;
    private float timeLimit = 100000000000000f;
    private float timeRemaining;
    private bool isGameActive = false;

    void Start()
    {
        enterButton.onClick.AddListener(CheckInput);
        StartGame();
    }

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                EndGame(false);
            }
        }
    }


    void StartGame()
    {

        timeRemaining = timeLimit;
        inputField.interactable = true;
        isGameActive = true;
        inputField.text = "";
        resultText.text = "";
    }

    void CheckInput()
    {
        string[] words = { "TESTER" };
        text = words[Random.Range(0, words.Length)];

        if(inputField.text == text)
        {
            EndGame(true);
        } else {
            EndGame(false);
        }
    }

    void EndGame(bool isWin)
    {
        inputField.interactable = false;

        if (isWin)
        {
            resultText.text = "Gagné !";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "Perdu !";
            resultText.color = Color.red;
        }
    }
}
