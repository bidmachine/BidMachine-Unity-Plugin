using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IInterstitialAdListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onAdLoaded(InterstitialAd ad);

        /**
        * Called when Ad load failed
        */
        void onAdLoadFailed(InterstitialAd ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onAdShown(InterstitialAd ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onAdImpression(InterstitialAd ad);

        /**
        * Called when Ad was clicked
        */
        void onAdClicked(InterstitialAd ad);

        /**
        * Called when Ad was clicked
        */
        void onAdClosed(InterstitialAd ad);

        /**
        * Called when Ad expired
        */
        void onAdExpired(InterstitialAd ad);

        /**
        * Called when ad show failed
        */
        void onAdShowFailed(InterstitialAd ad, BMError error);

        /**
        * Called when ad was closed (e.g - user click close button)
        */
        void onAdClosed(InterstitialAd ad, bool finished);
    }
}
