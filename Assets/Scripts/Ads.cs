using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Ads : MonoBehaviour, IRewardedVideoAdListener
{
    private GameManager _gameManager;

    private RewardType _currentRewardType;

    private const string APP_KEY= "6dabc23ec53d5fbd102fa046089c2f77ea861930b287a27c";
    //private readonly string[] DISABLED_NETWORKS = { "facebook", "inner-active", "ironsource", "ogury", "my_target" };

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Appodeal.setTesting(true);
        Appodeal.muteVideosIfCallsMuted(true);

        //foreach (string network in DISABLED_NETWORKS)
        //    Appodeal.disableNetwork(network);

        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();

        Appodeal.setRewardedVideoCallbacks(this);

        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, true);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void ShowRewarded(RewardType rewardType)
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);

            _currentRewardType = rewardType;
        }
        //как-то обрабатывать событие если реклама не загружена ( или скипнута ), например отключен интернет? 
        // + уведомлять игрока
    }

    public void onRewardedVideoLoaded(bool precache)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        switch (_currentRewardType)
        {
            case RewardType.REVIVE:
                _gameManager.ResumeGame();
                break;
            case RewardType.BONUS:
                _gameManager.DoubleMoneyWon();
                break;
        }
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
        throw new System.NotImplementedException();
    }
}

public enum RewardType { REVIVE, BONUS };
