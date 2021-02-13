using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollower : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private float _speed = 0.1f;

    private Vector3 _offset;

    private void Start()
    {
        transform.position += new Vector3(0, _levelGenerator.PlatformsHeight, 0);
        _offset = transform.position - _levelGenerator.TryGetTopPlatformTransform().position;
    }

    private void LateUpdate()
    {
        if (_levelGenerator.TryGetTopPlatformTransform() == null)
            return;

        Vector3 desiredPosition = _levelGenerator.TryGetTopPlatformTransform().position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _speed);
    }
}
