using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlatformAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _goodClip; 
    [SerializeField] private List<AudioClip> _badClips;

    private AudioSource _audio;

    private int _switcher = 0;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayAudio(bool isGood)
    {
        if (isGood)
        {
            _audio.clip = _goodClip;
            _audio.Play();
        }
        else
        {
            _switcher = _switcher == 1 ? 0 : 1;
            _audio.clip = _badClips[_switcher];

            _audio.Play();
        }
    }
}
