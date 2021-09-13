using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface INativeAdListener
    {
        void onNativeAdLoaded(NativeAd ad);
        void onNativeAdLoadFailed(NativeAd ad, BMError error);
        void onNativeAdShown(NativeAd ad);
        void onNativeAdImpression(NativeAd ad);
        void onNativeAdClicked(NativeAd ad);
        void onNativeAdExpired(NativeAd ad);
    }
}