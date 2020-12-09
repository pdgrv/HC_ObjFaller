﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Menu _menu;
    [SerializeField] private ObjectThrower _objectThrower;
    [SerializeField] private MovieProducer _movieProducer;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private int _rewardForPlatform;
    [SerializeField] private Ads _ads;

    private int _currentLevel = 1;
    private int _percentOfLevelPassed;
    private int _platformsCount;
    private int _restartsCount = 0;

    private int _totalReward
    {
        get
        {
            return _platformsCount * _rewardForPlatform;
        }
    }

    public event UnityAction<int> LevelChanged; // мб можно избавиться от ээтого события 

    private void Awake()
    {
        LoadProgress();

        LevelChanged?.Invoke(_currentLevel);
        _levelGenerator.StartLevel(_currentLevel);
    }

    private void OnEnable()
    {
        _levelGenerator.PlatformCountChanged += OnPlatformCountChanged;
    }

    private void OnDisable()
    {
        _levelGenerator.PlatformCountChanged -= OnPlatformCountChanged;
    }

    public void RestartLevel()
    {
        ++_restartsCount;

        if (_currentLevel > 5)
        {
            if (_restartsCount >= 2)
            {
                _restartsCount = 0;
                _ads.ShowInterstitial();
            }
        }
        else
        {
            if (_restartsCount >= 3)
            {
                _restartsCount = 0;
                _ads.ShowInterstitial();
            }
        }

        LoadScene();
    }

    public void StartLevel()
    {
        if (_currentLevel > 10)
        {
            if (_currentLevel % 3 == 0)
                _ads.ShowInterstitial();
        }
        else
        {
            if (_currentLevel % 4 == 0)
                _ads.ShowInterstitial();
        }

        LoadScene();
    }

    public void GameOver()
    {
        Debug.Log("Вы проиграли.");
        LoseLevel();
    }

    public void ResumeGame()
    {
        _objectThrower.GoThrow();
        _menu.HideCompletePanel();
    }

    public void DoubleMoneyWon()
    {
        _playerMoney.AddMoney(_totalReward);

        _menu.ShowCompletePanel(true, _currentLevel - 1, rewardAmount: _totalReward * 2);
    }

    private void OnPlatformCountChanged(int value, int maxValue)
    {
        _percentOfLevelPassed = (int)((float)value / maxValue * 100);
        _platformsCount = maxValue;

        if (value >= maxValue)
        {
            WinLevel();
        }
    }

    private void LoseLevel()
    {
        _objectThrower.StopThrow();
        _menu.ShowCompletePanel(false, _currentLevel, percentOfLevelPassed: _percentOfLevelPassed);
    }

    private void WinLevel()
    {
        _objectThrower.StopThrow();
        _menu.HideGameplayBars();
        _playerMoney.AddMoney(_totalReward);

        _currentLevel++;
        SaveProgress();
        LevelChanged?.Invoke(_currentLevel);

        _movieProducer.StartMovie();
        StartCoroutine(CompleteLevelAfterMovie());
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("Level", _currentLevel);
    }

    private void LoadProgress()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 1);
    }

    [ContextMenu("DeleteSaves")]
    private void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator CompleteLevelAfterMovie()
    {
        yield return new WaitUntil(() => _movieProducer.IsMovieEnded);

        _menu.ShowCompletePanel(true, _currentLevel - 1, rewardAmount: _totalReward);
    }
}
