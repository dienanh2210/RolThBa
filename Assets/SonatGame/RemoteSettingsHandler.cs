using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSettingsHandler : MonoBehaviour
{
    public static RemoteSettingsHandler remoteSettingsHandler;

    public bool display_home_ads = true;
    public bool display_banner_ads = true;
    public bool display_interstitial_ads = true;
    public bool display_video_ads = true;

    public string admob_banner_id = "ca-app-pub-4692613555071238/6763802217";
    public string admob_interstitial_id = "ca-app-pub-4692613555071238/9228638864";

    public string fan_banner_id = "YOUR_PLACEMENT_ID";
    public string fan_interstitial_id = "YOUR_PLACEMENT_ID";

    public float delay_interstital_time = 5;

    public int admob_banner_ratio = 50;
    public int fan_banner_ratio = 50;

    public int admob_interstitial_ratio = 50;
    public int fan_interstitial_ratio = 50;

    //public static int ball_bonus = 0;

    void Awake()
    {
        remoteSettingsHandler = this;
    }

    // Use this for initialization
    void Start()
    {
        RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(HandleRemoteUpdate);
    }

    private void HandleRemoteUpdate()
    {
        display_home_ads = RemoteSettings.GetBool("display_home_ads", true);
        display_banner_ads = RemoteSettings.GetBool("display_banner_ads", true);
        display_interstitial_ads = RemoteSettings.GetBool("display_interstitial_ads", true);
        display_video_ads = RemoteSettings.GetBool("display_video_ads", true);

        admob_banner_id = RemoteSettings.GetString("admob_banner_id", admob_banner_id);
        admob_interstitial_id = RemoteSettings.GetString("admob_interstitial_id", admob_interstitial_id);

        fan_banner_id = RemoteSettings.GetString("fan_banner_id", fan_banner_id);
        fan_interstitial_id = RemoteSettings.GetString("fan_interstitial_id", fan_interstitial_id);

        delay_interstital_time = RemoteSettings.GetFloat("delay_interstital_time", delay_interstital_time);

        admob_banner_ratio = RemoteSettings.GetInt("admob_banner_ratio", admob_banner_ratio);
        fan_banner_ratio = RemoteSettings.GetInt("fan_banner_ratio", fan_banner_ratio);

        admob_interstitial_ratio = RemoteSettings.GetInt("admob_interstitial_ratio", admob_interstitial_ratio);
        fan_interstitial_ratio = RemoteSettings.GetInt("fan_interstitial_ratio", fan_interstitial_ratio);

        //ball_bonus = RemoteSettings.GetInt("ball_bonus", 0);
    }
}
