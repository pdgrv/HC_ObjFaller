using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    [SerializeField] private RewardType _buttonReward;
    [SerializeField] private Ads _ads;

    private Button _button;
    private Animation _animation;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animation = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _ads.ShowRewarded(_buttonReward);

        _animation.Stop();
        _button.interactable = false;
    }
}
