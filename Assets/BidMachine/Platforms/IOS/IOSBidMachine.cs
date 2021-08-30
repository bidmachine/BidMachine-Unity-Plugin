#if UNITY_IPHONE
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using BidMachineAds.Unity.iOS;
using UnityEngine.iOS;
using System.Collections.Generic;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBidMachine : IBidMachine
    {
        const int GENDER_MALE = 1;
        const int GENDER_FERMALE = 2;
        const int GENDER_UNKNOWN = 3;

        #region Singleton
        iOSBidMachine() { }
        static readonly iOSBidMachine instance = new iOSBidMachine();
        public static iOSBidMachine Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public void initialize(string sellerId)
        {
            BidMachineObjCBridge.BidMachineInitialize(sellerId);
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

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            BidMachineObjCBridge.BidMachineSetGdprRequired(subjectToGDPR);
        }

        public void setConsentConfig(bool consent, string GDPRConsentString)
        {
            BidMachineObjCBridge.BidMachineSetConsentString(consent, GDPRConsentString);
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
            iOSTargetingParams targetingObjcBridge = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            IntPtr iTargetingParams = targetingObjcBridge.GetIntPtr();
            BidMachineObjCBridge.BidMachineSetTargeting(iTargetingParams);
        }
    }

    public class iOSTargetingParams : ITargetingParams
    {
        private TargetingObjcBridge bridge;

        public iOSTargetingParams()
        {
            bridge = new TargetingObjcBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setBirthdayYear(int year)
        {
            bridge.setBirthdayYear(year);
        }

        public void setBlockedAdvertiserDomain(string domains)
        {
            bridge.setBlockedAdvertiserDomain(domains);
        }

        public void setBlockedAdvertiserIABCategories(string categories)
        {
            bridge.setBlockedAdvertiserIABCategories(categories);
        }

        public void setBlockedApplication(string applications)
        {
            bridge.setBlockedApplication(applications);
        }

        public void setCity(string city)
        {
            bridge.setCity(city);
        }

        public void setCountry(string country)
        {
            bridge.setCountry(country);
        }

        public void setDeviceLocation(double latitude, double longitude)
        {
            bridge.setDeviceLocation(longitude, latitude);
        }

        public void setGender(Api.TargetingParams.Gender gender)
        {
            switch (gender)
            {
                case Api.TargetingParams.Gender.Omitted:
                    {
                        bridge.setGender(3);
                        break;
                    }
                case Api.TargetingParams.Gender.Male:
                    {
                        bridge.setGender(1);
                        break;
                    }
                case Api.TargetingParams.Gender.Female:
                    {
                        bridge.setGender(2);
                        break;
                    }
            }
        }

        public void setKeyWords(string[] keyWords)
        {
            bridge.setKeyWords(keyWords);
        }

        public void setPaid(bool paid)
        {
            bridge.setPaid(paid);
        }

        public void setStoreUrl(string storeUrl)
        {
            bridge.setStoreUrl(storeUrl);
        }

        public void setUserId(string id)
        {
            bridge.setUserId(id);
        }

        public void setZip(string zip)
        {
            bridge.setZip(zip);
        }
    }

    public class iOSPriceFloorParams : IPriceFloorParams
    {
        private PriceFloorObjcBridge bridge;

        public iOSPriceFloorParams()
        {
            bridge = new PriceFloorObjcBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public void setPriceFloor(string uniqueFloorId, double priceFloor)
        {
            bridge.setPriceFloor(uniqueFloorId, priceFloor);
        }

        public void setPriceFloor(double priceFloor)
        {
            Debug.Log("Support only on Android platform");
        }
    }

    public class iOSInterstitialRequest : IInterstitialRequest
    {
        private IntertitialRequestObjCBridge bridge;

        public iOSInterstitialRequest(IntPtr interstitialRequest)
        {
            bridge = new IntertitialRequestObjCBridge(interstitialRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }
    }

    public class iOSInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        private InterstitialRequestBuilderObjCBridge bridge;

        public iOSInterstitialRequestBuilder()
        {
            bridge = new InterstitialRequestBuilderObjCBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public IInterstitialRequest build()
        {
            return new iOSInterstitialRequest(GetIntPtr());
        }

        public void setAdContentType(InterstitialRequestBuilder.ContentType contentType)
        {
            switch (contentType)
            {
                case InterstitialRequestBuilder.ContentType.All:
                    {
                        bridge.setType(2);
                        break;
                    }
                case InterstitialRequestBuilder.ContentType.Video:
                    {
                        bridge.setType(1);
                        break;
                    }
                case InterstitialRequestBuilder.ContentType.Static:
                    {
                        bridge.setType(0);
                        break;
                    }
            }
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            iOSPriceFloorParams iOSPriceFloorParams = (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            IntPtr iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            iOSTargetingParams iOSTargetingParams = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            IntPtr iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
        }
    }

    public class iOSInterstitialAd : IInterstitialAd
    {
        public static Dictionary<IntPtr, IInterstitialAdListener> interstitialListeners = new Dictionary<IntPtr, IInterstitialAdListener>();

        private InterstitialAdObjCBridge bridge;

        public iOSInterstitialAd()
        {
            bridge = new InterstitialAdObjCBridge();
        }

        public iOSInterstitialAd(IntPtr interstitial)
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
            iOSInterstitialRequest iosInterstitialRequest = (iOSInterstitialRequest)interstitialRequest.GetInterstitialRequest();
            IntPtr iIntertitialRequestObjCBridge = iosInterstitialRequest.GetIntPtr();
            bridge.load(iIntertitialRequestObjCBridge);
        }

        public void setListener(IInterstitialAdListener interstitialAdListener)
        {
            bridge.setDelegate(interstitialAdLoaded, interstitialAdLoadFailed, interstitialAdShown, interstitialAdClicked, interstitialAdClosed);
            interstitialListeners.Add(bridge.GetIntPtr(), interstitialAdListener);
        }

        public void show()
        {
            bridge.show();
        }

        #region InterstitialAd Delegate
        [MonoPInvokeCallback(typeof(BidMachineInterstitialFailedCallback))]
        static void interstitialAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                BMError err = new BMError();
                err.code = BidMachineObjCBridge.BidMachineGetErrorCode(error);
                err.brief = BidMachineObjCBridge.BidMachineGetErrorBrief(error);
                err.message = BidMachineObjCBridge.BidMachineGetErrorMessage(error);
                interstitialListeners[ad].onAdLoadFailed(new InterstitialAd(new iOSInterstitialAd(ad)), err);
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void interstitialAdLoaded(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onAdLoaded(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void interstitialAdClosed(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onAdClosed(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void interstitialAdClicked(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onAdClicked(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void interstitialAdShown(IntPtr ad)
        {
            if (interstitialListeners.ContainsKey(ad))
            {
                interstitialListeners[ad].onAdShown(new InterstitialAd(new iOSInterstitialAd(ad)));
            }
        }
        #endregion
    }

    public class iOSRewardedRequest : IRewardedRequest
    {
        private RewardedRequestObjCBridge bridge;

        public iOSRewardedRequest(IntPtr rewardedRequest)
        {
            bridge = new RewardedRequestObjCBridge(rewardedRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }
    }

    public class iOSRewardedRequestBuilder : IRewardedRequestBuilder
    {
        private RewardedRequestBuilderObjCBridge bridge;

        public iOSRewardedRequestBuilder()
        {
            bridge = new RewardedRequestBuilderObjCBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public IRewardedRequest build()
        {
            return new iOSRewardedRequest(GetIntPtr());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            iOSPriceFloorParams iOSPriceFloorParams = (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            IntPtr iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            iOSTargetingParams iOSTargetingParams = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            IntPtr iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
        }
    }

    public class iOSRewardedAd : IRewardedAd
    {
        public static Dictionary<IntPtr, IRewardedAdListener> rewardedListeners = new Dictionary<IntPtr, IRewardedAdListener>();

        private RewardedAdObjCBridge bridge;

        public iOSRewardedAd()
        {
            bridge = new RewardedAdObjCBridge();
        }

        public iOSRewardedAd(IntPtr ad)
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
            iOSRewardedRequest rewardedAdRequest = (iOSRewardedRequest)rewardedRequest.GetRewardedRequest();
            IntPtr iRewardedRequestObjCBridge = rewardedAdRequest.GetIntPtr();
            bridge.load(iRewardedRequestObjCBridge);
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            bridge.setDelegate(rewardedAdLoaded, rewardedAdLoadFailed, rewardedAdShown, rewardedAdClicked, rewardedAdClosed);
            rewardedListeners.Add(bridge.GetIntPtr(), rewardedAdListener);
        }

        public void show()
        {
            bridge.show();
        }

        #region InterstitialAd Delegate
        [MonoPInvokeCallback(typeof(BidMachineRewardedFailedCallback))]
        static void rewardedAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                BMError err = new BMError();
                err.code = BidMachineObjCBridge.BidMachineGetErrorCode(error);
                err.brief = BidMachineObjCBridge.BidMachineGetErrorBrief(error);
                err.message = BidMachineObjCBridge.BidMachineGetErrorMessage(error);
                rewardedListeners[ad].onAdLoadFailed(new RewardedAd(new iOSRewardedAd(ad)), err);
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void rewardedAdLoaded(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onAdLoaded(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void rewardedAdClosed(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onAdClosed(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void rewardedAdClicked(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onAdClicked(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineInterstitialCallbacks))]
        static void rewardedAdShown(IntPtr ad)
        {
            if (rewardedListeners.ContainsKey(ad))
            {
                rewardedListeners[ad].onAdShown(new RewardedAd(new iOSRewardedAd(ad)));
            }
        }
        #endregion
    }

    public class iOSBannerViewRequest : IBannerRequest
    {
        private BannerViewRequestObjCBridge bridge;

        public iOSBannerViewRequest()
        {
        }

        public iOSBannerViewRequest(IntPtr bannerViewRequest)
        {
            bridge = new BannerViewRequestObjCBridge(bannerViewRequest);
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }
    }

    public class iOSBannerViewRequestBuilder : IBannerRequestBuilder
    {
        private BannerViewRequestBuilderObjCBridge bridge;

        public iOSBannerViewRequestBuilder()
        {
            bridge = new BannerViewRequestBuilderObjCBridge();
        }

        public IntPtr GetIntPtr()
        {
            return bridge.getNativeObject();
        }

        public IBannerRequest build()
        {
            return new iOSBannerViewRequest(GetIntPtr());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            iOSPriceFloorParams iOSPriceFloorParams = (iOSPriceFloorParams)priceFloorParameters.GetNativePriceFloorParams();
            IntPtr iPriceFloor = iOSPriceFloorParams.GetIntPtr();
            bridge.setPriceFloor(iPriceFloor);
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            iOSTargetingParams iOSTargetingParams = (iOSTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            IntPtr iTargetingParams = iOSTargetingParams.GetIntPtr();
            bridge.setTargetingParams(iTargetingParams);
        }

        public void setSize(BannerRequestBuilder.Size size)
        {
            switch (size)
            {
                case BannerRequestBuilder.Size.Size_320_50:
                    bridge.setBannerSize(0);
                    break;
                case BannerRequestBuilder.Size.Size_300_250:
                    bridge.setBannerSize(1);
                    break;
                case BannerRequestBuilder.Size.Size_728_90:
                    bridge.setBannerSize(2);
                    break;
            }
        }
    }

    public class iOSBanner : IBanner
    {
        private BannerViewObjCBridge bridge;

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


    public class iOSBannerView : IBannerView
    {
        public static Dictionary<IntPtr, IBannerListener> bannerListeners = new Dictionary<IntPtr, IBannerListener>();

        private BannerViewObjCBridge bridge;

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
            iOSBannerViewRequest bannerViewRequest = (iOSBannerViewRequest)bannerRequest.GetBannerRequest();
            IntPtr iBannerViewRequest = bannerViewRequest.GetIntPtr();
            bridge.load(iBannerViewRequest);
        }

        public void setListener(IBannerListener bannerListener)
        {
            bridge.setDelegate(bannerViewLoaded, bannerAdLoadFailed, bannerClicked);
            bannerListeners.Add(bridge.GetIntPtr(), bannerListener);
        }

      

        #region InterstitialAd Delegate
        [MonoPInvokeCallback(typeof(BidMachineBannerFailedCallback))]
        static void bannerAdLoadFailed(IntPtr ad, IntPtr error)
        {
            if (bannerListeners.ContainsKey(ad))
            {
                BMError err = new BMError();
                err.code = BidMachineObjCBridge.BidMachineGetErrorCode(error);
                err.brief = BidMachineObjCBridge.BidMachineGetErrorBrief(error);
                err.message = BidMachineObjCBridge.BidMachineGetErrorMessage(error);
                bannerListeners[ad].onAdLoadFailed(new BannerView(new iOSBannerView(ad)), err);
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineBannerCallbacks))]
        static void bannerViewLoaded(IntPtr ad)
        {
            if (bannerListeners.ContainsKey(ad))
            {
                bannerListeners[ad].onAdLoaded(new BannerView(new iOSBannerView(ad)));
            }
        }

        [MonoPInvokeCallback(typeof(BidMachineBannerCallbacks))]
        static void bannerClicked(IntPtr ad)
        {
            if (bannerListeners.ContainsKey(ad))
            {
                bannerListeners[ad].onAdClicked(new BannerView(new iOSBannerView(ad)));
            }
        }

        #endregion
    }
}
#endif
