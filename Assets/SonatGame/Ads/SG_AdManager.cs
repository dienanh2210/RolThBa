using UnityEngine;
using System.Collections;
using System;

public class SG_AdManager : MonoBehaviour
{

    //public GoogleAnalyticsV3 GA;

    public static SG_AdManager ads;

    public GoogleAdsManager googleAdsManager;

    public FanAdsManager fanAdsManager;

    public float DelayToShowInterstitial;

    public TypeAd bannerType = TypeAd.NONE;
    public TypeAd intertitialType = TypeAd.NONE;

    public enum TypeAd
    {
        NONE = 0,
        ADMOB = 1,
        FAN = 2
    }

    void Awake()
    {
        if (ads != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            ads = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LeanTween.delayedCall(3f, () =>
            {
                RequestAds();

                LeanTween.delayedCall(2f, () =>
                    {
                        if (RemoteSettingsHandler.remoteSettingsHandler.display_video_ads)
                        {
                            UnityAdsController.unityAdsController.InitializeUnityAds();
                        }
                    });
            });
    }

    public static bool IsInternetConnection()
    {
        bool isConnectedToInternet = false;
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
            Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            isConnectedToInternet = true;
        }
        return isConnectedToInternet;
    }

    public void RequestAds()
    {
        if (IsInternetConnection())
        {
            RequestBanner();

            LeanTween.delayedCall(5f, RequestIntertitial);
        }
    }

    public void RequestBanner()
    {
        if (RemoteSettingsHandler.remoteSettingsHandler.display_banner_ads)
        {
            GoogleAds.isReloadBanner = false;
            FanAdsManager.isReloadBanner = false;

            int randBanner = UnityEngine.Random.Range(0, (int)(RemoteSettingsHandler.remoteSettingsHandler.admob_banner_ratio
                + RemoteSettingsHandler.remoteSettingsHandler.fan_banner_ratio));

            if (randBanner < RemoteSettingsHandler.remoteSettingsHandler.admob_banner_ratio)
            {
                if (bannerType != TypeAd.ADMOB || !GoogleAds.isbannerLoaded)
                {
                    try
                    {
                        HideAllBanner();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    bannerType = TypeAd.ADMOB;

                    ads.googleAdsManager.InitAdmob();

                    RequestAdmobBanner();
                }
            }
            else
            {
                if (bannerType != TypeAd.FAN || !FanAdsManager.isBannerLoaded)
                {
                    try
                    {
                        HideAllBanner();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    bannerType = TypeAd.FAN;

                    RequestFanBanner();
                }
            }
        }
    }

    public void RequestIntertitial()
    {
        if (RemoteSettingsHandler.remoteSettingsHandler.display_interstitial_ads)
        {
            if (IsInternetConnection() && !IsInterstitialLoaded())
            {
                GoogleAds.isReloadIntertitial = false;
                FanAdsManager.isReloadIntertitial = false;

                int randIntertitial = UnityEngine.Random.Range(0, (int)(RemoteSettingsHandler.remoteSettingsHandler.admob_interstitial_ratio
                    + RemoteSettingsHandler.remoteSettingsHandler.fan_interstitial_ratio));

                if (randIntertitial < RemoteSettingsHandler.remoteSettingsHandler.admob_interstitial_ratio)
                {
                    ads.googleAdsManager.InitAdmob();
                    intertitialType = TypeAd.ADMOB;

                    RequestAdmobInterstitial();
                    //LeanTween.delayedCall(5f, RequestAdmobInterstitial);
                }
                else
                {
                    intertitialType = TypeAd.FAN;

                    RequestFanIntertitial();
                    //LeanTween.delayedCall(5f, RequestFanIntertitial);
                }
            }
        }
    }

    public void ShowBanner()
    {
        if (bannerType == TypeAd.FAN)
        {
            if (FanAdsManager.isBannerLoaded)
            {
                ShowFanBanner();
            }
            else
            {
                ShowAdmobBanner();
            }
        }
        else if (bannerType == TypeAd.ADMOB)
        {
            if (GoogleAds.isbannerLoaded)
            {
                ShowAdmobBanner();
            }
            else
            {
                ShowFanBanner();
            }
        }
    }

    public static void HideAllBanner()
    {
        try
        {
            DestroyAdmobBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        try
        {
            ads.fanAdsManager.DisposeBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    float timeStamp = 0;
    public void ShowIntertitial()
    {
        float currentTime = Time.unscaledTime;
        float deltaTime = currentTime - timeStamp;

        //Debug.Log("deltaTime: " + deltaTime);
        if (deltaTime < RemoteSettingsHandler.remoteSettingsHandler.delay_interstital_time)
        {
            return;
        }
        else
        {
            timeStamp = currentTime;
        }

        if (intertitialType == TypeAd.FAN)
        {
            if (FanAdsManager.isIntertitialLoaded)
            {
                ShowFanIntertitial();
            }
            else
            {
                ShowAdmobInterstitial();
            }
        }
        else if (intertitialType == TypeAd.ADMOB)
        {
            if (GoogleAds.isInterstitialLoaded)
            {
                ShowAdmobInterstitial();
            }
            else
            {
                ShowFanIntertitial();
            }
        }
    }

    public static bool IsBannerShowOnLoaded()
    {
        //if (FireBaseController.mobile_network_ads_priority_value == FAN_NETWORK)
        //{
        //    return FanAdsManager.isShowOnLoaded;
        //}
        //else if (FireBaseController.mobile_network_ads_priority_value == ADMOB_NETWORK)
        //{
        //    return ads.googleAdsManager.IsBannerShowOnLoaded();
        //}

        return (GoogleAds.isbannerOnShow || FanAdsManager.isShowOnLoaded);
        //return false;
    }

    public static bool IsInterstitialLoaded()
    {
        //if (FireBaseController.mobile_network_ads_priority_value == FAN_NETWORK)
        //{
        //    return FanAdsManager.isIntertitialLoaded;
        //}
        //else if (FireBaseController.mobile_network_ads_priority_value == ADMOB_NETWORK)
        //{
        //    return ads.googleAdsManager.IsInterstitialLoaded();
        //}

        return (GoogleAds.isInterstitialLoaded || FanAdsManager.isIntertitialLoaded);

        //return false;
    }

    #region admob
    public static void RequestAdmobBanner()
    {
        try
        {
            ads.googleAdsManager.RequestBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void ShowAdmobBanner()
    {
        try
        {
            ads.googleAdsManager.ShowBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void HideAdmobBanner()
    {
        try
        {
            ads.googleAdsManager.HideBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void DestroyAdmobBanner()
    {
        try
        {
            ads.googleAdsManager.DestroyBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void RequestAdmobInterstitial()
    {
        try
        {
            ads.googleAdsManager.RequestInterstitial();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    public static void ShowAdmobInterstitial()
    {
        try
        {
            ads.googleAdsManager.ShowInterstitial();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void RequestRewardedVideo()
    {
        ads.googleAdsManager.RequestVideoReward();
    }

    public static void ShowRewardedVideo()
    {
        ads.googleAdsManager.ShowVideoReward();
    }

    public static bool IsRewardedVideoLoaded()
    {
        return ads.googleAdsManager.IsVideoRewardLoaded();
    }

    #region Native Ads

    public static void RequestNativeAds()
    {
        ads.googleAdsManager.RequestNativeAds();
    }

    public static bool IsNativeAdsLoaded()
    {
        return ads.googleAdsManager.IsNativeAdsLoaded();
    }

    public static void ShowNativeAds()
    {
        ads.googleAdsManager.ShowNativeAds();
    }

    #endregion
    #endregion

    #region fan
    public static void RequestFanBanner()
    {
        try
        {
            ads.fanAdsManager.RequestBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void ShowFanBanner()
    {
        try
        {
            ads.fanAdsManager.ShowBanner();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void RequestFanIntertitial()
    {
        try
        {
            ads.fanAdsManager.RequestInterstitial();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void ShowFanIntertitial()
    {
        try
        {
            ads.fanAdsManager.ShowInterstitial();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    public static string GetRateLink()
    {
#if UNITY_IPHONE
        return "https://itunes.apple.com/app/";
#elif UNITY_WP8
        return "";
#else
        return ("https://play.google.com/store/apps/details?id=" + Application.identifier);
#endif
    }

    public static void Rate()
    {
#if UNITY_IPHONE
        Application.OpenURL(GetRateLink());
#elif UNITY_WP8
       ShowRate.Rate rate = new ShowRate.Rate();
       rate.ShowMarket();
#else
        Application.OpenURL(GetRateLink());
#endif
    }

    //void OnApplicationQuit()
    //{
    //    GA.StopSession();
    //}

}
