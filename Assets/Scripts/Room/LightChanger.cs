using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightChanger : MonoBehaviour
{
    [SerializeField] private float _targetIntensity;

    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();    
    }

    public void Dim()
    {
       // StartCoroutine(ChangeIntensity());
    }

    private IEnumerator ChangeIntensity()
    {
        var splitSecond = new WaitForSeconds(0.1f);
        float step = 0.05f;

        while (_light.intensity > _targetIntensity)
        {
            _light.intensity -= step;
            yield return splitSecond;
        }
    }
}
