using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    public static DialogUIManager Instance;

    public float PanelTransitionDuration = .3f;
    public float CharactersToWritePerSecond = 90;
    public CanvasGroup DialogCanvas;
    public TMP_Text dialogText;

    private Action _onDialogClosed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;

        DialogCanvas.alpha = 0;
    }

    public void ShowDialog(string dialog, Action onDialogShowed, Action onDialogClosed)
    {
        StartCoroutine(ShowDialogCoroutine(dialog, onDialogShowed, onDialogClosed));
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator ShowDialogCoroutine(string dialog, Action onDialogShowed, Action onDialogClosed)
    {
        DialogCanvas.interactable = false;

        float t = 0;
        while (t < PanelTransitionDuration) 
        {
            DialogCanvas.alpha = Mathf.Lerp(0, 1, t / PanelTransitionDuration);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        dialogText.text = "";

        foreach (char character in dialog)
        {
            dialogText.text += character;
            yield return new WaitForSeconds(1/CharactersToWritePerSecond);
        }

        DialogCanvas.interactable = true;
        _onDialogClosed = onDialogClosed;

        onDialogShowed?.Invoke();
    }

    public void HideDialog()
    {
        DialogCanvas.alpha = 0;
        DialogCanvas.interactable = false;
        Cursor.lockState = CursorLockMode.Locked;
        _onDialogClosed?.Invoke();
    }
}
