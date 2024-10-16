using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IInterstitialAd : IAd<IInterstitialAdListener>
    {
        void Show();
    }

    public interface IInterstitialAdListener : IFullscreenAdListener<IInterstitialAd> { }
}