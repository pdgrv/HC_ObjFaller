using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _platforms;
    [SerializeField] private int _platformCount;
    [SerializeField] private float _angleStep;
    [SerializeField] private float _platrormHeight;

    [ContextMenu("GenerateLevel")]
    private void GenerateLevel()
    {
        for (int i = 0; i < _platformCount; i++)
        {
            Instantiate(_platforms[0], Vector3.down * _platrormHeight * i, Quaternion.Euler(0, _angleStep * i, 0), transform);
        }
    }
    
    [ContextMenu("Clean")]
    private void Clean()
    {
        int childCount = transform.childCount;

        for (int i = childCount-1; i >=0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
