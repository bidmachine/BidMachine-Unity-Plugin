#if UNITY_IPHONE
using System.Runtime.InteropServices;
using System;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;
using BidMachineAds.Unity.iOS;
using UnityEngine.iOS;


namespace BidMachineAds.Unity.iOS
{
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
        internal static extern string BidMachineGetErrorBrief(IntPtr error);

        [DllImport("__Internal")]
        internal static extern string BidMachineGetErrorMessage(IntPtr error);

        [DllImport("__Internal")]
        internal static extern bool BidMachineIsInitialized();

        [DllImport("__Internal")]
        internal static extern void BidMachineSetEndpoint(string url);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetUSPrivacyString(string usPrivacyString);

        [DllImport("__Internal")]
        internal static extern void BidMachineSetPublisher(IntPtr publisher);
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

        public static void setKeyWords(string[] keyWords)
        {
            TargetingSetKeyWords(string.Join(",", keyWords));
        }

        public static void setBlockedAdvertiserIABCategories(string categories)
        {
            TargetingSetBlockedCategories(categories);
        }

        public static void setBlockedAdvertiserDomain(string domains)
        {
            TargetingSetBlockedAdvertisers(domains);
        }

        public static void setBlockedApplication(string applications)
        {
            TargetingSetBlockedApps(applications);
        }

        public static void setCity(string city)
        {
            TargetingSetCity(city);
        }

        public static void setCountry(string country)
        {
            TargetingSetCountry(country);
        }

        public static void setDeviceLocation(double longitude, double latitude)
        {
            TargetingSetDeviceLocation(longitude, latitude);
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

        public static void setFramework(string framework)
        {
            SetFramework(framework);
        }

        public static void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            SetDeviceLocation(latitude, longitude);
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
        internal static extern void TargetingSetBlockedCategories(string blockedCategories);

        [DllImport("__Internal")]
        internal static extern void TargetingSetBlockedAdvertisers(string blockedAdvertisers);

        [DllImport("__Internal")]
        internal static extern void TargetingSetBlockedApps(string blockedAdvertisers);

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
        internal static extern void SetFramework(string framework);

        [DllImport("__Internal")]
        internal static extern void SetDeviceLocation(double latitude, double longitude);
    }

    internal class PriceFloorObjcBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

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

    internal class IntertitialRequestObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public IntertitialRequestObjCBridge(IntPtr interstitialRequest)
        {
            nativeObject = interstitialRequest;
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }
    }

    internal class InterstitialRequestBuilderObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

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
            InterstitialRequestSetTargeting(targetingParams);
        }

        public void setType(int type)
        {
            InterstitialRequestSetType(type);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetInterstitialRequest();

        [DllImport("__Internal")]
        internal static extern void InterstitialRequestSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void InterstitialRequestSetTargeting(IntPtr targetingParams);

        [DllImport("__Internal")]
        internal static extern void InterstitialRequestSetType(int type);
    }

    internal class InterstitialAdObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public InterstitialAdObjCBridge()
        {
            nativeObject = GetInterstitialAd();
        }

        public InterstitialAdObjCBridge(IntPtr interstitial)
        {
            nativeObject = interstitial;
        }

        public IntPtr GetIntPtr()
        {
            return nativeObject;
        }

        public bool canShow()
        {
            return InterstitialAdCanShow(nativeObject);
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
        public IntPtr nativeObject = IntPtr.Zero;

        public RewardedAdObjCBridge()
        {
            nativeObject = GetRewarded();
        }

        public RewardedAdObjCBridge(IntPtr rewardedAd)
        {
            nativeObject = rewardedAd;
        }

        public IntPtr GetIntPtr()
        {
            return nativeObject;
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


    internal class RewardedRequestObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public RewardedRequestObjCBridge(IntPtr rewardedRequest)
        {
            nativeObject = rewardedRequest;
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }
    }

    internal class RewardedRequestBuilderObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public RewardedRequestBuilderObjCBridge()
        {
            nativeObject = GetRewardedRequest();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            RewardedSetPriceFlooor(priceFloor);
        }

        public void setTargetingParams(IntPtr targetingParams)
        {
            RewardedSetTargeting(targetingParams);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetRewardedRequest();

        [DllImport("__Internal")]
        internal static extern void RewardedSetPriceFlooor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void RewardedSetTargeting(IntPtr targetingParams);
    }

    internal class BannerViewObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public BannerViewObjCBridge()
        {
            nativeObject = GetBannerView();
        }

        public BannerViewObjCBridge(IntPtr bannerView)
        {
            nativeObject = bannerView;
        }

        public IntPtr GetIntPtr()
        {
            return nativeObject;
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

    internal class BannerViewRequestObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public BannerViewRequestObjCBridge(IntPtr bannerViewRequest)
        {
            nativeObject = bannerViewRequest;
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }
    }

    internal class BannerViewRequestBuilderObjCBridge
    {
        public IntPtr nativeObject = IntPtr.Zero;

        public BannerViewRequestBuilderObjCBridge()
        {
            nativeObject = GetBannerViewRequest();
        }

        public IntPtr getNativeObject()
        {
            return nativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            BannerViewRequestSetPriceFloor(priceFloor);
        }

        public void setTargetingParams(IntPtr targetingParams)
        {
            BannerViewRequestSetTargeting(targetingParams);
        }

        public void setBannerSize(int size)
        {
            BannerViewSetSize(size);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetBannerViewRequest();

        [DllImport("__Internal")]
        internal static extern void BannerViewRequestSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        internal static extern void BannerViewRequestSetTargeting(IntPtr targetingParams);

        [DllImport("__Internal")]
        internal static extern void BannerViewSetSize(int size);
    }
}
#endif