/*
 * @Author: CuongNH
 * @Description: Xu ly cac su kien lien quan toi Unity Ads
 * */

using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;

public class UnityAdsController : MonoBehaviour
{

    public string ANDROID_GAME_ID;
    public string IOS_GAME_ID;

    public static UnityAdsController unityAdsController;

    //public CurrencyManager.Currency currency;

    #region
    // Simple
    void Start()
    {
        //Advertisement.Initialize("73390", true);

        //StartCoroutine(ShowAdWhenReady());
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show();
    }
    #endregion

    #region
    // Ads Manager
    void Awake()
    {
        unityAdsController = this;

//#if UNITY_IPHONE
//                Advertisement.Initialize(IOS_GAME_ID, false);
//#elif UNITY_WP8
//                appId = APPID_WP;
//#else
//        InitializeUnityAds();
//#endif
    }

    public void InitializeUnityAds()
    {
        //FireBaseController.LogEvent("watch_video_request", "network_name", "unity");

#if UNITY_IPHONE
        Advertisement.Initialize(IOS_GAME_ID, false);
#else
        Advertisement.Initialize(ANDROID_GAME_ID, false);
#endif
    }

    //public void ShowAds()
    //{
    //    StartCoroutine(ShowAdsWhenReady());
    //}

    //IEnumerator ShowAdsWhenReady(string zone = "")
    //{
    //    while (!Advertisement.IsReady(zone))
    //    {
    //        yield return null;
    //    }

    //    //Advertisement.Show();
    //    ShowOptions options = new ShowOptions();
    //    options.resultCallback = AdCallbackhandler;

    //    if (Advertisement.IsReady(zone))
    //    {
    //        //FireBaseController.LogEvent("watch_video_show", "network_name", "unity");

    //        Advertisement.Show(zone, options);
    //    }
    //}

    public void ShowAd(string zone = "")
    {
#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif

        if (string.Equals(zone, ""))
        {
            zone = null;
        }

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            //FireBaseController.LogEvent("watch_video_show", "network_name", "unity");

            //MusicController.music.SetVolumn(0f);

            Advertisement.Show(zone, options);
        }
    }

    // Ads callback handler
    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");

			//FireBaseController.LogEvent ("watch_video_complete", "network_name", "unity");

            break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");

			//FireBaseController.LogEvent ("watch_video_close", "network_name", "unity");

                break;
        case ShowResult.Failed:
            Debug.Log("I swear this has never happened to me before");

            //FireBaseController.LogEvent("watch_video_load_fail", "network_name", "unity");

            break;
        default:

            break;
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
    #endregion
}
