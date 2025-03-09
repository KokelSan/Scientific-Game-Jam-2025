using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float RotationDuration = 1.0f;
    public Vector2 StartingEnergyRange = new Vector2(4, 8);

    public StarterAssetsInputsAdapter Inputs => _inputs;
    private StarterAssetsInputsAdapter _inputs;

    public int CurrentEnergy => _currentEnergy;
    private int _currentEnergy = 5;

    private int _collectedItemsNb;

    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputsAdapter>();
        GameManagerHandlerData.OnGameStarted += OnGameStarted;        
    }

    private void OnDestroy()
    {
        GameManagerHandlerData.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _currentEnergy = UnityEngine.Random.Range((int)StartingEnergyRange.x, (int)StartingEnergyRange.y + 1);
        GameUIManager.Instance.AddEnergyIcons(_currentEnergy);
    }

    public void LookAt(Transform direction, Action onRotationEnded)
    {
        StartCoroutine(LookAtCoroutine(direction, onRotationEnded));        
    }

    private IEnumerator LookAtCoroutine(Transform target, Action onRotationEnded)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, direction, transform.up);

        Vector3 initialRotation = transform.eulerAngles;
        Vector3 finalRotation = initialRotation + new Vector3(0, angle, 0);      

        float duration = 0;

        while (duration < RotationDuration) 
        {
            Vector3 rotation = Vector3.Lerp(initialRotation, finalRotation, duration / RotationDuration);
            transform.rotation = Quaternion.Euler(rotation);

            duration += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }      

        onRotationEnded?.Invoke();
    }

    public void AddEffect(bool positiveEffect)
    {
        if (!positiveEffect)
        {
            _currentEnergy--;
            GameUIManager.Instance.RemoveEnergyIcons(1);
        }
    }

    public void CollectItem(int energyCost)
    {
        _collectedItemsNb++;
        _currentEnergy -= energyCost;
        GameUIManager.Instance.RemoveEnergyIcons(energyCost);
        GameUIManager.Instance.AddItemIcon();
    }
}
