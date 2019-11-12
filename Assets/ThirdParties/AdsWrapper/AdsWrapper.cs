using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using SS.View;
using UnityEngine.Networking;

public class AdsWrapper : MonoBehaviour
{
    public static string appId;
    public static string bannerId;
    public static string videoId;
    public static string interstitialId;

    public delegate bool AdsDelegateBool();
    public AdsDelegateBool noAds;
   
    public delegate void AdsBoolDelegate(bool reward);
    public AdsBoolDelegate adsVideoRewardedCallback;

    public static AdsWrapper instance { get; protected set; }

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;

    private bool reward;

    public bool isShowBanner { get; protected set; }

    public float interstitialTime { get; protected set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(appId);

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        this.rewardBasedVideo.OnAdClosed += HandleVideoClosed;
        this.rewardBasedVideo.OnAdCompleted += HandleVideoCompleted;
        this.rewardBasedVideo.OnAdRewarded += HandleVideoRewarded;

        this.RequestBanner();
        this.RequestInterstitial();
        this.RequestRewardBasedVideo();
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Banner
    public void RequestBanner()
    {
        if (this.bannerView == null)
        {
            // Create a smart banner at the bottom of the screen.
            AdSize adSize = AdSize.Banner;
            this.bannerView = new BannerView(bannerId, adSize, AdPosition.Bottom);

            // Load a banner ad.
            this.bannerView.OnAdFailedToLoad += OnBannerAdsFailedToLoad;
            this.bannerView.LoadAd(this.CreateAdRequest());
        }
    }

    void OnBannerAdsFailedToLoad(object sender, EventArgs args)
    {
        DestroyBanner();
    }

    public void DestroyBanner()
    {
        if (this.bannerView != null)
        {
            this.bannerView.OnAdFailedToLoad -= OnBannerAdsFailedToLoad;
            this.bannerView.Destroy();
            this.bannerView = null;
        }
    }

    public void ShowBanner(float delay = 1f)
    {
        if (noAds != null && noAds())
            return;

        if (this.bannerView != null)
        {
            if (delay > 0 && Time.timeScale > 0)
            {
                Invoke("CoShowBanner", delay);
            }
            else
            {
                CoShowBanner();
            }
        }
        else
        {
            RequestBanner();
        }

        isShowBanner = true;
    }

    void CoShowBanner()
    {
        if (noAds != null && noAds())
            return;

        if (this.bannerView != null)
        {
            this.bannerView.Show();
        }
    }

    public void HideBanner()
    {
        if (noAds != null && noAds())
            return;

        CancelInvoke("CoShowBanner");

        if (this.bannerView != null)
        {
            this.bannerView.Hide();
        }

        isShowBanner = false;
    }
    #endregion

    #region Interstitial
    public void RequestInterstitial()
    {
        if (noAds != null && noAds())
            return;

        if (this.interstitial != null && !this.interstitial.IsLoaded())
        {
            this.interstitial.OnAdClosed -= HandleInterstitialClosed;
            this.interstitial.Destroy();
            this.interstitial = null;
        }

        if (this.interstitial == null)
        {
            this.interstitial = new InterstitialAd(interstitialId);
            this.interstitial.LoadAd(this.CreateAdRequest());
            this.interstitial.OnAdClosed += HandleInterstitialClosed;
        }
    }

    public void DestroyInterstitial()
    {
        if (this.interstitial != null)
        {
            this.interstitial.OnAdClosed -= HandleInterstitialClosed;
            this.interstitial.Destroy();
            this.interstitial = null;
        }
    }

    public void ShowInterstitial()
    {
        if (noAds != null && noAds())
            return;

        if (this.interstitial != null && this.interstitial.IsLoaded())
        {
            interstitialTime = Time.realtimeSinceStartup;
            this.interstitial.Show();
            return;
        }

        Manager.LoadingAnimation(true);
        RequestInterstitial();
        this.interstitial.OnAdLoaded += HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
    }

    public bool IsDestroyedInterstitial()
    {
        return (this.interstitial == null);
    }

    void HandleInterstitialClosed(object sender, EventArgs args)
    {
        DestroyInterstitial();
        RequestInterstitial();
    }

    void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        this.interstitial.OnAdLoaded -= HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad -= HandleInterstitialFailedToLoad;
        Manager.LoadingAnimation(false);

        ShowInterstitial();
    }

    void HandleInterstitialFailedToLoad(object sender, EventArgs args)
    {
        this.interstitial.OnAdLoaded -= HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad -= HandleInterstitialFailedToLoad;
        Manager.LoadingAnimation(false);
    }
    #endregion

    #region Reward
    public void ShowRewardBasedVideo(AdsBoolDelegate onVideoCompleted = null)
    {
        if (onVideoCompleted != null)
        {
            this.adsVideoRewardedCallback = onVideoCompleted;
        }

        if (this.rewardBasedVideo.IsLoaded())
        {
            this.reward = false;
            this.rewardBasedVideo.Show();
            return;
        }

        Manager.LoadingAnimation(true);
        this.rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += RewardBasedVideo_OnAdFailedToLoad;
        RequestRewardBasedVideo();
    }

    public void DestroyRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), string.Empty);
        }
    }

    void RewardBasedVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        this.rewardBasedVideo.OnAdLoaded -= RewardBasedVideo_OnAdLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad -= RewardBasedVideo_OnAdFailedToLoad;
        Manager.LoadingAnimation(false);

        // Temporary code
        this.reward = true;
        if (adsVideoRewardedCallback != null)
        {
            adsVideoRewardedCallback(this.reward);
            adsVideoRewardedCallback = null;
        }

        // Real code
        //Manager.Add(PopupController.POPUP_SCENE_NAME, new PopupData("No video available. Please try again later.", PopupType.OK));
    }

    void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        this.rewardBasedVideo.OnAdLoaded -= RewardBasedVideo_OnAdLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad -= RewardBasedVideo_OnAdFailedToLoad;
        Manager.LoadingAnimation(false);

        ShowRewardBasedVideo();
    }

    void RequestRewardBasedVideo()
    {
        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), videoId);
    }

    void HandleVideoClosed(object sender, EventArgs e)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            if (this.reward)
            {
                CallVideoRewared();
            }
            else
            {
                Manager.LoadingAnimation(true);
                Invoke("CallVideoRewared", 2f);
            }
        });
    }

    void CallVideoRewared()
    {
        Manager.LoadingAnimation(false);

        if (adsVideoRewardedCallback != null)
        {
            adsVideoRewardedCallback(this.reward);
            adsVideoRewardedCallback = null;
        }

        this.RequestRewardBasedVideo();
    }

    void HandleVideoCompleted(object sender, EventArgs args)
    {
        this.reward = true;
    }

    void HandleVideoRewarded(object sender, Reward e)
    {
        this.reward = true;
    }
    #endregion
}