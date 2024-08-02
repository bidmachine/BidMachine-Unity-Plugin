using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IInterstitialAd
        : IAd<IInterstitialAd, IInterstitialListener, IInterstitialRequest>,
            IShowableAd<IInterstitialAd, IInterstitialRequest> { }

    public interface IInterstitialRequest : IAdRequest<IInterstitialRequest> { }

    public interface IInterstitialListener
        : IAdListener<IInterstitialAd, BMError>,
            IFullscreenAdListener<IInterstitialAd, BMError> { }

    public interface IInterstitialRequestBuilder : IAdRequestBuilder<IInterstitialRequest> { }
}
