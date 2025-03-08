using StarterAssets;
using UnityEngine;

public class StarterAssetsInputsAdapter : MonoBehaviour
{
    private StarterAssetsInputs _input;

    private bool enableInputs = false;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();

        InputManagerHandlerData.OnMove += Move;
        InputManagerHandlerData.OnLook += Look;
        InputManagerHandlerData.OnSpace += Jump;
        InputManagerHandlerData.OnSprint += Sprint;

        GameManagerHandlerData.OnGameStarted += RestoreInputs;
        GameManagerHandlerData.OnGameResumed += RestoreInputs;
        GameManagerHandlerData.OnGamePaused += OnGamePaused;
    }

    private void OnDestroy()
    {
        InputManagerHandlerData.OnMove -= _input.MoveInput;
        InputManagerHandlerData.OnLook -= _input.LookInput;
        InputManagerHandlerData.OnSpace -= Jump; 
        InputManagerHandlerData.OnSprint -= Sprint;

        GameManagerHandlerData.OnGameStarted -= RestoreInputs;
        GameManagerHandlerData.OnGameResumed -= RestoreInputs;
        GameManagerHandlerData.OnGamePaused -= OnGamePaused;
    }

    public void OnGamePaused()
    {
        FreezeInputs();
        Cursor.lockState = CursorLockMode.None;
    }

    public void FreezeInputs()
    {
        enableInputs = false;
        _input.move = Vector2.zero;
        _input.look = Vector2.zero;
    }

    public void RestoreInputs()
    {
        enableInputs = true;
        Cursor.lockState = CursorLockMode.Locked;
    }    

    private void Move(Vector2 input)
    {
        if (!enableInputs) return;

        _input.MoveInput(input);
    }

    private void Look(Vector2 input)
    {
        if (!enableInputs) return;

        _input.LookInput(input);
    }

    private void Jump()
    {
        if (!enableInputs) return;

        _input.JumpInput(true);
    }

    private void Sprint()
    {
        if (!enableInputs) return;

        _input.SprintInput(true);
    }
}
