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

    private int _currentLevel = 1;
    private int _percentOfLevelPassed;

    public event UnityAction<int> LevelChanged; // мб можно избавиться от ээтого события 

    private void Awake()
    {
        LoadProgress();
        if (_currentLevel == 0)
            _currentLevel++;

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
        _currentLevel++;
        LevelChanged?.Invoke(_currentLevel);

        SaveProgress();

        RestartLevel();
    }

    public void GameOver()
    {
        Debug.Log("Вы проиграли.");
        CompleteLevel(false);
    }

    private void OnPlatformCountChanged(int value, int maxValue)
    {
        _percentOfLevelPassed = (int)((float)value / maxValue * 100);

        if (value >= maxValue)
        {
            CompleteLevel(true);
        }
    }

    private void CompleteLevel(bool isWin)
    {
        _objectThrower.Stop();
        _menu.CompleteLevel(isWin, _currentLevel, _percentOfLevelPassed);
        SaveProgress();
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
}
