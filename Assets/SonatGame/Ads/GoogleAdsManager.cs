using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

public class GoogleAdsManager : MonoBehaviour
{

    // public static GoogleAdsManager Ads;

    public string AndroidBannerId;
    public string AndroidIntersttitialId;
    public string AndroidVideoRewardId;
    public string AndroidNativeAdsId;

    public string IOSBannerId;
    public string IOSIntersttitialId;
    public string IOSVideoRewardId;
    public string IOSNativeAdsId;

    private GoogleAds googleAds;

    public AdPosition BannerPosition;
    public AdPosition NativeAdPosition;

    //void Awake()
    //{
    //    if (Ads != null)
    //    {
    //        DestroyImmediate(gameObject);
    //    }
    //    else
    //    {
    //        Ads = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    private static bool isInitGoogleAdmob = false;

    public void InitAdmob()
    {
        if (!isInitGoogleAdmob)
        {
            isInitGoogleAdmob = true;

            googleAds = new GoogleAds();
#if UNITY_ANDROID
            //googleAds.BannerId = AndroidBannerId;
            googleAds.BannerId = RemoteSettingsHandler.remoteSettingsHandler.admob_banner_id;
            //googleAds.InterstitialId = AndroidIntersttitialId;
            googleAds.InterstitialId = RemoteSettingsHandler.remoteSettingsHandler.admob_interstitial_id;
            //googleAds.VideoRewardId = AndroidVideoRewardId;
            //googleAds.NativeAdsId = AndroidNativeAdsId;
#elif UNITY_IOS
        googleAds.BannerId = IOSBannerId;
        googleAds.InterstitialId = IOSIntersttitialId;
        googleAds.VideoRewardId = IOSVideoRewardId;
        googleAds.NativeAdsId = IOSNativeAdsId;
#endif
            //googleAds.VideoRewarded += VideoRewardCompleted;
            googleAds.OnInterstitialClosed += Interstitial_OnAdClosed;
            googleAds.OnInterstitialOpening += Googleads_OnInterstitialOpening;
            //googleAds.VideoFailedToLoad += Googleads_VideoFailedToLoad;
        }
    }

    //void Start () {

    //    InitAdmob();
    //}
	
    public void RequestBanner()
    {
        googleAds.RequestBanner(BannerPosition);
    }

    public void ShowBanner()
    {
        googleAds.ShowBanner();
    }

    //public bool IsBannerLoaded()
    //{
    //    return GoogleAds.isbannerLoaded;
    //}

    //public bool IsBannerShowOnLoaded()
    //{
    //    return GoogleAds.isShowOnLoaded;
    //}

    public void HideBanner()
    {
        googleAds.HideBanner();
    }

    public void DestroyBanner()
    {
        googleAds.DestroyBanner();
    }

    public void RequestInterstitial()
    {
        googleAds.RequestInterstitial();
    }

    public void ShowInterstitial()
    {
        googleAds.ShowInterstitial();
    }

    //public bool IsInterstitialLoaded()
    //{
    //    return googleAds.IsInterstitialLoaded();
    //}

    public void RequestVideoReward()
    {
        googleAds.RequestRewardBasedVideo();
    }

    public void ShowVideoReward()
    {
        googleAds.ShowVideo();
    }

    public void VideoRewardCompleted(string type, float amount)
    {
  
    }

    public bool IsVideoRewardLoaded()
    {
        return googleAds.isVideoLoaded;
    }

    public void Interstitial_OnAdClosed()
    {
        //RequestInterstitial();
    }

    public void Interstitial_OnAdOpening()
    {

    }

    private void Googleads_OnInterstitialOpening()
    {

    }

    private void Googleads_VideoFailedToLoad()
    {
    }

    #region Native Ads

    public void RequestNativeAds()
    {
        googleAds.RequestNativeAds(NativeAdPosition);
    }

    public bool IsNativeAdsLoaded()
    {
        return googleAds.isNativeAdLoaded;
    }

    public void ShowNativeAds()
    {
        googleAds.ShowNativeAds();
    }

    #endregion
}
