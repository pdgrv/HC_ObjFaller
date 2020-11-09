using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundAudio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        BackgroundAudio[] loadedObjects = FindObjectsOfType<BackgroundAudio>();
        if (loadedObjects.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_audioSource.isPlaying)
            return;
        else
        {
            _audioSource.clip = _clips[Random.Range(0, _clips.Count)];
            _audioSource.Play();
        }
    }
}
