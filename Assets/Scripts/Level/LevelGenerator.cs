using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Platform> _templates;
    [SerializeField] private int _platformCount;
    [SerializeField] private float _platformHeight;
    [SerializeField] private float _angleStep;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _shiftCount;
    [SerializeField] private List<Material> _materialPool;
    [SerializeField] private GameManager _gameManager;

    private Platform _currentTemplate;
    private List<Platform> _spawnedPlatforms = new List<Platform>();

    private float _shiftAngle = 90;
    private int _destroyedPlatforms
    {
        get
        {
            return _platformCount - _spawnedPlatforms.Count;
        }
    }

    public event UnityAction<int, int> PlatformCountChanged;

    private void Awake()
    {
        StartLevel(1);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    public Transform GetTopPlatformPosition()
    {
        if (transform.childCount <= 0)
            return null;
        else
            return transform.GetChild(0);
    }

    public void StartLevel(int levelNumber)
    {
        Clean();
        GenerateLevel();
        RandomizeLevel();
    }

    [ContextMenu("GenerateLevel")]
    private void GenerateLevel()
    {
        _currentTemplate = _templates[Random.Range(0, _templates.Count)];
        _shiftAngle = 360 / _currentTemplate.PartsCount;

        for (int i = 0; i < _platformCount; i++)
        {
            AddPlatform(i);
        }
    }

    [ContextMenu("RandomizeLevel")]
    private void RandomizeLevel()
    {
        List<int> shiftingNumbers = new List<int>();
        int shiftingNumber = 0;

        shiftingNumbers.Add(shiftingNumber);

        for (int i = 1; i <= _shiftCount; i++)
        {
            shiftingNumber = Random.Range(shiftingNumber + 5, _platformCount / _shiftCount * i - 5);
            shiftingNumbers.Add(shiftingNumber);
        }

        shiftingNumbers.Add(_spawnedPlatforms.Count);

        for (int i = 0; i < shiftingNumbers.Count - 1; i++)
        {
            int randomShiftStep = Random.Range(1, _currentTemplate.PartsCount);
            int randomMat = Random.Range(0, _materialPool.Count);

            for (int j = shiftingNumbers[i]; j < shiftingNumbers[i + 1]; j++)
            {
                _spawnedPlatforms[j].transform.Rotate(0, randomShiftStep * _shiftAngle, 0);
                _spawnedPlatforms[j].SetMaterial(_materialPool[randomMat]);
            }
        }
    }

    private void AddPlatform(int platformNumber)
    {
        var newPlatform = Instantiate(_currentTemplate, Vector3.down * _platformHeight * platformNumber, Quaternion.Euler(0, _angleStep * platformNumber, 0), transform);
        newPlatform.Init(this, _gameManager);
        _spawnedPlatforms.Add(newPlatform);

        PlatformCountChanged?.Invoke(_destroyedPlatforms, _platformCount);
    }

    public void RemovePlatform(Platform platform)
    {
        _spawnedPlatforms.Remove(platform);
        Destroy(platform.gameObject);

        PlatformCountChanged?.Invoke(_destroyedPlatforms, _platformCount);
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
