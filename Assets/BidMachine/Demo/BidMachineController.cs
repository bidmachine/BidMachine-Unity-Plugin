using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

#pragma warning disable 649

public class BidMachineController : MonoBehaviour
{
    public Toggle tgTesting;

    public Toggle tgLogging;

    private TargetingParams targetingParams;
    private PriceFloorParams priceFloorParams;
    private CustomParams customParams;

    private BannerView bannerView;
    private readonly IAdListener<IBannerView> bannerListener = new BannerListener();
    private IAdRequest bannerRequest;
    private readonly IAdAuctionRequestListener bannerRequestListener = new BannerRequestListener();

    private InterstitialAd interstitialAd;
    private readonly IInterstitialAdListener interstitialListener =
        new InterstitialListener();
    private IAdRequest interstitialRequest;
    private readonly IAdAuctionRequestListener interstitialRequestListener =
        new InterstitialRequestListener();

    private RewardedAd rewardedAd;
    private readonly IRewardedAdListener rewardedListener = new RewardedAdListener();
    private IAdRequest rewardedRequest;
    private readonly IAdAuctionRequestListener rewardedRequestListener = new RewardedRequestListener();

    private void Start()
    {
#if UNITY_IOS
        RequestATT();
#endif
        tgTesting.isOn = true;
        tgLogging.isOn = true;

        priceFloorParams = new PriceFloorParams();
        priceFloorParams.AddPriceFloor(Guid.NewGuid().ToString(), 0.01);

        customParams = new CustomParams();
        customParams.AddParam("mediation_mode", "pdb_is");

        targetingParams = new TargetingParams
        {
            UserId = "UserId",
            gender = TargetingParams.Gender.Female,
            BirthdayYear = 1990,
            Keywords = new string[] { "keyword1", "keyword1" },
            DeviceLocation = new TargetingParams.Location
            {
                Provider = "GPS",
                Latitude = 43.9006,
                Longitude = 27.5590
            },
            Country = "Country",
            City = "City",
            Zip = "zip_code",
            StoreUrl = "https://play.google.com/store/apps/details?id=com.example.app",
            StoreCategory = "Category",
            StoreSubCategories = new string[] { "SubCategory1", "SubCategory2" },
            Framework = "unity",
            IsPaid = true,
            externalUserIds = new ExternalUserId[]
            {
                new() { SourceId = "ad_network_1", Value = "value_1" },
                new() { SourceId = "ad_network_2", Value = "value_2" }
            },
            BlockedDomains = new HashSet<string> { "domain1.com", "domain2.com" },
            BlockedCategories = new HashSet<string> { "category1", "category2" },
            BlockedApplications = new HashSet<string> { "com.unwanted.app1", "com.unwanted.app2" }
        };
    }

    public void RequestATT()
    {
#if UNITY_IOS
        int currentStatus = BidMachineGetATTrackingStatus();
        Debug.Log($"BidMachine: Current ATT status: {(ATTrackingStatus)currentStatus}");

        if (currentStatus == (int)ATTrackingStatus.NotDetermined)
        {
            Debug.Log("BidMachine: Requesting ATT permission...");
            StartCoroutine(RequestATTPermission());
        }
        else
        {
            Debug.Log($"BidMachine: ATT status already determined: {(ATTrackingStatus)currentStatus}");
        }
#else
        Debug.Log("BidMachine: ATT is only required on iOS");
#endif
    }

#if UNITY_IOS
    private IEnumerator RequestATTPermission()
    {
        BidMachineRequestATTracking();

        // Wait a frame for the request to be processed
        yield return new WaitForSeconds(0.1f);

        // Check the status after request
        int status = BidMachineGetATTrackingStatus();
        Debug.Log($"BidMachine: ATT request completed with status: {(ATTrackingStatus)status}");

        switch ((ATTrackingStatus)status)
        {
            case ATTrackingStatus.Authorized:
                Debug.Log("BidMachine: User authorized tracking");
                break;
            case ATTrackingStatus.Denied:
                Debug.Log("BidMachine: User denied tracking");
                break;
            case ATTrackingStatus.Restricted:
                Debug.Log("BidMachine: Tracking is restricted");
                break;
            case ATTrackingStatus.NotDetermined:
                Debug.Log("BidMachine: Tracking status not determined");
                break;
        }
    }

    private enum ATTrackingStatus
    {
        NotDetermined = 0,
        Restricted = 1,
        Denied = 2,
        Authorized = 3
    }

    [DllImport("__Internal")]
    private static extern int BidMachineGetATTrackingStatus();

    [DllImport("__Internal")]
    private static extern void BidMachineRequestATTracking();
#endif

    public void BidMachineInitialize()
    {
        // BidMachine.SetTargetingParams(targetingParams);
        // BidMachine.SetPublisher(
        //     new Publisher
        //     {
        //         Id = "1",
        //         Name = "AdTest",
        //         Domain = "us",
        //         Categories = new[] { "sports", "technology" }
        //     }
        // );
        // BidMachine.SetEndpoint("https://test.com");
        BidMachine.SetSubjectToGDPR(false);
        BidMachine.SetCoppa(false);
        BidMachine.SetConsentConfig(true, "test consent string");
        BidMachine.SetUSPrivacyString("test_string");
        BidMachine.SetLoggingEnabled(tgLogging.isOn);
        BidMachine.SetTestMode(tgTesting.isOn);
        BidMachine.Initialize("122");
    }

    public void IsInitialized()
    {
        Debug.Log($"isInitialized: {BidMachine.IsInitialized()}");
    }

    #region Banner & MREC Ads

    public void LoadBanner()
    {
        LoadBanner(BannerAdSize.Banner);
    }

    public void LoadMrec()
    {
        LoadBanner(BannerAdSize.MediumRectangle);
    }

    public void LoadBanner(BannerAdSize bannerAdSize)
    {
        // Destroy any existing banner and request before creating new one
        DestroyBanner();

        var config = AdPlacementConfig.BannerBuilder(bannerAdSize)
            // .WithPlacementId("placement_bannerRequest")
            // .WithCustomParams(customParams)
            .Build();

        bannerRequest = new BannerRequest.Builder(config)
            .SetPriceFloorParams(priceFloorParams)
            // .SetTargetingParams(targetingParams)
            // .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(bannerRequestListener)
            .Build();

        bannerView = new BannerView();
        bannerView.SetListener(bannerListener);
        bannerView.Load(bannerRequest);
    }

    public void ShowBannerView()
    {
        if (bannerView == null || bannerRequest == null)
        {
            Debug.Log("BidMachine: Cannot show banner - banner or request is null");
            return;
        }

        if (!bannerView.CanShow())
        {
            Debug.Log("BidMachine: Cannot show banner - banner is not ready");
            return;
        }

        // Hide first to remove from parent view if already shown
        bannerView.Hide();

        var size = ((IBannerRequest)bannerRequest).GetBannerAdSize();
        bannerView.Show(
            BidMachine.BANNER_VERTICAL_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerView,
            size
        );
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            // Hide first to remove from view hierarchy
            bannerView.Hide();
            bannerView.SetListener(null);
            bannerView.Destroy();
            bannerView = null;
        }

        bannerRequest = null;
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        // Destroy any existing interstitial and request before creating new one
        DestroyInterstitial();

        var config = AdPlacementConfig.InterstitialBuilder(AdContentType.All)
            // .WithPlacementId("placement_interstitialRequest")
            // .WithCustomParams(customParams)
            .Build();

        interstitialRequest = new InterstitialRequest.Builder(config)
            // .SetPriceFloorParams(priceParam)
            // .SetTargetingParams(targetingParams)
            // .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(interstitialRequestListener)
            .Build();

        interstitialAd = new InterstitialAd();
        interstitialAd.SetListener(interstitialListener);
        interstitialAd.Load(interstitialRequest);
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd == null || !interstitialAd.CanShow())
        {
            return;
        }

        interstitialAd.Show();
    }

    public void DestroyInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.SetListener(null);
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        interstitialRequest = null;
    }

    #endregion

    #region Rewarded Video Ad

    public void LoadRewardedAd()
    {
        // Destroy any existing rewarded ad and request before creating new one
        DestroyRewardedVideo();

        var config = AdPlacementConfig.RewardedBuilder(AdContentType.All)
            // .WithPlacementId("placement_rewardedRequest")
            // .WithCustomParams(customParams)
            .Build();

        rewardedRequest = new RewardedRequest.Builder(config)
            // .SetPriceFloorParams(priceFloorParams)
            // .SetTargetingParams(targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(rewardedRequestListener)
            .Build();

        rewardedAd = new RewardedAd();
        rewardedAd.SetListener(rewardedListener);
        rewardedAd.Load(rewardedRequest);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd == null || !rewardedAd.CanShow())
        {
            return;
        }

        rewardedAd.Show();
    }

    public void DestroyRewardedVideo()
    {
        if (rewardedAd != null)
        {
            rewardedAd.SetListener(null);
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        rewardedRequest = null;
    }

    #endregion

    #region Listeners

    private class BannerListener : IAdListener<IBannerView>
    {
        public void onAdExpired(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerAd - OnAdExpired");
        }

        public void onAdImpression(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerAd - OnAdImpression");
        }

        public void onAdLoaded(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerView: OnAdLoaded");
        }

        public void onAdLoadFailed(IBannerView ad, BMError error)
        {
            Debug.Log($"BidMachine: BannerView: OnAdLoadFailed");
        }

        public void onAdShowFailed(IBannerView ad, BMError error)
        {
            Debug.Log($"BidMachine: BannerView: OnAdShowFailed");
        }

        public void onAdShown(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerView: OnAdShown");
        }
    }

    private class BannerRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: BannerRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: BannerRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: BannerRequest: OnAdRequestSuccess");
            Debug.Log("auctionResul----t" + auctionResult);
            Debug.Log("auctionResul----p" + auctionResult.Price);
            Debug.Log("auctionResul----st" + auctionResult.ToString());
        }
    }

    private class InterstitialListener : IInterstitialAdListener
    {
        public void onAdClosed(IInterstitialAd ad, bool finished)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdClosed");
        }

        public void onAdExpired(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdExpired");
        }

        public void onAdImpression(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdImpression");
        }

        public void onAdLoaded(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdLoaded");
        }

        public void onAdLoadFailed(IInterstitialAd ad, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IInterstitialAd ad, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdShowFailed");
        }

        public void onAdShown(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdShown");
        }
    }

    private class InterstitialRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: InterstitialRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: InterstitialRequest: OnAdRequestSuccess");
        }
    }

    private class RewardedAdListener : IRewardedAdListener
    {
        public void onAdClosed(IRewardedAd ad, bool finished)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdClosed");
        }

        public void onAdExpired(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdExpired");
        }

        public void onAdImpression(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdImpression");
        }

        public void onAdLoaded(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdLoaded");
        }

        public void onAdLoadFailed(IRewardedAd ad, BMError error)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IRewardedAd ad, BMError error)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdShowFailed");
        }

        public void onAdShown(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdShown");
        }

        public void onAdRewarded(IRewardedAd ad)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdRewarded");
        }
    }

    private class RewardedRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: RewardedRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: RewardedRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: RewardedRequest: OnAdRequestSuccess");
        }
    }

    #endregion
}
