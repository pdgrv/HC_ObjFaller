using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private GameObject _throwerDelayBar;
    [SerializeField] private LevelCompletePanel _levelCompletePanel;

    public void ShowCompletePanel(bool IsWin, int currentLevel, int rewardAmount = 0, int percentOfLevelPassed = 0)
    {
        HideGameplayBars();
        _levelCompletePanel.gameObject.SetActive(true);
        _levelCompletePanel.Show();

        _levelCompletePanel.UpdateInfo(IsWin, currentLevel, rewardAmount, percentOfLevelPassed);
    }

    public void HideCompletePanel()
    {
        ShowGameplayBars();
        _levelCompletePanel.gameObject.SetActive(false);
    }

    public void ShowGameplayBars()
    {
        _progressBar.SetActive(true);
        _throwerDelayBar.SetActive(true);
    }

    public void HideGameplayBars()
    {
        _progressBar.SetActive(false);
        _throwerDelayBar.SetActive(false);
    }
}
