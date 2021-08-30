using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    public class AndroidInterstitialAdListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IInterstitialAdListener listener;
        internal AndroidInterstitialAdListener(IInterstitialAdListener listener) : base("io.bidmachine.interstitial.InterstitialListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdLoaded(interstitialAd);
        }

        void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdClosed(interstitialAd, finished);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onAdLoadFailed(interstitialAd, bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdShown(interstitialAd);
        }

        void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onAdShowFailed(interstitialAd, bmError);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdImpression(interstitialAd);
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdClicked(interstitialAd);
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            InterstitialAd interstitialAd = new InterstitialAd(new AndroidInterstitialAd(ad));
            listener.onAdExpired(interstitialAd);
        }
    }
#else
    {
        public AndroidInterstitialAdListener(IInterstitialAdListener interstitialAdListener) { }
    }
#endif
}
