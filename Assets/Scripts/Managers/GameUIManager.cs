using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public GameObject ItemIconPrefab;
    public GameObject EnergyIconPrefab;
    public CanvasGroup IconsPanel;
    public Transform EnergyIconsParent;
    public Transform ItemIconsParent;
    public Button EndgameQuitButton;

    private List<GameObject> EnergyIcons = new List<GameObject>();
    private List<GameObject> ItemIcons = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        IconsPanel.alpha = 0f;
    }

    private void Start()
    {
        GameManagerHandlerData.OnGameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameManagerHandlerData.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        IconsPanel.alpha = 1.0f;
    }

    public void AddEnergyIcons(int nb)
    {
        for (int i = 0; i < nb; i++) 
        {
            GameObject icon = Instantiate(EnergyIconPrefab, EnergyIconsParent);            
            EnergyIcons.Add(icon);
        }
    }

    public void RemoveEnergyIcons(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject icon = EnergyIcons.Last();
            EnergyIcons.Remove(icon);
            Destroy(icon);
        }
    }

    public void AddItemIcon()
    {
        GameObject icon = Instantiate(ItemIconPrefab, ItemIconsParent);
        ItemIcons.Add(icon);
    }

    public void RemoveItemIcon()
    {
        GameObject icon = ItemIcons.Last();
        ItemIcons.Remove(icon);
        Destroy(icon);
    }

    public void ShowGameOverPanel()
    {
        EndgameQuitButton.transform.parent.gameObject.SetActive(true);
        EndgameQuitButton.onClick.AddListener(GameManagerHandlerData.StopGame);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
