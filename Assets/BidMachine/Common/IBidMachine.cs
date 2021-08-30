using BidMachineAds.Unity.Android;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Common
{
	public interface IBidMachine
	{
		void initialize(string sallerId);
		void setLoggingEnabled(bool logging);
		void setTestMode(bool test);
        void setTargetingParams(TargetingParams targetingParams);
        void setConsentConfig(bool consent, string consentConfig);
        void setSubjectToGDPR(bool subjectToGDPR);
        void setCoppa(bool coppa);
        bool checkAndroidPermissions(string permission);
        void requestAndroidPermissions();
    }

    public interface ITargetingParams
    {
        void setUserId(string id);
        void setGender(TargetingParams.Gender gender);
        void setBirthdayYear(int year);
        void setKeyWords(string[] keyWords);
        void setDeviceLocation(double latitude, double longitude);
        void setCountry(string country);
        void setCity(string city);
        void setZip(string zip);
        void setStoreUrl(string storeUrl);
        void setPaid(bool paid);
        void setBlockedAdvertiserIABCategories(string categories);
        void setBlockedAdvertiserDomain(string domains);
        void setBlockedApplication(string applications);
    }

    public interface IPriceFloorParams
    {
        void setPriceFloor(double priceFloor);
        void setPriceFloor(string uniqueFloorId, double priceFloor);
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

    public interface IBannerRequest{ }
    public interface IInterstitialRequest { }
    public interface IRewardedRequest { }

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
