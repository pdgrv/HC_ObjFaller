﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Thrower : MonoBehaviour
{
    [SerializeField] private ThrowedObject _currentTemplate;
    [SerializeField] private ThrowerPool _pool;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private Transform _target;
    [SerializeField] private HitParticle _hitParticle;

    private ThrowerDelay _throwerDelay;
    private GameObject _targetVisual;

    private bool _canThrow = true;
    private float _elapsedTime;

    private void Start()
    {
        _throwerDelay = GetComponent<ThrowerDelay>();
        _targetVisual = _target.GetComponentInChildren<Light>().gameObject;

        _elapsedTime = _throwerDelay.Delay;

        _pool.InitializePool(_currentTemplate, _target, _hitParticle);
    }

    private void Update()
    {
        if (!_canThrow)
            return;

        _elapsedTime += Time.deltaTime;

#if (UNITY_ANDROID && !UNITY_EDITOR)
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
#elif (UNITY_EDITOR && UNITY_ANDROID)
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
#endif
        {
            if (_elapsedTime >= _throwerDelay.Delay)
            {
                Throw();
                _elapsedTime = 0;
            }
        }
    }

    public void SetThrowedObject(ThrowedObject template)
    {
        _currentTemplate = template;

        _pool.InitializePool(template, _target, _hitParticle);
    }

    public void AllowThrow()
    {
        _canThrow = true;
        _targetVisual.SetActive(true);
    }

    public void ProhibitThrow()
    {
        _canThrow = false;
        _targetVisual.SetActive(false);

        _pool.DisableAllObjects();
    }

    private void Throw()
    {
        Vector3 spawnPoint = RandomPointInArea(_spawnArea);
        ThrowedObject throwedObject = _pool.GetAvailableObject();

        throwedObject.transform.position = spawnPoint;

        throwedObject.gameObject.SetActive(true);
    }

    private Vector3 RandomPointInArea(Transform area)
    {
        float x = Random.Range(-area.localScale.x / 2, area.localScale.x / 2);
        float y = Random.Range(-area.localScale.y / 2, area.localScale.y / 2);
        float z = Random.Range(-area.localScale.z / 2, area.localScale.z / 2);

        return area.position + new Vector3(x, y, z);
    }
}
