using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    private int _money;

    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        LoadProgress();
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int value)
    {
        ChangeMoneyAmount(value);
    }

    public bool TryRemoveMoney(int value)
    {
        if (_money >= value)
        {
            ChangeMoneyAmount(-value);
            return true;
        }
        else
            return false;
    }

    private void ChangeMoneyAmount(int value)
    {
        _money += value;
        MoneyChanged?.Invoke(_money);
        SaveProgress();
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("Money", _money);
    }

    private void LoadProgress()
    {
        _money = PlayerPrefs.GetInt("Money", 0);
    }
}
