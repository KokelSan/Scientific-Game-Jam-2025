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

        GameManagerHandlerData.OnGameStarted += OnGameStartedOrResumed;
        GameManagerHandlerData.OnGameResumed += OnGameStartedOrResumed;
        GameManagerHandlerData.OnGamePaused += OnGamePaused;
    }

    private void OnDestroy()
    {
        InputManagerHandlerData.OnMove -= _input.MoveInput;
        InputManagerHandlerData.OnLook -= _input.LookInput;
        InputManagerHandlerData.OnSpace -= Jump; 
        InputManagerHandlerData.OnSprint -= Sprint;

        GameManagerHandlerData.OnGameStarted -= OnGameStartedOrResumed;
        GameManagerHandlerData.OnGameResumed -= OnGameStartedOrResumed;
        GameManagerHandlerData.OnGamePaused -= OnGamePaused;
    }

    private void OnGameStartedOrResumed()
    {
        enableInputs = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnGamePaused()
    {
        enableInputs = false;
        _input.move = Vector2.zero;
        _input.look = Vector2.zero;
        Cursor.lockState = CursorLockMode.None;
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
