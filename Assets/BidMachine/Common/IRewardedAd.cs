using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IRewardedAd
        : IAd<IRewardedAd, IRewardedlListener, IRewardedRequest>,
            IShowableAd<IRewardedAd, IRewardedRequest> { }

    public interface IRewardedRequest : IAdRequest<IRewardedRequest> { }

    public interface IRewardedlListener
        : IAdListener<IRewardedAd, BMError>,
            IFullscreenAdListener<IRewardedAd, BMError> { }

    public interface IRewardedRequestBuilder : IAdRequestBuilder<IRewardedRequest> { }
}
