using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private TMP_Text _levelProgressLabel;
    [SerializeField] private TMP_Text _rewardAmount;
    [SerializeField] private GameObject _winOptions;
    [SerializeField] private GameObject _loseOptions;

    public void UpdateInfo(bool IsWin, int currentLevel, int percentOfLevelPassed = 0)
    {
        _levelLabel.text = currentLevel.ToString();
        if (IsWin)
        {
            _levelProgressLabel.text = "WIN!";
            //_rewardAmount.text = 

            _winOptions.SetActive(true);
            _loseOptions.SetActive(false);
        }
        else
        {
            _levelProgressLabel.text = "FAIL\n" + percentOfLevelPassed.ToString() + '%';

            _winOptions.SetActive(false);
            _loseOptions.SetActive(true);
        }
    }
}
