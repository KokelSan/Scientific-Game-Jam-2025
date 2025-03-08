using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    public Transform NpcParent;
    public List<Transform> NpcSpawnPoints = new List<Transform>();
    public NPC NpcPrefab;
    public DialogsSO Dialogs;

    List<string> _availablePositiveDialogs;
    List<string> _availableNegativeDialogs;

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
        _availablePositiveDialogs = new List<string>(Dialogs.PositiveDialogs);
        _availableNegativeDialogs = new List<string>(Dialogs.NegativeDialogs);

        for (int i = 0; i < NpcSpawnPoints.Count; i++)
        {
            Transform spawnPoint = NpcSpawnPoints[i];

            // random color ?
            NPC npc = Instantiate(NpcPrefab, spawnPoint.position, spawnPoint.rotation, NpcParent);
            npc.Initialize(i, ChoseDialog());
        }
    }

    private Dialog ChoseDialog()
    {
        string dialog;
        bool positiveDialog = Random.Range(0, 2) == 1;

        List<string> candidates = positiveDialog ? _availablePositiveDialogs : _availableNegativeDialogs;

        int index = Random.Range(0, candidates.Count);
        dialog = candidates[index];
        candidates.RemoveAt(index);

        return new Dialog(dialog, positiveDialog); 
    }
}
