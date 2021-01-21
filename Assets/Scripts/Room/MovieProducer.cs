using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieProducer : MonoBehaviour
{
    [SerializeField] private FallingStar _fallingStar;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Girl _girl;
    [SerializeField] private LightChanger _lightChanger;

    [SerializeField] private float _cameraMoveDelay;
    [SerializeField] private float _girlAnimDelay;
    [SerializeField] private float _remainingMovieTime;

    public bool IsMovieEnded { get; private set; } = false;

    public void StartMovie()
    {
        StartCoroutine(Movie());
    }

    private IEnumerator Movie()
    {
        _fallingStar.StartFalling();
        yield return new WaitForSeconds(_cameraMoveDelay);

        _cameraAnimator.SetTrigger("MoveCamera");
        yield return new WaitForSeconds(_girlAnimDelay);

        _girl.RollOver();
        //_lightChanger.Dim(); //заменен постобработкой

        yield return new WaitForSeconds(_remainingMovieTime);
        IsMovieEnded = true;
    }    
}
