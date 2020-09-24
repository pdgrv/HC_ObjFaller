using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumbers : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _nextLevel;

    private void OnEnable()
    {
        _gameManager.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _gameManager.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int value)
    {
        _currentLevel.text = value.ToString();
        _nextLevel.text = (value + 1).ToString();
    }
}
