using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private LevelGenerator _levelGenerator;

    private void OnEnable()
    {
        _levelGenerator.PlatformCountChanged += OnPlatformCountChanged;
    }

    private void OnDisable()
    {
        _levelGenerator.PlatformCountChanged -= OnPlatformCountChanged;
    }

    private void OnPlatformCountChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
    }
}