using UnityEngine;

public abstract class SellableItem : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private bool _isActivated = false;

    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool IsActivated => _isActivated;

    public void Buy()
    {
        _isBuyed = true;
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public void DeActivate()
    {
        _isActivated = false;
    }
}