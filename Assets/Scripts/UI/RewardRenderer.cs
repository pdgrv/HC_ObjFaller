using System.Collections;
using TMPro;
using UnityEngine;

public class RewardRenderer : MonoBehaviour
{
    [SerializeField] private float _cascadeSpeedMultiplier;
    [SerializeField] private TMP_Text _rewardAmount;
    [SerializeField] private Animation _animation;

    public void Render(int reward)
    {
        StartCoroutine(ShowReward(reward));
    }

    private IEnumerator ShowReward(int reward)
    {
        int cascadeSpeed = (int)(reward * _cascadeSpeedMultiplier);

        for (int i = 0; i < reward / cascadeSpeed; i++)
        {
            _rewardAmount.text = "+" + (i * cascadeSpeed).ToString();
            yield return new WaitForFixedUpdate();
        }

        _rewardAmount.text = "+" + reward.ToString();
        _animation.Play();
    }
}
