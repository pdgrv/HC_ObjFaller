using System.Collections;
using UnityEngine;

public abstract class ThrowedObject : SellableItem
{
    [SerializeField] protected float Speed = 7f;
    [SerializeField] private float _startScale = 0.3f;
    [SerializeField] private float _increaseScaleSpeed = 1.2f;

    protected Transform Target;

    private Coroutine _increaseSizeJob;
    private HitParticle _hitParticle;

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

    public void Init(Transform target, HitParticle hitParticle)
    {
        Target = target;

        _hitParticle = hitParticle;
    }

    public void Hit()
    {
        _hitParticle.Play(transform.position);

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
