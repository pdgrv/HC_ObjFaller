using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    public int _currentMoney;

    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        LoadProgress();
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void AddMoney(int value)
    {
        ChangeMoneyAmount(value);
    }

    public bool TryRemoveMoney(int value)
    {
        if (_currentMoney >= value)
        {
            ChangeMoneyAmount(-value);
            return true;
        }
        else
            return false;
    }

    private void ChangeMoneyAmount(int value)
    {
        _currentMoney += value;
        MoneyChanged?.Invoke(_currentMoney);
        SaveProgress();
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("Money", _currentMoney);
    }

    private void LoadProgress()
    {
        _currentMoney = PlayerPrefs.GetInt("Money", 0);
    }
}
