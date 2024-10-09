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
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSBidMachine();
#else
            return new DummyBidMachine();
#endif
        }

        internal static IBannerView GetBannerView()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerView();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSBannerAd();
#else
            return new DummuBannerAd();
#endif
        }

        internal static IBannerRequestBuilder GetBannerRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSBannerRequestBuilder();
#else
            return new DummuBannerRequestBuilder();
#endif
        }

        internal static IFullscreenAd GetInterstitialAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSInterstitialAd();
#else
            return new DummuInterstitialAd();
#endif
        }

        internal static IAdRequestBuilder GetInterstitialRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSInterstitialRequestBuilder();
#else
            return new DummuInterstitialRequestBuilder();
#endif
        }

        internal static IFullscreenAd GetRewardedAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedAd();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSRewardedAd();
#else
            return new DummuRewardedAd();
#endif
        }

        internal static IAdRequestBuilder GetRewardedRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedRequestBuilder();
#elif UNITY_IPHONE && !UNITY_EDITOR
            return new iOS.iOSRewardedRequestBuilder();
#else
            return new DummuRewardedRequestBuilder();
#endif
        }

        internal static IUserPermissions GetUserPermissions()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new BidMachineAds.Unity.Android.AndroidUserPermissions();
#else
            return new DummuUserPermissions();
#endif
        }
    }
}
