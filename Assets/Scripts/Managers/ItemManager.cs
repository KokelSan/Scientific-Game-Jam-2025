using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public Transform ItemsParent;
    public Vector2 ItemToSpawnRange = new Vector2(1, 3);
    public List<CollectableItem> ItemPrefabs;
    public List<Transform> ItemSpawnpoints;

    private List<CollectableItem> _spawnedItems;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void Start()
    {
        SpawnItems();
    }

    public void SpawnItems()
    {
        List<Transform> availableSpawnPoints = new List<Transform>(ItemSpawnpoints);
        int itemsNb = Random.Range((int)ItemToSpawnRange.x, (int)ItemToSpawnRange.y + 1);

        for (int i = 0; i < itemsNb; i++)
        {
            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
            int itemIndex = Random.Range(0, ItemPrefabs.Count);

            Transform spawnPoint = availableSpawnPoints[spawnIndex];
            CollectableItem item = Instantiate(ItemPrefabs[itemIndex], spawnPoint.position, spawnPoint.rotation, ItemsParent);

            availableSpawnPoints.RemoveAt(spawnIndex);
        }
    }
}