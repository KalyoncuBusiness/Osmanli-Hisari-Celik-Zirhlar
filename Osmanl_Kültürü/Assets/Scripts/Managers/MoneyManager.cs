using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private float _money = 50f;

    public float Money => _money;

    public class OnMoneyChangeEventArgs : EventArgs
    {
        public float money;
    }

    public static event EventHandler<OnMoneyChangeEventArgs> OnMoneyChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddMoney(float money)
    {
        _money += money;

        MoneyChanged();
    }

    public void RemoveMoney(float money)
    {
        _money -= money;

        MoneyChanged();
    }

    private void MoneyChanged()
    {
        OnMoneyChange(this, new OnMoneyChangeEventArgs { money = _money });
    }
}
