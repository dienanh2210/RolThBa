/*
 * @Author: CuongNH
 * @Description: Demo viec su dung Unity Ads
 * */

using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;

public class UnityAdsDemo : MonoBehaviour
{
    public string ANDROID_GAME_ID = "73390";
    public string IOS_GAME_ID;

    private int coins = 0;

    public int coinsBonus = 5;
    public UnityEngine.UI.Text coinTxt;

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

    //[SerializeField]
    //public string gameID = "73390";

    #region
    // Ads Manager
    void Awake()
    {
#if UNITY_IPHONE
        Advertisement.Initialize(IOS_GAME_ID, false);
#elif UNITY_WP8
        appId = APPID_WP;
#else
        Advertisement.Initialize(ANDROID_GAME_ID, true);
#endif
    }

    public void ShowAds()
    {
        StartCoroutine(ShowAdsWhenReady());
    }

    IEnumerator ShowAdsWhenReady(string zone = "")
    {
        while (!Advertisement.IsReady(zone))
        {
            yield return null;
        }

        //Advertisement.Show();
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
        }
    }

    public void ShowAd(string zone = "")
    {
        #if UNITY_EDITOR
            StartCoroutine(WaitForAd ());
        #endif

        if (string.Equals(zone, ""))
        {
            zone = null;
        }

        ShowOptions options = new ShowOptions ();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
        }
    }

    void AdCallbackhandler (ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                Debug.Log ("Ad Finished. Rewarding player...");

                coins += coinsBonus;
                coinTxt.text = coins.ToString();

                break;
            case ShowResult.Skipped:
                Debug.Log ("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
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
