using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowedItem : SellableItem
{
    [SerializeField] protected float Speed = 7f;
    [SerializeField] private float _startScale = 0.3f;
    [SerializeField] private float _increaseScaleSpeed = 1.2f;

    protected Transform Target;

    private Coroutine _increaseSizeJob;

    protected void OnEnable()
    {
        if (_increaseSizeJob != null)
        {
            StopCoroutine(_increaseSizeJob);
            _increaseSizeJob = null;
        }

        _increaseSizeJob = StartCoroutine(IncreaseSize());
    }

    private void OnDisable()
    {
        transform.localScale = _startScale * Vector3.one;
    }

    public void Init(Transform target)
    {
        Target = target;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator IncreaseSize()
    {
        while (Vector3.Distance(transform.localScale, Vector3.one) > 0.01f)
        {
            transform.localScale += _increaseScaleSpeed * Vector3.one * Time.deltaTime;
            yield return null;
        }

        _increaseSizeJob = null;
    }
}
