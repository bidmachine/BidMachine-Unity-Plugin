#if UNITY_IPHONE
using System.Runtime.InteropServices;
using System;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS
{
    #region BannerRequestDelegate

    internal delegate void BannerRequestSuccessCallback(IntPtr ad, string auctionResult);

    internal delegate void BannerRequestFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void BannerRequestExpiredCallback(IntPtr ad);

    #endregion

    #region InterstitialRequestDelegate

    internal delegate void InterstitialRequestSuccessCallback(IntPtr ad, string auctionResult);

    internal delegate void InterstitialRequestFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void InterstitialRequestExpiredCallback(IntPtr ad);

    #endregion

    #region RewardedRequestDelegate

    internal delegate void RewardedRequestSuccessCallback(IntPtr ad, string auctionResult);

    internal delegate void RewardedRequestFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void RewardedRequestExpiredCallback(IntPtr ad);

    #endregion

    #region NativeRequestDelegate

    internal delegate void NativeRequestSuccessCallback(IntPtr ad, string auctionResult);

    internal delegate void NativeRequestFailedCallback(IntPtr ad, IntPtr error);

    internal delegate void NativeRequestExpiredCallback(IntPtr ad);

    #endregion

    #region BannerAdDelegate

    internal delegate void BidMachineBannerCallbacks(IntPtr ad);

    internal delegate void BidMachineBannerFailedCallback(IntPtr ad, IntPtr error);

    #endregion

    #region InterstitialAdDelegate

    internal delegate void BidMachineInterstitialCallbacks(IntPtr ad);

    internal delegate void BidMachineInterstitialFailedCallback(IntPtr ad, IntPtr error);

    #endregion

    #region RewardedAdDelegate

    internal delegate void BidMachineRewardedCallbacks(IntPtr ad);

    internal delegate void BidMachineRewardedFailedCallback(IntPtr ad, IntPtr error);

    #endregion

    #region NativeAdDelegate

    internal delegate void BidMachineNativeCallbacks(IntPtr ad);

    internal delegate void BidMachineNativeFailedCallback(IntPtr ad, IntPtr error);

    #endregion

    #region BidMachine

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

    #endregion

    #region SessionAdParams

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

        public static void setSessionDuration(int value)
        {
            SetSessionDuration(value);
        }

        public static void setImpressionCount(int value)
        {
            SetImpressionCount(value);
        }

        public static void setClickRate(int value)
        {
            SetClickRate(value);
        }

        public static void setLastAdomain(string value)
        {
            SetLastAdomain(value);
        }

        public static void setCompletionRate(int value)
        {
            SetCompletionRate(value);
        }

        public static void setLastClickForImpression(int value)
        {
            SetLastClickForImpression(value);
        }

        public static void setLastBundle(string value)
        {
            SetLastBundle(value);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetSessionAdParams();

        [DllImport("__Internal")]
        private static extern void SetSessionDuration(int value);

        [DllImport("__Internal")]
        private static extern void SetImpressionCount(int value);

        [DllImport("__Internal")]
        private static extern void SetClickRate(int value);

        [DllImport("__Internal")]
        private static extern void SetLastAdomain(string value);

        [DllImport("__Internal")]
        private static extern void SetCompletionRate(int value);

        [DllImport("__Internal")]
        private static extern void SetLastClickForImpression(int value);

        [DllImport("__Internal")]
        private static extern void SetLastBundle(string value);
    }

    #endregion

    #region TargetingParams

    [SuppressMessage("ReSharper", "InconsistentNaming")]
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

        public static void setDeviceLocation(double latitude, double longitude)
        {
            TargetingSetDeviceLocation(latitude, longitude);
        }

        public static void setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            TargetingSetExternalUserIds(BidMachineUtils.ToJson(externalUserIdList));
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
        private static extern IntPtr GetTargeting();

        [DllImport("__Internal")]
        private static extern void TargetingSetUserId(string userId);

        [DllImport("__Internal")]
        private static extern void TargetingSetGender(int gender);

        [DllImport("__Internal")]
        private static extern void TargetingSetYearOfBirth(int yearOfBirth);

        [DllImport("__Internal")]
        private static extern void TargetingSetKeyWords(string keywords);

        [DllImport("__Internal")]
        private static extern void TargetingSetCity(string city);

        [DllImport("__Internal")]
        private static extern void TargetingSetCountry(string country);

        [DllImport("__Internal")]
        private static extern void TargetingSetDeviceLocation(double longitude, double latitude);

        [DllImport("__Internal")]
        private static extern void TargetingSetPaid(bool paid);

        [DllImport("__Internal")]
        private static extern void TargetingSetStoreUrl(string storeUrl);

        [DllImport("__Internal")]
        private static extern void TargetingSetZip(string zip);

        [DllImport("__Internal")]
        private static extern void TargetingSetStoreCategory(string storeCategory);

        [DllImport("__Internal")]
        private static extern void TargetingSetStoreSubCategories(string storeSubCategories);

        [DllImport("__Internal")]
        private static extern void TargetingSetFramework(string framework);

        [DllImport("__Internal")]
        public static extern void TargetingSetExternalUserIds(string users);

        [DllImport("__Internal")]
        private static extern void TargetingSetBlockedApps(string blockedAdvertisers);

        [DllImport("__Internal")]
        private static extern void TargetingSetBlockedAdvertisers(string advertiser);

        [DllImport("__Internal")]
        private static extern void TargetingSetBlockedCategories(string categories);

        [DllImport("__Internal")]
        private static extern void TargetingSetStoreId(string storeId);
    }

    #endregion

    #region PriceFloorParams

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

        public static void setPriceFloor(string id, double value)
        {
            PriceFloorAddPriceFloor(id, value);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetPriceFloor();

        [DllImport("__Internal")]
        private static extern void PriceFloorAddPriceFloor(string id, double value);
    }

    #endregion

    #region InterstitialAd

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

        public string getAuctionResult()
        {
            return GetAuctionResult();
        }

        [DllImport("__Internal")]
        private static extern string GetAuctionResult();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    internal class InterstitialRequestBuilderObjCBridge
    {
        private readonly IntPtr nativeObject;

        public InterstitialRequestBuilderObjCBridge()
        {
            nativeObject = GetInterstitialRequest();
        }

        public IntPtr getIntPtr()
        {
            return nativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            InterstitialRequestSetPriceFloor(priceFloor);
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

        public void setInterstitialRequestDelegate(
            InterstitialRequestSuccessCallback interstitialRequestSuccessCallback,
            InterstitialRequestFailedCallback interstitialRequestFailedCallback,
            InterstitialRequestExpiredCallback interstitialRequestExpiredCallback)
        {
            SetInterstitialRequestDelegate(interstitialRequestSuccessCallback, interstitialRequestFailedCallback,
                interstitialRequestExpiredCallback);
        }

        [DllImport("__Internal")]
        internal static extern IntPtr GetInterstitialRequest();

        [DllImport("__Internal")]
        internal static extern void SetInterstitialRequestDelegate(
            InterstitialRequestSuccessCallback interstitialRequestSuccessCallback,
            InterstitialRequestFailedCallback interstitialRequestFailedCallback,
            InterstitialRequestExpiredCallback interstitialRequestExpiredCallback);

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
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
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
        private static extern IntPtr GetInterstitialAd();

        [DllImport("__Internal")]
        private static extern bool InterstitialAdCanShow(IntPtr no);

        [DllImport("__Internal")]
        private static extern void InterstitialAdDestroy();

        [DllImport("__Internal")]
        private static extern void InterstitialAdShow();

        [DllImport("__Internal")]
        private static extern void InterstitialAdLoad(IntPtr interstitialRequest);

        [DllImport("__Internal")]
        private static extern void InterstitialAdSetDelegate(
            BidMachineInterstitialCallbacks onAdLoaded,
            BidMachineInterstitialFailedCallback onAdLoadFailed,
            BidMachineInterstitialCallbacks onAdShown,
            BidMachineInterstitialCallbacks onAdClicked,
            BidMachineInterstitialCallbacks onAdClosed);
    }

    #endregion

    #region RewardedAd

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
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
            BidMachineRewardedCallbacks onAdClosed
        )
        {
            RewardedSetDelegate(onAdLoaded, onAdLoadFailed, onAdShown, onAdClicked, onAdClosed);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetRewarded();

        [DllImport("__Internal")]
        private static extern bool RewardedCanShow();

        [DllImport("__Internal")]
        private static extern void RewardedAdDestroy();

        [DllImport("__Internal")]
        private static extern void RewardedShow();

        [DllImport("__Internal")]
        private static extern void RewardedLoad(IntPtr rewardedRequest);

        [DllImport("__Internal")]
        private static extern void RewardedSetDelegate(
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

        public string getAuctionResult()
        {
            return GetAuctionResult();
        }

        [DllImport("__Internal")]
        private static extern string GetAuctionResult();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    internal class RewardedRequestBuilderObjCBridge
    {
        private readonly IntPtr NativeObject;

        public RewardedRequestBuilderObjCBridge()
        {
            NativeObject = GetRewardedRequest();
        }

        public IntPtr getIntPtr()
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

        public void setRewardedRequestDelegate(RewardedRequestSuccessCallback requestSuccessCallback,
            RewardedRequestFailedCallback requestFailedCallback, RewardedRequestExpiredCallback requestExpiredCallback)
        {
            SetRewardedRequestDelegate(requestSuccessCallback, requestFailedCallback, requestExpiredCallback);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetRewardedRequest();

        [DllImport("__Internal")]
        private static extern void SetRewardedRequestDelegate(RewardedRequestSuccessCallback requestSuccessCallback,
            RewardedRequestFailedCallback requestFailedCallback, RewardedRequestExpiredCallback requestExpiredCallback);

        [DllImport("__Internal")]
        private static extern void RewardedSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        private static extern void RewardedSetBidPayload(string value);

        [DllImport("__Internal")]
        private static extern void RewardedSetPlacementId(string value);

        [DllImport("__Internal")]
        private static extern void RewardedSetLoadingTimeOut(int value);

        [DllImport("__Internal")]
        private static extern void RewardedSetSessionAdParams(IntPtr value);
    }

    #endregion

    #region BannerAd

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
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
            BidMachineBannerCallbacks onAdShown,
            BidMachineBannerCallbacks onAdImpression,
            BidMachineBannerCallbacks onAdClicked,
            BidMachineBannerCallbacks onAdExpired)
        {
            BannerViewSetDelegate(onAdLoaded, onAdLoadFailed, onAdShown, onAdImpression, onAdClicked, onAdExpired);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetBannerView();

        [DllImport("__Internal")]
        private static extern bool BannerViewAdCanShow();

        [DllImport("__Internal")]
        private static extern void BannerViewDestroy();

        [DllImport("__Internal")]
        private static extern void BannerViewShow(int YAxis, int XAxis);

        [DllImport("__Internal")]
        private static extern void BannerViewLoad(IntPtr bannerViewRequest);

        [DllImport("__Internal")]
        private static extern void BannerViewSetDelegate(
            BidMachineBannerCallbacks onAdLoaded,
            BidMachineBannerFailedCallback onAdLoadFailed,
            BidMachineBannerCallbacks onAdShown,
            BidMachineBannerCallbacks onAdImpression,
            BidMachineBannerCallbacks onAdClicked,
            BidMachineBannerCallbacks onAdExpired);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
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

        public BannerSize getSize()
        {
            switch (GetSize())
            {
                case 0:
                    return BannerSize.Size_320х50;
                    break;
                case 1:
                    return BannerSize.Size_300х250;
                    break;
                case 2:
                    return BannerSize.Size_728х90;
                    break;
                default:
                    return BannerSize.Size_320х50;
                    break;
            }
        }

        public string getAuctionResult()
        {
            return GetAuctionResult();
        }

        [DllImport("__Internal")]
        private static extern string GetAuctionResult();

        [DllImport("__Internal")]
        private static extern int GetSize();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
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
        private static extern IntPtr GetBannerViewRequest();

        [DllImport("__Internal")]
        private static extern void SetBannerRequestDelegate(BannerRequestSuccessCallback bannerRequestSuccessCallback,
            BannerRequestFailedCallback bannerRequestFailedCallback,
            BannerRequestExpiredCallback bannerRequestExpiredCallback);

        [DllImport("__Internal")]
        private static extern void BannerViewRequestSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        private static extern void BannerViewSetSize(int size);

        [DllImport("__Internal")]
        private static extern void BannerViewSetBidPayload(string value);

        [DllImport("__Internal")]
        private static extern void BannerViewSetPlacementId(string value);

        [DllImport("__Internal")]
        private static extern void BannerViewSetLoadingTimeOut(int value);

        [DllImport("__Internal")]
        private static extern void BannerViewSetSessionAdParams(IntPtr value);
    }

    #endregion

    #region NativeAd

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class NativeRequestObjCBridge
    {
        private readonly IntPtr NativeObject;

        public NativeRequestObjCBridge(IntPtr nativeRequest)
        {
            NativeObject = nativeRequest;
        }

        public IntPtr getNativeObject()
        {
            return NativeObject;
        }

        public string getAuctionResult()
        {
            return GetAuctionResult();
        }

        [DllImport("__Internal")]
        private static extern string GetAuctionResult();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    internal class NativeRequestBuilderObjCBridge
    {
        private readonly IntPtr NativeObject;

        public NativeRequestBuilderObjCBridge()
        {
            NativeObject = GetNativeRequest();
        }

        public IntPtr GetIntPtr()
        {
            return NativeObject;
        }

        public void setPriceFloor(IntPtr priceFloor)
        {
            NativeSetPriceFloor(priceFloor);
        }

        public void setBidPayload(string bidPayLoad)
        {
            NativeSetBidPayload(bidPayLoad);
        }

        public void setPlacementId(string placementId)
        {
            NativeSetPlacementId(placementId);
        }

        public void setLoadingTimeOut(int value)
        {
            NativeSetLoadingTimeOut(value);
        }

        public void setSessionAdParams(IntPtr sessionAdParams)
        {
            NativeSetSessionAdParams(sessionAdParams);
        }

        public void setMediaAssetTypes(NativeAdParams nativeAdParams)
        {
            foreach (var value in nativeAdParams.getMediaAssetTypes())
            {
                NativeSetMediaAssetTypes(value.ToString());
            }
        }

        public void setNativeRequestDelegate(NativeRequestSuccessCallback requestSuccessCallback,
            NativeRequestFailedCallback requestFailedCallback, NativeRequestExpiredCallback requestExpiredCallback)
        {
            SetNativeRequestDelegate(requestSuccessCallback, requestFailedCallback, requestExpiredCallback);
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetNativeRequest();

        [DllImport("__Internal")]
        private static extern IntPtr NativeSetMediaAssetTypes(string value);

        [DllImport("__Internal")]
        private static extern void SetNativeRequestDelegate(NativeRequestSuccessCallback requestSuccessCallback,
            NativeRequestFailedCallback requestFailedCallback, NativeRequestExpiredCallback requestExpiredCallback);

        [DllImport("__Internal")]
        private static extern void NativeSetPriceFloor(IntPtr priceFloor);

        [DllImport("__Internal")]
        private static extern void NativeSetBidPayload(string value);

        [DllImport("__Internal")]
        private static extern void NativeSetPlacementId(string value);

        [DllImport("__Internal")]
        private static extern void NativeSetLoadingTimeOut(int value);

        [DllImport("__Internal")]
        private static extern void NativeSetSessionAdParams(IntPtr value);
    }


    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    internal class NativeAdObjCBridge
    {
        public IntPtr NativeObject;

        public NativeAdObjCBridge()
        {
            NativeObject = GetNativeAd();
        }

        public NativeAdObjCBridge(IntPtr nativeAd)
        {
            NativeObject = nativeAd;
        }

        public IntPtr GetIntPtr()
        {
            return NativeObject;
        }

        public bool canShow()
        {
            return NativeAdCanShow();
        }

        public void destroy()
        {
            NativeAdDestroy();
        }

        public void load(IntPtr nativeRequest)
        {
            NativeAdLoad(nativeRequest);
        }

        public void setDelegate(BidMachineNativeCallbacks onAdLoaded,
            BidMachineNativeFailedCallback onAdLoadFailed,
            BidMachineNativeCallbacks onAdShown,
            BidMachineNativeCallbacks onAdClicked,
            BidMachineNativeCallbacks onAdImpression,
            BidMachineNativeCallbacks onAdExpired
        )
        {
            NativeSetDelegate(onAdLoaded, onAdLoadFailed, onAdShown, onAdClicked, onAdImpression, onAdExpired);
        }

        public string getTitle()
        {
            return GetTitle();
        }

        public string getDescription()
        {
            return GetDescription();
        }

        public string getCallToAction()
        {
            return GetCallToAction();
        }

        public float getRating()
        {
            return GetRating();
        }

        [DllImport("__Internal")]
        private static extern IntPtr GetNativeAd();

        [DllImport("__Internal")]
        private static extern string GetTitle();

        [DllImport("__Internal")]
        private static extern string GetDescription();

        [DllImport("__Internal")]
        private static extern string GetCallToAction();

        [DllImport("__Internal")]
        private static extern float GetRating();

        [DllImport("__Internal")]
        private static extern bool NativeAdCanShow();

        [DllImport("__Internal")]
        private static extern void NativeAdDestroy();

        [DllImport("__Internal")]
        private static extern void NativeAdLoad(IntPtr nativeRequest);

        [DllImport("__Internal")]
        private static extern void NativeSetDelegate(
            BidMachineNativeCallbacks onAdLoaded,
            BidMachineNativeFailedCallback onAdLoadFailed,
            BidMachineNativeCallbacks onAdShown,
            BidMachineNativeCallbacks onAdClicked,
            BidMachineNativeCallbacks onAdImpression,
            BidMachineNativeCallbacks onAdExpired);
    }

    #endregion

    #region Utils

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class BidMachineUtils
    {
        public static T[] FromJson<T>(string json)
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            var wrapper = new Wrapper<T> { Items = array };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            var wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static string fixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    #endregion
}
#endif