using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Ads : MonoBehaviour, IRewardedVideoAdListener, IInterstitialAdListener
{
    private const string APP_KEY = "6dabc23ec53d5fbd102fa046089c2f77ea861930b287a27c";

    [SerializeField] private GameManager _gameManager;

    private RewardType _currentRewardType;

    private bool _isRewardedFinished = false;    

    private void Start()
    {
        Initialize();

        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.setInterstitialCallbacks(this);
    }

    private void Update()
    {
        if (_isRewardedFinished)
        {
            switch (_currentRewardType)
            {
                case RewardType.REVIVE:
                    _gameManager.ResumeGame();
                    break;
                case RewardType.BONUS:
                    _gameManager.DoubleMoneyWon();
                    break;
                default:
                    Debug.Log("switch eat shii");
                    break;
            }

            _isRewardedFinished = false;
        }   
    }

    private void Initialize()
    {
        Appodeal.setTesting(true);
        Appodeal.muteVideosIfCallsMuted(true);

        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();

        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, true);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void ShowRewarded(RewardType rewardType)
    {
        _currentRewardType = rewardType;

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        //как-то обрабатывать событие если реклама не загружена ( или скипнута ), например отключен интернет? 
        // + уведомлять игрока
    }

    public void onRewardedVideoLoaded(bool precache)
    {
    }

    public void onRewardedVideoFailedToLoad()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoShowFailed()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoShown()
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>
        {
            {"AdNetwork","Appodeal" },
            {"Type", "Rewarded" }
        };

        AppMetrica.Instance.ReportEvent("ShowAd", eventParameters);
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        _isRewardedFinished = true;
    }

    public void onRewardedVideoClosed(bool finished)
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoExpired()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoClicked()
    {
    }
    // -----------------------------------------------------
    public void onInterstitialLoaded(bool isPrecache)
    {
        throw new System.NotImplementedException();
    }

    public void onInterstitialFailedToLoad()
    {
        throw new System.NotImplementedException();
    }

    public void onInterstitialShowFailed()
    {
        throw new System.NotImplementedException();
    }

    public void onInterstitialShown()
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>
        {
            {"AdNetwork","Appodeal" },
            {"Type", "Interstitial" }
        };

        AppMetrica.Instance.ReportEvent("ShowAd", eventParameters);
    }

    public void onInterstitialClosed()
    {
        throw new System.NotImplementedException();
    }

    public void onInterstitialClicked()
    {
        throw new System.NotImplementedException();
    }

    public void onInterstitialExpired()
    {
        throw new System.NotImplementedException();
    }
}

public enum RewardType { REVIVE, BONUS };
