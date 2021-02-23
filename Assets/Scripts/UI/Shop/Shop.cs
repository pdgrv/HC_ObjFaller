using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _throwedContainer;
    [SerializeField] private List<ThrowedObject> _throwedItems;
    [SerializeField] private Thrower _thrower;
    [SerializeField] private Transform _roomContainer;
    [SerializeField] private List<RoomItem> _roomItems;
    [SerializeField] private PlayerMoney _playerMoney;

    private int _currentThrowedItem;
    private List<ItemView> _throwedViewsList = new List<ItemView>();
    private List<ItemView> _roomViewsList = new List<ItemView>();

    private void OnDisable()
    {
        foreach (var item in _throwedViewsList)
        {
            item.ButtonClick -= OnButtonClick;
        }

        foreach (var item in _roomViewsList)
        {
            item.ButtonClick -= OnButtonClick;
        }
    }

    private void Awake()
    {
        foreach (var item in _throwedItems)
        {
            AddItem(item, _throwedContainer, _throwedViewsList);
        }

        foreach (var item in _roomItems)
        {
            AddItem(item, _roomContainer, _roomViewsList);
        }

        LoadAllItems();
        TryActivateItem(_throwedItems[_currentThrowedItem]);
        ReRenderAll();
    }

    private void AddItem(SellableItem item, Transform container, List<ItemView> _itemViewList)
    {
        var view = Instantiate(_template, container);
        _itemViewList.Add(view);

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
                return;
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
        if (item is ThrowedObject)
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
        else if (item is RoomItem)
        {
            RoomItem upItem = item as RoomItem;

            upItem.TryRender();
        }

        SaveAllItems();
    }

    private void ReRenderAll()
    {
        for (int i = 0; i < _throwedViewsList.Count; i++)
        {
            _throwedViewsList[i].Render(_throwedItems[i]);
        }

        for (int i = 0; i < _roomViewsList.Count; i++)
        {
            _roomViewsList[i].Render(_roomItems[i]);
        }
    }

    private void SaveAllItems()
    {
        PlayerPrefs.SetInt("CurrentThrowedItem", _currentThrowedItem);

        SaveItems("BuyedThrowedItems", _throwedItems);
        SaveItems("BuyesRoomItems", _roomItems);
    }

    private void SaveItems<T>(string key, List<T> items) where T : SellableItem
    {
        string buyedItemBools = "";
        foreach (T item in items)
        {
            if (item.IsBuyed)
                buyedItemBools += 1;
            else
                buyedItemBools += 0;
        }

        PlayerPrefs.SetString(key, buyedItemBools);
    }

    private void LoadAllItems()
    {
        _currentThrowedItem = PlayerPrefs.GetInt("CurrentThrowedItem");

        LoadItems("BuyedThrowedItems", _throwedItems);
        LoadItems("BuyesRoomItems", _roomItems);
    }

    private void LoadItems<T>(string key, List<T> items) where T : SellableItem
    {
        string buyedItemBools = PlayerPrefs.GetString(key);

        if (!string.IsNullOrEmpty(buyedItemBools))
        {
            for (int i = 0; i < buyedItemBools.Length; i++)
            {
                if (buyedItemBools[i] == '1')
                    items[i].Buy();
            }
        }
    }
}
