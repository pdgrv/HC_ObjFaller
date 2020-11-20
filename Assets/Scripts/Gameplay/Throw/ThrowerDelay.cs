using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
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