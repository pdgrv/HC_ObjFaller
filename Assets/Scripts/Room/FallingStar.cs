﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _fullSize;
    [SerializeField] private float _growSpeed;

    [SerializeField] private Transform _rotateAroundPoint;

    private GameObject _currentStar;
    private Vector3 _rotateAxis;
    private Vector3 _rotatePoint;
    private Vector3 _increasingSize;

    private bool _needMove = false;

    private Coroutine _increaseSizeJob;

    private void Start()
    {
        _currentStar = _stars[1];

        _rotatePoint = _rotateAroundPoint.position;

        if (_increaseSizeJob != null)
            StopCoroutine(_increaseSizeJob);
    }

    private void Update()
    {
        if (_needMove)
        {
            if (Vector3.Distance(_currentStar.transform.position, _target.transform.position) < 0.01f)
                _needMove = false;

            _currentStar.transform.position = Vector3.MoveTowards(_currentStar.transform.position, _target.transform.position, _speed * Time.deltaTime);
            _currentStar.transform.RotateAround(_rotatePoint, -_rotateAxis, _rotateSpeed * Time.deltaTime);
        }
    }

    public void StartFalling()
    {
        _currentStar.transform.parent = null;
        _rotateAxis = _target.position - _currentStar.transform.position;

        _increaseSizeJob = StartCoroutine(IncreaseSizeThanMove());
    }

    private IEnumerator IncreaseSizeThanMove()
    {
        _increasingSize = new Vector3(_growSpeed, _growSpeed, _growSpeed);

        var waitForFixedUpdate = new WaitForFixedUpdate();

        while (_currentStar.transform.localScale.x < _fullSize * 1.2f)
        {
            _currentStar.transform.localScale += _increasingSize;
            yield return waitForFixedUpdate;
        }

        yield return waitForFixedUpdate;

        while (_currentStar.transform.localScale.x > _fullSize)
        {
            _currentStar.transform.localScale -= _increasingSize;
            yield return waitForFixedUpdate;
        }

        yield return waitForFixedUpdate;

        _needMove = true;
    }
}
