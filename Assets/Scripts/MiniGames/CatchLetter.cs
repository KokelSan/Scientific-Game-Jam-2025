using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class CatchLetter : OutlinableItem
{
    public static CatchLetter Instance;
    public Letter LetterPrefab;
    public Transform LettersParent;
    public int LetterToInstantiateNb;
    public List<string> list = new List<string>(){"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
    public int counter = 0;
    public TMP_Text DisplayCounter;

    private Letter lastLetter;
    
    protected bool startMiniGame = false;

    private float spownLetter = 0.5f;

    public float lyfeCycleLetter = 10f;
    public float LaunchDistance = .5f;

    private bool _isLaunching = false;

    public List<LyfeCycle> lyfeCycles = new List<LyfeCycle>();

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    protected void StartMiniGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _player.Inputs.FreezeInputs();

        DisplayCounter.gameObject.SetActive(true);

        StartCoroutine(InitializeLetters());
    }

    private void OnTriggerStay(Collider other)
    {
        if (_player == null) return;

        if (Vector3.Distance(transform.position, _player.transform.position) <= LaunchDistance)
        {
            if (!_isLaunching)
            {
                _isLaunching = true;
                StartMiniGame();
            }
        }
    }

    private string ComputeLetter(){
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public void NotifyLetterCaught(Letter letter)
    {
        counter++;
        DisplayCounter.text = $"{counter}";
        Destroy(letter.gameObject);
        Debug.Log($"Letter caught, count = {counter}");
    }

    public void NotifyLetterDestroy(Letter letter)
    {
        if(letter == lastLetter) 
        {
        _player.Inputs.RestoreInputs();
        DisplayCounter.gameObject.SetActive(false);
        }

        Destroy(letter.gameObject);
    }

    IEnumerator InitializeLetters()
    {
        for (int i = 0; i < LetterToInstantiateNb; i++)
        {
            Letter letter = Instantiate(LetterPrefab, LettersParent);
            letter.Initialize(i, ComputeLetter(), lyfeCycleLetter);

            if(i == LetterToInstantiateNb - 1)
            {
                lastLetter = letter;
            }
            
            yield return new WaitForSeconds(spownLetter);
        }
    }
    };



[Serializable]
public class LyfeCycle 
{
    public int state = 0;

    public float lyfeCycle = 0f;

}
