using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitParticle;
    [SerializeField] private Vector3 _offset;

    public void Play(Vector3 hitPosition)
    {
        _hitParticle.transform.position = hitPosition + _offset;

        _hitParticle.Play();
    }
}
