using TMPro;
using UnityEngine;
using static MoneyManager;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        MoneyManager.OnMoneyChange += OnMoneyChange;

        textMeshProUGUI.text = MoneyManager.Instance.Money.ToString();
    }

    private void OnMoneyChange(object e, OnMoneyChangeEventArgs eventArgs)
    {
        textMeshProUGUI.text = eventArgs.money.ToString();
    }
}
