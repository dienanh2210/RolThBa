using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using AudienceNetwork;
using AudienceNetwork.Utility;

public class FanAdsManager : MonoBehaviour {

    //// Use this for initialization
    //void Start () {
		
    //}
	
    //// Update is called once per frame
    //void Update () {
		
    //}

    public static bool isBannerLoaded = false;
    public static bool isShowOnLoaded = false;
    public static bool isIntertitialLoaded = false;
    public static bool isIntertitial2Loaded = false;

    public static bool isReloadBanner = false;
    public static bool isReloadIntertitial = false;

    private AdView banner;
    private InterstitialAd interstitialAd;

    private static bool isFirstReLoadBanner = false;
    private static bool isFirstReloadIntertitial = false;

    public void RequestBanner()
    {
        //Debug.Log("RequestBanner");

        // Create a banner's ad view with a unique placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        AdView adView = new AdView(RemoteSettingsHandler.remoteSettingsHandler.fan_banner_id, AdSize.BANNER_HEIGHT_50);
        this.banner = adView;
        this.banner.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.banner.AdViewDidLoad = (delegate()
        {
            //Debug.Log("Ad view loaded.");

            isBannerLoaded = true;

            //isReloadBanner = false;

            //this.adView.Show(AdUtility.height() - 50);
            //this.adView.Show(100);

            SG_AdManager.ads.ShowBanner();
        });

        adView.AdViewDidFailWithError = (delegate(string error)
        {
            //Debug.Log("Ad view failed to load with error: " + error);

            isBannerLoaded = false;
            DisposeBanner();

            if (!isFirstReLoadBanner)
            {
                isFirstReLoadBanner = true;

                RequestBanner();
            }
            else
            {
                if (!isReloadBanner)
                {
                    //Debug.Log("ReloadBanner");

                    isReloadBanner = true;

                    SG_AdManager.ads.googleAdsManager.InitAdmob();
                    SG_AdManager.RequestAdmobBanner();
                }
            }
        });

        adView.AdViewWillLogImpression = (delegate()
        {
            //Debug.Log("Ad view logged impression.");

            //FireBaseController.LogEvent("fan_banner_impression", "state_name", MainState.state.ToString());
        });

        adView.AdViewDidClick = (delegate()
        {
            //Debug.Log("Ad view clicked.");

            //FireBaseController.LogEvent("fan_banner_click", "state_name", MainState.state.ToString());
        });

        // Initiate a request to load an ad.
        adView.LoadAd();
    }

    public void ShowBanner()
    {
        if (isBannerLoaded)
        {
            isShowOnLoaded = true;
            this.banner.Show(AdUtility.height() - 50);
        }
    }

    public void RequestInterstitial()
    {
        if (isIntertitialLoaded)
        {
            return;
        }

        //Debug.Log("RequestInterstitial");

        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        InterstitialAd interstitialAd = new InterstitialAd(RemoteSettingsHandler.remoteSettingsHandler.fan_interstitial_id);
        this.interstitialAd = interstitialAd;
        this.interstitialAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.interstitialAd.InterstitialAdDidLoad = (delegate()
        {
            //Debug.Log("Interstitial ad loaded.");

            isIntertitialLoaded = true;

            //isReloadIntertitial = false;
        });

        interstitialAd.InterstitialAdDidFailWithError = (delegate(string error)
        {
            //Debug.Log("Interstitial ad failed to load with error: " + error);

            isIntertitialLoaded = false;

            if (!isFirstReloadIntertitial)
            {
                isFirstReloadIntertitial = true;

                RequestInterstitial();
            }
            else
            {
                if (!isReloadIntertitial)
                {
                    //Debug.Log("Reload Interstitial");

                    isReloadIntertitial = true;

                    SG_AdManager.ads.googleAdsManager.InitAdmob();
                    SG_AdManager.RequestAdmobInterstitial();
                }
            }
        });

        interstitialAd.InterstitialAdWillLogImpression = (delegate()
        {
            //Debug.Log("Interstitial ad logged impression.");

            //FireBaseController.LogEvent("fan_intertitial_impression", "state_name", MainState.state.ToString());
        });

        interstitialAd.InterstitialAdDidClick = (delegate()
        {
            //Debug.Log("Interstitial ad clicked.");

            //FireBaseController.LogEvent("fan_intertitial_click", "state_name", MainState.state.ToString());
        });

        interstitialAd.interstitialAdDidClose = (delegate()
        {
            //Debug.Log("Interstitial ad closed.");

            //Time.timeScale = 1f;

            //MainAudio.Main.MuteSound((Settings.GetSound == 1) ? false : true);

            isIntertitialLoaded = false;
            GoogleAds.isInterstitialLoaded = false;

            SG_AdManager.ads.RequestIntertitial();

            //if (MainState.state == MainState.State.Ingame)
            //{
            //    //RequestInterstitial();
            //    SG_AdManager.ads.RequestIntertitial();
            //}
        });

        // Initiate the request to load the ad.
        this.interstitialAd.LoadAd();
    }

    // Show button
    public void ShowInterstitial()
    {
        if (isIntertitialLoaded)
        {
            this.interstitialAd.Show();
            isIntertitialLoaded = false;
        }
        else
        {
            
        }
    }

    public void DisposeBanner()
    {
        // Dispose of banner ad when the scene is destroyed
        if (this.banner != null)
        {
            this.banner.Dispose();

            isBannerLoaded = false;
            isShowOnLoaded = false;

            //Debug.Log("AdViewTest was destroyed!");
        }
    }

    public void DisposeIntertitial()
    {
        // Dispose of interstitial ad when the scene is destroyed
        if (this.interstitialAd != null)
        {
            this.interstitialAd.Dispose();

            isIntertitialLoaded = false;

            //Debug.Log("InterstitialAdTest was destroyed!");
        }
    }

    void OnDestroy()
    {
        DisposeBanner();

        DisposeIntertitial();
    }
}
