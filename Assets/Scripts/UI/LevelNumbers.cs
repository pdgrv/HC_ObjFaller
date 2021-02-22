using TMPro;
using UnityEngine;

public class LevelNumbers : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _nextLevel;

    private void OnEnable()
    {
        _game.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _game.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int value)
    {
        _currentLevel.text = value.ToString();
        _nextLevel.text = (value + 1).ToString();
    }
}
