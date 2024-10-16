using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IRewardedAd : IAd<IRewardedAdListener>
    {
        void Show();
    }

    public interface ICommonRewardedAdListener<TAd, TAdError>
        : ICommonFullscreenAdListener<TAd, TAdError>
    {
        void onAdRewarded(TAd ad) { }
    }

    public interface IRewardedAdListener : ICommonRewardedAdListener<IRewardedAd, BMError> { }
}
