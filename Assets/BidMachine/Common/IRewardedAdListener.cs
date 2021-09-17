using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Android;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedAdListener
    {
        
        void onRewardedAdLoaded(RewardedAd ad);
        void onRewardedAdLoadFailed(RewardedAd ad, BMError error);
        void onRewardedAdShown(RewardedAd ad);
        void onRewardedAdShowFailed(RewardedAd ad, BMError error);
        void onRewardedAdImpression(RewardedAd ad);
        void onRewardedAdClicked(RewardedAd ad);
        void onRewardedAdRewarded(RewardedAd ad);
        void onRewardedAdClosed(RewardedAd ad, bool finished);
        void onRewardedAdExpired(RewardedAd ad);
    }
}
