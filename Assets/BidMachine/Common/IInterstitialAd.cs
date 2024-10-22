using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IInterstitialAd : IAd<IInterstitialAdListener>
    {
        void Show();
    }

    public interface ICommonInterstitialAdListener<TAd, TAdError>
        : ICommonFullscreenAdListener<TAd, TAdError> { }

    public interface IInterstitialAdListener
        : ICommonInterstitialAdListener<IInterstitialAd, BMError> { }
}
