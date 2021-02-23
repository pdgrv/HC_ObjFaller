using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;

    private TMP_Text _label;

    private void Awake()
    {
        _label = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _playerMoney.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _playerMoney.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _label.text = money.ToString();
    }
}
