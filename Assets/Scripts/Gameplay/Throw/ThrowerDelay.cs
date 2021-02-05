﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ThrowerDelay : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _changeDelaySpeed;

    public event UnityAction<float, float> ChangeThrowerDelay;

    public float Delay { get; private set; }

    private void Start()
    {
        Delay = _maxDelay;
    }

    private void FixedUpdate()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
#elif (UNITY_EDITOR && UNITY_ANDROID)
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
#endif
        {
            if (Delay >= _minDelay + _changeDelaySpeed)
                ChangeDelay(-_changeDelaySpeed);
        }
        else
        {
            if (Delay < _maxDelay)
                ChangeDelay(_changeDelaySpeed);
        }
    }

    private void ChangeDelay(float amount)
    {
        Delay += amount;
        ChangeThrowerDelay?.Invoke(_maxDelay - Delay, _maxDelay - _minDelay);
    }
}