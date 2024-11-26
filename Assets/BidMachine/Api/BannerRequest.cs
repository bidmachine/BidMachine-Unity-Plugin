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

        public BannerSize GetSize()
        {
            return client.GetSize();
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

            public Builder()
            {
                client = BidMachineClientFactory.GetBannerRequestBuilder();
            }

            public IAdRequestBuilder SetSize(BannerSize size)
            {
                client.SetSize(size);
                return this;
            }

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
