#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialAd : AndroidFullscreenAd
    {
        public AndroidInterstitialAd()
            : base(
                AndroidUtils.InterstitialAdClassName,
                AndroidUtils.InterstitialListenerClassName,
                delegate(AndroidJavaObject ad)
                {
                    return new InterstitialAd(new AndroidInterstitialAd(ad));
                },
                delegate(IAdRequest request)
                {
                    return ((AndroidInterstitialRequest)request).JavaObject;
                }
            ) { }

        public AndroidInterstitialAd(AndroidJavaObject javaObject)
            : base(
                javaObject,
                AndroidUtils.InterstitialListenerClassName,
                delegate(AndroidJavaObject ad)
                {
                    return new InterstitialAd(new AndroidInterstitialAd(ad));
                },
                delegate(IAdRequest request)
                {
                    return ((AndroidInterstitialRequest)request).JavaObject;
                }
            ) { }
    }
}
#endif
