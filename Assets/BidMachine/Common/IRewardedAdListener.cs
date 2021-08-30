using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    public interface IRewardedAdListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onAdLoaded(RewardedAd ad);

        /**
        * Called when Ad load failed
        */
        void onAdLoadFailed(RewardedAd ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onAdShown(RewardedAd ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onAdImpression(RewardedAd ad);

        /**
        * Called when Ad was clicked
        */
        void onAdClosed(RewardedAd ad);

        /**
        * Called when Ad was clicked
        */
        void onAdClicked(RewardedAd ad);

        /**
        * Called when Ad expired
        */
        void onAdExpired(RewardedAd ad);

        /**
        * Called when ad show failed
        */
        void onAdShowFailed(RewardedAd ad, BMError error);

        /**
        * Called when ad was closed (e.g - user click close button)
        */
        void onAdClosed(RewardedAd ad, bool finished);

        /**
        * Called when Rewarded Ad Complete (e.g - the video has been played to the end).
        * You can use this event to initialize your reward
        */
        void onAdRewarded(RewardedAd ad);
    }
}
