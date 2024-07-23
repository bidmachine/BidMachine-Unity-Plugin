using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBannerListener
    {
        void onBannerAdLoaded(BannerView ad);
        void onBannerAdLoadFailed(BannerView ad, BMError error);
        void onBannerAdImpression(BannerView ad);
        void onBannerAdShowFailed(BannerView ad, BMError error);
        void onBannerAdClicked(BannerView ad);
        void onBannerAdExpired(BannerView ad);
    }
}