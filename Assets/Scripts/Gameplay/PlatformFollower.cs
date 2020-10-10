﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollower : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private float _speed = 5;

    private float _offset;

    private void Start()
    {
        _offset = transform.position.y - _levelGenerator.TryGetTopPlatformPosition().position.y;
    }

    private void Update()
    {
        if (_levelGenerator.TryGetTopPlatformPosition() != null && transform.position.y - _levelGenerator.TryGetTopPlatformPosition().position.y > _offset)
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }
    }
}
