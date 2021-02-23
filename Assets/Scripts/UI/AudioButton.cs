using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class AudioButton : MonoBehaviour
{
    [SerializeField] private Sprite _enableSprite;
    [SerializeField] private Sprite _disableSprite;

    private Button _button;
    private Image _image;
    private bool _isAudioEnable;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        LoadAudioState();
        ChangeAudioState();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _isAudioEnable = !_isAudioEnable;
        ChangeAudioState();
        SaveAudioState();
    }

    private void ChangeAudioState()
    {
        if (_isAudioEnable)
        {
            _image.sprite = _enableSprite;
            AudioListener.volume = 1f;
        }
        else
        {
            _image.sprite = _disableSprite;
            AudioListener.volume = 0f;
        }
    }

    private void SaveAudioState()
    {
        PlayerPrefs.SetInt("Audio", _isAudioEnable ? 1 : 0);
    }

    private void LoadAudioState()
    {
        _isAudioEnable = PlayerPrefs.GetInt("Audio", 1) == 1;
    }
}
