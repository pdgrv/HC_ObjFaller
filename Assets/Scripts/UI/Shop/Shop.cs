using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _throwedContainer;
    [SerializeField] private List<ThrowedItem> _throwedItems;
    [SerializeField] private ObjectThrower _thrower;
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

        LoadItems();
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
        if (item is ThrowedItem)
        {
            ThrowedItem upItem = item as ThrowedItem;

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

        SaveItems();
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

    private void SaveItems()
    {
        PlayerPrefs.SetInt("CurrentThrowedItem", _currentThrowedItem);

        string buyedThrowedItemBools = "";
        foreach (ThrowedItem item in _throwedItems)
        {
            if (item.IsBuyed)
                buyedThrowedItemBools += 1;
            else
                buyedThrowedItemBools += 0;
        }

        PlayerPrefs.SetString("BuyedThrowedItems", buyedThrowedItemBools);

        string buyedRoomItemsBools = "";
        foreach (RoomItem item in _roomItems)
        {
            if (item.IsBuyed)
                buyedRoomItemsBools += 1;
            else
                buyedRoomItemsBools += 0;
        }

        PlayerPrefs.SetString("BuyedRoomItems", buyedRoomItemsBools);
    }

    private void LoadItems()
    {
        _currentThrowedItem = PlayerPrefs.GetInt("CurrentThrowedItem");

        string buyedThrowedItemBools = PlayerPrefs.GetString("BuyedThrowedItems");

        if (!string.IsNullOrEmpty(buyedThrowedItemBools))
        {
            for (int i = 0; i < buyedThrowedItemBools.Length; i++)
            {
                if (buyedThrowedItemBools[i] == '1')
                    _throwedItems[i].Buy();
            }
        }

        string buyedRoomItemBools = PlayerPrefs.GetString("BuyedRoomItems");

        if (!string.IsNullOrEmpty(buyedRoomItemBools))
        {
            for (int i = 0; i < buyedRoomItemBools.Length; i++)
            {
                if (buyedRoomItemBools[i] == '1')
                    _roomItems[i].Buy();
            }
        }
    }
}
