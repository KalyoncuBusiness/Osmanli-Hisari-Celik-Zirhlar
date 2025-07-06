using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private SoldierTypeSO _selectedBuilding;
    private CanvasGroup _canvasGroup;

    public SoldierTypeSO SelectedBuilding => _selectedBuilding;
    public Action OnSelectedBuildingChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;

        GameManager.OnFightStart += OnFightStart;
        GameManager.OnFightEnd += OnFightEnd;

    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;

        SetSelectedBuilding(null);
    }

    private void OnFightStart(object sender, GameManager.OnFightStartEventArgs e)
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
    }

    public void SetSelectedBuilding(SoldierTypeSO selectedBuilding)
    {
        OnSelectedBuildingChanged?.Invoke();
        _selectedBuilding = selectedBuilding;
    }
}
