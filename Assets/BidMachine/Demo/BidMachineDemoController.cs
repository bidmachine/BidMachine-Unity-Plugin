using System;
using System.Collections.Generic;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;
using UnityEngine.UI;

public class BidMachineDemoController : MonoBehaviour, IInterstitialAdListener, IRewardedAdListener, IBannerListener
{
    [SerializeField] public Toggle tgTesting;
    [SerializeField] public Toggle tgLogging;

    TargetingParams targetingParams;
    PriceFloorParams priceFloorParams;

    InterstitialRequest interstitialRequest;
    InterstitialRequestBuilder interstitialRequestBuilder;
    InterstitialAd interstitialAd;

    RewardedRequest rewardedRequest;
    RewardedRequestBuilder rewardedRequestBuilder;
    RewardedAd rewardedAd;

    BannerRequest bannerRequest;
    BannerRequestBuilder bannerRequestBuilder;
    BannerView bannerView;
    Banner banner;

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;
    }

    public void BidMachineInitialize()
    {
        targetingParams = new TargetingParams();
        targetingParams.setUserId("1");
        targetingParams.setGender(TargetingParams.Gender.Female);
        targetingParams.setBirthdayYear(1991);
        targetingParams.setKeyWords(new[] { "games, sport" });
        targetingParams.setCountry("Belarus");
        targetingParams.setCity("Minsk");
        targetingParams.setZip("220059");
        targetingParams.setStoreUrl("https://store.url");
        targetingParams.setStoreCategory("cards");
        targetingParams.setStoreSubCategories(new string[]{"games", "cards"});
        targetingParams.setFramework("unity");
        targetingParams.setFramework("unity");
        targetingParams.setPaid(true);
        targetingParams.setDeviceLocation("", 22.0d, 22.0d);
        targetingParams.setExternalUserIds(new ExternalUserId[]
        {
            new ExternalUserId("sourceId_1", "1"), 
            new ExternalUserId("sourceId_2", "2")
        });
        targetingParams.addBlockedApplication("com.appodeal.test");
        targetingParams.addBlockedAdvertiserIABCategory("IAB-71");
        targetingParams.addBlockedAdvertiserDomain("ua");


        Publisher publisher = new Publisher();
        publisher.setId("1");
        publisher.setName("Gena");
        publisher.setDomain("ua");
        publisher.addCategory("sport");
        publisher.addCategories(new[] { "games, cards" });
        
        BidMachine.setPublisher(publisher);
        BidMachine.setLoggingEnabled(tgLogging.isOn);
        BidMachine.setTestMode(tgTesting.isOn);
        BidMachine.setEndpoint("https://test.com");
        BidMachine.setTargetingParams(targetingParams);
        BidMachine.initialize("1");
        BidMachine.setConsentConfig(true, "test consent string");
        BidMachine.setSubjectToGDPR(true);
        BidMachine.setCoppa(true);
        BidMachine.setUSPrivacyString("test_string");
        BidMachine.checkAndroidPermissions(Permission.CoarseLocation);
        
    }

    public void IsInitialized()
    {
        Debug.Log($"isInitialized - {BidMachine.isInitialized()}");
    }

    public void CheckPermissions()
    {
        Debug.Log($"Permission.CoarseLocation - {BidMachine.checkAndroidPermissions(Permission.CoarseLocation)}");
        Debug.Log($"Permission.FineLocation - {BidMachine.checkAndroidPermissions(Permission.FineLocation)}");
    }

    public void RequestPermissions()
    {
        BidMachine.requestAndroidPermissions();
    }

    public void LoadInterstitialAd()
    {
        if (interstitialAd == null)
        {
            interstitialAd = new InterstitialAd();
        }

        if (interstitialRequest == null)
        {
            interstitialRequest = new InterstitialRequestBuilder()
                .setAdContentType(InterstitialRequestBuilder.ContentType.All)
                //.setTargetingParams(targetingParams)
                //.setPriceFloorParams(priceFloorParams)
                .build();
        }

        interstitialAd.setListener(this);
        interstitialAd.load(interstitialRequest);
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd == null)
        {
            rewardedAd = new RewardedAd();
        }

        if (rewardedRequest == null)
        {
            rewardedRequest = new RewardedRequestBuilder()
                //.setTargetingParams(targetingParams)
                //.setPriceFloorParams(priceFloorParams)
                .build();
        }

        rewardedAd.setListener(this);
        rewardedAd.load(rewardedRequest);
    }

    public void LoadBanner()
    {
        bannerRequest = new BannerRequestBuilder()
            .setSize(BannerRequestBuilder.Size.Size_320_50)
            //.setTargetingParams(targetingParams)
            //.setPriceFloorParams(priceFloorParams)
            .build();

        banner = new Banner();
        bannerView = banner.GetBannerView();

        bannerView.setListener(this);
        bannerView.load(bannerRequest);
    }

    public void DestroyBanner()
    {
        bannerView.destroy();
        banner.hideBannerView();
    }

    public void DestroyRewardedVideo()
    {
        if (rewardedAd == null) return;
        rewardedAd.destroy();
        rewardedAd = null;
        rewardedRequest = null;
    }

    public void DestroyInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.destroy();
            interstitialAd = null;
            interstitialRequest = null;
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.canShow())
        {
            interstitialAd.show();
        }
        else
        {
            Debug.Log("canShow - InterstitialAd - false");
        }
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.canShow())
        {
            rewardedAd.show();
        }
        else
        {
            Debug.Log("canShow - RewardedAd - false");
        }
    }


    #region InterstitialAd Callbacks

    public void onAdLoaded(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoaded");
    }

    public void onAdLoadFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoadFailed");
    }

    public void onAdShown(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShown");
    }

    public void onAdImpression(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdImpression");
    }

    public void onAdClosed(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    public void onAdExpired(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdExpired");
    }

    public void onAdShowFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShowFailed");
    }

    public void onAdClosed(InterstitialAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClosed");
    }

    public void onAdClicked(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    #endregion

    #region RewardedAd Callbacks

    public void onAdLoaded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoaded");
    }

    public void onAdLoadFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoadFailed");
    }

    public void onAdShown(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShown");
    }

    public void onAdImpression(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdImpression");
    }

    public void onAdClicked(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClicked");
    }

    public void onAdExpired(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdExpired");
    }

    public void onAdShowFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShowFailed");
    }

    public void onAdClosed(RewardedAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
        Debug.Log("BidMachineUnity: RewardedAd - isFinished " + finished);
    }

    public void onAdRewarded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    public void onAdClosed(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    #endregion

    #region BannerView Callbacks

    public void onAdLoaded(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoaded");
        banner.showBannerView(BidMachine.BANNER_TOP, BidMachine.BANNER_HORIZONTAL_CENTER, bannerView);
    }

    public void onAdLoadFailed(BannerView ad, BMError error)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoadFailed");
    }

    public void onAdShown(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdShown");
    }

    public void onAdImpression(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdImpression");
    }

    public void onAdClicked(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdClicked");
    }

    public void onAdExpired(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdExpired");
    }

    #endregion
}