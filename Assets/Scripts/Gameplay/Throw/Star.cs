using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ThrowedItem
{
    [SerializeField] private float _maxRotateSpeed;

    private float _rotateSpeed;

    private void Start()
    {
        _rotateSpeed = Random.Range(_maxRotateSpeed / 2, _maxRotateSpeed);
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);

        if (Target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }
}
