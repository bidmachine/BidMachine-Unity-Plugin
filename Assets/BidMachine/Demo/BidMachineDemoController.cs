using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;
using UnityEngine.UI;

#pragma warning disable 649

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "InvertIf")]
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

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;

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

        BidMachine.setPublisher(new Publisher("1", "Gena", "ua", new[] { "games, cards" }));
        //BidMachine.setEndpoint("https://test.com");
        BidMachine.setSubjectToGDPR(true);
        //BidMachine.setCoppa(true);
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
        if (bannerRequest == null)
        {
            bannerRequest = new BannerRequestBuilder()
                .setSize(BannerSize.Size_320х50)
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                .setPlacementId("placement_bannerRequest")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();

            if (bannerView == null)
            {
                bannerView = new BannerView();
                bannerView.setListener(this);
                bannerView.load(bannerRequest);
            }
        }
    }

    public void ShowBannerView()
    {
        bannerView?.showBannerView(
            BidMachine.BANNER_VERTICAL_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerView, bannerRequest.getSize());
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.hideBannerView();
            bannerView.destroy();
            bannerView = null;
            bannerRequest = null;
        }
    }

    #endregion

    #region Native Ad

    public void LoadNativeAd()
    {
        var nativeAdParams = new NativeAdParams();
        nativeAdParams.setMediaAssetTypes(NativeAdParams.MediaAssetType.Icon, NativeAdParams.MediaAssetType.Image);

        if (nativeRequest == null)
        {
            nativeRequest = new NativeRequestBuilder()
                .setMediaAssetTypes(nativeAdParams)
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                .setPlacementId("placement_nativeRequest")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();

            if (nativeAd == null)
            {
                nativeAd = new NativeAd();
                nativeAd.setListener(this);
                if (nativeRequest != null)
                {
                    nativeAd.load(nativeRequest);
                }
            }
        }
    }

    public void DestroyNativeAd()
    {
        nativeAd.destroy();
        nativeAdView.destroyNativeView();
        nativeAd = null;
        nativeRequest = null;
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        if (interstitialRequest == null)
        {
            interstitialRequest = new InterstitialRequestBuilder()
                .setAdContentType(AdContentType.All)
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                .setPlacementId("placement_interstitialRequest")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();

            if (interstitialAd == null)
            {
                interstitialAd = new InterstitialAd();
                interstitialAd.setListener(this);
                interstitialAd.load(interstitialRequest);
            }
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null)
        {
            if (interstitialAd.canShow())
            {
                interstitialAd.show();
            }
        }
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

    #endregion

    #region Rewarded Video Ad

    public void LoadRewardedAd()
    {
        if (rewardedRequest == null)
        {
            rewardedRequest = new RewardedRequestBuilder()
                // .setTargetingParams(targetingParams)
                // .setPriceFloorParams(priceFloorParams)
                // .setSessionAdParams(sessionAdParams)
                .setPlacementId("placement_rewardedRequest")
                // .setLoadingTimeOut(123)
                // .setBidPayload("123")
                // .setNetworks("admob")
                .setListener(this)
                .build();

            if (rewardedAd == null)
            {
                rewardedAd = new RewardedAd();
                rewardedAd.setListener(this);
                rewardedAd.load(rewardedRequest);
            }
        }
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            if (rewardedAd.canShow())
            {
                rewardedAd.show();
            }
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
        Debug.Log("BidMachineUnity: onInterstitialAdLoaded");
    }

    public void onInterstitialAdLoadFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdLoadFailed");
    }

    public void onInterstitialAdShown(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdShown");
    }

    public void onInterstitialAdImpression(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdImpression");
    }

    public void onInterstitialAdExpired(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdExpired");
    }

    public void onInterstitialAdShowFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdShowFailed");
    }

    public void onInterstitialAdClosed(InterstitialAd ad, bool finished)
    {
        Debug.Log($"BidMachineUnity: onInterstitialAdClosed - finished: {finished}");
    }

    public void onInterstitialAdClicked(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: onInterstitialAdClicked");
    }

    #endregion

    #region RewardedAd Callbacks

    public void onRewardedAdLoaded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdLoaded");
    }

    public void onRewardedAdLoadFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: onRewardedAdLoadFailed");
    }

    public void onRewardedAdShown(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdShown");
    }

    public void onRewardedAdImpression(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdImpression");
    }

    public void onRewardedAdClicked(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdClicked");
    }

    public void onRewardedAdExpired(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdExpired");
    }

    public void onRewardedAdShowFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: onRewardedAdShowFailed");
    }

    public void onRewardedAdClosed(RewardedAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: onRewardedAdClosed - finished: " + finished);
    }

    public void onRewardedAdRewarded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdRewarded");
    }

    #endregion

    #region NativeAd Callbacks

    public void onNativeAdLoaded(NativeAd ad)
    {
        Debug.Log($"BidMachineUnity: onNativeAdLoaded " +
                  $" title - {ad.getTitle()}" +
                  $" description - {ad.getDescription()}" +
                  $" rating - {ad.getRating():0.0000}" +
                  $" callToAction - {ad.getCallToAction()}" +
                  $" icon - {ad.getIcon(ad)}" +
                  $" image - {ad.getImage(ad)}");

        if (nativeAdView)
        {
            nativeAdView.setNativeAd(ad);
        }
    }

    public void onNativeAdLoadFailed(NativeAd ad, BMError error)
    {
        Debug.Log($"BidMachineUnity: onNativeAdLoadFailed - {error.message} - {error.code} ");
    }

    public void onNativeAdShown(NativeAd ad)
    {
        Debug.Log("BidMachineUnity: onNativeAdShown");
    }

    public void onNativeAdImpression(NativeAd ad)
    {
        Debug.Log("BidMachineUnity: onNativeAdImpression");
    }

    public void onNativeAdClicked(NativeAd ad)
    {
        Debug.Log("BidMachineUnity: onNativeAdClicked");
    }

    public void onNativeAdExpired(NativeAd ad)
    {
        Debug.Log("BidMachineUnity: onNativeAdExpired");
    }

    #endregion

    #region BannerView Callbacks

    public void onBannerAdLoaded(BannerView ad)
    {
        Debug.Log("BidMachineUnity: onBannerAdLoaded");
    }

    public void onBannerAdLoadFailed(BannerView ad, BMError error)
    {
        Debug.Log("BidMachineUnity: onBannerAdLoadFailed");
    }

    public void onBannerAdShown(BannerView ad)
    {
        Debug.Log("BidMachineUnity: onBannerAdShown");
    }

    public void onBannerAdImpression(BannerView ad)
    {
        Debug.Log("BidMachineUnity: onBannerAdImpression");
    }

    public void onBannerAdClicked(BannerView ad)
    {
        Debug.Log("BidMachineUnity: onBannerAdClicked");
    }

    public void onBannerAdExpired(BannerView ad)
    {
        Debug.Log("BidMachineUnity: onBannerAdExpired");
    }

    #endregion

    #region BannerRequest Callbacks

    public void onBannerRequestSuccess(BannerRequest request, string auctionResult)
    {
        Debug.Log("BidMachineUnity: onBannerRequestSuccess");
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
    }

    public void onBannerRequestFailed(BannerRequest request, BMError error)
    {
        Debug.Log("BidMachineUnity: onBannerRequestFailed");
        Debug.Log("BannerRequestListener - onBannerRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onBannerRequestExpired(BannerRequest request)
    {
        Debug.Log("BidMachineUnity: onBannerRequestExpired");
        Debug.Log("BannerRequestListener - onBannerRequestExpired");
    }

    #endregion

    #region InterstitialRequest Callbacks

    public void onInterstitialRequestSuccess(InterstitialRequest request, string auctionResult)
    {
        Debug.Log("BidMachineUnity: onInterstitialRequestSuccess");

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
        Debug.Log("BidMachineUnity: onInterstitialRequestFailed");
        Debug.Log($"InterstitialRequestListener - onInterstitialRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onInterstitialRequestExpired(InterstitialRequest request)
    {
        Debug.Log("BidMachineUnity: onInterstitialRequestExpired");
        Debug.Log($"InterstitialRequestListener - onInterstitialRequestExpired");
    }

    #endregion

    #region RewardedRequest Callbacks

    public void onRewardedRequestSuccess(RewardedRequest request, string auctionResult)
    {
        Debug.Log("BidMachineUnity: onRewardedRequestSuccess");

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
        Debug.Log("BidMachineUnity: onRewardedRequestFailed");
        Debug.Log($"RewardedRequestListener - onRewardedRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onRewardedRequestExpired(RewardedRequest request)
    {
        Debug.Log("BidMachineUnity: onRewardedRequestExpired");
        Debug.Log($"RewardedRequestListener - onRewardedRequestExpired");
    }

    #endregion

    #region NativeRequest Callbacks

    public void onNativeRequestSuccess(NativeRequest request, string auctionResult)
    {
        Debug.Log("BidMachineUnity: onNativeRequestSuccess");

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
        Debug.Log("BidMachineUnity: onNativeRequestFailed");
        Debug.Log($"NativeRequestListener - onNativeRequestFailed" +
                  $"BMError - {error.code} - {error.message}");
    }

    public void onNativeRequestExpired(NativeRequest request)
    {
        Debug.Log("BidMachineUnity: onNativeRequestExpired");
        Debug.Log($"NativeRequestListener - onNativeRequestExpired");
    }

    #endregion
}