using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
#if UNITY_IOS
    public const string APP_URL = "http://itunes.apple.com/app/id1484437063";
#else
    public const string APP_URL = "https://play.google.com/store/apps/details?id=vn.zenga.dragshoot";
#endif

#if DEBUG_ADMOB

#if UNITY_EDITOR || UNITY_STANDALONE
    public const string ADMOB_APP_ID = "";
    public const string ADMOB_BANNER_ID = "";
    public const string ADMOB_VIDEO_ID = "";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "";
#elif UNITY_IOS
    public const string ADMOB_APP_ID = "ca-app-pub-3940256099942544~1458002511";
    public const string ADMOB_BANNER_ID = "ca-app-pub-3940256099942544/2934735716";
    public const string ADMOB_VIDEO_ID = "ca-app-pub-3940256099942544/1712485313";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "ca-app-pub-3940256099942544/4411468910";
#elif UNITY_ANDROID
    public const string ADMOB_APP_ID = "ca-app-pub-3940256099942544~3347511713";
    public const string ADMOB_BANNER_ID = "ca-app-pub-3940256099942544/6300978111";
    public const string ADMOB_VIDEO_ID = "ca-app-pub-3940256099942544/5224354917";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "ca-app-pub-3940256099942544/1033173712";
#endif

#else

#if UNITY_EDITOR || UNITY_STANDALONE
    public const string ADMOB_APP_ID = "";
    public const string ADMOB_BANNER_ID = "";
    public const string ADMOB_VIDEO_ID = "";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "";
#elif UNITY_IOS
    public const string ADMOB_APP_ID = "ca-app-pub-6955407458153338~2748048113";
    public const string ADMOB_BANNER_ID = "ca-app-pub-6955407458153338/8453183822";
    public const string ADMOB_VIDEO_ID = "ca-app-pub-6955407458153338/4513938816";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "ca-app-pub-6955407458153338/7140102159";
#elif UNITY_ANDROID
    public const string ADMOB_APP_ID = "ca-app-pub-6955407458153338~6060607166";
    public const string ADMOB_BANNER_ID = "ca-app-pub-6955407458153338/8644755512";
    public const string ADMOB_VIDEO_ID = "ca-app-pub-6955407458153338/6994414885";
    public const string ADMOB_CENTER_INTERSTITIAL_ID = "ca-app-pub-6955407458153338/4444273167";
#endif

#endif
    public const int NUM_TOTAL_STAGE = 50;
    public const int START_ADS = 5;
    public const float TIME_BETWEEN_ADS = 60;
}
