using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(AudioSource))]
public class Girl : MonoBehaviour
{
    [SerializeField] private float _audioDelay;
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private ParticleSystem _finalParticle;

    private Animator _animator;
    private AudioSource _audio;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    public void RollOver()
    {
        _animator.SetTrigger("RollOver");

        _audio.clip = _clips[Random.Range(0, _clips.Count)];
        _audio.PlayDelayed(_audioDelay);
    }

    public void PlayFinalParticle()
    {
        _finalParticle.Play();
    }
}
