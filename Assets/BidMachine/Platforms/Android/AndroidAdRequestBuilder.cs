#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdRequestBuilder<TAdRequest> : IAdRequestBuilder<TAdRequest>
    {
        private readonly AndroidJavaObject javaObject;

        private readonly Func<AndroidJavaObject, TAdRequest> factory;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidAdRequestBuilder(
            string className,
            Func<AndroidJavaObject, TAdRequest> factory
        )
        {
            javaObject = new AndroidJavaObject(className);
            this.factory = factory;
        }

        public void SetAdContentType(AdContentType adContentType)
        {
            string contentTypeString = adContentType.ToString();

            // Handle the default case
            if (!Enum.IsDefined(typeof(AdContentType), adContentType))
            {
                contentTypeString = AdContentType.All.ToString();
            }

            AndroidJavaClass adContentTypeClass = new AndroidJavaClass(
                "io.bidmachine.AdContentType"
            );
            AndroidJavaObject javaAdContentType = adContentTypeClass.GetStatic<AndroidJavaObject>(
                contentTypeString
            );

            javaObject.Call<AndroidJavaObject>("setAdContentType", javaAdContentType);
        }

        public void SetBidPayload(string bidPayLoad)
        {
            if (string.IsNullOrEmpty(bidPayLoad))
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setBidPayload",
                AndroidUtils.GetJavaPrimitiveObject(bidPayLoad)
            );
        }

        public void SetListener(IAdRequestListener<TAdRequest, string, BMError> listener)
        {
            if (listener == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdRequestListener<TAdRequest>(
                    AndroidUtils.BannerListenerClassName,
                    listener,
                    factory
                )
            );
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            javaObject.Call<AndroidJavaObject>(
                "setLoadingTimeOut",
                AndroidUtils.GetJavaPrimitiveObject(loadingTimeout)
            );
        }

        public void SetNetworks(string jsonNetworksData)
        {
            if (string.IsNullOrEmpty(jsonNetworksData))
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setNetworks",
                AndroidUtils.GetJavaPrimitiveObject(jsonNetworksData)
            );
        }

        public void SetPlacementId(string placementId)
        {
            if (string.IsNullOrEmpty(placementId))
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setPlacementId",
                AndroidUtils.GetJavaPrimitiveObject(placementId)
            );
        }

        public void SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setPriceFloorParams",
                new AndroidPriceFloorParams(priceFloorParams)
            );
        }

        public void SetSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setSessionAdParams",
                new AndroidSessionAdParams(sessionAdParams)
            );
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setTargetingParams",
                new AndroidTargetingParams(targetingParams).JavaObject
            );
        }

        public TAdRequest Build()
        {
            return factory(javaObject.Call<AndroidJavaObject>("build"));
        }
    }
}
#endif
