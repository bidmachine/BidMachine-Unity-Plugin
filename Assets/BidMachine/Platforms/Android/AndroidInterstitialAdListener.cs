using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidInterstitialAdListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        readonly IInterstitialAdListener listener;
        internal AndroidInterstitialAdListener(IInterstitialAdListener listener) : base("io.bidmachine.interstitial.InterstitialListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            listener.onAdLoaded(new InterstitialAd(new AndroidInterstitialAd(ad)));
        }

        void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed((new InterstitialAd(new AndroidInterstitialAd(ad))), finished);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onAdLoadFailed((new InterstitialAd(new AndroidInterstitialAd(ad))), bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            listener.onAdShown((new InterstitialAd(new AndroidInterstitialAd(ad))));
        }

        void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onAdShowFailed((new InterstitialAd(new AndroidInterstitialAd(ad))), bmError);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            listener.onAdImpression((new InterstitialAd(new AndroidInterstitialAd(ad))));
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            listener.onAdClicked((new InterstitialAd(new AndroidInterstitialAd(ad))));
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            listener.onAdExpired((new InterstitialAd(new AndroidInterstitialAd(ad))));
        }
    }
#else
    {
        public AndroidInterstitialAdListener(IInterstitialAdListener interstitialAdListener) { }
    }
#endif
}
