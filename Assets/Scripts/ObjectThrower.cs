using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _delay;
    [SerializeField] private LevelGenerator _levelGenerator;

    private float _elapsedTime;

    private void Start()
    {
        _elapsedTime = _delay;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButton(0) && _elapsedTime >= _delay)
        {
            Throw();
            _elapsedTime = 0;
        }
    }

    private void Throw()
    {
        Instantiate(_ball,transform);
    }
}
