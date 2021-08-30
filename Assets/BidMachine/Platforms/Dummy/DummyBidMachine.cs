using System.Collections.Generic;
using BidMachineAds.Unity.Android;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Dummy
{
    public class DummyBidMachine : IBidMachine, ITargetingParams, IPriceFloorParams, IInterstitialRequestBuilder, IInterstitialAd, IInterstitialRequest,
        IRewardedRequestBuilder, IRewardedRequest, IRewardedAd, IBannerRequestBuilder, IBannerRequest, IBannerView, IBanner, IExternalUserId
    {
       
        public void build(InterstitialRequestBuilder interstitialRequestBuilder)
        {
            Debug.Log("Not supported on this platform");
        }

        public void build(RewardedRequestBuilder rewardedRequestBuilder)
        {
            Debug.Log("Not supported on this platform");
        }

        public void build(BannerRequestBuilder bannerRequestBuilder)
        {
            Debug.Log("Not supported on this platform");
        }

        public void build()
        {
            Debug.Log("Not supported on this platform");
        }

        public bool canShow()
        {
            Debug.Log("Not supported on this platform");
            return false;
        }

        public void setUSPrivacyString(string usPrivacyString)
        {
            throw new System.NotImplementedException();
        }

        public bool checkAndroidPermissions(string permission)
        {
            Debug.Log("Not supported on this platform");
            return false;
        }

        public void destroy()
        {
            Debug.Log("Not supported on this platform");
        }

        public IBannerView getBannerView()
        {
            Debug.Log("Not supported on this platform");
            return null;
        }

        public void hideBannerView()
        {
            Debug.Log("Not supported on this platform");
        }

        public void initialize(string sallerId)
        {
            Debug.Log("Not supported on this platform");
        }

        public bool isInitialized()
        {
            return false;
        }

        public void setEndpoint(string url)
        {
            throw new System.NotImplementedException();
        }

        public void load(InterstitialRequest interstitialRequest)
        {
            Debug.Log("Not supported on this platform");
        }

        public void load(RewardedRequest rewardedRequest)
        {
            Debug.Log("Not supported on this platform");
        }

        public void load(BannerRequest bannerRequest)
        {
            Debug.Log("Not supported on this platform");
        }

        public void requestAndroidPermissions()
        {
            Debug.Log("Not supported on this platform");
        }

        public void setAdContentType(InterstitialRequestBuilder.ContentType contentType)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setBirthdayYear(int year)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setBlockedAdvertiserDomain(string domains)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            throw new System.NotImplementedException();
        }

        public void setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            throw new System.NotImplementedException();
        }

        public void setExternalUserIds(List<ExternalUserId> externalUserIdList)
        {
            throw new System.NotImplementedException();
        }

        public void addBlockedApplication(string bundleOrPackage)
        {
            throw new System.NotImplementedException();
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            throw new System.NotImplementedException();
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            throw new System.NotImplementedException();
        }

        public void setBlockedAdvertiserIABCategories(string categories)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setBlockedApplication(string applications)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setCity(string city)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setConsentConfig(bool consent, string consentConfig)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setCoppa(bool coppa)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setCountry(string country)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setDeviceLocation(double latitude, double longitude)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setGender(TargetingParams.Gender gender)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setKeyWords(string[] keyWords)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setListener(AndroidInterstitialAdListener listener)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setListener(IInterstitialAdListener listener)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setListener(IBannerListener bannerViewListener)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setLoggingEnabled(bool logging)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setFramework(string framework)
        {
            throw new System.NotImplementedException();
        }

        public void setPaid(bool paid)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setPriceFloor(double priceFloor)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setPriceFloor(string uniqueFloorId, double priceFloor)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setSize(BannerRequestBuilder.Size size)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setStoreUrl(string storeUrl)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setStoreCategory(string storeCategory)
        {
            throw new System.NotImplementedException();
        }

        public void setStoreSubCategories(string storeSubCategories)
        {
            throw new System.NotImplementedException();
        }

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setTestMode(bool test)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setUserId(string id)
        {
            Debug.Log("Not supported on this platform");
        }

        public void setZip(string zip)
        {
            Debug.Log("Not supported on this platform");
        }

        public void show()
        {
            Debug.Log("Not supported on this platform");
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            Debug.Log("Not supported on this platform");
        }

        IInterstitialRequest IInterstitialRequestBuilder.build()
        {
            Debug.Log("Not supported on this platform");
            return null;
        }

        IBannerRequest IBannerRequestBuilder.build()
        {
            Debug.Log("Not supported on this platform");
            return null;
        }

        IRewardedRequest IRewardedRequestBuilder.build()
        {
            Debug.Log("Not supported on this platform");
            return null;
        }
    }
}
