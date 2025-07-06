using KalyoncuBusiness;
using KalyoncuBusiness.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuItem : MonoBehaviour
{
    [SerializeField] private SoldierTypeSO soldierTypeSO;



    private GameObject frame;

    private void Start()
    {
        InventoryManager.Instance.OnSelectedBuildingChanged += OnSelectedBuildingChanged;

        transform.GetChild(0).GetComponent<Image>().sprite = soldierTypeSO.icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = soldierTypeSO.nameString;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = soldierTypeSO.cost.ToString("");
    }

    public void OnClick()
    {
        if (soldierTypeSO == null)
        {
            Debug.LogWarning($"build type so is empty - {gameObject.name}");
            return;
        }

        InventoryManager.Instance.SetSelectedBuilding(soldierTypeSO);

        frame = UI_Sprite.CreateImage(Assets.i.s_SelectFrame, transform);

        Stats.Instance.ShowStats(soldierTypeSO);
    }

    public void OnSelectedBuildingChanged()
    {
        if (frame == null)
            return;

        Destroy(frame);
    }
}
