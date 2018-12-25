using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using MoreApp;
    public class AdsItem : MonoBehaviour
    {
        public GameObject Loading;

        public Image download;

        public Image AppImage;

        public string AppStoreUrl;

        public Text AppTitle;

        public Text AppDescription;

        public int imageindex;
        //0 = big ads
        //1,2 = small ads
        //3 = more app
       
        public void ImageClick()
        {
            if (!string.IsNullOrEmpty(AppStoreUrl))
            {
                Application.OpenURL(AppStoreUrl);

                UnityEngine.Analytics.Analytics.CustomEvent("click_more_app");
                UnityEngine.Analytics.Analytics.CustomEvent("click_more_app", new Dictionary<string, object>
                {
                    { "app_link", AppStoreUrl }
                });
            }
        }

   public bool isLoaded = false;
    bool isdone = false;
    void Update()
    {
        if (!isdone)
        {
            if (isLoaded)
            {
                Loading.SetActive(false);
                isdone = true;
            }
            else
            {
                CheckIndex();
            }
        }
    }
     
    void CheckIndex()
    {
        if (imageindex == 0 && MoreAppController.instance.adsinfo.smart_more_app.big_ad != null && MoreAppController.instance.adsinfo.smart_more_app.big_ad.sprite != null)
        {
            SetInfo(MoreAppController.instance.adsinfo.smart_more_app.big_ad);
        }
        else if (imageindex < 3 && imageindex > 0 && MoreAppController.instance.adsinfo.smart_more_app.small_ad.Count > imageindex - 1 && MoreAppController.instance.adsinfo.smart_more_app.small_ad[imageindex - 1].sprite != null)
        {
            SetInfo(MoreAppController.instance.adsinfo.smart_more_app.small_ad[imageindex - 1]);
        }
        else if (imageindex == 3 && MoreAppController.instance.adsinfo.more_app != null && MoreAppController.instance.adsinfo.more_app.sprite != null)
        {
            SetInfo(MoreAppController.instance.adsinfo.more_app);
        }
    }
    void SetInfo(AdsItemType2 ads)
    {
        isLoaded = true;

        AppImage.sprite = ads.sprite;

        AppStoreUrl = ads.storeUrl;

        download.gameObject.SetActive(true);
      //  AppTitle.text = ads.title;
    }  
}