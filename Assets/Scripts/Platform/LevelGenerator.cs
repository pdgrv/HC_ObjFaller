using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Platform> _templates;
    [SerializeField] private int _platformCount;
    [SerializeField] private int _countIncreasing;
    [SerializeField] private float _platformHeight;
    [SerializeField] private float _angleStep;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _speedIncreasing;
    [SerializeField] private int _shiftCount;
    [SerializeField] private int _shiftCountIncreasingDivider = 10;
    [SerializeField] private List<Material> _materialPool;

    private Platform _currentTemplate;
    private List<Platform> _spawnedPlatforms = new List<Platform>();

    private float _shiftAngle = 90f;
    private int _destroyedPlatforms
    {
        get
        {
            return _platformCount - _spawnedPlatforms.Count;
        }
    }

    public float PlatformsHeight
    {
        get
        {
            return _platformCount * _platformHeight;
        }
    }

    public event UnityAction<int, int> PlatformCountChanged;

    private void OnEnable()
    {
        PlatformEventsHandler.PlatformDestoyed += OnPlatformDestroyed;
    }
    private void OnDisable()
    {
        PlatformEventsHandler.PlatformDestoyed -= OnPlatformDestroyed;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    public Transform TryGetTopPlatformTransform()
    {
        if (_spawnedPlatforms.Count <= 0)
            return null;
        else
            return _spawnedPlatforms[0].transform;
    }

    public void StartLevel(int levelNumber)
    {
        RecalculateParametrs(levelNumber);
        MoveToHighestPoint();
        GenerateLevel();
        RandomizeLevel();
        _spawnedPlatforms[0].ActivatePlatform();
    }

    private void RecalculateParametrs(int levelNumber)
    {
        _platformCount += levelNumber * _countIncreasing;
        _rotateSpeed += levelNumber * _speedIncreasing;
        _shiftCount += (levelNumber / _shiftCountIncreasingDivider);
    }

    private void MoveToHighestPoint()
    {
        transform.position += new Vector3(0, PlatformsHeight, 0);
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

        int prevShiftStep = -1, prevMat = -1;

        for (int i = 0; i < shiftingNumbers.Count - 1; i++)
        {
            int randomShiftStep, randomMat;
            do
            {
                randomShiftStep = Random.Range(1, _currentTemplate.PartsCount);
                randomMat = Random.Range(0, _materialPool.Count);
            } while (randomShiftStep == prevShiftStep || randomMat == prevMat);

            for (int j = shiftingNumbers[i]; j < shiftingNumbers[i + 1]; j++)
            {
                _spawnedPlatforms[j].transform.Rotate(0, randomShiftStep * _shiftAngle, 0);
                _spawnedPlatforms[j].SetMaterial(_materialPool[randomMat]);
            }

            prevShiftStep = randomShiftStep;
            prevMat = randomMat;
        }
    }

    private void AddPlatform(int platformNumber)
    {
        var newPlatform = Instantiate(_currentTemplate, transform.position + 
            Vector3.down * _platformHeight * platformNumber, Quaternion.Euler(0, _angleStep * platformNumber, 0), transform);
        _spawnedPlatforms.Add(newPlatform);

        PlatformCountChanged?.Invoke(_destroyedPlatforms, _platformCount);
    }

    private void OnPlatformDestroyed(Platform platform)
    {
        _spawnedPlatforms.Remove(platform);
        if (_spawnedPlatforms.Count >= 1)
            _spawnedPlatforms[0].ActivatePlatform();

        Destroy(platform.gameObject);

        PlatformCountChanged?.Invoke(_destroyedPlatforms, _platformCount);
    }
}
