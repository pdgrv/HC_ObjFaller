using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _delay;
    [SerializeField] private ThrowerTarget _target;

    private List<Ball> _ballPool; // сделать throw через пул

    private bool _canThrow = true;
    private float _elapsedTime;


    private void Start()
    {
        _elapsedTime = _delay;
    }

    private void Update()
    {
        if (!_canThrow)
            return;

        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButton(0) && _elapsedTime >= _delay)
        {
            Throw();
            _elapsedTime = 0;
        }
    }

    public void Stop()
    {
        _canThrow = false;//Добавить удаление уже вылетевших после стопа шаров
    }

    private void Throw()
    {
        Instantiate(_ball, transform);
    }
}
