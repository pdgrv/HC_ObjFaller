using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieProducer : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _girlAnimator;
    [SerializeField] private float _girlAnimDelay;
    [SerializeField] private Animation _mainLightAnimation;

    [SerializeField] private float _remainingMovieTime;

    private Coroutine _movieJob;

    public bool IsMovieEnded { get; private set; } = false;

    public void StartMovie()
    {
        _movieJob = StartCoroutine(Movie());
    }

    private IEnumerator Movie()
    {
        _cameraAnimator.SetTrigger("MoveCamera");
        yield return new WaitForSeconds(_girlAnimDelay);

        _girlAnimator.SetTrigger("RollOver");
        _mainLightAnimation.Play();

        yield return new WaitForSeconds(_remainingMovieTime);
        IsMovieEnded = true;
    }    
}
