using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Android;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBidMachine
    {
        void initialize(string sellerId);
        bool isInitialized();
        void setEndpoint(string url);
        void setLoggingEnabled(bool logging);
        void setTestMode(bool test);
        void setTargetingParams(TargetingParams targetingParams);
        void setConsentConfig(bool consent, string consentConfig);
        void setSubjectToGDPR(bool subjectToGDPR);
        void setCoppa(bool coppa);
        void setUSPrivacyString(string usPrivacyString);

        void setPublisher(Publisher publisher);

        // void registerNetworks(@ NetworkConfig... networkConfigs);
        // void registerNetworks(Context context, @NonNull String jsonData);
        // void registerAdRequestListener(@NonNull AdRequest.AdRequestListener adRequestListener);
        // void unregisterAdRequestListener(@NonNull AdRequest.AdRequestListener adRequestListener);
        bool checkAndroidPermissions(string permission);
        void requestAndroidPermissions();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ITargetingParams
    {
        void setUserId(string id);
        void setGender(TargetingParams.Gender gender);
        void setBirthdayYear(int year);
        void setKeyWords(string[] keyWords);
        void setCountry(string country);
        void setCity(string city);
        void setZip(string zip);
        void setStoreUrl(string storeUrl);
        void setStoreCategory(string storeCategory);
        void setStoreSubCategories(string[] storeSubCategories);
        void setStoreId(string storeId);
        void setFramework(string framework);
        void setPaid(bool paid);
        void setDeviceLocation(string providerName, double latitude, double longitude);
        void setExternalUserIds(ExternalUserId[] externalUserIdList);
        void addBlockedApplication(string bundleOrPackage);
        void addBlockedAdvertiserIABCategory(string category);
        void addBlockedAdvertiserDomain(string domain);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IPriceFloorParams
    {
        void addPriceFloor(double priceFloor);
        void addPriceFloor(string uniqueFloorId, double priceFloor);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ISessionAdParams
    {
        void setImpressionCount(int value);
        void setSessionDuration(int value);
        void setClickRate(float value);
        void setCompletionRate(float value);
        void setLastClickForImpression(int value);
        void setLastBundle(string value);
        void setLastAdomain(string value);
        void setIsUserClickedOnLastAd(bool value);
        
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBannerRequestBuilder
    {
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setSize(BannerSize size);
        void setListener(IBannerRequestListener bannerRequestListener);
        void setSessionAdParams(SessionAdParams sessionAdParams);
        void setLoadingTimeOut(int value);
        void setPlacementId(string placementId);
        void setBidPayload(string bidPayLoad);
        void setNetworks(string networks);

        IBannerRequest build();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialRequestBuilder
    {
        void setAdContentType(AdContentType contentType);
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setListener(IInterstitialRequestListener bannerRequestListener);
        void setSessionAdParams(SessionAdParams sessionAdParams);
        void setLoadingTimeOut(int value);
        void setPlacementId(string placementId);
        void setBidPayload(string bidPayLoad);
        void setNetworks(string networks);

        IInterstitialRequest build();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedRequestBuilder
    {
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setListener(IRewardedRequestListener rewardedRequestListener);
        void setSessionAdParams(SessionAdParams sessionAdParams);
        void setLoadingTimeOut(int value);
        void setPlacementId(string placementId);
        void setBidPayload(string bidPayLoad);
        void setNetworks(string networks);
        IRewardedRequest build();
    }

    public interface IBannerRequest
    {
        BannerSize getSize();
    }

    public interface IInterstitialRequest
    {
    }

    public interface IRewardedRequest
    {
    }

    public interface IBannerView
    {
        void setListener(IBannerListener bannerViewListener);
        void load(BannerRequest bannerRequest);
        void destroy();
    }

    public interface IBanner
    {
        void showBannerView(int YAxis, int XAxis, BannerView bannerView);
        void hideBannerView();
        IBannerView getBannerView();
    }

    public interface IInterstitialAd
    {
        bool canShow();
        void show();
        void destroy();
        void setListener(IInterstitialAdListener interstitialAdListener);
        void load(InterstitialRequest interstitialRequest);
    }

    public interface IRewardedAd
    {
        bool canShow();
        void show();
        void destroy();
        void setListener(IRewardedAdListener rewardedAdListener);
        void load(RewardedRequest rewardedRequest);
    }
}