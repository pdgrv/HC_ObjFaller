using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Ads : MonoBehaviour
{
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

        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, true);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void ShowRewarded()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            Appodeal.show(Appodeal.REWARDED_VIDEO);
    }
}
