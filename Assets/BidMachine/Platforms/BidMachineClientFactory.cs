using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Dummy;

namespace BidMachineAds.Unity
{
    internal class BidMachineClientFactory
    {
        internal static IBidMachine GetBidMachine()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidBidMachine();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return BidMachineAds.Unity.iOS.iOSBidMachine.Instance;
#else
            return new DummyBidMachine();
#endif
        }

        internal static IBannerAd GetBannerAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidBannerAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSBannerView();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IBannerRequestBuilder GetBannerRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidBannerRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSBannerViewRequestBuilder();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IInterstitialAd GetInterstitialAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidInterstitialAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSInterstitialAd();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IInterstitialRequestBuilder GetInterstitialRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidInterstitialRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSInterstitialRequestBuilder();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IRewardedAd GetRewardedAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidRewardedAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSRewardedAd();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IRewardedRequestBuilder GetRewardedRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidRewardedRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new BidMachineAds.Unity.iOS.iOSRewardedRequestBuilder();
#else
            return new DummyBidMachine();
#endif
        }
    }
}
