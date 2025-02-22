using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Dummy;

namespace BidMachineAds.Unity
{
    internal class BidMachineClientFactory
    {
        internal static IBidMachine GetBidMachine()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBidMachine();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSBidMachine();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IBannerView GetBannerView()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerView();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSBannerAd();
#else
            return new DummyBannerAd();
#endif
        }

        internal static IBannerRequestBuilder GetBannerRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSBannerRequestBuilder();
#else
            return new DummyBannerRequestBuilder();
#endif
        }

        internal static IInterstitialAd GetInterstitialAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialAd();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSInterstitialAd();
#else
            return new DummyInterstitialAd();
#endif
        }

        internal static IAdRequestBuilder GetInterstitialRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSInterstitialRequestBuilder();
#else
            return new DummyInterstitialRequestBuilder();
#endif
        }

        internal static IRewardedAd GetRewardedAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedAd();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSRewardedAd();
#else
            return new DummyRewardedAd();
#endif
        }

        internal static IAdRequestBuilder GetRewardedRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new iOS.iOSRewardedRequestBuilder();
#else
            return new DummyRewardedRequestBuilder();
#endif
        }
    }
}
