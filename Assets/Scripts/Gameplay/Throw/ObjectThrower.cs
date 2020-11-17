using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private ThrowedObject _object;
    [SerializeField] private GameObject _container;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _changeDelaySpeed;

    private float _delay;

    private List<ThrowedObject> _objectPool = new List<ThrowedObject>();

    private bool _canThrow = true;
    private float _elapsedTime;

    private void Start()
    {
        _delay = _maxDelay;
        _elapsedTime = _delay;

        InitializePool();
    }

    private void Update()
    {
        if (!_canThrow)
            return;

        _elapsedTime += Time.deltaTime;

        //if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_elapsedTime >= _delay)
            {
                Throw();
                _elapsedTime = 0;
            }

            if (_delay > _minDelay)
            {
                _delay -= _changeDelaySpeed * Time.deltaTime;
            }
        }
        else
        {
            if (_delay < _maxDelay)
                _delay += _changeDelaySpeed * Time.deltaTime * 2;
        }
    }

    public void SetThrowedObject(ThrowedObject template)
    {
        _object = template;

        CleanPool();
        InitializePool();
    }

    public void GoThrow()
    {
        _canThrow = true;
    }

    public void StopThrow()
    {
        _canThrow = false;

        foreach (ThrowedObject throwedObject in _objectPool)
            throwedObject.gameObject.SetActive(false);
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            ThrowedObject newObject = Instantiate(_object, _container.transform);
            newObject.gameObject.SetActive(false);

            newObject.Init(_target);
            _objectPool.Add(newObject);
        }
    }

    private void CleanPool()
    {
        foreach (var item in _objectPool)
        {
            Destroy(item.gameObject);
        }
        _objectPool.Clear();
    }

    private void Throw()
    {
        Vector3 spawnPoint = RandomPointInArea(_spawnArea);
        ThrowedObject throwedObject = _objectPool.First(p => p.gameObject.activeSelf == false);
        if (throwedObject == null)
            Debug.Log("no free object in objectpool");

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
