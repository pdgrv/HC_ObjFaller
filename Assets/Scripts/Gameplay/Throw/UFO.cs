using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : ThrowedObject
{
    //сделать наклон по направлению движения
    [SerializeField] private float _maxRotateSpeed;    

    private float _rotateSpeed;

    private void Start()
    {
        _rotateSpeed = Random.Range(_maxRotateSpeed / 2, _maxRotateSpeed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);

        if (Target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }
}
