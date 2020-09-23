using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Platform> _templates;
    [SerializeField] private int _platformCount;
    [SerializeField] private float _platformHeight;
    [SerializeField] private float _angleStep;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _shiftCount;
    [SerializeField] private List<Material> _materialPool;

    private Platform _currentPlatform;
    private List<Platform> _spawnedPlatforms = new List<Platform>();
    private float _shiftAngle = 90;

    private void Awake()
    {
        Clean();
        GenerateLevel();
        RandomizeRotation();
    }

    private void Start()
    {
        _shiftAngle = 360 / _currentPlatform.PartsCount;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    public Transform GetTopPlatformPosition()
    {
        return transform.GetChild(0);
    }

    [ContextMenu("GenerateLevel")]
    private void GenerateLevel()
    {
        int random = Random.Range(0, _templates.Count);
        _currentPlatform = _templates[random];

        for (int i = 0; i < _platformCount; i++)
        {
            var newPlatform = Instantiate(_currentPlatform, Vector3.down * _platformHeight * i, Quaternion.Euler(0, _angleStep * i, 0), transform);
            _spawnedPlatforms.Add(newPlatform);
        }
    }

    [ContextMenu("RandomizeRotation")]
    private void RandomizeRotation()
    {
        int shiftingNumber = 0;

        for (int i = 1; i <= _shiftCount; i++)
        {
            shiftingNumber = Random.Range(shiftingNumber + 5, _platformCount / _shiftCount * i - 5);

            int random = Random.Range(1, _currentPlatform.PartsCount);
            int randomMat = Random.Range(0, _materialPool.Count);

            for (int j = shiftingNumber; j < _platformCount; j++)
            {
                //transform.GetChild(j).Rotate(0, random * _shiftAngle, 0);
                _spawnedPlatforms[j].transform.Rotate(0, random * _shiftAngle, 0);
                _spawnedPlatforms[j].SetMaterial(_materialPool[randomMat]);
            }
        }
    }

    [ContextMenu("Clean")]
    private void Clean()
    {
        //int childCount = transform.childCount;
        //for (int i = childCount - 1; i >= 0; i--)
        //{
        //    DestroyImmediate(transform.GetChild(i).gameObject);
        //}
        foreach (Platform platform in _spawnedPlatforms)
            DestroyImmediate(platform.gameObject);            

        _spawnedPlatforms.Clear();
    }
}
