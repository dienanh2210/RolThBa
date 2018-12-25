using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class GoogleAds
{

    string _bannerId;
    string _interstitialId;
    string _videorewardId;
    string _nativeAdsId;

    BannerView banner;
    InterstitialAd interstitial;
    RewardBasedVideoAd rewardBasedVideo;
    NativeExpressAdView nativeExpressAdView;

    public event Action OnAdFailedToLoad;

    public event Action OnInterstitialAdFailedToLoad;

    public event Action OnInterstitialClosed;

    public event Action OnInterstitialOpening;

    public static bool isbannerLoaded = false;
    public static bool isbannerOnShow = false;
    public static bool isShowOnLoaded = false;


    public static bool isInterstitialLoaded;
    public bool isVideoLoaded;
    public bool isNativeAdLoaded;

    public static bool isReloadBanner = false;
    public static bool isReloadIntertitial = false;

    public event Action VideoFailedToLoad;

    public event Action<string, float> VideoRewarded;

    public event Action VideoStarted;

    public event Action VideoOpened;

    public static bool isInited;

    public GoogleAds()
    {
        if (!isInited)
        {
            isbannerLoaded = false;
            isbannerOnShow = false;
            isInterstitialLoaded = false;
            isShowOnLoaded = false;
        }

    }

    //----------set adUnitId ------------------------------
    public string BannerId
    {
        set
        {
            _bannerId = value;

#if UNITY_EDITOR
            _bannerId = "unused";
#endif
        }
    }

    public string InterstitialId
    {
        set
        {
            _interstitialId = value;
#if UNITY_EDITOR
            _interstitialId = "unused";
#endif
        }
    }

    public string VideoRewardId
    {
        set
        {
            _videorewardId = value;
#if UNITY_EDITOR
            _videorewardId = "unused";
#endif
        }
    }
    //-- banner------------------------------------------------
    public void RequestBanner(AdPosition position)
    {
        // Create a 320x50 banner at the top of the screen.
        banner = new BannerView(_bannerId, AdSize.Banner, position);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
            .Build();
        // AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        banner.LoadAd(request);

        banner.OnAdFailedToLoad += Banner_OnAdFailedToLoad;
        banner.OnAdLoaded += Banner_OnAdLoaded;
        banner.OnAdOpening += Banner_OnAdOpening;
    }


    public void ShowBanner()
    {
        if (isbannerLoaded)
        {
            isShowOnLoaded = true;
            banner.Show();
        }
    }

    public void HideBanner()
    {
        isShowOnLoaded = false;
        banner.Hide();
    }

    public void DestroyBanner()
    {
        isbannerLoaded = false;
        isShowOnLoaded = false;
        banner.Destroy();
    }

    private void Banner_OnAdLoaded(object sender, System.EventArgs e)
    {
        isbannerLoaded = true;

        //isReloadBanner = false;

        if (!isShowOnLoaded)
        {
            banner.Hide();
        }

        SG_AdManager.ads.ShowBanner();
    }

    private void Banner_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        //OnAdFailedToLoad();
        Debug.Log("Banner_OnAdFailedToLoad");

        DestroyBanner();

        if (!isReloadBanner)
        {
            isReloadBanner = true;

            SG_AdManager.RequestFanBanner();
        }
    }

    private void Banner_OnAdOpening(object sender, System.EventArgs e)
    {
        isbannerOnShow = true;
    }

    //-- Interstitial------------------------------------------------
    public void RequestInterstitial()
    {
        if (isInterstitialLoaded)
        {
            return;
        }
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(_interstitialId);
        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
            .Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;

        interstitial.OnAdOpening += Interstitial_OnAdOpening;

        interstitial.OnAdLoaded += Interstitial_OnAdLoaded;

    }

    public bool IsInterstitialLoaded()
    {
        return isInterstitialLoaded;
    }

    public void ShowInterstitial()
    {
        if (isInterstitialLoaded)
        {
            interstitial.Show();

            isInterstitialLoaded = false;
        }
    }

    public void DestroyInterstitial()
    {
        interstitial.Destroy();
    }

    private void Interstitial_OnAdLoaded(object sender, EventArgs e)
    {
        isInterstitialLoaded = true;

        //isReloadIntertitial = false;
    }


    private void Interstitial_OnAdOpening(object sender, EventArgs e)
    {
        Time.timeScale = 0f;

        //MainAudio.Main.MuteSound(true);

        OnInterstitialOpening();
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        //OnInterstitialAdFailedToLoad();
        isInterstitialLoaded = false;
        
        if (!isReloadIntertitial)
        {
            isReloadIntertitial = true;

            SG_AdManager.RequestFanIntertitial();
        }
    }

    private void Interstitial_OnAdClosed(object sender, EventArgs e)
    {
        Time.timeScale = 1f;

        //MainAudio.Main.MuteSound((Settings.GetSound == 1) ? false : true);

        isInterstitialLoaded = false;
        FanAdsManager.isIntertitialLoaded = false;
        //OnInterstitialClosed();

        SG_AdManager.ads.RequestIntertitial();

        //if (MainState.state == MainState.State.Ingame)
        //{
        //    //RequestInterstitial();
        //    SG_AdManager.ads.RequestIntertitial();
        //}
    }

    //------------video rewards-------------------
    bool handleRewardedVideoSet = false;

    public void RequestRewardBasedVideo()
    {
        if (isVideoLoaded)
        {
            return;
        }

        rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder()
             .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
             .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
             .Build();

        rewardBasedVideo.LoadAd(request, _videorewardId);

        if (!handleRewardedVideoSet)
        {
            // Ad event fired when the rewarded video ad
            // has been received.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // has rewarded the user.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // is leaving the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            handleRewardedVideoSet = true;
        }
    }

    public void ShowVideo()
    {
        if (isVideoLoaded)
        {
            rewardBasedVideo.Show();
        }
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {

    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        isVideoLoaded = false;

        //GameMessageManager.Instance.popupRewardedVideo.OnCloseVideo();

        RequestRewardBasedVideo();
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        //GameMessageManager.Instance.popupRewardedVideo.OnHandleWatchVideoComplete();

        VideoRewarded(e.Type, (float)e.Amount);
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
        VideoStarted();
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        VideoOpened();
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        isVideoLoaded = false;
        VideoFailedToLoad();
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        isVideoLoaded = true;
    }

    #region Native Ads

    public string NativeAdsId
    {
        set
        {
            _nativeAdsId = value;
#if UNITY_EDITOR
            _nativeAdsId = "unused";
#endif
        }
    }

    bool handleNativeAdsSet = false;

    public void RequestNativeAds(AdPosition nativeAdPosition)
    {
        if (isNativeAdLoaded)
        {
            return;
        }

        // Create a 320x50 native express ad at the top of the screen.
        nativeExpressAdView = new NativeExpressAdView(_nativeAdsId, new AdSize(320, 150), nativeAdPosition);
        // Load a banner ad.
        nativeExpressAdView.LoadAd(new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
            .Build());

        //Debug.Log("Load native ads");

        if (!handleNativeAdsSet)
        {
            // Called when an ad request has successfully loaded.
            nativeExpressAdView.OnAdLoaded += HandleOnNativeAdLoaded;
            // Called when an ad request failed to load.
            nativeExpressAdView.OnAdFailedToLoad += HandleOnNativeAdFailedToLoad;

            // Called when an ad is clicked.
            //nativeExpressAdView.OnAdOpened += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            //nativeExpressAdView.OnAdClosed += HandleOnNativeAdClosed;
            // Called when the ad click caused the user to leave the application.
            //nativeExpressAdView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            handleNativeAdsSet = true;
        }
    }

    public void HandleOnNativeAdLoaded(object sender, EventArgs args)
    {
        isNativeAdLoaded = true;

        Debug.Log("Ad loaded");
        // Handle the ad failed to load event.
    }

    public void HandleOnNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isNativeAdLoaded = false;

        Debug.Log("Ad failed to load: " + args.Message);
        // Handle the ad failed to load event.
    }

    public void ShowNativeAds()
    {
        if (isNativeAdLoaded)
        {
            nativeExpressAdView.Show();
        }
    }

    public void CleanNativeAds()
    {
        nativeExpressAdView.Destroy();

        isNativeAdLoaded = false;
    }

    #endregion
}
