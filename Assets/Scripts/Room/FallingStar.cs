using System.Collections;
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

    private GameObject _currentStar;
    private Vector3 _rotateAxis;
    private Vector3 _increasingSize;

    private bool _needMove = false;

    private void Update()
    {
        if (_needMove)
        {
            if (Vector3.Distance(_currentStar.transform.position, _target.transform.position) < 0.01f)
                _needMove = false;

            _currentStar.transform.position = Vector3.MoveTowards(_currentStar.transform.position, _target.transform.position, _speed * Time.deltaTime);
            _currentStar.transform.RotateAround(Vector3.zero, _rotateAxis, _rotateSpeed * Time.deltaTime);
        }
    }

    public void StartFalling()
    {
        _currentStar = _stars[Random.Range(0, _stars.Count)];
        _currentStar.transform.parent = null;
        _rotateAxis = (_target.position - _currentStar.transform.position);

        StartCoroutine(IncreaseSizeThanMove());
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

        while (_currentStar.transform.localScale.x > _fullSize)
        {
            _currentStar.transform.localScale -= _increasingSize;
            yield return waitForFixedUpdate;
        }

        _needMove = true;
    }
}
