#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdRequestBuilder : IAdRequestBuilder
    {
        private readonly AndroidJavaObject jObject;

        private readonly string listenerClassName;

        private readonly Func<AndroidJavaObject, IAdRequest> factory;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidAdRequestBuilder(
            string className,
            string listenerClassName,
            Func<AndroidJavaObject, IAdRequest> factory
        )
        {
            jObject = new AndroidJavaObject(className);
            this.listenerClassName = listenerClassName;
            this.factory = factory;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType adContentType)
        {
            var contentTypeString = adContentType.ToString();
            if (!Enum.IsDefined(typeof(AdContentType), adContentType))
            {
                contentTypeString = AdContentType.All.ToString();
            }

            var jcAdContentType = new AndroidJavaClass("io.bidmachine.AdContentType");
            var jAdContentType = jcAdContentType.GetStatic<AndroidJavaObject>(contentTypeString);

            jObject.Call<AndroidJavaObject>("setAdContentType", jAdContentType);

            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayLoad)
        {
            if (string.IsNullOrEmpty(bidPayLoad))
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setBidPayload",
                AndroidNativeConverter.GetObject(bidPayLoad)
            );

            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            if (listener == null)
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdRequestListener(listenerClassName, listener, factory)
            );

            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            jObject.Call<AndroidJavaObject>(
                "setLoadingTimeOut",
                AndroidNativeConverter.GetObject(loadingTimeout)
            );

            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            if (string.IsNullOrEmpty(networks))
            {
                return this;
            }

            var networksArray = networks.Split(',');
            if (networksArray.Length == 0)
            {
                return this;
            }

            var networksJson = JsonUtility.ToJson(networksArray);
            if (string.IsNullOrEmpty(networksJson))
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setNetworks",
                AndroidNativeConverter.GetObject(networksJson)
            );

            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            if (string.IsNullOrEmpty(placementId))
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setPlacementId",
                AndroidNativeConverter.GetObject(placementId)
            );

            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null)
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setPriceFloorParams",
                AndroidNativeConverter.GetPriceFloorParams(priceFloorParams)
            );

            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null)
            {
                return this;
            }

            jObject.Call<AndroidJavaObject>(
                "setTargetingParams",
                AndroidNativeConverter.GetTargetingParams(targetingParams)
            );

            return this;
        }

        public IAdRequest Build()
        {
            return factory(jObject.Call<AndroidJavaObject>("build"));
        }
    }
}
#endif
