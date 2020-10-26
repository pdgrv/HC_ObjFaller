using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowedObject : SellableItem
{
    [SerializeField] protected float Speed;

    protected Transform Target;

    public void Init(Transform target)
    {
        Target = target;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
