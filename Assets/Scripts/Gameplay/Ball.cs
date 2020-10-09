using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    private Transform _target;

    private void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0, 180));    
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);

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
