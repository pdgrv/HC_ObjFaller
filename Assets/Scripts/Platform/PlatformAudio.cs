using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlatformAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _goodClip;
    [SerializeField] private List<AudioClip> _badClips;

    private AudioSource _audio;

    private int _switcher = 0;

    private void OnEnable()
    {
        PlatformEventsHandler.PlatformDestoyed += OnPlatformDestroyed;
        PlatformEventsHandler.BadPlatformHit += OnBadPlatformHit;
    }

    private void OnDisable()
    {
        PlatformEventsHandler.PlatformDestoyed -= OnPlatformDestroyed;
        PlatformEventsHandler.BadPlatformHit -= OnBadPlatformHit;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnPlatformDestroyed(Platform platform)
    {
        _audio.clip = _goodClip;
        _audio.Play();
    }

    private void OnBadPlatformHit()
    {
        _switcher = _switcher == 1 ? 0 : 1;
        _audio.clip = _badClips[_switcher];

        _audio.Play();
    }
}
