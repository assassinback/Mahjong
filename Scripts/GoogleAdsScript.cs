using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAdsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public BannerView bannerView;
    public InterstitialAd interstitial;
    public RewardedAd rewardedAd;
    public static GoogleAdsScript _instance;
    //string adUnitId = "";
    private void Awake()
    {
        _instance = this;
        
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        RequestInterstitial();
        RequestRewarded();
    }
    public void RequestRewarded()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-4123289309001974/1143969438";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-4123289309001974/4468970922";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        RotateWheel.adWatched = true;
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.LoadAdError.GetCause());
        RotateWheel.adWatched = false;
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        RotateWheel.adWatched = true;
        GameObject.Find("768x1024(Clone)").GetComponent<Canvas>().sortingOrder = 101;
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetCause());
        RotateWheel.adWatched = false;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RotateWheel.adWatched = false;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        RotateWheel.adWatched = true;
        RequestRewarded();
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4123289309001974/2882263375";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4123289309001974/3430139857";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoadedNormalAd;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadNormalAd;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpeningNormalAd;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosedNormalAd;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdLoadedNormalAd(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
    public void HandleOnAdFailedToLoadNormalAd(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetCause());
    }
    public void HandleOnAdOpeningNormalAd(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
        GameObject.Find("768x1024(Clone)").GetComponent<Canvas>().sortingOrder = 101;
    }
    public void HandleOnAdClosedNormalAd(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4123289309001974/9256100035";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4123289309001974/8352375612";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner,AdPosition.Top);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoadedBanner;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBanner;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpenedBanner;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosedBanner;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        

    }
    public void HandleOnAdLoadedBanner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        GameObject.Find("BANNER(Clone)").GetComponent<Canvas>().sortingOrder= 100;
        GameObject.Find("BANNER(Clone)").GetComponent<Canvas>().transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(GameObject.Find("BANNER(Clone)").GetComponent<Canvas>().transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x,0);
    }
    public void HandleOnAdFailedToLoadBanner(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }
    public void HandleOnAdOpenedBanner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }
    public void HandleOnAdClosedBanner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }
}
