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
    [SerializeField] private float _shiftAngle;

    private void Start()
    {
        Clean();
        GenerateLevel();
        RandomizeRotation();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    [ContextMenu("GenerateLevel")]
    private void GenerateLevel()
    {
        int random = Random.Range(0, _templates.Count);

        for (int i = 0; i < _platformCount; i++)
        {
            Instantiate(_templates[random], Vector3.down * _platformHeight * i, Quaternion.Euler(0, _angleStep * i, 0), transform);
        }
    }

    private void RandomizeRotation()
    {
        int shiftingNumber = 0;

        for (int i = 1; i <= _shiftCount; i++)
        {
            shiftingNumber = Random.Range(shiftingNumber + 10, _platformCount / _shiftCount * i - 5);

            int random = Random.Range(1, 4);
            for (int j = shiftingNumber; j < _platformCount; j++)
                transform.GetChild(j).Rotate(0, random * _shiftAngle, 0);
        }
    }

    [ContextMenu("Clean")]
    private void Clean()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
