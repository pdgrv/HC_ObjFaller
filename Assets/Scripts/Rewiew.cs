using Google.Play.Review;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewiew : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _targetLevel = 5;

    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;

    private void OnEnable()
    {
        _gameManager.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _gameManager.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        if (level == _targetLevel)
        {
            _reviewManager = new ReviewManager();

            StartCoroutine(RequestRewiew());
        }
#endif
    }

    private IEnumerator RequestRewiew()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

    }
}