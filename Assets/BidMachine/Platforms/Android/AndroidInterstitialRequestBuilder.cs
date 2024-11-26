#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialRequestBuilder : IAdRequestBuilder
    {
        private readonly AndroidAdRequestBuilder requestBuilder;

        public AndroidInterstitialRequestBuilder()
        {
            requestBuilder = new AndroidAdRequestBuilder(
                AndroidConsts.InterstitialRequestBuilderClassName,
                AndroidConsts.InterstitialRequestListenerClassName,
                delegate (AndroidJavaObject request)
                {
                    return new AndroidInterstitialRequest(request);
                }
            );
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            return requestBuilder.SetTargetingParams(targetingParams);
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            return requestBuilder.SetPriceFloorParams(priceFloorParams);
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            return requestBuilder.SetCustomParams(customParams);
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            return requestBuilder.SetListener(listener);
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            return requestBuilder.SetListener(listener);
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            return requestBuilder.SetLoadingTimeOut(loadingTimeout);
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            return requestBuilder.SetPlacementId(placementId);
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            return requestBuilder.SetBidPayload(bidPayload);
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            return requestBuilder.SetNetworks(networks);
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            return requestBuilder.SetAdContentType(contentType);
        }

        public IAdRequest Build()
        {
            return requestBuilder.Build();
        }
    }
}
#endif
