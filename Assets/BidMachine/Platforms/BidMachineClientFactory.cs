// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Dummy;

namespace BidMachineAds.Unity
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    internal class BidMachineClientFactory
    {
        internal static IBidMachine GetBidMachine()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidBidMachine();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return BidMachineAds.Unity.iOS.iOSBidMachine.Instance;
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static ITargetingParams GetTargetingParams()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidTargetingParams();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSTargetingParams();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IPriceFloorParams GetPriceFloorParametrs()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidPriceFloorParams();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSPriceFloorParams();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static ISessionAdParams GetSessionAdParams()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidSessionAdParams();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSSessionAdParams();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IInterstitialRequestBuilder GetInterstitialRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidInterstitialRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSInterstitialRequestBuilder();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IInterstitialAd GetInterstitialAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidInterstitialAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSInterstitialAd();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IRewardedRequestBuilder GetRewardedRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidRewardedRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSRewardedRequestBuilder();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }
        
        internal static INativeRequestBuilder GetNativeRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidNativeRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSNativeRequestBuilder();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IRewardedAd GetRewardedAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidRewardedAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSRewardedAd();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }
        
        internal static INativeAd GetNativeAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidNativeAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSNativeAd();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IBannerRequestBuilder GetBannerRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidBannerRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
        return new BidMachineAds.Unity.iOS.iOSBannerViewRequestBuilder();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IBannerView GetAndroidBannerView()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidBanner().getBannerView();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSBannerView();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }

        internal static IBanner GetAndroidBanner()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          return new BidMachineAds.Unity.Android.AndroidBanner();
#elif UNITY_IPHONE && !UNITY_EDITOR
          return new BidMachineAds.Unity.iOS.iOSBanner();
#else
            return new BidMachineAds.Unity.Dummy.DummyBidMachine();
#endif
        }
    }
}