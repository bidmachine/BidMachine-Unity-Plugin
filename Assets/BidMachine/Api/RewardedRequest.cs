using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class RewardedRequest : IRewardedRequest
    {
        private readonly IRewardedRequest client;

        public RewardedRequest(IRewardedRequest client)
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

        public sealed class Builder : IRewardedRequestBuilder
        {
            private readonly IRewardedRequestBuilder client;

            public Builder()
            {
                client = BidMachineClientFactory.GetRewardedRequestBuilder();
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

            public void SetListener(IAdRequestListener<IRewardedRequest, string, BMError> listener)
            {
                client.SetListener(listener);
            }

            public void SetSessionAdParams(SessionAdParams sessionAdParams)
            {
                client.SetSessionAdParams(sessionAdParams);
            }

            public void SetLoadingTimeOut(int loadingTimeout)
            {
                client.SetLoadingTimeOut(loadingTimeout);
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

            public IRewardedRequest Build()
            {
                return client.Build();
            }
        }
    }
}
