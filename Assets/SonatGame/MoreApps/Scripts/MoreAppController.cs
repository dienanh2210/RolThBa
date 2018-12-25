using UnityEngine;
using System.Collections;
using MoreApp;

public class MoreAppController : MonoBehaviour
{

    public static MoreAppController instance;
    public GameObject SmartMoreApp;
    public string AppID;

    public AdsInfo adsinfo;

    public AdsItem[] adsItems;

    public bool isLoaded = false;

    public static bool isShowing = false;

    private const string Url = "http://sonatanhtrang.com/product/index.php/Global_advertise/get_android_ad";

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        WWWForm wwwform = new WWWForm();

        wwwform.AddField("appId", AppID);

        WWW www = new WWW(Url, wwwform);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Load moreapp failed");
            isLoaded = false;
        }
        else
        {
            Parse p = new Parse(www.text);
            adsinfo = p.GetAdsInfo();
            Invoke("LoadSprite", 0.5f);
            isLoaded = true;
        }        
    }

    void LoadSprite()
    {
        if (adsinfo.smart_more_app.big_ad != null)
        {
            StartCoroutine(adsinfo.smart_more_app.big_ad.LoadSprite());
        }
        for (int i = 0; i < adsinfo.smart_more_app.small_ad.Count; i++)
        {
            StartCoroutine(adsinfo.smart_more_app.small_ad[i].LoadSprite());
        }
        if (adsinfo.more_app != null)
        {
            StartCoroutine(adsinfo.more_app.LoadSprite());
        }
    }

    public void ShowSmartMoreApp()
    {
        if (isLoaded && RemoteSettingsHandler.remoteSettingsHandler.display_home_ads)
        {
            SmartMoreApp.SetActive(true);

            isShowing = true;

            UnityEngine.Analytics.Analytics.CustomEvent("show_more_app");
        }
    }

    public void HideSmartMoreApp()
    {
        isShowing = false;

        SmartMoreApp.SetActive(false);

        UnityEngine.Analytics.Analytics.CustomEvent("close_more_app");
    }

}
