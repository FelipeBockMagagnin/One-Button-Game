﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAds : MonoBehaviour
{
private InterstitialAd interstitial;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InterstitialAds");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        RequestInterstitial();
        PlayerPrefs.SetInt("InterstitialAds", 1);
    }

    /// <summary>
    /// Requisita um ad para se mostrado
    /// </summary>
    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8861904667614686/8228309151";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    /// <summary>
    /// Mostra o ad carregado ao usuario
    /// </summary>
    public void ShowInterstitalAd()
    {
        if (AdsManager.ADSenabled)
        {
            if (PlayerPrefs.HasKey("InterstitialAds"))
            {
                if (PlayerPrefs.GetInt("InterstitialAds") == 6)
                {
                    if (interstitial.IsLoaded())
                    {
                        interstitial.Show();
                        PlayerPrefs.SetInt("InterstitialAds", 1);
                    }
                    else
                    {
                        RequestInterstitial();
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("InterstitialAds", PlayerPrefs.GetInt("InterstitialAds") + 1);
                }
            }
            else
            {
                PlayerPrefs.SetInt("InterstitialAds", 1);
            }
        }
    }


        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            RequestInterstitial();
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
}

