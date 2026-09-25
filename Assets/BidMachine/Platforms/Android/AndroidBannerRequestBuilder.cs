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

        public AndroidBannerRequestBuilder(AdPlacementConfig config)
        {
            requestBuilder = new AndroidAdRequestBuilder(
                AndroidConsts.BannerRequestBuilderClassName,
                AndroidConsts.BannerRequestListenerClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidBannerRequest(request);
                },
                config
            );
        }

        [System.Obsolete("Use constructor with AdPlacementConfig parameter")]
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

#pragma warning disable CS0618
        public IAdRequestBuilder SetSize(BannerSize size)
        {
            requestBuilder.JavaObject.Call<AndroidJavaObject>(
                "setSize",
                AndroidNativeConverter.GetBannerSize(size)
            );

            return this;
        }
#pragma warning restore CS0618

        public IAdRequestBuilder SetSize(BannerAdSize size)
        {
            if (size == null)
            {
                return this;
            }

            // BannerRequest.Builder has no setSize(BannerAdSize); the size belongs to AdPlacementConfig.
            // Fixed sizes can still go through the legacy setSize(BannerSize), adaptive ones cannot.
            if (size.IsAdaptive)
            {
                Debug.LogWarning(
                    "BidMachine: adaptive BannerAdSize can't be set on the request builder. "
                        + "Use AdPlacementConfig.BannerBuilder(BannerAdSize.Adaptive(...)) instead."
                );
                return this;
            }

#pragma warning disable CS0618
            return SetSize(size.ToLegacyBannerSize());
#pragma warning restore CS0618
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            // Banner requests have no ad content type
            return this;
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
