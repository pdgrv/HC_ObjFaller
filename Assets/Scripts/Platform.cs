using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<PlatformPart> _platformParts;

    public int PartsCount => _platformParts.Count;

    public void SetMaterial(Material mat)
    {
        foreach (PlatformPart part in _platformParts)
        {
            if (!part.IsEnemy)
                part.SetMaterial(mat);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
