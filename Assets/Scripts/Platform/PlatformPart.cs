using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Animation))]
public class PlatformPart : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private int _durable = 1;
    [SerializeField] private Material _crackedMateial;

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
        if (other.TryGetComponent(out ThrowedItem throwedObject))
        {
            if (_platform.IsActivated)
            {
                throwedObject.Hit();

                if (_isEnemy)
                {
                    BadCollision();
                    return;
                }

                _platform.PlayAudio(true);
                _platform.Destroy();
            }
        }
    }

    private void BadCollision()
    {
        if (--_durable <= 0)
            _platform.GameOver();

        _badAnimation.Play();
        _platform.PlayAudio(false);

        SetMaterial(_crackedMateial);

    }
}
