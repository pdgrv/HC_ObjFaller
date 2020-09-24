﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
