#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerRequestBuilder : IBannerRequestBuilder
    {
        private readonly AndroidAdRequestBuilder requestBuilder;

        public AndroidBannerRequestBuilder()
        {
            requestBuilder = new AndroidAdRequestBuilder(
                AndroidConsts.BannerRequestBuilderClassName,
                AndroidConsts.BannerRequestListenerClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidBannerRequest(request);
                }
            );
        }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            requestBuilder.JavaObject.Call<AndroidJavaObject>(
                "setSize",
                AndroidNativeConverter.GetBannerSize(size)
            );

            return this;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            return requestBuilder.SetAdContentType(contentType);
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

        public IAdRequest Build()
        {
            return requestBuilder.Build();
        }
    }
}
#endif
