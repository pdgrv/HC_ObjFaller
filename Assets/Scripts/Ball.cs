using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _target;

    private void Update()
    {
        //transform.position += Vector3.down * _speed * Time.deltaTime;
        if (_target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}
