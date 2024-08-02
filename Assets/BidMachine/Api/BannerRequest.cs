using BidMachineAds.Unity.Common;

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

            public void SetSize(BannerSize size)
            {
                client.SetSize(size);
            }

            public void SetAdContentType(AdContentType contentType)
            {
                client.SetAdContentType(contentType);
            }

            public void SetTargetingParams(TargetingParams targetingParams)
            {
                client.SetTargetingParams(targetingParams);
            }

            public void SetPriceFloorParams(PriceFloorParams priceFloorParameters)
            {
                client.SetPriceFloorParams(priceFloorParameters);
            }

            public void SetListener(IAdRequestListener<IBannerRequest, string, BMError> listener)
            {
                client.SetListener(listener);
            }

            public void SetSessionAdParams(SessionAdParams sessionAdParams)
            {
                client.SetSessionAdParams(sessionAdParams);
            }

            public void SetLoadingTimeOut(int value)
            {
                client.SetLoadingTimeOut(value);
            }

            public void SetPlacementId(string placementId)
            {
                client.SetPlacementId(placementId);
            }

            public void SetBidPayload(string bidPayLoad)
            {
                client.SetBidPayload(bidPayLoad);
            }

            public void SetNetworks(string jsonNetworksData)
            {
                client.SetNetworks(jsonNetworksData);
            }

            public IBannerRequest Build()
            {
                return client.Build();
            }
        }
    }
}
