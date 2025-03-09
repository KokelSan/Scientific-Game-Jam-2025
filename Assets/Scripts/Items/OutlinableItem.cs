using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(SphereCollider))]
public class OutlinableItem : MonoBehaviour
{
    public float OutlineActivationDistance = 5;
    public SphereCollider OutlineCollider;

    private Outline _outline;
    protected Player _player;

    private void Start()
    {
        _outline = GetComponent<Outline>();
    }

    private void OnValidate()
    {
        OutlineCollider.radius = OutlineActivationDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _player = player;
            SetOutline(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _player = null;
            SetOutline(false);
        }
    }

    private void SetOutline(bool outlineVisible)
    {
        _outline.enabled = outlineVisible;
    }
}
