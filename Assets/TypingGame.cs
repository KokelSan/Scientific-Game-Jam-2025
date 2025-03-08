using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class TypingGame : MonoBehaviour
{
    public TMP_Text consigneText; 
    public TMP_InputField inputField;
    public TMP_Text resultText;
    public Button enterButton;
    Dictionary<int, string> consignes = new Dictionary<int, string>();

    public string text;
    private float timeLimit = 100000000000000f;
    private float timeRemaining;
    private bool isGameActive = false; 


    void Start()
    {
        enterButton.onClick.AddListener(CheckInput);

        consignes.Add(1, "schrifblokrbsts");
        consignes.Add(2, "laxydbzwo");
        consignes.Add(3, "soutien");

        consigneText.text = consignes[1];

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

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                CheckInput();
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
        if (inputField.text.Length == consigneText.text.Length)
        {
            bool isMatching = true;

            for (int i = 0; i < consigneText.text.Length; i++)
            {
                if (inputField.text[i] != consigneText.text[i])
                {
                    isMatching = false;  
                    break;
                }
            }

            if (isMatching)
            {
                EndGame(true);
            }
    } else {
        {
            EndGame(false);
        }
    }
    }

    void EndGame(bool isWin)
    {
        inputField.interactable = true;
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
