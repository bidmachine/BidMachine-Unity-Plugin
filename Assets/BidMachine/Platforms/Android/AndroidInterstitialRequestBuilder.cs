#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        private readonly AndroidAdRequestBuilder<IInterstitialRequest> requestBuilder;

        public AndroidInterstitialRequestBuilder()
        {
            requestBuilder = new AndroidAdRequestBuilder<IInterstitialRequest>(
                AndroidUtils.InterstitialRequestBuilderClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidInterstitialRequest(request);
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

        public void SetListener(IAdRequestListener<IInterstitialRequest, string, BMError> listener)
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

        public IInterstitialRequest Build()
        {
            return requestBuilder.Build();
        }
    }
}
#endif
