using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<PlatformPart> _platformParts;
    [SerializeField] private ParticleSystem _particleSystem;

    private LevelGenerator _levelGenerator;
    private GameManager _gameManager;
    private PlatformAudio _platformAudio;
    private bool _isActivated;

    public int PartsCount => _platformParts.Count;
    public bool IsActivated => _isActivated;

    public void Init(LevelGenerator levelGenerator, GameManager gameManager, PlatformAudio platformAudio)
    {
        _levelGenerator = levelGenerator;
        _gameManager = gameManager;
        _platformAudio = platformAudio;
    }

    public void SetMaterial(Material mat)
    {
        foreach (PlatformPart part in _platformParts)
        {
            if (!part.IsEnemy)
            {
                part.SetMaterial(mat);

                var particleMain = _particleSystem.main;
                particleMain.startColor = mat.color;
                _particleSystem.transform.parent = null;
            }
        }
    }

    public void Destroy()
    {
        _levelGenerator.RemovePlatform(this);
        _particleSystem.Play();
    }

    public void GameOver()
    {
        _gameManager.GameOver();
    }

    public void PlayAudio(bool isGood)
    {
        _platformAudio.PlayAudio(isGood);
    }

    public void ActivatePlatform()
    {
        _isActivated = true;
    }
}
