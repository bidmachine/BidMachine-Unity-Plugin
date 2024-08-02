#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerRequestBuilder : IBannerRequestBuilder
    {
        private readonly AndroidAdRequestBuilder<IBannerRequest> requestBuilder;

        public AndroidBannerRequestBuilder()
        {
            requestBuilder = new AndroidAdRequestBuilder<IBannerRequest>(
                AndroidUtils.BannerRequestBuilderClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidBannerRequest(request);
                }
            );
        }

        public void SetSize(BannerSize size)
        {
            string sizeString = size.ToString();

            // Handle the default case
            if (!Enum.IsDefined(typeof(BannerSize), size))
            {
                sizeString = BannerSize.Size_320x50.ToString();
            }

            AndroidJavaClass bannerSizeClass = new AndroidJavaClass(
                "io.bidmachine.banner.BannerSize"
            );
            AndroidJavaObject javaBannerSize = bannerSizeClass.GetStatic<AndroidJavaObject>(
                sizeString
            );

            requestBuilder.JavaObject.Call<AndroidJavaObject>("setSize", javaBannerSize);
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

        public void SetListener(IAdRequestListener<IBannerRequest, string, BMError> listener)
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

        public IBannerRequest Build()
        {
            return requestBuilder.Build();
        }
    }
}
#endif
