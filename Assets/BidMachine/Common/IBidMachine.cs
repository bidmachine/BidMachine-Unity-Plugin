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
        void setSessionDuration(int value);
        void setImpressionCount(int value);
        void setClickRate(float value);
        void setIsUserClickedOnLastAd(bool value);
        void setCompletionRate(float value);
    }

    public interface IBannerRequestBuilder
    {
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setSize(BannerRequestBuilder.Size size);
        IBannerRequest build();
    }

    public interface IInterstitialRequestBuilder
    {
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setAdContentType(InterstitialRequestBuilder.ContentType contentType);
        IInterstitialRequest build();
    }

    public interface IRewardedRequestBuilder
    {
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        IRewardedRequest build();
    }

    public interface IBannerRequest
    {
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