using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IAd<TAd, TAdListener, TAdRequest>
    {
        bool CanShow();

        void Destroy();

        void SetListener(TAdListener listener);

        void Load(TAdRequest request);
    }

    public interface IShowableAd<TAd, TAdRequrst>
    {
        void Show();
    }

    public interface IAdListener<TAd, TAdError>
    {
        void OnAdLoaded(TAd ad);

        void OnAdLoadFailed(TAd ad, TAdError error);

        void OnAdShown(TAd ad);

        void OnAdShowFailed(TAd ad, TAdError error);

        void OnAdImpression(TAd ad);

        void OnAdExpired(TAd ad);
    }

    public interface ICloseableAdListener<TAd>
    {
        void OnAdClosed(TAd ad, bool finished);
    }

    public interface IFullscreenAdListener<TAd, TAdError>
        : IAdListener<TAd, TAdError>,
            ICloseableAdListener<TAd> { }

    public interface IAdRequest<TAdRequest>
    {
        string GetAuctionResult();

        bool IsDestroyed();

        bool IsExpired();
    }

    public interface IAdRequestBuilder<TAdRequest>
    {
        void SetAdContentType(AdContentType contentType);

        void SetTargetingParams(TargetingParams targetingParams);

        void SetPriceFloorParams(PriceFloorParams priceFloorParams);

        void SetListener(IAdRequestListener<TAdRequest, string, BMError> listener);

        void SetSessionAdParams(SessionAdParams sessionAdParams);

        void SetLoadingTimeOut(int loadingTimeout);

        void SetPlacementId(string placementId);

        void SetBidPayload(string bidPayload);

        void SetNetworks(string networks);

        TAdRequest Build();
    }

    public interface IAdRequestListener<TAdRequest, TResult, TAdError>
    {
        void OnAdRequestSuccess(TAdRequest request, TResult auctionResult);

        void OnAdRequestFailed(TAdRequest request, TAdError error);

        void OnAdRequestExpired(TAdRequest request);
    }
}
