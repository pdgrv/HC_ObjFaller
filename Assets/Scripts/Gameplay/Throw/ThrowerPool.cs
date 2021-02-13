using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowerPool : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 7;

    private List<ThrowedItem> _objectPool = new List<ThrowedItem>();

    public void InitializePool(ThrowedItem template, Transform target, HitParticle hitParticle)
    {
        CleanPool();

        for (int i = 0; i < _poolCapacity; i++)
        {
            ThrowedItem newObject = Instantiate(template, transform);
            newObject.gameObject.SetActive(false);

            newObject.Init(target, hitParticle);
            _objectPool.Add(newObject);
        }
    }

    public ThrowedItem GetAvailableObject()
    {
        return _objectPool.First(p => p.gameObject.activeSelf == false);
    }

    public void DisableAllObjects()
    {
        foreach (ThrowedItem throwedObject in _objectPool)
            throwedObject.gameObject.SetActive(false);
    }

    private void CleanPool()
    {
        foreach (var item in _objectPool)
        {
            Destroy(item.gameObject);
        }
        _objectPool.Clear();
    }
}
