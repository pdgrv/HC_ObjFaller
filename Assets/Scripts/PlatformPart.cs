using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPart : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private int _durable = 1;

    public bool IsEnemy => _isEnemy;

    private void Start()
    {
        _platform = GetComponentInParent<Platform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            ball.Destroy();

            if (_isEnemy)
            {
                if (--_durable <= 0)
                {
                    Debug.Log("Вы проиграли.");
                    Time.timeScale = 0;
                }
                return;
            }

            _platform.Destroy();
        }
    }
}
