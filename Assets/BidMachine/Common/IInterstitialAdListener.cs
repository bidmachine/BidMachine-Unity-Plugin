using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialAdListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onInterstitialAdLoaded(InterstitialAd ad);

        /**
        * Called when Ad load failed
        */
        void onInterstitialAdLoadFailed(InterstitialAd ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onInterstitialAdShown(InterstitialAd ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onInterstitialAdImpression(InterstitialAd ad);

        /**
        * Called when Ad was clicked
        */
        void onInterstitialAdClicked(InterstitialAd ad);

        /**
        * Called when Ad was clicked
        */
        void onInterstitialAdClosed(InterstitialAd ad);

        /**
        * Called when Ad expired
        */
        void onInterstitialAdExpired(InterstitialAd ad);

        /**
        * Called when ad show failed
        */
        void onInterstitialAdShowFailed(InterstitialAd ad, BMError error);

        /**
        * Called when ad was closed (e.g - user click close button)
        */
        void onInterstitialAdClosed(InterstitialAd ad, bool finished);
    }
}
