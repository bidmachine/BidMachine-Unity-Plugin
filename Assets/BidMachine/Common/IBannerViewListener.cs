using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    public interface IBannerListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onBannerAdLoaded(BannerView ad);

        /**
        * Called when Ad load failed
        */
        void onBannerAdLoadFailed(BannerView ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onBannerAdShown(BannerView ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onBannerAdImpression(BannerView ad);

        /**
        * Called when Ad was clicked
        */
        void onBannerAdClicked(BannerView ad);

        /**
        * Called when Ad expired
        */
        void onBannerAdExpired(BannerView ad);
    }
}
