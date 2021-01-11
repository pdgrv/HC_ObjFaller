using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private ThrowedItem _currentTemplate;
    [SerializeField] private GameObject _container;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private Transform _target;

    private ThrowerDelay _throwerDelay;
    private GameObject _targetVisual;

    private List<ThrowedItem> _objectPool = new List<ThrowedItem>();

    private bool _canThrow = true;
    private float _elapsedTime;

    private void Start()
    {
        _throwerDelay = GetComponent<ThrowerDelay>();
        _targetVisual = _target.GetComponentInChildren<Light>().gameObject;

        _elapsedTime = _throwerDelay.Delay;

        InitializePool();
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

    public void SetThrowedObject(ThrowedItem template)
    {
        _currentTemplate = template;

        CleanPool();
        InitializePool();
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

        foreach (ThrowedItem throwedObject in _objectPool)
            throwedObject.gameObject.SetActive(false);
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            ThrowedItem newObject = Instantiate(_currentTemplate, _container.transform);
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
        ThrowedItem throwedObject = _objectPool.First(p => p.gameObject.activeSelf == false);
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
