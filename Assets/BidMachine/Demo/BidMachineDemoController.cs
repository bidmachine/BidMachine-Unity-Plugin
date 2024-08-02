using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

#pragma warning disable 649

public class BidMachineDemoController : MonoBehaviour
{
    [SerializeField]
    public Toggle tgTesting;

    [SerializeField]
    public Toggle tgLogging;

    private TargetingParams targetingParams;
    private PriceFloorParams priceFloorParams;
    private SessionAdParams sessionAdParams;

    // Banner Ad fields
    private BannerAd bannerAd;
    private IBannerListener bannerListener = new BannerListener();
    private IBannerRequest bannerRequest;
    private BannerRequest.Builder bannerRequestBuilder;
    private BannerRequestListener bannerRequestListener = new BannerRequestListener();

    // Interstitial Ad fields
    private InterstitialAd interstitialAd;
    private IInterstitialListener interstitialListener = new InterstitialListener();
    private IInterstitialRequest interstitialRequest;
    private InterstitialRequest.Builder interstitialRequestBuilder;
    private InterstitialRequestListener interstitialRequestListener =
        new InterstitialRequestListener();

    // Existing Rewarded Ad fields
    private RewardedAd rewardedAd;
    private IRewardedlListener rewardedListener = new RewardedListener();
    private IRewardedRequest rewardedRequest;
    private RewardedRequest.Builder rewardedRequestBuilder;
    private RewardedRequestListener rewardedRequestListener = new RewardedRequestListener();

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;

        priceFloorParams = new PriceFloorParams();
        priceFloorParams.AddPriceFloor("123", 1.2d);

        sessionAdParams = new SessionAdParams { SessionDuration = 123 };
    }

    public void BidMachineInitialize()
    {
        BidMachine.SetTargetingParams(
            new TargetingParams
            {
                UserId = "user123",
                gender = TargetingParams.Gender.Female,
                BirthdayYear = 1990,
                Keywords = new string[] { "gaming", "sports", "technology" },
                DeviceLocation = new TargetingParams.Location
                {
                    Provider = "GPS",
                    Latitude = 43.9006,
                    Longitude = 27.5590
                },
                Country = "Ireland",
                City = "Dublin",
                Zip = "D22 YD82",
                StoreUrl = "https://play.google.com/store/apps/details?id=com.example.app",
                StoreCategory = "Games",
                StoreSubCategories = new string[] { "Puzzle", "Strategy" },
                Framework = "Unity",
                IsPaid = true,
                externalUserIds = new ExternalUserId[]
                {
                    new() { SourceId = "adnetwork1", Value = "abc123" },
                    new() { SourceId = "adnetwork2", Value = "xyz789" }
                },
                BlockedDomains = new HashSet<string> { "example.com", "spam.com" },
                BlockedCategories = new HashSet<string> { "IAB-26", "IAB-25" },
                BlockedApplications = new HashSet<string>
                {
                    "com.unwanted.app1",
                    "com.unwanted.app2"
                }
            }
        );
        BidMachine.SetPublisher(
            new Publisher
            {
                Id = "1",
                Name = "AdTest",
                Domain = "us",
                Categories = new[] { "sports", "technology" }
            }
        );
        // BidMachine.SetEndpoint("https://test.com");
        BidMachine.SetSubjectToGDPR(true);
        BidMachine.SetCoppa(true);
        BidMachine.SetConsentConfig(true, "test consent string");
        BidMachine.SetUSPrivacyString("test_string");
        BidMachine.CheckAndroidPermissions(Permission.CoarseLocation);
        BidMachine.SetLoggingEnabled(tgLogging.isOn);
        BidMachine.SetTestMode(tgTesting.isOn);
        BidMachine.Initialize("122");
    }

    public void IsInitialized()
    {
        Debug.Log($"isInitialized - {BidMachine.IsInitialized()}");
    }

    public void CheckPermissions()
    {
        Debug.Log(
            $"Permission.CoarseLocation - {BidMachine.CheckAndroidPermissions(Permission.CoarseLocation)}"
        );
        Debug.Log(
            $"Permission.FineLocation - {BidMachine.CheckAndroidPermissions(Permission.FineLocation)}"
        );
    }

    public void RequestPermissions()
    {
        BidMachine.RequestAndroidPermissions();
    }

    #region Banner & MREC Ads

    public void LoadBanner()
    {
        LoadBanner(BannerSize.Size_320x50);
    }

    public void LoadMrec()
    {
        LoadBanner(BannerSize.Size_300x250);
    }

    public void LoadBanner(BannerSize bannerSize)
    {
        if (bannerRequest == null)
        {
            bannerRequest = new BannerRequest.Builder()
            // .setSize(bannerSize)
            // .setTargetingParams(targetingParams)
            // .setPriceFloorParams(priceFloorParams)
            // .setSessionAdParams(sessionAdParams)
            // .setPlacementId("placement_bannerRequest")
            // .setLoadingTimeOut(123)
            // .setBidPayload("123")
            // .setNetworks("admob")
            // .setListener(bannerListener)
            .Build();

            if (bannerAd == null)
            {
                bannerAd = new BannerAd();
                bannerAd.SetListener(bannerListener);
                bannerAd.Load(bannerRequest);
            }
        }
    }

    public void ShowBannerView()
    {
        bannerAd?.Show(
            BidMachine.BANNER_VERTICAL_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerAd,
            bannerRequest.GetSize()
        );
    }

    public void DestroyBanner()
    {
        if (bannerAd != null)
        {
            bannerAd.Destroy();
            bannerAd = null;
            bannerRequest = null;
        }
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        if (interstitialRequest == null)
        {
            interstitialRequest = new InterstitialRequest.Builder()
            // .setAdContentType(AdContentType.All)
            // .setTargetingParams(targetingParams)
            // .setPriceFloorParams(priceParam)
            // .setSessionAdParams(sessionAdParams)
            // .setPlacementId("placement_interstitialRequest")
            // .setLoadingTimeOut(30000)
            // .setBidPayload("123")
            // .setNetworks("admob")
            // .setListener(this)
            .Build();

            if (interstitialAd == null)
            {
                interstitialAd = new InterstitialAd();
                interstitialAd.SetListener(interstitialListener);
                interstitialAd.Load(interstitialRequest);
            }
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null)
        {
            if (interstitialAd.CanShow())
            {
                interstitialAd.Show();
            }
        }
    }

    public void DestroyInterstitial()
    {
        if (interstitialAd == null)
            return;
        {
            interstitialAd.Destroy();
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
            rewardedRequest = new RewardedRequest.Builder()
            // .setTargetingParams(targetingParams)
            // .setPriceFloorParams(priceFloorParams)
            // .setSessionAdParams(sessionAdParams)
            // .setPlacementId("placement_rewardedRequest")
            // .setLoadingTimeOut(123)
            // .setBidPayload("123")
            // .setNetworks("admob")
            // .setListener(this)
            .Build();

            if (rewardedAd == null)
            {
                rewardedAd = new RewardedAd();
                rewardedAd.SetListener(rewardedListener);
                rewardedAd.Load(rewardedRequest);
            }
        }
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            if (rewardedAd.CanShow())
            {
                rewardedAd.Show();
            }
        }
    }

    public void DestroyRewardedVideo()
    {
        if (rewardedAd == null)
            return;
        rewardedAd.Destroy();
        rewardedAd = null;
        rewardedRequest = null;
    }

    #endregion

    private class BannerListener : IBannerListener
    {
        public void OnAdExpired(IBannerAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdImpression(IBannerAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoaded(IBannerAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoadFailed(IBannerAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShowFailed(IBannerAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShown(IBannerAd ad)
        {
            throw new System.NotImplementedException();
        }
    }

    private class BannerRequestListener : IAdRequestListener<BannerRequest, string, BMError>
    {
        public void OnAdRequestExpired(BannerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestFailed(BannerRequest request, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestSuccess(BannerRequest request, string auctionResult)
        {
            throw new System.NotImplementedException();
        }
    }

    private class InterstitialListener : IInterstitialListener
    {
        public void OnAdClosed(IInterstitialAd ad, bool finished)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdExpired(IInterstitialAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdImpression(IInterstitialAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoaded(IInterstitialAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoadFailed(IInterstitialAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShowFailed(IInterstitialAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShown(IInterstitialAd ad)
        {
            throw new System.NotImplementedException();
        }
    }

    private class InterstitialRequestListener
        : IAdRequestListener<InterstitialRequest, string, BMError>
    {
        public void OnAdRequestExpired(InterstitialRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestFailed(InterstitialRequest request, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestSuccess(InterstitialRequest request, string auctionResult)
        {
            throw new System.NotImplementedException();
        }
    }

    private class RewardedListener : IRewardedlListener
    {
        public void OnAdClosed(IRewardedAd ad, bool finished)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdExpired(IRewardedAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdImpression(IRewardedAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoaded(IRewardedAd ad)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdLoadFailed(IRewardedAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShowFailed(IRewardedAd ad, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdShown(IRewardedAd ad)
        {
            throw new System.NotImplementedException();
        }
    }

    private class RewardedRequestListener : IAdRequestListener<RewardedRequest, string, BMError>
    {
        public void OnAdRequestExpired(RewardedRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestFailed(RewardedRequest request, BMError error)
        {
            throw new System.NotImplementedException();
        }

        public void OnAdRequestSuccess(RewardedRequest request, string auctionResult)
        {
            throw new System.NotImplementedException();
        }
    }
}
