using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialAdListener
    {
        void onInterstitialAdLoaded(InterstitialAd ad);
        void onInterstitialAdLoadFailed(InterstitialAd ad, BMError error);
        void onInterstitialAdShown(InterstitialAd ad);
        void onInterstitialAdShowFailed(InterstitialAd ad, BMError error);
        void onInterstitialAdImpression(InterstitialAd ad);
        void onInterstitialAdClicked(InterstitialAd ad);
        void onInterstitialAdClosed(InterstitialAd ad, bool finished);
        void onInterstitialAdExpired(InterstitialAd ad);
    }
}