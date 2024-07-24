using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

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
        void setGPP(string gppString, int[] gppIds);
        void setPublisher(Publisher publisher);
        bool checkAndroidPermissions(string permission);
        void requestAndroidPermissions();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ITargetingParams
    {
        void setUserId(string id);
        void setGender(TargetingParams.Gender gender);
        void setBirthdayYear(int year);
        void setKeywords(string[] keyWords);
        void setDeviceLocation(string providerName, double latitude, double longitude);
        void setCountry(string country);
        void setCity(string city);
        void setZip(string zip);
        void setStoreUrl(string storeUrl);
        void setStoreCategory(string storeCategory);
        void setStoreSubCategories(string[] storeSubCategories);
        void setStoreId(string storeId);
        void setFramework(string framework);
        void setPaid(bool paid);
        void setExternalUserIds(ExternalUserId[] externalUserIds);
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
    public interface ICustomParams
    {
        void addParam(string key, string value);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ISessionAdParams
    {
        void setSessionDuration(int value);
        void setImpressionCount(int value);
        void setClickRate(float value);
        void setIsUserClickedOnLastAd(bool value);
        void setCompletionRate(float value);
        void setLastBundle(string value);
        void setLastAdDomain(string value);
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
        void setNetworks(string jsonNetworksData);
        void setCustomParams(CustomParams customParams);
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
        void setNetworks(string jsonNetworksData);
        void setCustomParams(CustomParams customParams);
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
        void setNetworks(string jsonNetworksData);
        void setCustomParams(CustomParams customParams);
        IRewardedRequest build();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface INativeRequestBuilder
    {
        void setMediaAssetTypes(NativeAdParams nativeAdParams);
        void setTargetingParams(TargetingParams targetingParams);
        void setPriceFloorParams(PriceFloorParams priceFloorParameters);
        void setSessionAdParams(SessionAdParams sessionAdParams);
        void setLoadingTimeOut(int value);
        void setPlacementId(string placementId);
        void setBidPayload(string bidPayLoad);
        void setNetworks(string jsonNetworksData);
        void setCustomParams(CustomParams customParams);
        void setListener(INativeRequestListener nativeRequestListener);
        INativeRequest build();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBannerRequest
    {
        BannerSize? getSize();
        string getAuctionResult();
        bool isDestroyed();
        bool isExpired();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialRequest
    {
        string getAuctionResult();
        bool isDestroyed();
        bool isExpired();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedRequest
    {
        string getAuctionResult();
        bool isDestroyed();
        bool isExpired();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface INativeRequest
    {
        string getAuctionResult();
        bool isDestroyed();
        bool isExpired();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBannerView
    {
        bool showBannerView(int YAxis, int XAxis, BannerView bannerView, BannerSize bannerSize);
        void hideBannerView();
        void setListener(IBannerListener bannerViewListener);
        void load(BannerRequest bannerRequest);
        void destroy();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialAd
    {
        bool canShow();
        void show();
        void destroy();
        void setListener(IInterstitialAdListener interstitialAdListener);
        void load(InterstitialRequest interstitialRequest);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedAd
    {
        bool canShow();
        void show();
        void destroy();
        void setListener(IRewardedAdListener rewardedAdListener);
        void load(RewardedRequest rewardedRequest);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface INativeAd
    {
        string getTitle();
        string getDescription();
        string getCallToAction();
        string getImage(NativeAd nativeAd);
        string getIcon(NativeAd nativeAd);
        float getRating();
        bool canShow();
        void load(NativeRequest nativeRequest);
        void destroy();
        void setListener(INativeAdListener nativeAdListener);
        void dispatchClick(NativeAd nativeAd);
        void dispatchImpression(NativeAd nativeAd);
    }
}