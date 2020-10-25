using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _button;

    private SellableItem _item;

    public event UnityAction<SellableItem, ItemView> ButtonClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(SellableItem item)
    {
        _item = item;

        //_icon.sprite = item.Icon;

        if (!item.IsBuyed)
            _price.text = item.Price.ToString();
        else
            _price.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        ButtonClick?.Invoke(_item, this);
    }
}