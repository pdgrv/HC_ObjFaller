using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Ads : MonoBehaviour, IRewardedVideoAdListener, IInterstitialAdListener
{
    private const string APP_KEY = "aeb1d463c13e2661e73d2c88da7e3024671024512888152d";

    [SerializeField] private bool _isTesting = false;
    [SerializeField] private Game _game;

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
                    _game.ResumeGame();
                    break;
                case RewardType.BONUS:
                    _game.DoubleMoneyWon();
                    break;
                default:
                    break;
            }

            _isRewardedFinished = false;
        }
    }

    private void Initialize()
    {
        Appodeal.setTesting(_isTesting);
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
