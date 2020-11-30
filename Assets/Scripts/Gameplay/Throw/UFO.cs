using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : ThrowedItem
{
    //сделать наклон по направлению движения
    [SerializeField] private float _maxRotateSpeed;
    [SerializeField] private float _tiltStep = 15f;

    private float _rotateSpeed;

    private void Start()
    {
        _rotateSpeed = Random.Range(_maxRotateSpeed / 2, _maxRotateSpeed);
    }

    protected void OnEnable()
    {
        base.OnEnable();

        LookAtTarget();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);

        if (Target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }

    private void LookAtTarget()
    {
        float tiltRate = transform.position.x;

        transform.eulerAngles = Vector3.zero;

        transform.Rotate(0, 0, tiltRate * _tiltStep, Space.World);
    }
}
