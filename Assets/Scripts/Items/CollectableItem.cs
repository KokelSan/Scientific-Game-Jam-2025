using UnityEngine;

public class CollectableItem : OutlinableItem
{
    public int EnergyCost = 1;
    public float PickUpDistance = .5f;

    private bool _collectionPending = false;

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
}
