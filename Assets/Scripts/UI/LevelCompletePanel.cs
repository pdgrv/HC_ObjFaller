﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private TMP_Text _levelProgressLabel;
    [SerializeField] private GameObject _winOptions;
    [SerializeField] private GameObject _loseOptions;
    [SerializeField] private RewardRenderer _rewardAmount;
    [SerializeField] private Animation _openAnimation;

    public void UpdateInfo(bool IsWin, int currentLevel, int rewardAmount, int percentOfLevelPassed)
    {
        _levelLabel.text = currentLevel.ToString();
        if (IsWin)
        {
            _levelProgressLabel.text = "WIN!";
            //_rewardAmount.text = "+" + rewardAmount.ToString();
            _rewardAmount.Render(rewardAmount);

            _winOptions.SetActive(true);
            _loseOptions.SetActive(false);
        }
        else
        {
            _levelProgressLabel.text = "FAIL " + percentOfLevelPassed.ToString() + '%';

            _winOptions.SetActive(false);
            _loseOptions.SetActive(true);
        }
    }

    public void Show()
    {
        _openAnimation.Play();
    }
}
