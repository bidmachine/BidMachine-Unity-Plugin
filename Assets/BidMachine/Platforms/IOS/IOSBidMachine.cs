#if UNITY_IPHONE
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    #region BidMachine

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class iOSBidMachine : IBidMachine
    {
        public const string DUMMY_MESSAGE = "method doesn't support on iOS platform";
        private const int GENDER_MALE = 1;
        private const int GENDER_FEMALE = 2;
        private const int GENDER_UNKNOWN = 3;

        #region Singleton

        private iOSBidMachine()
        {
        }

        public static iOSBidMachine Instance { get; } = new iOSBidMachine();

        #endregion

        public void initialize(string sellerId)
        {
            BidMachineObjCBridge.BidMachineInitialize(sellerId);
        }

        public bool isInitialized()
        {
            return BidMachineObjCBridge.BidMachineIsInitialized();
        }

        public void setEndpoint(string url)
        {
            BidMachineObjCBridge.BidMachineSetEndpoint(!string.IsNullOrEmpty(url) ? url : "");
        }

        public void setLoggingEnabled(bool logging)
        {
            BidMachineObjCBridge.BidMachineSetLogging(logging);
        }

        public void setTestMode(bool testing)
        {
            BidMachineObjCBridge.BidMachineSetTestMode(testing);
        }

        public void setCoppa(bool coppa)
        {
            BidMachineObjCBridge.BidMachineSetCoppa(coppa);
        }

        public void setUSPrivacyString(string usPrivacyString)
        {
            BidMachineObjCBridge.BidMachineSetUSPrivacyString(!string.IsNullOrEmpty(usPrivacyString)
                ? usPrivacyString
                : "");
        }

        public void setPublisher(Publisher publisher)
        {
            if (publisher != null)
            {
                BidMachineObjCBridge.BidMachineSetPublisher(
                    !string.IsNullOrEmpty(publisher.ID) ? publisher.ID : "null",
                    !string.IsNullOrEmpty(publisher.Name) ? publisher.Name : "null",
                    !string.IsNullOrEmpty(publisher.Domain) ? publisher.Domain : "null",
                    publisher.Categories.Length > 0
                        ? string.Join(",", publisher.Categories)
                        : string.Join(",", new string[] { "null" }));
            }
        }

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            BidMachineObjCBridge.BidMachineSetGdprRequired(subjectToGDPR);
        }

        public void setConsentConfig(bool consent, string GDPRConsentString)
        {
            BidMachineObjCBridge.BidMachineSetConsentString(consent,
                !string.IsNullOrEmpty(GDPRConsentString) ? GDPRConsentString : "");
        }

        public bool checkAndroidPermissions(string permission)
        {
            Debug.Log($"checkAndroidPermissions() + {DUMMY_MESSAGE}");
            return false;
        }

        public void requestAndroidPermissions()
        {
            Debug.Log($"requestAndroidPermissions() + {DUMMY_MESSAGE}");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var targetingObjcBridge = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            BidMachineObjCBridge.BidMachineSetTargeting(targetingObjcBridge.GetIntPtr());
        }
    }

    #endregion

    #region TargetingParams

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSTargetingParams : ITargetingParams
    {
        private readonly TargetingObjcBridge bridge;

        public iOSTargetingParams()
        {
            bridge = new TargetingObjcBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setUserId(string id)
        {
            TargetingObjcBridge.setUserId(!string.IsNullOrEmpty(id) ? id : "");
        }

        public void setGender(TargetingParams.Gender gender)
        {
            switch (gender)
            {
                case TargetingParams.Gender.Omitted:
                {
                    TargetingObjcBridge.setGender(3);
                    break;
                }
                case TargetingParams.Gender.Male:
                {
                    TargetingObjcBridge.setGender(1);
                    break;
                }
                case TargetingParams.Gender.Female:
                {
                    TargetingObjcBridge.setGender(2);
                    break;
                }
                default:
                    TargetingObjcBridge.setGender(3);
                    break;
            }
        }

        public void setBirthdayYear(int year)
        {
            TargetingObjcBridge.setBirthdayYear(year);
        }

        public void setKeyWords(string[] keyWords)
        {
            TargetingObjcBridge.setKeyWords(keyWords.Length > 0
                ? string.Join(",", keyWords)
                : string.Join(",", "null"));
        }

        public void setCountry(string country)
        {
            TargetingObjcBridge.setCountry(!string.IsNullOrEmpty(country) ? country : "");
        }

        public void setCity(string city)
        {
            TargetingObjcBridge.setCity(!string.IsNullOrEmpty(city) ? city : "");
        }

        public void setZip(string zip)
        {
            TargetingObjcBridge.setZip(!string.IsNullOrEmpty(zip) ? zip : "");
        }

        public void setStoreUrl(string storeUrl)
        {
            TargetingObjcBridge.setStoreUrl(!string.IsNullOrEmpty(storeUrl) ? storeUrl : "");
        }

        public void setStoreCategory(string storeCategory)
        {
            TargetingObjcBridge.setStoreCategory(!string.IsNullOrEmpty(storeCategory) ? storeCategory : "");
        }

        public void setStoreSubCategories(string[] storeSubCategories)
        {
            TargetingObjcBridge.setStoreSubCategories(storeSubCategories.Length > 0
                ? storeSubCategories
                : new[] { "" });
        }

        public void setStoreId(string storeId)
        {
            TargetingObjcBridge.setStoreId(!string.IsNullOrEmpty(storeId) ? storeId : "");
        }

        public void setFramework(string framework)
        {
            TargetingObjcBridge.setFramework(!string.IsNullOrEmpty(framework) ? framework : "");
        }

        public void setPaid(bool paid)
        {
            TargetingObjcBridge.setPaid(paid);
        }

        public void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            TargetingObjcBridge.setDeviceLocation(latitude, longitude);
        }

        public void setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            TargetingObjcBridge.setExternalUserIds(externalUserIdList);
        }

        public void addBlockedApplication(string bundleOrPackage)
        {
            TargetingObjcBridge.addBlockedApplication(!string.IsNullOrEmpty(bundleOrPackage) ? bundleOrPackage : "");
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            TargetingObjcBridge.addBlockedAdvertiserIABCategory(!string.IsNullOrEmpty(category) ? category : "");
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            TargetingObjcBridge.addBlockedAdvertiserDomain(!string.IsNullOrEmpty(domain) ? domain : "");
        }
    }

    #endregion

    #region SessionAdParams

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSSessionAdParams : ISessionAdParams
    {
        private readonly SessionAdParamsObjcBridge bridge;

        public iOSSessionAdParams()
        {
            bridge = new SessionAdParamsObjcBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setSessionDuration(int value)
        {
            SessionAdParamsObjcBridge.setSessionDuration(value);
        }

        public void setImpressionCount(int value)
        {
            SessionAdParamsObjcBridge.setImpressionCount(value);
        }

        public void setClickRate(float value)
        {
            SessionAdParamsObjcBridge.setClickRate((int)value);
        }

        public void setLastAdomain(string value)
        {
            SessionAdParamsObjcBridge.setLastAdomain(!string.IsNullOrEmpty(value) ? value : "");
        }

        public void setIsUserClickedOnLastAd(bool value)
        {
            Debug.Log($"setIsUserClickedOnLastAd() + {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public void setCompletionRate(float value)
        {
            SessionAdParamsObjcBridge.setCompletionRate((int)value);
        }

        public void setLastClickForImpression(int value)
        {
            SessionAdParamsObjcBridge.setLastClickForImpression(value);
        }

        public void setLastBundle(string value)
        {
            SessionAdParamsObjcBridge.setLastBundle(!string.IsNullOrEmpty(value) ? value : "");
        }
    }

    #endregion

    #region PriceFloorParams

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSPriceFloorParams : IPriceFloorParams
    {
        private readonly PriceFloorObjcBridge bridge;

        public iOSPriceFloorParams()
        {
            bridge = new PriceFloorObjcBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void addPriceFloor(double priceFloor)
        {
            Debug.Log($"addPriceFloor() + {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public void addPriceFloor(string uniqueFloorId, double priceFloor)
        {
            PriceFloorObjcBridge.setPriceFloor(!string.IsNullOrEmpty(uniqueFloorId) ? uniqueFloorId : "", priceFloor);
        }
    }

    #endregion

    #region Interstitial

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSInterstitialRequest : IInterstitialRequest
    {
        private readonly InterstitialRequestObjCBridge bridge;

        public iOSInterstitialRequest(IntPtr interstitialRequest)
        {
            bridge = new InterstitialRequestObjCBridge(interstitialRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public string getAuctionResult()
        {
            //TODO implement
            // return bridge.getAuctionResult();
            return null;
        }

        public bool isDestroyed()
        {
            Debug.Log($"isDestroyed() + {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }

        public bool isExpired()
        {
            Debug.Log($"isExpired() + {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        private readonly InterstitialRequestBuilderObjCBridge bridge;

        private static readonly Dictionary<IntPtr, IInterstitialRequestListener> interstitialRequestListeners =
            new Dictionary<IntPtr, IInterstitialRequestListener>();

        public iOSInterstitialRequestBuilder()
        {
            bridge = new InterstitialRequestBuilderObjCBridge();
        }

        private IntPtr GetIntPtr()
        {
            return bridge.getIntPtr();
        }

        public void setNetworks(string networks)
        {
            Debug.Log($"setNetworks() + {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public IInterstitialRequest build()
        {
            return new iOSInterstitialRequest(GetIntPtr());
        }

        public void setAdContentType(AdContentType contentType)
        {
            switch (contentType)
            {
                case AdContentType.All:
                {
                    bridge.setType(2);
                    break;
                }
                case AdContentType.Video:
                {
                    bridge.setType(1);
                    break;
                }
                case AdContentType.Static:
                {
                    bridge.setType(0);
                    break;
                }
                default:
                    bridge.setType(2);
                    break;
            }
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            if (priceFloorParameters == null) return;
            var iOSPriceFloorParams =
                (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            bridge.setPriceFloor(iOSPriceFloorParams.GetIntPtr());
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var iOSSessionAdParams = (iOSSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            bridge.setSessionAdParams(iOSSessionAdParams.GetIntPtr());
        }

        public void setLoadingTimeOut(int value)
        {
            bridge.setLoadingTimeOut(value);
        }

        public void setPlacementId(string placementId)
        {
            bridge.setPlacementId(!string.IsNullOrEmpty(placementId) ? placementId : "");
        }

        public void setBidPayload(string bidPayLoad)
        {
            bridge.setBidPayload(!string.IsNullOrEmpty(bidPayLoad) ? bidPayLoad : "");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            Debug.Log($"setTargetingParams() {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public void setListener(IInterstitialRequestListener interstitialAdRequestListener)
        {
            if (interstitialAdRequestListener == null) return;
            bridge.setInterstitialRequestDelegate(onInterstitialRequestSuccess, onInterstitialRequestFailed,
                onInterstitialRequestExpired);
            interstitialRequestListeners.Add(bridge.getIntPtr(), interstitialAdRequestListener);
        }

        #region InterstitialRequestCallbacks

        [MonoPInvokeCallback(typeof(InterstitialRequestSuccessCallback))]
        private static void onInterstitialRequestSuccess(IntPtr ad, string auctionResult)
        {
            if (!interstitialRequestListeners.ContainsKey(ad)) return;
            interstitialRequestListeners[ad]
                .onInterstitialRequestSuccess(new InterstitialRequest(new iOSInterstitialRequest(ad)), auctionResult);
        }

        [MonoPInvokeCallback(typeof(InterstitialRequestFailedCallback))]
        private static void onInterstitialRequestFailed(IntPtr ad, IntPtr error)
        {
            if (!interstitialRequestListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            interstitialRequestListeners[ad]
                .onInterstitialRequestFailed(new InterstitialRequest(new iOSInterstitialRequest(ad)), err);
        }

        [MonoPInvokeCallback(typeof(InterstitialRequestExpiredCallback))]
        private static void onInterstitialRequestExpired(IntPtr ad)
        {
            if (!interstitialRequestListeners.ContainsKey(ad)) return;
            interstitialRequestListeners[ad]
                .onInterstitialRequestExpired(new InterstitialRequest(new iOSInterstitialRequest(ad)));
        }

        #endregion
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSInterstitialAd : IInterstitialAd
    {
        private static readonly Dictionary<IntPtr, IInterstitialAdListener> interstitialListeners =
            new Dictionary<IntPtr, IInterstitialAdListener>();

        private readonly InterstitialAdObjCBridge bridge;

        public iOSInterstitialAd()
        {
            bridge = new InterstitialAdObjCBridge();
        }

        private iOSInterstitialAd(IntPtr interstitial)
        {
            bridge = new InterstitialAdObjCBridge(interstitial);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.GetIntPtr();
        }

        public bool canShow()
        {
            return bridge.canShow();
        }

        public void destroy()
        {
            bridge.destroy();

            if (interstitialListeners.ContainsKey(bridge.NativeObject))
            {
                interstitialListeners.Remove(bridge.NativeObject);
            }
        }

        public void load(InterstitialRequest interstitialRequest)
        {
            if (interstitialRequest == null) return;
            var iosInterstitialRequest =
                (iOSInterstitialRequest)interstitialRequest.GetInterstitialRequest();
            bridge.load(iosInterstitialRequest.GetIntPtr());
        }

        public void setListener(IInterstitialAdListener interstitialAdListener)
        {
            if (interstitialAdListener == null) return;
            bridge.setDelegate(interstitialAdLoaded, interstitialAdLoadFailed, interstitialAdShown,
                interstitialAdClicked, interstitialAdClosed);
            interstitialListeners.Add(bridge.GetIntPtr(), interstitialAdListener);
        }

        public void show()
        {
            bridge.show();
        }

        #region InterstitialAd Delegate

        [MonoPInvokeCallback(typeof(BidMachineInterstitialFailedCallback))]
        private static void interstitialAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (!interstitialListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            interstitialListeners[ad].onInterstitialAdLoadFailed(new InterstitialAd(new iOSInterstitialAd(ad)), err);
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void interstitialAdLoaded(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onInterstitialAdLoaded(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void interstitialAdClosed(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onInterstitialAdClosed(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void interstitialAdClicked(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onInterstitialAdClicked(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void interstitialAdShown(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onInterstitialAdShown(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        #endregion
    }

    #endregion

    #region Rewarded

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSRewardedRequest : IRewardedRequest
    {
        private readonly RewardedRequestObjCBridge bridge;

        public iOSRewardedRequest(IntPtr rewardedRequest)
        {
            bridge = new RewardedRequestObjCBridge(rewardedRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public string getAuctionResult()
        {
            //TODO Implementation 
            // return bridge.getAuctionResult();
            return null;
        }

        public bool isDestroyed()
        {
            Debug.Log($"isDestroyed() {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }

        public bool isExpired()
        {
            Debug.Log($"isExpired() {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSRewardedRequestBuilder : IRewardedRequestBuilder
    {
        private readonly RewardedRequestBuilderObjCBridge bridge;

        private static readonly Dictionary<IntPtr, IRewardedRequestListener> rewardedRequestListeners =
            new Dictionary<IntPtr, IRewardedRequestListener>();

        public iOSRewardedRequestBuilder()
        {
            bridge = new RewardedRequestBuilderObjCBridge();
        }

        private IntPtr GetIntPtr()
        {
            return bridge.getIntPtr();
        }

        public void setNetworks(string networks)
        {
            Debug.Log($"setNetworks() {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public IRewardedRequest build()
        {
            return new iOSRewardedRequest(GetIntPtr());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            if (priceFloorParameters == null) return;
            var iOSPriceFloorParams =
                (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            bridge.setPriceFloor(iOSPriceFloorParams.GetIntPtr());
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var sessionAdParamsObjcBridge = (iOSSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            bridge.setSessionAdParams(sessionAdParamsObjcBridge.GetIntPtr());
        }

        public void setLoadingTimeOut(int value)
        {
            bridge.setLoadingTimeOut(value);
        }

        public void setPlacementId(string placementId)
        {
            bridge.setPlacementId(!string.IsNullOrEmpty(placementId) ? placementId : "");
        }

        public void setBidPayload(string bidPayLoad)
        {
            bridge.setBidPayload(!string.IsNullOrEmpty(bidPayLoad) ? bidPayLoad : "");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            Debug.Log($"setTargetingParams() {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public void setListener(IRewardedRequestListener rewardedAdRequestListener)
        {
            if (rewardedAdRequestListener == null) return;
            bridge.setRewardedRequestDelegate(onRewardedRequestSuccess, onRewardedRequestFailed,
                onRewardedRequestExpired);
            rewardedRequestListeners.Add(bridge.getIntPtr(), rewardedAdRequestListener);
        }

        #region RewardedRequestCallbacks

        [MonoPInvokeCallback(typeof(RewardedRequestSuccessCallback))]
        private static void onRewardedRequestSuccess(IntPtr ad, string auctionResult)
        {
            if (!rewardedRequestListeners.ContainsKey(ad)) return;
            rewardedRequestListeners[ad]
                .onRewardedRequestSuccess(new RewardedRequest(new iOSRewardedRequest(ad)), auctionResult);
        }

        [MonoPInvokeCallback(typeof(RewardedRequestFailedCallback))]
        private static void onRewardedRequestFailed(IntPtr ad, IntPtr error)
        {
            if (!rewardedRequestListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            rewardedRequestListeners[ad].onRewardedRequestFailed(new RewardedRequest(new iOSRewardedRequest(ad)), err);
        }

        [MonoPInvokeCallback(typeof(RewardedRequestExpiredCallback))]
        private static void onRewardedRequestExpired(IntPtr ad)
        {
            if (!rewardedRequestListeners.ContainsKey(ad)) return;
            rewardedRequestListeners[ad].onRewardedRequestExpired(new RewardedRequest(new iOSRewardedRequest(ad)));
        }

        #endregion
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSRewardedAd : IRewardedAd
    {
        private static readonly Dictionary<IntPtr, IRewardedAdListener> rewardedListeners =
            new Dictionary<IntPtr, IRewardedAdListener>();

        private readonly RewardedAdObjCBridge bridge;

        public iOSRewardedAd()
        {
            bridge = new RewardedAdObjCBridge();
        }

        private iOSRewardedAd(IntPtr ad)
        {
            bridge = new RewardedAdObjCBridge(ad);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.GetIntPtr();
        }

        public bool canShow()
        {
            return bridge.canShow();
        }

        public void destroy()
        {
            bridge.destroy();

            if (rewardedListeners.ContainsKey(bridge.NativeObject))
            {
                rewardedListeners.Remove(bridge.NativeObject);
            }
        }

        public void load(RewardedRequest rewardedRequest)
        {
            if (rewardedRequest == null) return;
            var rewardedAdRequest = (iOSRewardedRequest)rewardedRequest.GetRewardedRequest();
            bridge.load(rewardedAdRequest.GetIntPtr());
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            if (rewardedAdListener == null) return;
            bridge.setDelegate(rewardedAdLoaded, rewardedAdLoadFailed, rewardedAdShown, rewardedAdClicked,
                rewardedAdClosed);
            rewardedListeners.Add(bridge.GetIntPtr(), rewardedAdListener);
        }

        public void show()
        {
            bridge.show();
        }

        #region RewardedDelegate

        [MonoPInvokeCallback(typeof(BidMachineRewardedFailedCallback))]
        private static void rewardedAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (!rewardedListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            rewardedListeners[ad].onRewardedAdLoadFailed(new RewardedAd(new iOSRewardedAd(ad)), err);
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void rewardedAdLoaded(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onRewardedAdLoaded(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void rewardedAdClosed(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onRewardedAdClosed(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void rewardedAdClicked(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onRewardedAdClicked(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        private static void rewardedAdShown(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onRewardedAdShown(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        #endregion
    }

    #endregion

    #region Banner

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSBannerViewRequest : IBannerRequest
    {
        private readonly BannerViewRequestObjCBridge bridge;

        public iOSBannerViewRequest(IntPtr bannerViewRequest)
        {
            bridge = new BannerViewRequestObjCBridge(bannerViewRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public BannerSize getSize()
        {
            //TODO NEED TO IMPLEMENT
            //return bridge.getSize();
            return BannerSize.Size_300х250;
        }

        public string getAuctionResult()
        {
            //TODO NEED TO IMPLEMENT
            //return bridge.getAuctionResult();
            return null;
        }

        public bool isDestroyed()
        {
            Debug.Log($"isDestroyed() {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }

        public bool isExpired()
        {
            Debug.Log($"isExpired() {iOSBidMachine.DUMMY_MESSAGE}");
            return false;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class iOSBannerViewRequestBuilder : IBannerRequestBuilder
    {
        private readonly BannerViewRequestBuilderObjCBridge bridge;

        private static readonly Dictionary<IntPtr, IBannerRequestListener> bannerRequestListeners =
            new Dictionary<IntPtr, IBannerRequestListener>();

        public iOSBannerViewRequestBuilder()
        {
            bridge = new BannerViewRequestBuilderObjCBridge();
        }

        private IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setNetworks(string networks)
        {
            Debug.Log($"setNetworks() {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public IBannerRequest build()
        {
            return new iOSBannerViewRequest(GetIntPtr());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            if (priceFloorParameters == null) return;
            var iOSPriceFloorParams =
                (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            bridge.setPriceFloor(iOSPriceFloorParams.GetIntPtr());
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var iOSsAdParams = (iOSSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            bridge.setSessionAdParams(iOSsAdParams.GetIntPtr());
        }

        public void setLoadingTimeOut(int value)
        {
            bridge.setLoadingTimeOut(value);
        }

        public void setPlacementId(string placementId)
        {
            bridge.setPlacementId(!string.IsNullOrEmpty(placementId) ? placementId : "");
        }

        public void setBidPayload(string bidPayLoad)
        {
            bridge.setBidPayload(!string.IsNullOrEmpty(bidPayLoad) ? bidPayLoad : "");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            Debug.Log($"setTargetingParams() {iOSBidMachine.DUMMY_MESSAGE}");
        }

        public void setSize(BannerSize size)
        {
            switch (size)
            {
                case BannerSize.Size_320х50:
                    bridge.setBannerSize(0);
                    break;
                case BannerSize.Size_300х250:
                    bridge.setBannerSize(1);
                    break;
                case BannerSize.Size_728х90:
                    bridge.setBannerSize(2);
                    break;
                default:
                    bridge.setBannerSize(0);
                    break;
            }
        }

        public void setListener(IBannerRequestListener bannerAdRequestListener)
        {
            if (bannerAdRequestListener == null) return;
            bridge.setBannerRequestDelegate(onBannerRequestSuccess, onBannerRequestFailed, onBannerRequestExpired);
            bannerRequestListeners.Add(bridge.GetIntPtr(), bannerAdRequestListener);
        }

        #region BannerRequestCallbacks

        [MonoPInvokeCallback(typeof(BannerRequestSuccessCallback))]
        private static void onBannerRequestSuccess(IntPtr ad, string auctionResult)
        {
            if (!bannerRequestListeners.ContainsKey(ad)) return;
            bannerRequestListeners[ad]
                .onBannerRequestSuccess(new BannerRequest(new iOSBannerViewRequest(ad)), auctionResult);
        }

        [MonoPInvokeCallback(typeof(BannerRequestFailedCallback))]
        private static void onBannerRequestFailed(IntPtr ad, IntPtr error)
        {
            if (!bannerRequestListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            bannerRequestListeners[ad].onBannerRequestFailed(new BannerRequest(new iOSBannerViewRequest(ad)), err);
        }

        [MonoPInvokeCallback(typeof(BannerRequestExpiredCallback))]
        private static void onBannerRequestExpired(IntPtr ad)
        {
            if (!bannerRequestListeners.ContainsKey(ad)) return;
            bannerRequestListeners[ad].onBannerRequestExpired(new BannerRequest(new iOSBannerViewRequest(ad)));
        }

        #endregion
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSBanner : IBanner
    {
        private readonly BannerViewObjCBridge bridge;

        public iOSBanner()
        {
            bridge = new BannerViewObjCBridge();
        }

        public IBannerView getBannerView()
        {
            return new iOSBannerView();
        }

        public IBannerView getBannerView(IntPtr bannerView)
        {
            return new iOSBannerView(bannerView);
        }

        public void hideBannerView()
        {
            bridge.destroy();
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            bridge.show(YAxis, XAxis);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSBannerView : IBannerView
    {
        private static readonly Dictionary<IntPtr, IBannerListener> bannerListeners =
            new Dictionary<IntPtr, IBannerListener>();

        private readonly BannerViewObjCBridge bridge;

        public iOSBannerView()
        {
            bridge = new BannerViewObjCBridge();
        }

        public iOSBannerView(IntPtr ad)
        {
            bridge = new BannerViewObjCBridge(ad);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.GetIntPtr();
        }

        public bool canShow()
        {
            return bridge.canShow();
        }

        public void destroy()
        {
            bridge.destroy();

            if (bannerListeners.ContainsKey(bridge.NativeObject))
            {
                bannerListeners.Remove(bridge.NativeObject);
            }
        }

        public void load(BannerRequest bannerRequest)
        {
            if (bannerRequest == null) return;
            var bannerViewRequest = (iOSBannerViewRequest)bannerRequest.GetBannerRequest();
            bridge.load(bannerViewRequest.GetIntPtr());
        }

        public void setListener(IBannerListener bannerListener)
        {
            if (bannerListener == null) return;
            bridge.setDelegate(bannerViewLoaded, bannerAdLoadFailed, bannerClicked);
            bannerListeners.Add(bridge.GetIntPtr(), bannerListener);
        }

        #region InterstitialAd Delegate

        [MonoPInvokeCallback(typeof(BidMachineBannerFailedCallback))]
        private static void bannerAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (!bannerListeners.ContainsKey(ad)) return;
            var err = new BMError
            {
                code = BidMachineObjCBridge.BidMachineGetErrorCode(error),
                message = BidMachineObjCBridge.BidMachineGetErrorMessage(error)
            };
            bannerListeners[ad].onBannerAdLoadFailed(new BannerView(new iOSBannerView(ad)), err);
        }

        [MonoPInvokeCallback(typeof(BidMachineBannerCallbacks))]
        private static void bannerViewLoaded(IntPtr ad)
        {
            if (bannerListeners.ContainsKey(ad))
            {
                bannerListeners[ad].onBannerAdLoaded(new BannerView(new iOSBannerView(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineBannerCallbacks))]
        private static void bannerClicked(IntPtr ad)
        {
            if (bannerListeners.ContainsKey(ad))
            {
                bannerListeners[ad].onBannerAdClicked(new BannerView(new iOSBannerView(ad)));
            }
        }

        #endregion
    }

    #endregion
}
#endif