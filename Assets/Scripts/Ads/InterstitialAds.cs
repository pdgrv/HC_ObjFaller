using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAds : MonoBehaviour
{
    [SerializeField] private Ads _ads;
    [SerializeField] private int _adsFrequency;

    private int _tickCount = 0;

    private void Start()
    {
        _tickCount = PlayerPrefs.GetInt("TickCount", 0);
    }

    public void ShowTick()
    {
        _tickCount++;

        if (_tickCount >= _adsFrequency)
        {
            _tickCount = 0;

            _ads.ShowInterstitial();
        }

        PlayerPrefs.SetInt("TickCount", _tickCount);
    }
}
