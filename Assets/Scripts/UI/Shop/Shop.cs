using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _throwedContainer;
    [SerializeField] private List<ThrowedObject> _throwedItems;
    [SerializeField] private ObjectThrower _thrower;
    [SerializeField] private PlayerMoney _playerMoney;

    private int _currentThrowedItem;
    private List<ItemView> _viewsList = new List<ItemView>();

    private void Start()
    {
        foreach (var item in _throwedItems)
        {
            AddItem(item);
        }

        LoadItems();
        TryActivateItem(_throwedItems[_currentThrowedItem]);
    }

    private void AddItem(SellableItem item)
    {
        var view = Instantiate(_template, _throwedContainer);
        _viewsList.Add(view);

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

        ReRenderAll();
    }

    private void TryActivateItem(SellableItem item)
    {
        if (_throwedItems.Contains((ThrowedObject)item))
        {
            ThrowedObject upItem = item as ThrowedObject;

            _thrower.SetThrowedObject(upItem);

            _currentThrowedItem = _throwedItems.IndexOf(upItem);

            foreach (var item_ in _throwedItems)
            {
                item_.DeActivate();
            }
            item.Activate();
        }
        //else if (_roomItems.Contains((RoomItem)item)) { }

        SaveItems();
    }

    private void ReRenderAll()
    {
        for (int i = 0; i < _viewsList.Count; i++)
        {
            _viewsList[i].Render(_throwedItems[i]);
        }
    }

    private void SaveItems()
    {
        PlayerPrefs.SetInt("CurrentThrowedItem", _currentThrowedItem);

        string buyedItemBools = "";
        foreach (ThrowedObject item in _throwedItems)
        {
            if (item.IsBuyed)
                buyedItemBools += 1;
            else
                buyedItemBools += 0;
        }
        PlayerPrefs.SetString("BuyedItems", buyedItemBools);
    }

    private void LoadItems()
    {
        _currentThrowedItem = PlayerPrefs.GetInt("CurrentThrowedItem");

        string buyedItemBools = PlayerPrefs.GetString("BuyedItems");

        for (int i = 0; i < _throwedItems.Count; i++)
        {
            if (buyedItemBools[i] == 1)
                _throwedItems[i].Buy();
        }
    }
}
