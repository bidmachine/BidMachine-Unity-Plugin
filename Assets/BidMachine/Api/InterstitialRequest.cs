using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class InterstitialRequest : IFullscreenRequest
    {
        private readonly IFullscreenRequest client;

        public InterstitialRequest(IFullscreenRequest client)
        {
            this.client = client;
        }

        public AdContentType GetAdContentType()
        {
            return client.GetAdContentType();
        }

        public string GetAuctionResult()
        {
            return client.GetAuctionResult();
        }

        public bool IsDestroyed()
        {
            return client.IsDestroyed();
        }

        public bool IsExpired()
        {
            return client.IsExpired();
        }

        public class Builder : IFullscreenAdRequestBuilder
        {
            private readonly IFullscreenAdRequestBuilder client;

            public Builder()
            {
                client = BidMachineClientFactory.GetInterstitialRequestBuilder();
            }

            public IAdRequestBuilder SetAdContentType(AdContentType contentType)
            {
                client.SetAdContentType(contentType);
                return this;
            }

            public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
            {
                client.SetTargetingParams(targetingParams);
                return this;
            }

            public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParameters)
            {
                client.SetPriceFloorParams(priceFloorParameters);
                return this;
            }

            public IAdRequestBuilder SetListener(IAdRequestListener listener)
            {
                client.SetListener(listener);
                return this;
            }

            public IAdRequestBuilder SetLoadingTimeOut(int value)
            {
                client.SetLoadingTimeOut(value);
                return this;
            }

            public IAdRequestBuilder SetPlacementId(string placementId)
            {
                client.SetPlacementId(placementId);
                return this;
            }

            public IAdRequestBuilder SetBidPayload(string bidPayLoad)
            {
                client.SetBidPayload(bidPayLoad);
                return this;
            }

            public IAdRequestBuilder SetNetworks(string networks)
            {
                client.SetNetworks(networks);
                return this;
            }

            public IAdRequest Build()
            {
                return client.Build();
            }
        }
    }
}
