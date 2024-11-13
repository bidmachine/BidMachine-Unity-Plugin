using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IAd<TAdListener>
    {
        bool CanShow();

        void Destroy();

        void SetListener(TAdListener listener);

        void Load(IAdRequest request);
    }

    public interface ICommonAdListener<TAd, TAdError>
    {
        void onAdLoaded(TAd ad) { }

        void onAdLoadFailed(TAd ad, TAdError error) { }

        void onAdShown(TAd ad) { }

        void onAdShowFailed(TAd ad, TAdError error) { }

        void onAdImpression(TAd ad) { }

        void onAdExpired(TAd ad) { }
    }

    public interface IAdListener<TAd> : ICommonAdListener<TAd, BMError> { }

    public interface IAdRequest
    {
        AuctionResult GetAuctionResult();

        bool IsDestroyed();

        bool IsExpired();
    }

    public interface IAdRequestBuilder
    {
        IAdRequestBuilder SetAdContentType(AdContentType contentType);

        IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams);

        IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams);

        IAdRequestBuilder SetCustomParams(CustomParams customParams);

        IAdRequestBuilder SetListener(IAdRequestListener listener);

        IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout);

        IAdRequestBuilder SetPlacementId(string placementId);

        IAdRequestBuilder SetBidPayload(string bidPayload);

        IAdRequestBuilder SetNetworks(string networks);

        IAdRequest Build();
    }

    public interface ICommonAdRequestListener<TAdRequest, TResult, TAdError>
    {
        void onRequestSuccess(TAdRequest request, TResult auctionResult) { }

        void onRequestFailed(TAdRequest request, TAdError error) { }

        void onRequestExpired(TAdRequest request) { }
    }

    public interface IAdRequestListener : ICommonAdRequestListener<IAdRequest, AuctionResult, BMError> { }
}
