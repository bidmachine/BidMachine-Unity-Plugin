using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;
using UnityEngine.UI;

#pragma warning disable 649

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class BidMachineDemoController : MonoBehaviour, IInterstitialAdListener, IRewardedAdListener, IBannerListener,
    IBannerRequestListener, IInterstitialRequestListener, IRewardedRequestListener, INativeRequestListener,
    INativeAdListener
{
    [SerializeField] public Toggle tgTesting;
    [SerializeField] public Toggle tgLogging;
    [SerializeField] public NativeAdView nativeAdView;

    private TargetingParams targetingParams;
    private PriceFloorParams priceFloorParams;
    private SessionAdParams sessionAdParams;

    private InterstitialRequest interstitialRequest;
    private InterstitialRequestBuilder interstitialRequestBuilder;
    private InterstitialAd interstitialAd;

    private RewardedRequest rewardedRequest;
    private RewardedRequestBuilder rewardedRequestBuilder;
    private RewardedAd rewardedAd;

    private NativeRequest nativeRequest;
    private NativeRequestBuilder nativeRequestBuilder;
    private NativeAd nativeAd;

    private BannerRequest bannerRequest;
    private BannerRequestBuilder bannerRequestBuilder;
    private BannerView bannerView;
    private Banner banner;

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;

        BidMachine.setTargetingParams(
            new TargetingParams()
                .setUserId("1")
                .setStoreId("12345")
                .setGender(TargetingParams.Gender.Female)
                .setBirthdayYear(1991)
                .setKeyWords(new[] { "games, sport" })
                .setCountry("Belarus")
                .setCity("Minsk")
                .setZip("220059")
                .setStoreUrl("https://store.url")
                .setStoreCategory("cards")
                .setStoreId("12345")
                .setStoreSubCategories(new[] { "games", "cards" })
                .setFramework("unity")
                .setPaid(true)
                .setDeviceLocation("", 22.0d, 22.0d)
                .addBlockedApplication("com.appodeal.test")
                .addBlockedAdvertiserIABCategory("IAB-71")
                .addBlockedAdvertiserDomain("ua")
                .setExternalUserIds(new[]
                    {
                        new ExternalUserId("sourceId_1", "1"),
                        new ExternalUserId("sourceId_2", "2")
                    }
                ));

        priceFloorParams = new PriceFloorParams();
        priceFloorParams.addPriceFloor("123", 1.2d);

        sessionAdParams = new SessionAdParams()
            .setSessionDuration(123)
            .setImpressionCount(123)
            .setClickRate(1.2f)
            .setIsUserClickedOnLastAd(true)
            .setCompletionRate(1.3f)
            .setLastBundle("test")
            .setLastAdomain("test");
    }

    public void BidMachineInitialize()
    {
        BidMachine.setPublisher(new Publisher("1", "Gena", "ua", new[] { "games, cards" }));
        BidMachine.setEndpoint("https://test.com");
        BidMachine.setSubjectToGDPR(true);
        BidMachine.setCoppa(true);
        BidMachine.setConsentConfig(true, "test consent string");
        BidMachine.setConsentConfig(true, "test consent string");
        BidMachine.setUSPrivacyString("test_string");
        BidMachine.checkAndroidPermissions(Permission.CoarseLocation);
        BidMachine.setLoggingEnabled(tgLogging.isOn);
        BidMachine.setTestMode(tgTesting.isOn);
        BidMachine.initialize("1");
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

    #region Banner Ad

    public void LoadBanner()
    {
        bannerRequest = new BannerRequestBuilder()
            .setSize(BannerSize.Size_320х50)
            // .setTargetingParams(targetingParams)
            // .setPriceFloorParams(priceFloorParams)
            // .setSessionAdParams(sessionAdParams)
            // .setPlacementId("placement1")
            // .setLoadingTimeOut(123)
            // .setBidPayload("123")
            // .setNetworks("admob")
            .setListener(this)
            .build();

        banner = new Banner();
        bannerView = banner.GetBannerView();

        bannerView.setListener(this);
        bannerView.load(bannerRequest);
    }

    public void ShowBannerView()
    {
        banner.showBannerView(
            BidMachine.BANNER_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerView);
    }

    public void DestroyBanner()
    {
        bannerView.destroy();
        banner.hideBannerView();
    }

    #endregion

    #region Native Ad

    public void LoadNativeAd()
    {
        if (nativeAd == null)
        {
            nativeAd = new NativeAd();
        }

        var nativeAdParams = new NativeAdParams();
        nativeAdParams.setMediaAssetTypes(NativeAdParams.MediaAssetType.Icon, NativeAdParams.MediaAssetType.Image);

        if (nativeRequest == null)
        {
            nativeRequest = new NativeRequestBuilder()
                .setMediaAssetTypes(nativeAdParams)
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                // .setPlacementId("placement1")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();
        }

        nativeAd.setListener(this);
        nativeAd.load(nativeRequest);
    }

    public void DestroyNativeAd()
    {
        if (nativeAd == null) return;
        nativeAd.destroy();
        nativeAdView.destroyNativeView();
        nativeAd = null;
        nativeRequest = null;
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        if (interstitialAd == null)
        {
            interstitialAd = new InterstitialAd();
        }

        if (interstitialRequest == null)
        {
            interstitialRequest = new InterstitialRequestBuilder()
                .setAdContentType(AdContentType.All)
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                // .setPlacementId("placement1")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();
        }

        interstitialAd.setListener(this);
        interstitialAd.load(interstitialRequest);
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

    public void DestroyInterstitial()
    {
        if (interstitialAd == null) return;
        interstitialAd.destroy();
        interstitialAd = null;
        interstitialRequest = null;
    }

    #endregion

    #region Rewarded Video Ad

    public void LoadRewardedAd()
    {
        if (rewardedAd == null)
        {
            rewardedAd = new RewardedAd();
        }

        if (rewardedRequest == null)
        {
            rewardedRequest = new RewardedRequestBuilder()
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                // .setPlacementId("placement1")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();
        }

        rewardedAd.setListener(this);
        rewardedAd.load(rewardedRequest);
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

    public void DestroyRewardedVideo()
    {
        if (rewardedAd == null) return;
        rewardedAd.destroy();
        rewardedAd = null;
        rewardedRequest = null;
    }

    #endregion

    #region InterstitialAd Callbacks

    public void onInterstitialAdLoaded(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoaded");
    }

    public void onInterstitialAdLoadFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoadFailed");
    }

    public void onInterstitialAdShown(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShown");
    }

    public void onInterstitialAdImpression(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdImpression");
    }

    public void onInterstitialAdClosed(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    public void onInterstitialAdExpired(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdExpired");
    }

    public void onInterstitialAdShowFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShowFailed");
    }

    public void onInterstitialAdClosed(InterstitialAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClosed");
    }

    public void onInterstitialAdClicked(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    #endregion

    #region RewardedAd Callbacks

    public void onRewardedAdLoaded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoaded");
    }

    public void onRewardedAdLoadFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoadFailed");
    }

    public void onRewardedAdShown(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShown");
    }

    public void onRewardedAdImpression(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdImpression");
    }

    public void onRewardedAdClicked(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClicked");
    }

    public void onRewardedAdExpired(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdExpired");
    }

    public void onRewardedAdShowFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShowFailed");
    }

    public void onRewardedAdClosed(RewardedAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
        Debug.Log("BidMachineUnity: RewardedAd - isFinished " + finished);
    }

    public void onRewardedAdRewarded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    public void onRewardedAdClosed(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    #endregion

    #region BannerView Callbacks

    public void onBannerAdLoaded(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoaded");
    }

    public void onBannerAdLoadFailed(BannerView ad, BMError error)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoadFailed");
    }

    public void onBannerAdShown(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdShown");
    }

    public void onBannerAdImpression(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdImpression");
    }

    public void onBannerAdClicked(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdClicked");
    }

    public void onBannerAdExpired(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdExpired");
    }

    #endregion

    #region BannerRequestListener

    public void onBannerRequestSuccess(BannerRequest request, string auctionResult)
    {
        if (request != null)
        {
            Debug.Log($"onBannerRequestSuccess - request.getSize() - {request.getSize()}");
            Debug.Log($"onBannerRequestSuccess - request.getAuctionResult() - {request.getAuctionResult()}");
            Debug.Log($"onBannerRequestSuccess - request.isExpired() - {request.isExpired()}");
            Debug.Log($"onBannerRequestSuccess - request.isDestroyed() - {request.isDestroyed()}");
        }

        if (!string.IsNullOrEmpty(auctionResult))
        {
            Debug.Log($"BannerRequestListener - onBannerRequestSuccess" +
                      $"auctionResult - {auctionResult}");
        }
        else
        {
            Debug.Log("auctionResult - IsNullOrEmpty");
        }
    }

    public void onBannerRequestFailed(BannerRequest request, BMError error)
    {
        Debug.Log("BannerRequestListener - onBannerRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onBannerRequestExpired(BannerRequest request)
    {
        Debug.Log("BannerRequestListener - onBannerRequestExpired");
    }

    #endregion

    #region InterstitialRequestListener

    public void onInterstitialRequestSuccess(InterstitialRequest request, string auctionResult)
    {
        if (request != null)
        {
            Debug.Log($"onInterstitialRequestSuccess - request.getAuctionResult() - {request.getAuctionResult()}");
            Debug.Log($"onInterstitialRequestSuccess - request.isExpired() - {request.isExpired()}");
            Debug.Log($"onInterstitialRequestSuccess - request.isDestroyed() - {request.isDestroyed()}");
        }

        if (!string.IsNullOrEmpty(auctionResult))
        {
            Debug.Log($"InterstitialRequestListener - onInterstitialRequestSuccess" +
                      $"auctionResult - {auctionResult}");
        }
        else
        {
            Debug.Log("auctionResult - IsNullOrEmpty");
        }
    }

    public void onInterstitialRequestFailed(InterstitialRequest request, BMError error)
    {
        Debug.Log($"InterstitialRequestListener - onInterstitialRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onInterstitialRequestExpired(InterstitialRequest request)
    {
        Debug.Log($"InterstitialRequestListener - onInterstitialRequestExpired");
    }

    #endregion

    #region RewardedRequestListener

    public void onRewardedRequestSuccess(RewardedRequest request, string auctionResult)
    {
        if (request != null)
        {
            Debug.Log($"onRewardedRequestSuccess - request.getAuctionResult() - {request.getAuctionResult()}");
            Debug.Log($"onRewardedRequestSuccess - request.isExpired() - {request.isExpired()}");
            Debug.Log($"onRewardedRequestSuccess - request.isDestroyed() - {request.isDestroyed()}");
        }

        if (!string.IsNullOrEmpty(auctionResult))
        {
            Debug.Log($"RewardedRequestListener - onRewardedRequestSuccess" +
                      $"auctionResult - {auctionResult}");
        }
        else
        {
            Debug.Log("auctionResult - IsNullOrEmpty");
        }
    }

    public void onRewardedRequestFailed(RewardedRequest request, BMError error)
    {
        Debug.Log($"RewardedRequestListener - onRewardedRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onRewardedRequestExpired(RewardedRequest request)
    {
        Debug.Log($"RewardedRequestListener - onRewardedRequestExpired");
    }

    #endregion

    #region NativeRequestListener

    public void onNativeRequestSuccess(NativeRequest request, string auctionResult)
    {
        if (request != null)
        {
            Debug.Log($"onNativeRequestSuccess - request.getAuctionResult() - {request.getAuctionResult()}");
            Debug.Log($"onNativeRequestSuccess - request.isExpired() - {request.isExpired()}");
            Debug.Log($"onNativeRequestSuccess - request.isDestroyed() - {request.isDestroyed()}");
        }

        if (!string.IsNullOrEmpty(auctionResult))
        {
            Debug.Log($"NativeRequestListener - onNativeRequestSuccess" +
                      $"auctionResult - {auctionResult}");
        }
        else
        {
            Debug.Log("auctionResult - IsNullOrEmpty");
        }
    }

    public void onNativeRequestFailed(NativeRequest request, BMError error)
    {
        Debug.Log($"NativeRequestListener - onNativeRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onNativeRequestExpired(NativeRequest request)
    {
        Debug.Log($"NativeRequestListener - onNativeRequestExpired");
    }

    #endregion

    #region NativeAdListener

    public void onNativeAdLoaded(NativeAd ad)
    {
        Debug.Log($"onNativeAdLoaded - ad.getTitle() - {ad.getTitle()}");
        Debug.Log($"onNativeAdLoaded - ad.getDescription() - {ad.getDescription()}");
        Debug.Log($"onNativeAdLoaded - ad.getRating() - {ad.getRating().ToString("0.0000")}");
        Debug.Log($"onNativeAdLoaded - ad.getCallToAction() - {ad.getCallToAction()}");

        if (nativeAdView)
        {
            nativeAdView.setNativeAd(ad);
        }
    }

    public void onNativeAdLoadFailed(NativeAd ad, BMError error)
    {
        Debug.Log($"onNativeAdLoadFailed - {error.message} - {error.code} ");
    }

    public void onNativeAdShown(NativeAd ad)
    {
        Debug.Log("onNativeAdShown");
    }

    public void onNativeAdImpression(NativeAd ad)
    {
        Debug.Log("onNativeAdImpression");
    }

    public void onNativeAdClicked(NativeAd ad)
    {
        Debug.Log("onNativeAdClicked");
    }

    public void onNativeAdExpired(NativeAd ad)
    {
        Debug.Log("onNativeAdExpired");
    }

    #endregion
}