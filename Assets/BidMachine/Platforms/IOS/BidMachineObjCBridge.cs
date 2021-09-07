#if UNITY_IPHONE
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BidMachineAds.Unity.Api;
using UnityEngine;


namespace BidMachineAds.Unity.iOS
{
    internal delegate void BannerRequestSuccessCallback(IntPtr ad, string auctionResult);

    internal delegate void BannerRequestFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void BannerRequestExpiredCallback(IntPtr ad);

    internal delegate void BidMachineInterstitialCallbacks(IntPtr ad);

    internal delegate void BidMachineInterstitialFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void BidMachineRewardedCallbacks(IntPtr ad);

    internal delegate void BidMachineRewardedFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void BidMachineBannerCallbacks(IntPtr ad);

    internal delegate void BidMachineBannerFailedCallback(IntPtr ad, IntPtr error);

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class BidMachineObjCBridge
    {
        [DllImport("__Internal")]
        internal static extern void BidMachineInitialize(string sellerId);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetLogging(bool logging);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetTestMode(bool testing);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetCoppa(bool coppa);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetGdprRequired(bool gdprRequired);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetConsentString(bool consent, string gdprConsentString);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetTargeting(IntPtr targetingParams);

        [DllImport("__Internal")]
        internal static extern int BidMachineGetErrorCode(IntPtr error);

        [DllImport("__Internal")]
        internal static extern string BidMachineGetErrorMessage(IntPtr error);

        [DllImport("__Internal")]
        internal static extern bool BidMachineIsInitialized();

        [DllImport("__Internal")]
        internal static extern void BidMachineSetEndpoint(string url);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetUSPrivacyString(string usPrivacyString);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetPublisher(string id, string name, string domain, string categories);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class SessionAdParamsObjcBridge
    {
        private readonly IntPtr nativeObject;

        public SessionAdParamsObjcBridge()
        {
            nativeObject = GetSessionAdParams();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public void setSessionDuration(int value)
        {
            SetSessionDuration(value);
        }

        public void setImpressionCount(int value)
        {
            SetImpressionCount(value);
        }

        public void setClickRate(int value)
        {
            SetClickRate(value);
        }

        public void setLastAdomain(string value)
        {
            SetLastAdomain(value);
        }

        public void setCompletionRate(int value)
        {
            SetCompletionRate(value);
        }

        public void setLastClickForImpression(int value)
        {
            SetLastClickForImpression(value);
        }

        public void setLastBundle(string value)
        {
            SetLastBundle(value);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetSessionAdParams();

        [DllImport("__Internal")]
        internal static extern void SetSessionDuration(int value);

        [DllImport("__Internal")]
        internal static extern void SetImpressionCount(int value);

        [DllImport("__Internal")]
        internal static extern void SetClickRate(int value);

        [DllImport("__Internal")]
        internal static extern void SetLastAdomain(string value);

        [DllImport("__Internal")]
        internal static extern void SetCompletionRate(int value);

        [DllImport("__Internal")]
        internal static extern void SetLastClickForImpression(int value);

        [DllImport("__Internal")]
        internal static extern void SetLastBundle(string value);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class TargetingObjcBridge
    {
        private readonly IntPtr nativeObject;

        public TargetingObjcBridge()
        {
            nativeObject = GetTargeting();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public static void setUserId(string id)
        {
            TargetingSetUserId(id);
        }

        public static void setGender(int gender)
        {
            TargetingSetGender(gender);
        }

        public static void setBirthdayYear(int year)
        {
            TargetingSetYearOfBirth(year);
        }

        public static void setKeyWords(string keyWords)
        {
            TargetingSetKeyWords(keyWords);
        }

        public static void setCity(string city)
        {
            TargetingSetCity(city);
        }

        public static void setCountry(string country)
        {
            TargetingSetCountry(country);
        }

        public static void setPaid(bool paid)
        {
            TargetingSetPaid(paid);
        }

        public static void setStoreUrl(string storeUrl)
        {
            TargetingSetStoreUrl(storeUrl);
        }

        public static void setZip(string zip)
        {
            TargetingSetZip(zip);
        }

        public static void setStoreCategory(string storeCategory)
        {
            TargetingSetStoreCategory(storeCategory);
        }

        public static void setStoreSubCategories(string[] storeSubCategories)
        {
            TargetingSetStoreSubCategories(string.Join(",", storeSubCategories));
        }

        public static void setFramework(string framework)
        {
            TargetingSetFramework(framework);
        }

        public static void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            TargetingSetDeviceLocation(latitude, longitude);
        }

        public static void setExternalUserIds(IEnumerable<ExternalUserId> externalUserIdList)
        {
            TargetingSetExternalUserIds(JsonUtility.ToJson(externalUserIdList.ToList()));
        }

        public static void addBlockedApplication(string bundleOrPackage)
        {
            TargetingSetBlockedApps(bundleOrPackage);
        }

        public static void addBlockedAdvertiserIABCategory(string category)
        {
            TargetingSetBlockedCategories(category);
        }

        public static void addBlockedAdvertiserDomain(string advertise)
        {
            TargetingSetBlockedAdvertisers(advertise);
        }

        public static void setStoreId(string storeId)
        {
            TargetingSetStoreId(storeId);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetTargeting();

        [DllImport("__Internal")]
        internal static extern void TargetingSetUserId(string userId);

        [DllImport("__Internal")]
        internal static extern void TargetingSetGender(int gender);

        [DllImport("__Internal")]
        internal static extern void TargetingSetYearOfBirth(int yearOfBirth);

        [DllImport("__Internal")]
        internal static extern void TargetingSetKeyWords(string keywords);

        [DllImport("__Internal")]
        internal static extern void TargetingSetCity(string city);

        [DllImport("__Internal")]
        internal static extern void TargetingSetCountry(string country);

        [DllImport("__Internal")]
        internal static extern void TargetingSetDeviceLocation(double longitude, double latitude);

        [DllImport("__Internal")]
        internal static extern void TargetingSetPaid(bool paid);

        [DllImport("__Internal")]
        internal static extern void TargetingSetStoreUrl(string storeUrl);

        [DllImport("__Internal")]
        internal static extern void TargetingSetZip(string zip);

        [DllImport("__Internal")]
        internal static extern void TargetingSetStoreCategory(string storeCategory);

        [DllImport("__Internal")]
        internal static extern void TargetingSetStoreSubCategories(string storeSubCategories);

        [DllImport("__Internal")]
        internal static extern void TargetingSetFramework(string framework);


        [DllImport("__Internal")]
        internal static extern void TargetingSetExternalUserIds(string users);

        [DllImport("__Internal")]
        internal static extern void TargetingSetBlockedApps(string blockedAdvertisers);

        [DllImport("__Internal")]
        internal static extern void TargetingSetBlockedAdvertisers(string advertiser);


        [DllImport("__Internal")]
        internal static extern void TargetingSetBlockedCategories(string categories);

        [DllImport("__Internal")]
        internal static extern void TargetingSetStoreId(string storeId);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class PriceFloorObjcBridge
    {
        private readonly IntPtr nativeObject;

        public PriceFloorObjcBridge()
        {
            nativeObject = GetPriceFloor();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public void setPriceFloor(string id, double value)
        {
            PriceFloorAddPrifeFloor(id, value);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetPriceFloor();

        [DllImport("__Internal")]
        internal static extern void PriceFloorAddPrifeFloor(string id, double value);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class InterstitialRequestObjCBridge
    {
        private readonly IntPtr nativeObject;

        public InterstitialRequestObjCBridge(IntPtr interstitialRequest)
        {
            nativeObject = interstitialRequest;
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class InterstitialRequestBuilderObjCBridge
    {
        private readonly IntPtr nativeObject;

        public InterstitialRequestBuilderObjCBridge()
        {
            nativeObject = GetInterstitialRequest();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            InterstitialRequestSetPriceFloor(priceFloor);
        }

        public void setTargetingParams(IntPtr targetingParams)
        {
            Debug.Log("Not support on iOS platform");
        }

        public void setType(int type)
        {
            InterstitialRequestSetType(type);
        }

        public void setBidPayload(string bidPayLoad)
        {
            InterstitialSetBidPayload(bidPayLoad);
        }

        public void setSessionAdParams(IntPtr sessionAdParams)
        {
            InterstitialSetSessionAdParams(sessionAdParams);
        }

        public void setLoadingTimeOut(int value)
        {
            InterstitialSetLoadingTimeOut(value);
        }

        public void setPlacementId(string placementId)
        {
            InterstitialSetPlacementId(placementId);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetInterstitialRequest();

        [DllImport("__Internal")]
        internal static extern void InterstitialRequestSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void InterstitialRequestSetType(int type);

        [DllImport("__Internal")]
        internal static extern void InterstitialSetBidPayload(string value);

        [DllImport("__Internal")]
        internal static extern void InterstitialSetSessionAdParams(IntPtr value);

        [DllImport("__Internal")]
        internal static extern void InterstitialSetLoadingTimeOut(int type);

        [DllImport("__Internal")]
        internal static extern void InterstitialSetPlacementId(string type);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class InterstitialAdObjCBridge
    {
        public IntPtr NativeObject;

        public InterstitialAdObjCBridge()
        {
            NativeObject = GetInterstitialAd();
        }

        public InterstitialAdObjCBridge(IntPtr interstitial)
        {
            NativeObject = interstitial;
        }

        public IntPtr GetIntPtr()
        {
            return NativeObject;
        }

        public bool canShow()
        {
            return InterstitialAdCanShow(NativeObject);
        }

        public void destroy()
        {
            InterstitialAdDestroy();
        }

        public void show()
        {
            InterstitialAdShow();
        }

        public void load(IntPtr interstitialRequest)
        {
            InterstitialAdLoad(interstitialRequest);
        }

        public void setDelegate(BidMachineInterstitialCallbacks onAdLoaded,
            BidMachineInterstitialFailedCallback onAdLoadFailed,
            BidMachineInterstitialCallbacks onAdShown,
            BidMachineInterstitialCallbacks onAdClicked,
            BidMachineInterstitialCallbacks onAdClosed)
        {
            InterstitialAdSetDelegate(onAdLoaded, onAdLoadFailed, onAdShown, onAdClicked, onAdClosed);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetInterstitialAd();

        [DllImport("__Internal")]
        internal static extern bool InterstitialAdCanShow(IntPtr no);

        [DllImport("__Internal")]
        internal static extern void InterstitialAdDestroy();

        [DllImport("__Internal")]
        internal static extern void InterstitialAdShow();

        [DllImport("__Internal")]
        internal static extern void InterstitialAdLoad(IntPtr interstitialRequest);

        [DllImport("__Internal")]
        internal static extern void InterstitialAdSetDelegate(
            BidMachineInterstitialCallbacks onAdLoaded,
            BidMachineInterstitialFailedCallback onAdLoadFailed,
            BidMachineInterstitialCallbacks onAdShown,
            BidMachineInterstitialCallbacks onAdClicked,
            BidMachineInterstitialCallbacks onAdClosed);
    }

    internal class RewardedAdObjCBridge
    {
        public IntPtr NativeObject;

        public RewardedAdObjCBridge()
        {
            NativeObject = GetRewarded();
        }

        public RewardedAdObjCBridge(IntPtr rewardedAd)
        {
            NativeObject = rewardedAd;
        }

        public IntPtr GetIntPtr()
        {
            return NativeObject;
        }

        public bool canShow()
        {
            return RewardedCanShow();
        }

        public void destroy()
        {
            RewardedAdDestroy();
        }

        public void show()
        {
            RewardedShow();
        }

        public void load(IntPtr rewardedRequest)
        {
            RewardedLoad(rewardedRequest);
        }

        public void setDelegate(BidMachineRewardedCallbacks onAdLoaded,
            BidMachineRewardedFailedCallback onAdLoadFailed,
            BidMachineRewardedCallbacks onAdShown,
            BidMachineRewardedCallbacks onAdClicked,
            BidMachineRewardedCallbacks onAdClosed)
        {
            RewardedSetDelegate(onAdLoaded, onAdLoadFailed, onAdShown, onAdClicked, onAdClosed);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetRewarded();

        [DllImport("__Internal")]
        internal static extern bool RewardedCanShow();

        [DllImport("__Internal")]
        internal static extern void RewardedAdDestroy();

        [DllImport("__Internal")]
        internal static extern void RewardedShow();

        [DllImport("__Internal")]
        internal static extern void RewardedLoad(IntPtr rewardedRequest);

        [DllImport("__Internal")]
        internal static extern void RewardedSetDelegate(
            BidMachineRewardedCallbacks onAdLoaded,
            BidMachineRewardedFailedCallback onAdLoadFailed,
            BidMachineRewardedCallbacks onAdShown,
            BidMachineRewardedCallbacks onAdClicked,
            BidMachineRewardedCallbacks onAdClosed);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class RewardedRequestObjCBridge
    {
        private readonly IntPtr NativeObject;

        public RewardedRequestObjCBridge(IntPtr rewardedRequest)
        {
            NativeObject = rewardedRequest;
        }

        public IntPtr getNativeObject()
        {
            return NativeObject;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class RewardedRequestBuilderObjCBridge
    {
        private readonly IntPtr NativeObject;

        public RewardedRequestBuilderObjCBridge()
        {
            NativeObject = GetRewardedRequest();
        }

        public IntPtr getNativeObject()
        {
            return NativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            RewardedSetPriceFloor(priceFloor);
        }

        public void setBidPayload(string bidPayLoad)
        {
            RewardedSetBidPayload(bidPayLoad);
        }

        public void setPlacementId(string placementId)
        {
            RewardedSetPlacementId(placementId);
        }

        public void setLoadingTimeOut(int value)
        {
            RewardedSetLoadingTimeOut(value);
        }

        public void setSessionAdParams(IntPtr sessionAdParams)
        {
            RewardedSetSessionAdParams(sessionAdParams);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetRewardedRequest();

        [DllImport("__Internal")]
        internal static extern void RewardedSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void RewardedSetBidPayload(string value);

        [DllImport("__Internal")]
        internal static extern void RewardedSetPlacementId(string value);

        [DllImport("__Internal")]
        internal static extern void RewardedSetLoadingTimeOut(int value);

        [DllImport("__Internal")]
        internal static extern void RewardedSetSessionAdParams(IntPtr value);
    }

    internal class BannerViewObjCBridge
    {
        public IntPtr NativeObject;

        public BannerViewObjCBridge()
        {
            NativeObject = GetBannerView();
        }

        public BannerViewObjCBridge(IntPtr bannerView)
        {
            NativeObject = bannerView;
        }

        public IntPtr GetIntPtr()
        {
            return NativeObject;
        }

        public bool canShow()
        {
            return BannerViewAdCanShow();
        }

        public void destroy()
        {
            BannerViewDestroy();
        }

        public void show(int YAxis, int XAxis)
        {
            BannerViewShow(YAxis, XAxis);
        }

        public void load(IntPtr bannerViewRequest)
        {
            BannerViewLoad(bannerViewRequest);
        }

        public void setDelegate(BidMachineBannerCallbacks onAdLoaded,
            BidMachineBannerFailedCallback onAdLoadFailed,
            BidMachineBannerCallbacks onAdClicked)
        {
            BannerViewSetDelegate(onAdLoaded, onAdLoadFailed, onAdClicked);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetBannerView();

        [DllImport("__Internal")]
        internal static extern bool BannerViewAdCanShow();

        [DllImport("__Internal")]
        internal static extern void BannerViewDestroy();

        [DllImport("__Internal")]
        internal static extern void BannerViewShow(int YAxis, int XAxis);

        [DllImport("__Internal")]
        internal static extern void BannerViewLoad(IntPtr bannerViewRequest);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetDelegate(
            BidMachineBannerCallbacks onAdLoaded,
            BidMachineBannerFailedCallback onAdLoadFailed,
            BidMachineBannerCallbacks onAdClicked);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class BannerViewRequestObjCBridge
    {
        private readonly IntPtr NativeObject;

        public BannerViewRequestObjCBridge(IntPtr bannerViewRequest)
        {
            NativeObject = bannerViewRequest;
        }

        public IntPtr getNativeObject()
        {
            return NativeObject;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class BannerViewRequestBuilderObjCBridge
    {
        private readonly IntPtr nativeObject;

        public BannerViewRequestBuilderObjCBridge()
        {
            nativeObject = GetBannerViewRequest();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public IntPtr GetIntPtr()
        {
            return nativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            BannerViewRequestSetPriceFloor(priceFloor);
        }

        public void setBannerSize(int size)
        {
            BannerViewSetSize(size);
        }

        public void setBidPayload(string bidPayLoad)
        {
            BannerViewSetBidPayload(bidPayLoad);
        }

        public void setPlacementId(string placementId)
        {
            BannerViewSetPlacementId(placementId);
        }

        public void setLoadingTimeOut(int value)
        {
            BannerViewSetLoadingTimeOut(value);
        }

        public void setSessionAdParams(IntPtr sessionAdParams)
        {
            BannerViewSetSessionAdParams(sessionAdParams);
        }

        public void setBannerRequestDelegate(BannerRequestSuccessCallback bannerRequestSuccessCallback,
            BannerRequestFailedCallback bannerRequestFailedCallback,
            BannerRequestExpiredCallback bannerRequestExpiredCallback)
        {
            SetBannerRequestDelegate(bannerRequestSuccessCallback,
                bannerRequestFailedCallback,
                bannerRequestExpiredCallback);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetBannerViewRequest();

        [DllImport("__Internal")]
        internal static extern void SetBannerRequestDelegate(BannerRequestSuccessCallback bannerRequestSuccessCallback,
            BannerRequestFailedCallback bannerRequestFailedCallback,
            BannerRequestExpiredCallback bannerRequestExpiredCallback);

        [DllImport("__Internal")]
        internal static extern void BannerViewRequestSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetSize(int size);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetBidPayload(string value);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetPlacementId(string value);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetLoadingTimeOut(int value);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetSessionAdParams(IntPtr value);
    }
}
#endif