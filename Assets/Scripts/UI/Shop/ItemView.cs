using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _itemFrame;
    [SerializeField] private Animator _backgroundAnimator;
    [SerializeField] private Color _boughtFrameColor;
    [SerializeField] private Color _notBoughtFrameColor;
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

        _itemIcon.sprite = item.Icon;

        if (item.IsBuyed)
        {
            _price.gameObject.SetActive(false);
            _itemFrame.color = _boughtFrameColor;
        }
        else
        {
            _price.text = item.Price.ToString();
            _itemFrame.color = _notBoughtFrameColor;
        }

        if (item.IsActivated)
        {
            _backgroundAnimator.ResetTrigger("Stop");
            _backgroundAnimator.SetTrigger("Start");
        }
        else
        {
            _backgroundAnimator.ResetTrigger("Start");
            _backgroundAnimator.SetTrigger("Stop");
        }
    }

    private void OnButtonClick()
    {
        ButtonClick?.Invoke(_item, this);
    }
}