using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class RewardedRequest : IAdRequest
    {
        private readonly IAdRequest client;

        public RewardedRequest(IAdRequest client)
        {
            this.client = client;
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

        public sealed class Builder : IAdRequestBuilder
        {
            private readonly IAdRequestBuilder client;

            public Builder()
            {
                client = BidMachineClientFactory.GetRewardedRequestBuilder();
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

            public IAdRequestBuilder SetCustomParams(CustomParams customParams)
            {
                client.SetCustomParams(customParams);
                return this;
            }

            public IAdRequestBuilder SetListener(IAdRequestListener listener)
            {
                client.SetListener(listener);
                return this;
            }

            public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
            {
                client.SetLoadingTimeOut(loadingTimeout);
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
