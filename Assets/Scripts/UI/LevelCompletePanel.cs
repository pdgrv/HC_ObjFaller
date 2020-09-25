using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private TMP_Text _levelProgressLabel;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Button _continueButton;

    public void UpdateInfo(bool IsWin, int currentLevel, int percentOfLevelPassed)
    {
        _levelLabel.text = currentLevel.ToString();
        if (IsWin)
        {
            _levelProgressLabel.text = "WIN!";
            _nextButton.gameObject.SetActive(true);
            _rewardButton.gameObject.SetActive(true);

            _replayButton.gameObject.SetActive(false);
            _continueButton.gameObject.SetActive(false);
        }
        else
        {
            _levelProgressLabel.text = percentOfLevelPassed.ToString() + '%';
            _nextButton.gameObject.SetActive(false);
            _rewardButton.gameObject.SetActive(false);

            _replayButton.gameObject.SetActive(true);
            _continueButton.gameObject.SetActive(true);
        }
    }
}
