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
    private int _percentLevelPassed;

    public event UnityAction<int> LevelChanged;

    private void Start()
    {
        LevelChanged?.Invoke(_currentLevel);
    }

    private void OnEnable()
    {
        _levelGenerator.PlatformCountChanged += OnPlatformCountChanged;
    }

    private void OnDisable()
    {
        _levelGenerator.PlatformCountChanged -= OnPlatformCountChanged;
    }
    private void CompleteLevel(bool isWin)
    {
        _objectThrower.Stop();
        _menu.CompleteLevel(isWin, _currentLevel, _percentLevelPassed);
    }

    private void OnPlatformCountChanged(int value, int maxValue)
    {
        _percentLevelPassed = (int)((float)value / maxValue * 100);

        if (value >= maxValue)
        {
            CompleteLevel(true);
        }
    }

    public void GameOver()
    {
        Debug.Log("Вы проиграли.");
        CompleteLevel(false);
    }

    public void NextLevel()
    {
        _currentLevel++;
        LevelChanged?.Invoke(_currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
