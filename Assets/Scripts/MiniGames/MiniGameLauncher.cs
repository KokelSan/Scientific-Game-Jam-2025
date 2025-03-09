using UnityEngine;

public class MiniGameLauncher : OutlinableItem
{
    public CatchLetter CatchLetter;
    public float LaunchDistance = .5f;

    private bool _isLaunching = false;

    private void OnTriggerStay(Collider other)
    {
        if (_player == null) return;

        if (Vector3.Distance(transform.position, _player.transform.position) <= LaunchDistance)
        {
            if (!_isLaunching)
            {
                _isLaunching = true;
                StartMinigame();
            }
        }
    }

    private void StartMinigame()
    {
        // Game.Launch();
    }
}
