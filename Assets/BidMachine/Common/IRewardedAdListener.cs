using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedAdListener
    {
        /**
        * Called when Ad was loaded and ready to be shown
        */
        void onRewardedAdLoaded(RewardedAd ad);

        /**
        * Called when Ad load failed
        */
        void onRewardedAdLoadFailed(RewardedAd ad, BMError error);

        /**
        * Called when Ad shown
        */
        void onRewardedAdShown(RewardedAd ad);

        /**
        * Called when Ad Impression was tracked
        */
        void onRewardedAdImpression(RewardedAd ad);

        /**
        * Called when Ad was clicked
        */
        void onRewardedAdClosed(RewardedAd ad);

        /**
        * Called when Ad was clicked
        */
        void onRewardedAdClicked(RewardedAd ad);

        /**
        * Called when Ad expired
        */
        void onRewardedAdExpired(RewardedAd ad);

        /**
        * Called when ad show failed
        */
        void onRewardedAdShowFailed(RewardedAd ad, BMError error);

        /**
        * Called when ad was closed (e.g - user click close button)
        */
        void onRewardedAdClosed(RewardedAd ad, bool finished);

        /**
        * Called when Rewarded Ad Complete (e.g - the video has been played to the end).
        * You can use this event to initialize your reward
        */
        void onRewardedAdRewarded(RewardedAd ad);
    }
}
