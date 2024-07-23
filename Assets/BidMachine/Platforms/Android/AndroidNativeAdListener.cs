using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    public class AndroidNativeAdListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        readonly INativeAdListener listener;

        internal AndroidNativeAdListener(INativeAdListener listener) : base(
            "io.bidmachine.nativead.NativeListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            listener.onNativeAdLoaded(new NativeAd(new AndroidNativeAd(ad)));
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onNativeAdLoadFailed((new NativeAd(new AndroidNativeAd(ad))), bmError);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            listener.onNativeAdImpression(new NativeAd(new AndroidNativeAd(ad)));
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            listener.onNativeAdClicked(new NativeAd(new AndroidNativeAd(ad)));
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            listener.onNativeAdExpired(new NativeAd(new AndroidNativeAd(ad)));
        }
    }
#else
    {
        public AndroidNativeAdListener(INativeAdListener listener) { }
    }
#endif
}