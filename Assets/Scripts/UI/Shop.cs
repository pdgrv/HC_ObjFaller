﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _throwedContainer;
    [SerializeField] private List<ThrowedObject> _throwedItems;
    [SerializeField] private ObjectThrower _thrower;
    [SerializeField] private PlayerMoney _playerMoney;

    private void Start()
    {
        foreach (var item in _throwedItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(SellableItem item)
    {
        var view = Instantiate(_template, _throwedContainer);
        view.ButtonClick += OnButtonClick;

        view.Render(item);
    }

    private void OnButtonClick(SellableItem item, ItemView view)
    {
        if (!item.IsBuyed)
        {
            if (_playerMoney.TryRemoveMoney(item.Price))
            {
                item.Buy();

                TryActivateItem(item);
            }
            else
            {
                Debug.Log("Недостаточно денег");
            }
        }
        else
        {
            TryActivateItem(item);
        }
    }

    private void TryActivateItem(SellableItem item)
    {
        if (_throwedItems.Contains((ThrowedObject)item))
        {
            ThrowedObject upItem = item as ThrowedObject; 
            _thrower.SetThrowedObject(upItem);
        }
    }
}
