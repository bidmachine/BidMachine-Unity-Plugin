#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedRequestBuilder : IRewardedRequestBuilder
    {
        private readonly AndroidAdRequestBuilder<IRewardedRequest> requestBuilder;

        public AndroidRewardedRequestBuilder()
        {
            requestBuilder = new AndroidAdRequestBuilder<IRewardedRequest>(
                AndroidUtils.RewardedRequestBuilderClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidRewardedRequest(request);
                }
            );
        }

        public void SetAdContentType(AdContentType contentType)
        {
            requestBuilder.SetAdContentType(contentType);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            requestBuilder.SetTargetingParams(targetingParams);
        }

        public void SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            requestBuilder.SetPriceFloorParams(priceFloorParams);
        }

        public void SetListener(IAdRequestListener<IRewardedRequest, string, BMError> listener)
        {
            requestBuilder.SetListener(listener);
        }

        public void SetSessionAdParams(SessionAdParams sessionAdParams)
        {
            requestBuilder.SetSessionAdParams(sessionAdParams);
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            requestBuilder.SetLoadingTimeOut(loadingTimeout);
        }

        public void SetPlacementId(string placementId)
        {
            requestBuilder.SetPlacementId(placementId);
        }

        public void SetBidPayload(string bidPayload)
        {
            requestBuilder.SetBidPayload(bidPayload);
        }

        public void SetNetworks(string networks)
        {
            requestBuilder.SetNetworks(networks);
        }

        public IRewardedRequest Build()
        {
            return requestBuilder.Build();
        }
    }
}
#endif
