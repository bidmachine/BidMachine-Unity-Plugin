using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    public interface IBannerListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onAdLoaded(BannerView ad);

        /**
        * Called when Ad load failed
        */
        void onAdLoadFailed(BannerView ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onAdShown(BannerView ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onAdImpression(BannerView ad);

        /**
        * Called when Ad was clicked
        */
        void onAdClicked(BannerView ad);

        /**
        * Called when Ad expired
        */
        void onAdExpired(BannerView ad);
    }
}
