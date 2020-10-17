using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<PlatformPart> _platformParts;
    [SerializeField] private ParticleSystem _particleSystem;

    private LevelGenerator _levelGenerator;
    private GameManager _gameManager;

    public int PartsCount => _platformParts.Count;

    public void Init(LevelGenerator levelGenerator, GameManager gameManager)
    {
        _levelGenerator = levelGenerator;
        _gameManager = gameManager;
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
        Destroy(gameObject);

        _particleSystem.Play();
    }

    public void GameOver()
    {
        _gameManager.GameOver();
    }
}
