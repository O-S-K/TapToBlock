using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class MenuController : Controller
{
    public const string MENU_SCENE_NAME = "Menu";

    public override string SceneName()
    {
        return MENU_SCENE_NAME;
    }

    void Awake()
    {
        AdsWrapper.appId = Const.ADMOB_APP_ID;
        AdsWrapper.bannerId = Const.ADMOB_BANNER_ID;
        AdsWrapper.interstitialId = Const.ADMOB_CENTER_INTERSTITIAL_ID;
        AdsWrapper.videoId = Const.ADMOB_VIDEO_ID;

        FirebaseManager.CheckGooglePlayService();
    }

    void Start()
    {

        Manager.ShieldColor = new Color(0f, 0f, 0f, 0.8f);
        Manager.LoadingSceneName = LoadingController.LOADING_SCENE_NAME;
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }
}