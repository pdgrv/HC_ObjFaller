using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnArea;

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
        Vector3 spawnPoint = RandomPointInArea(_spawnArea);
        Ball newBall = Instantiate(_ball, spawnPoint, Quaternion.identity, transform);

        newBall.Init(_target);
    }

    private Vector3 RandomPointInArea(Transform area)
    {
        float x = Random.Range(-area.localScale.x / 2, area.localScale.x / 2);
        float y = Random.Range(-area.localScale.y / 2, area.localScale.y / 2);
        float z = Random.Range(-area.localScale.z / 2, area.localScale.z / 2);

        return area.position + new Vector3(x, y, z);
    }    
}
