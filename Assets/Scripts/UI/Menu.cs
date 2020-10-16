﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private LevelCompletePanel _levelCompletePanel;

    private void Start()
    {
        _levelCompletePanel.gameObject.SetActive(false);
    }

    public void CompleteLevel(bool IsWin, int currentLevel, int percentOfLevelPassed = 0)
    {
        HideProgressBar();
        _levelCompletePanel.gameObject.SetActive(true);

        _levelCompletePanel.UpdateInfo(IsWin, currentLevel, percentOfLevelPassed);
    }

    public void HideProgressBar()
    {
        _progressBar.SetActive(false);
    }
}
