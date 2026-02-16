using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class InterstitialRequest : IAdRequest
    {
        private readonly IAdRequest client;

        public InterstitialRequest(IAdRequest client)
        {
            this.client = client;
        }

        public string GetAuctionResult()
        {
            return client.GetAuctionResult();
        }

        public AuctionResult GetAuctionResultObject()
        {
            return client.GetAuctionResultObject();
        }

        public bool IsDestroyed()
        {
            return client.IsDestroyed();
        }

        public bool IsExpired()
        {
            return client.IsExpired();
        }

        public class Builder : IAdRequestBuilder
        {
            private readonly IAdRequestBuilder client;

            public Builder(AdPlacementConfig config)
            {
                // Config is passed to platform builder, no need to call setters
                client = BidMachineClientFactory.GetInterstitialRequestBuilder(config);
            }

            [System.Obsolete("Use Builder(AdPlacementConfig) constructor instead.")]
            public Builder()
            {
                client = BidMachineClientFactory.GetInterstitialRequestBuilder();
            }

            [System.Obsolete("SetAdContentType() is deprecated. Use AdPlacementConfig.InterstitialBuilder(AdContentType) instead.")]
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

            [System.Obsolete("SetCustomParams() is deprecated. Use AdPlacementConfig.InterstitialBuilder().WithCustomParams() instead.")]
            public IAdRequestBuilder SetCustomParams(CustomParams customParams)
            {
                client.SetCustomParams(customParams);
                return this;
            }

            public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
            {
                client.SetListener(listener);
                return this;
            }

            public IAdRequestBuilder SetLoadingTimeOut(int value)
            {
                client.SetLoadingTimeOut(value);
                return this;
            }

            [System.Obsolete("SetPlacementId() is deprecated. Use AdPlacementConfig.InterstitialBuilder().WithPlacementId() instead.")]
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
