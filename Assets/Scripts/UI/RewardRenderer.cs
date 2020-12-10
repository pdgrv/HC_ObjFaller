using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardRenderer : MonoBehaviour
{
    [SerializeField] private int _cascadeSpeed;
    [SerializeField] private TMP_Text _rewardAmount;
    [SerializeField] private Animation _animation;

    public void Render(int reward)
    {
        StartCoroutine(ShowReward(reward));
    }

    private IEnumerator ShowReward(int reward)
    {
        for (int i = 0; i < reward / _cascadeSpeed; i++)
        {
            _rewardAmount.text = "+" + (i * _cascadeSpeed).ToString();
            yield return new WaitForFixedUpdate();
        }

        _rewardAmount.text = "+" + reward.ToString();
        _animation.Play();
    }
}
