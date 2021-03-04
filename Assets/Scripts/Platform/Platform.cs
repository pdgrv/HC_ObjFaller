using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<PlatformPart> _platformParts;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _isActivated;

    public int PartsCount => _platformParts.Count;
    public bool IsActivated => _isActivated;

    public void OnEnable()
    {
        _particleSystem.transform.parent = null;
    }

    public void SetMaterial(Material mat)
    {
        foreach (PlatformPart part in _platformParts)
        {
            if (!(part is BadPlatformPart))
            {
                part.SetMaterial(mat);

                SetParticleColor(mat.color);
            }
        }
    }

    public void Destroy()
    {
        PlatformEventsHandler.RaisePlatformDestroyed(this);
        _particleSystem.Play();
    }

    public void ActivatePlatform()
    {
        _isActivated = true;
    }

    private void SetParticleColor(Color color)
    {
        var particleMain = _particleSystem.main;
        particleMain.startColor = color;
    }
}
