using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowerPool : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 7;

    private List<ThrowedObject> _objectPool = new List<ThrowedObject>();

    public void InitializePool(ThrowedObject template, Transform target, HitParticle hitParticle)
    {
        CleanPool();

        for (int i = 0; i < _poolCapacity; i++)
        {
            ThrowedObject newObject = Instantiate(template, transform);
            newObject.gameObject.SetActive(false);

            newObject.Init(target, hitParticle);
            _objectPool.Add(newObject);
        }
    }

    public ThrowedObject GetAvailableObject()
    {
        return _objectPool.First(p => p.gameObject.activeSelf == false);
    }

    public void DisableAllObjects()
    {
        foreach (ThrowedObject throwedObject in _objectPool)
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
