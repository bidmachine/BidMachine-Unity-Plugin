using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Api
{
    public sealed class BannerRequest : IBannerRequest
    {
        private readonly IBannerRequest client;

        public BannerRequest(IBannerRequest client)
        {
            this.client = client;
        }

        [System.Obsolete("GetSize() is deprecated. Use GetBannerAdSize() instead.")]
        public BannerSize GetSize()
        {
            return client.GetSize();
        }

        public BannerAdSize GetBannerAdSize()
        {
            return client.GetBannerAdSize();
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

        public class Builder : IBannerRequestBuilder
        {
            private readonly IBannerRequestBuilder client;

            public Builder(AdPlacementConfig config)
            {
                // Config is passed to platform builder, no need to call setters
                client = BidMachineClientFactory.GetBannerRequestBuilder(config);
            }

            [System.Obsolete("Use Builder(AdPlacementConfig) constructor instead.")]
            public Builder()
            {
                client = BidMachineClientFactory.GetBannerRequestBuilder();
            }

#pragma warning disable CS0618
            [System.Obsolete("SetSize(BannerSize) is deprecated. Use AdPlacementConfig.BannerBuilder(BannerAdSize) instead.")]
            public IAdRequestBuilder SetSize(BannerSize size)
            {
                client.SetSize(size);
                return this;
            }
#pragma warning restore CS0618

            [System.Obsolete("SetSize(BannerAdSize) is deprecated. Use AdPlacementConfig.BannerBuilder(BannerAdSize) instead.")]
            public IAdRequestBuilder SetSize(BannerAdSize size)
            {
                client.SetSize(size);
                return this;
            }

            [System.Obsolete("SetAdContentType() is deprecated in SDK 3.5.0+.")]
            public IAdRequestBuilder SetAdContentType(AdContentType contentType)
            {
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

            [System.Obsolete("SetCustomParams() is deprecated. Use AdPlacementConfig.BannerBuilder().WithCustomParams() instead.")]
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

            [System.Obsolete("SetPlacementId() is deprecated. Use AdPlacementConfig.BannerBuilder().WithPlacementId() instead.")]
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
