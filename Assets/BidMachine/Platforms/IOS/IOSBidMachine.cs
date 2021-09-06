#if UNITY_IPHONE
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class iOSBidMachine : IBidMachine
    {
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
            if (!string.IsNullOrEmpty(url))
            {
                BidMachineObjCBridge.BidMachineSetEndpoint(url);
            }
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
            if (!string.IsNullOrEmpty(usPrivacyString))
            {
                BidMachineObjCBridge.BidMachineSetUSPrivacyString(usPrivacyString);
            }
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
            if (!string.IsNullOrEmpty(GDPRConsentString))
            {
                BidMachineObjCBridge.BidMachineSetConsentString(consent, GDPRConsentString);
            }
        }

        public bool checkAndroidPermissions(string permission)
        {
            Debug.Log("This method doesn't support on this platform");
            return false;
        }

        public void requestAndroidPermissions()
        {
            Debug.Log("This method doesn't support on this platform");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var targetingObjcBridge = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            var iTargetingParams = targetingObjcBridge.GetIntPtr();
            BidMachineObjCBridge.BidMachineSetTargeting(iTargetingParams);
        }
    }

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
            TargetingObjcBridge.setUserId(!string.IsNullOrEmpty(id) ? id : "null");
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
            TargetingObjcBridge.setCountry(!string.IsNullOrEmpty(country) ? country : "empty");
        }

        public void setCity(string city)
        {
            TargetingObjcBridge.setCity(!string.IsNullOrEmpty(city) ? city : "empty");
        }

        public void setZip(string zip)
        {
            TargetingObjcBridge.setZip(!string.IsNullOrEmpty(zip) ? zip : "empty");
        }

        public void setStoreUrl(string storeUrl)
        {
            TargetingObjcBridge.setStoreUrl(!string.IsNullOrEmpty(storeUrl) ? storeUrl : "empty");
        }

        public void setStoreCategory(string storeCategory)
        {
            TargetingObjcBridge.setStoreCategory(!string.IsNullOrEmpty(storeCategory) ? storeCategory : "empty");
        }

        public void setStoreSubCategories(string[] storeSubCategories)
        {
            TargetingObjcBridge.setStoreSubCategories(storeSubCategories.Length > 0
                ? storeSubCategories
                : new[] { "empty" });
        }

        public void setStoreId(string storeId)
        {
            TargetingObjcBridge.setStoreId(storeId);
        }

        public void setFramework(string framework)
        {
            TargetingObjcBridge.setFramework(!string.IsNullOrEmpty(framework) ? framework : "empty");
        }

        public void setPaid(bool paid)
        {
            TargetingObjcBridge.setPaid(paid);
        }

        public void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            TargetingObjcBridge.setDeviceLocation(providerName, latitude, longitude);
        }

        public void setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            TargetingObjcBridge.setExternalUserIds(externalUserIdList.ToList());
        }

        public void addBlockedApplication(string bundleOrPackage)
        {
            TargetingObjcBridge.addBlockedApplication(bundleOrPackage);
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            TargetingObjcBridge.addBlockedAdvertiserIABCategory(category);
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            TargetingObjcBridge.addBlockedAdvertiserDomain(domain);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
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
            bridge.setSessionDuration(value);
        }

        public void setImpressionCount(int value)
        {
            bridge.setImpressionCount(value);
        }

        public void setClickRate(float value)
        {
            bridge.setClickRate((int)value);
        }

        public void setLastAdomain(string value)
        {
            bridge.setLastAdomain(value);
        }

        public void setIsUserClickedOnLastAd(bool value)
        {
            Debug.Log("Not support on iOS platform");
        }

        public void setCompletionRate(float value)
        {
            bridge.setCompletionRate((int)value);
        }

        public void setLastClickForImpression(int value)
        {
            bridge.setLastClickForImpression(value);
        }

        public void setLastBundle(string value)
        {
            bridge.setLastBundle(value);
        }
    }


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
            Debug.Log("Support only on Android platform");

        }

        public void addPriceFloor(string uniqueFloorId, double priceFloor)
        {
            bridge.setPriceFloor(uniqueFloorId, priceFloor);
        }
    }

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
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        private readonly InterstitialRequestBuilderObjCBridge bridge;

        public iOSInterstitialRequestBuilder()
        {
            bridge = new InterstitialRequestBuilderObjCBridge();
        }

        private IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setNetworks(string networks)
        {
           Debug.Log("Not support on iOS platform");
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
            var iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setListener(IInterstitialRequestListener bannerRequestListener)
        {
            //TODO implementation
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var iOSSessionAdParams = (iOSSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            var iSessionAdParams = iOSSessionAdParams.GetIntPtr();
            bridge.setSessionAdParams(iSessionAdParams);
        }

        public void setLoadingTimeOut(int value)
        {
            bridge.setLoadingTimeOut(value);

        }

        public void setPlacementId(string placementId)
        {
            bridge.setPlacementId(placementId);

        }

        public void setBidPayload(string bidPayLoad)
        {
            bridge.setBidPayload(bidPayLoad);
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var iOSTargetingParams =
                (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            var iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
        }
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
            if (interstitialListeners.ContainsKey(bridge.nativeObject))
            {
                interstitialListeners.Remove(bridge.nativeObject);
            }
        }

        public void load(InterstitialRequest interstitialRequest)
        {
            if (interstitialRequest == null) return;
            var iosInterstitialRequest =
                (iOSInterstitialRequest)interstitialRequest.GetInterstitialRequest();
            var InterstitialRequestObjCBridge = iosInterstitialRequest.GetIntPtr();
            bridge.load(InterstitialRequestObjCBridge);
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
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSRewardedRequestBuilder : IRewardedRequestBuilder
    {
        private readonly RewardedRequestBuilderObjCBridge bridge;

        public iOSRewardedRequestBuilder()
        {
            bridge = new RewardedRequestBuilderObjCBridge();
        }

        private IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setNetworks(string networks)
        {
            throw new NotImplementedException();
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
            var iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setListener(IRewardedRequestListener rewardedRequestListener)
        {
            if (rewardedRequestListener != null)
            {
                //bridge.setListener(rewardedRequestListener);
            }
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams != null)
            {
                //bridge.setSessionAdParams(sessionAdParams);
            }
        }

        public void setLoadingTimeOut(int value)
        {
            throw new NotImplementedException();
        }

        public void setPlacementId(string placementId)
        {
            if (!string.IsNullOrEmpty(placementId))
            {
                //bridge.setPlacementId(placementId);
            }
        }

        public void setBidPayload(string bidPayLoad)
        {
            if (!string.IsNullOrEmpty(bidPayLoad))
            {
                //bridge.setBidPayload(bidPayLoad);
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var iOSTargetingParams =
                (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            var iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
        }
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
            if (rewardedListeners.ContainsKey(bridge.nativeObject))
            {
                rewardedListeners.Remove(bridge.nativeObject);
            }
        }

        public void load(RewardedRequest rewardedRequest)
        {
            if (rewardedRequest == null) return;
            var rewardedAdRequest = (iOSRewardedRequest)rewardedRequest.GetRewardedRequest();
            var iRewardedRequestObjCBridge = rewardedAdRequest.GetIntPtr();
            bridge.load(iRewardedRequestObjCBridge);
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

        #region InterstitialAd Delegate

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
            return BannerSize.Size_300х250;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSBannerViewRequestBuilder : IBannerRequestBuilder
    {
        private readonly BannerViewRequestBuilderObjCBridge bridge;

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
            throw new NotImplementedException();
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
            var iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setListener(IBannerRequestListener bannerRequestListener)
        {
            if (bannerRequestListener != null)
            {
                //bridge.setListener(sessionAdParams);
            }
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams != null)
            {
                //bridge.setSessionAdParams(sessionAdParams);
            }
        }

        public void setLoadingTimeOut(int value)
        {
            //bridge.setLoadingTimeOut(value);
        }

        public void setPlacementId(string placementId)
        {
            if (!string.IsNullOrEmpty(placementId))
            {
                //bridge.setPlacementId(placementId);
            }
        }

        public void setBidPayload(string bidPayLoad)
        {
            if (!string.IsNullOrEmpty(bidPayLoad))
            {
                //bridge.setBidPayload(bidPayLoad);
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var iOSTargetingParams =
                (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            var iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
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
            if (bannerListeners.ContainsKey(bridge.nativeObject))
            {
                bannerListeners.Remove(bridge.nativeObject);
            }
        }

        public void load(BannerRequest bannerRequest)
        {
            if (bannerRequest == null) return;
            var bannerViewRequest = (iOSBannerViewRequest)bannerRequest.GetBannerRequest();
            var iBannerViewRequest = bannerViewRequest.GetIntPtr();
            bridge.load(iBannerViewRequest);
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
}
#endif