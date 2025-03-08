using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(SphereCollider))]
public class CollectableItem : MonoBehaviour
{
    public int EnergyCost = 1;
    public float OutlineActivationDistance = 5;
    public float PickUpDistance = .5f;
    public SphereCollider OutlineCollider;

    private Outline _outline;
    private Player _player;

    private bool _collectionPending = false;
    
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

    private void OnTriggerStay(Collider other)
    {
        if (_player == null) return;

        if (Vector3.Distance(transform.position, _player.transform.position) <= PickUpDistance)
        {
            if (!_collectionPending && _player.CurrentEnergy >= EnergyCost)
            {
                _collectionPending = true;
                _player.CollectItem(EnergyCost);
                Destroy(gameObject);
            }
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
