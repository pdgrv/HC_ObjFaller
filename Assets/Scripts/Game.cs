using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Menu _menu;
    [SerializeField] private Thrower _objectThrower;
    [SerializeField] private MovieProducer _movieProducer;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private int _rewardForPlatform;
    [SerializeField] private InterstitialAds _interstitialAds;

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

    public event UnityAction<int> LevelChanged;

    private void Awake()
    {
        LoadProgress();

        LevelChanged?.Invoke(_currentLevel);
        _levelGenerator.StartLevel(_currentLevel);
    }

    private void OnEnable()
    {
        _levelGenerator.PlatformCountChanged += OnPlatformCountChanged;
        PlatformEventsHandler.BadPlatformDestroyed += OnBadPlatformDestroyed;
    }

    private void OnDisable()
    {
        _levelGenerator.PlatformCountChanged -= OnPlatformCountChanged;
        PlatformEventsHandler.BadPlatformDestroyed -= OnBadPlatformDestroyed;
    }

    private void OnBadPlatformDestroyed()
    {
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

    public void StartLevel()
    {
        LoadScene();
    }

    public void ResumeGame()
    {
        _objectThrower.AllowThrow();
        _menu.HideCompletePanel();
    }

    public void DoubleMoneyWon()
    {
        _playerMoney.AddMoney(_totalReward * 2);

        _menu.ShowCompletePanel(true, _currentLevel - 1, rewardAmount: _totalReward * 3);
    }

    private void LoseLevel()
    {
        _objectThrower.ProhibitThrow();
        _menu.ShowCompletePanel(false, _currentLevel, percentOfLevelPassed: _percentOfLevelPassed);

        _interstitialAds.ShowTick();
    }

    private void WinLevel()
    {
        _objectThrower.ProhibitThrow();
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

    private IEnumerator CompleteLevelAfterMovie()
    {
        yield return new WaitUntil(() => _movieProducer.IsMovieEnded);

        _menu.ShowCompletePanel(true, _currentLevel - 1, rewardAmount: _totalReward);

        _interstitialAds.ShowTick();
    }
}
