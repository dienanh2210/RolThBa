using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace MoreApp {
    [System.Serializable]
    public class AdsInfo {

        public Number_level_passed_to_rate number_level_passed_to_rate = null;

        public Smart_more_app smart_more_app;

        public AdsItemType2 more_app = null;

        public AdsInfo()
        {
            smart_more_app = new Smart_more_app();
        }

    }
    [System.Serializable]
    public class Number_level_passed_to_rate
    {
        public int rate_condition = 10;

        public void SetRateCondition(string s)
        {
            int.TryParse(s, out rate_condition);
        }
    }
    [System.Serializable]
    public class Smart_more_app
    {
        public AdsItemType2 big_ad = null;

        public List<AdsItemType2> small_ad = new List<AdsItemType2>();
    }
    [System.Serializable]
    public class AdsItemType1
    {
        public string storeUrl = string.Empty;

        public string imageUrl = string.Empty;

        public Sprite sprite;

        public IEnumerator LoadSprite()
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                WWW www = new WWW(imageUrl);

                yield return www;

                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D texture = new Texture2D(1, 1);

                    www.LoadImageIntoTexture(texture);

                    Sprite s;

                    s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    sprite = s;
                }
                else
                {
                    Debug.Log(www.error);
                }
            }

            yield return null;
        }
    }
    [System.Serializable]
    public class AdsItemType2 : AdsItemType1
    {
        public string title = string.Empty;

        public string description = string.Empty;

        public int stars = 0;
    }
}