using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Animator DoorAnimator;
    public string DoorAnimationStateName = "DoorOpening";

    void Start()
    {
        GameManagerHandlerData.OnGameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameManagerHandlerData.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        //DoorAnimator.Play(DoorAnimationStateName);
    }
}
