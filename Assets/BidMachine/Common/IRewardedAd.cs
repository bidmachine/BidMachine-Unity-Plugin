using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IRewardedAd : IAd<IRewardedAdListener> { 
        void Show();
    }

    public interface IRewardedAdListener : IFullscreenAdListener<IRewardedAd>
    {
        void onAdRewarded(IRewardedAd ad) { }
    }
}