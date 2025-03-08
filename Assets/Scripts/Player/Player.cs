using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float RotationDuration = 1.0f;

    public StarterAssetsInputsAdapter Inputs => _inputs;
    private StarterAssetsInputsAdapter _inputs;

    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputsAdapter>();
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

}
