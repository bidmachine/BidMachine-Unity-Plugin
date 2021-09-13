using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface INativeAdListener
    {
        void onAdLoaded(NativeAd ad);
        void onAdLoadFailed(NativeAd ad, BMError error);
        void onAdShown(NativeAd ad);
        void onAdImpression(NativeAd ad);
        void onAdClicked(NativeAd ad);
        void onAdExpired(NativeAd ad);
    }
}