using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(Animation))]
public class PlatformPart : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private int _durable = 1;

    private Animation _badAnimation;

    public bool IsEnemy => _isEnemy;

    private void Start()
    {
        _badAnimation = GetComponent<Animation>();

        _platform = GetComponentInParent<Platform>();
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThrowedObject throwedObject))
        {
            throwedObject.Die();

            if (_isEnemy)
            {
                BadCollision();

                if (--_durable <= 0)
                {
                    _platform.GameOver();
                }
                return;
            }

            _platform.PlayAudio(true);
            _platform.Destroy();
        }
    }

    private void BadCollision()
    {
        _badAnimation.Play();
        _platform.PlayAudio(false);
    }
}
