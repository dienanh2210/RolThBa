using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;

namespace MoreApp
{
    public class Parse
    {
        string JsonData;

        private const string NUMBER_LEVEL_PASSED_TO_RATE = "number_level_passed_to_rate";

        private const string SMART_MORE_APP = "smart_more_app";

        private const string RATE_CONDITION = "rate_condition";

        private const string MORE_APP = "more_app";

        private const string BIG_AD = "big_ad";

        private const string SMALL_AD = "small_ad";

        private const string URL = "url";

        private const string APPURL = "appUrl";

        private const string IMAGEURL = "image";

        private const string TITLE = "title";

        private const string DESCRIPTION = "description";

        private const string THUMBNAILURL = "thumbnailUrl";

        private const string STAR = "star";

        public Parse(string jsondata)
        {
            JsonData = jsondata;
            //Debug.Log(jsondata);
        }

        public AdsInfo GetAdsInfo()
        {
            AdsInfo adsinfo = new AdsInfo();

            try
            {
                var dict = Json.Deserialize(JsonData) as Dictionary<string, object>;
                //----------number_level_passed_to_rate-------------------
                if (dict.ContainsKey(NUMBER_LEVEL_PASSED_TO_RATE))
                {
                    var number_level_passed_to_rate = dict[NUMBER_LEVEL_PASSED_TO_RATE] as Dictionary<string, object>;

                    adsinfo.number_level_passed_to_rate = getNumber_level_passed_to_rate(number_level_passed_to_rate);
                }
                //---------------------SMART_MORE_APP-----------------------
                if (dict.ContainsKey(SMART_MORE_APP))
                {

                    var smart_more_app = dict[SMART_MORE_APP] as Dictionary<string, object>;

                    //---------------------Big ads-----------------------

                    if (smart_more_app.ContainsKey(BIG_AD))
                    {
                        var big_ad = smart_more_app[BIG_AD] as Dictionary<string, object>;

                        adsinfo.smart_more_app.big_ad = getSmartMoreAppItem(big_ad);

                    }
                    //---------------------small_ad-----------------------
                    if (smart_more_app.ContainsKey(SMALL_AD))
                    {
                        var small_ad = smart_more_app[SMALL_AD] as IList;

                        foreach (Dictionary<string, object> item in small_ad)
                        {
                            adsinfo.smart_more_app.small_ad.Add(getSmartMoreAppItem(item));
                        }
                    }

                }
                //---------------------more app-----------------------
                if (dict.ContainsKey(MORE_APP))
                {
                    var more_app = dict[MORE_APP] as IList;
                    if (more_app.Count > 0)
                    {
                        adsinfo.more_app = GetMoreApp(more_app[0] as Dictionary<string, object>);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }

            return adsinfo;
        }


        Number_level_passed_to_rate getNumber_level_passed_to_rate(Dictionary<string, object> d)
        {
            Number_level_passed_to_rate n = new Number_level_passed_to_rate();

            if (d != null && d.ContainsKey(RATE_CONDITION))
            {
                n.SetRateCondition(d[RATE_CONDITION].ToString());
            }

            return n;
        }

        AdsItemType2 getSmartMoreAppItem(Dictionary<string, object> d)
        {
            AdsItemType2 a = new AdsItemType2();

            if (d == null)
            {
                return a;
            }

            if (d.ContainsKey(URL))
            {
                a.storeUrl = d[URL].ToString();
            }
            if (d.ContainsKey(IMAGEURL))
            {
                a.imageUrl = d[IMAGEURL].ToString();
            }
            return a;
        }
        AdsItemType2 GetMoreApp(Dictionary<string, object> d)
        {
            AdsItemType2 a = new AdsItemType2();

            if (d == null)
            {
                return a;
            }
            else
            {
                if (d.ContainsKey(APPURL))
                {
                    a.storeUrl = d[APPURL].ToString();
                }
                if (d.ContainsKey(TITLE))
                {
                  a.title = d[TITLE].ToString();
                }
                if (d.ContainsKey(DESCRIPTION))
                {
                    a.description = d[DESCRIPTION].ToString();
                }
                if (d.ContainsKey(THUMBNAILURL))
                {
                    a.imageUrl = d[THUMBNAILURL].ToString();
                }
                if (d.ContainsKey(STAR))
                {
                    int.TryParse(d[STAR].ToString(), out a.stars);
                }
            }
            return a;
        }

    }

    
        
}
