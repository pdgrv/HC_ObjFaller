using System.Collections;
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

    private int _currentLevel = 1;
    private int _percentOfLevelPassed;
    private int _platformsCount;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        RestartLevel();
    }

    public void GameOver()
    {
        Debug.Log("Вы проиграли.");
        LoseLevel();
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
