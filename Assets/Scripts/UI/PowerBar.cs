using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private ThrowerDelay _throwerDelay;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animation _textAnimation;

    private void OnEnable()
    {
        _throwerDelay.ChangeThrowerDelay += OnChangeThrowerDelay;
    }

    private void OnDisable()
    {
        _throwerDelay.ChangeThrowerDelay -= OnChangeThrowerDelay;
    }

    private void OnChangeThrowerDelay(float value, float maxValue)
    {
        if (value > 0.001f)
        {
            _slider.gameObject.SetActive(true);
            _slider.value = value / maxValue;
        }
        else
            _slider.gameObject.SetActive(false);

        if (_slider.value > 0.9f)
        {
            _text.gameObject.SetActive(true);
            _text.text = "Awesome!";
            _textAnimation.Play();
        }
        else if (_slider.value > 0.6f)
        {
            _text.gameObject.SetActive(true);
            _text.text = "Cool!    ";
            _textAnimation.Stop();
        }
        else
            _text.gameObject.SetActive(false);
    }
}
