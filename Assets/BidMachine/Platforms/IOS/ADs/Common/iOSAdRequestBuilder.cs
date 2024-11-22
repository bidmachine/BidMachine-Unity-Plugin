#if UNITY_IOS
using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    using PriceFloorList = KeyValueList<string, double>;
    using CustomParamsList = KeyValueList<string, string>;

    public interface IiOSAdRequestBuilderBridge {
        public void Build();

        public void SetPriceFloorParams(string jsonString);

        public void SetPlacementId(string placementId);

        public void SetBidPayload(string bidPayload);

        public void SetLoadingTimeOut(int loadingTimeout);

        public void SetNetworks(string networks);

        public void SetCustomParams(string jsonString);

        public void SetAdContentType(string contentType);

        public void SetAdRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
         );
    }

    public class iOSAdRequestBuilder<Bridge, Request> : IAdRequestBuilder
        where Bridge : IiOSAdRequestBuilderBridge, new() 
        where Request: IAdRequest, new() 
    {

        private static IAdRequestListener requestListener;
        private static IAdAuctionRequestListener auctionListener;

        public readonly Bridge requestBuilderBridge;

        public iOSAdRequestBuilder() {
           requestBuilderBridge = new Bridge();
        }

        public IAdRequest Build()
        {
            requestBuilderBridge.Build();
            return new Request();
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            string contentTypeString = contentType.ToString();
            if (!Enum.IsDefined(typeof(AdContentType), contentType))
            {
                contentTypeString = AdContentType.All.ToString();
            }
            requestBuilderBridge.SetAdContentType(contentTypeString);
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            requestBuilderBridge.SetBidPayload(bidPayload);
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            requestBuilderBridge.SetLoadingTimeOut(loadingTimeout);
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            requestBuilderBridge.SetNetworks(networks);
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            requestBuilderBridge.SetPlacementId(placementId);
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            PriceFloorList list = new PriceFloorList(priceFloorParams.PriceFloors);
            string jsonString = JsonUtility.ToJson(list);

            requestBuilderBridge.SetPriceFloorParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            // Delete from interface?
            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            CustomParamsList list = new CustomParamsList(customParams.Params);
            string jsonString = JsonUtility.ToJson(list);

            requestBuilderBridge.SetCustomParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            iOSAdRequestBuilder<Bridge, Request>.requestListener = listener;
            SetAdRequestDelegate();

            return this;
        }

        public IAdRequestBuilder SetRequestListener(IAdAuctionRequestListener listener)
        {
            iOSAdRequestBuilder<Bridge, Request>.auctionListener = listener;
            SetAdRequestDelegate();

            return this;
        }

        [MonoPInvokeCallback(typeof(AdRequestSuccessCallback))]
        private static void didLoadRequest(IntPtr ad, IntPtr auctionResultUnamagedPointer)
        {
            if (hasAnyListener())
            {
                string auctionString = Marshal.PtrToStringAuto(auctionResultUnamagedPointer);
                var request = new Request();

                if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
                {
                    iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestSuccess(request, auctionString);
                }
                if (iOSAdRequestBuilder<Bridge, Request>.auctionListener != null) 
                {
                    AuctionResultWrapper auctionResultWrapped = JsonUtility.FromJson<AuctionResultWrapper>(auctionString);
                    AuctionResult auctionResult = auctionResultWrapped.ToAuctionResult();

                    iOSAdRequestBuilder<Bridge, Request>.auctionListener.onRequestSuccess(request, auctionResult);
                }
            }
            iOSPointersBridge.ReleasePointer(auctionResultUnamagedPointer);
        }

        [MonoPInvokeCallback(typeof(AdRequestFailedCallback))]
        private static void didFailRequest(IntPtr error)
        {
            if (hasAnyListener())
            {
                var request = new Request();
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };

                if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
                {
                    iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestFailed(request, bmError);
                }
                if (iOSAdRequestBuilder<Bridge, Request>.auctionListener != null) 
                {
                    iOSAdRequestBuilder<Bridge, Request>.auctionListener.onRequestFailed(request, bmError);
                }
            }
        }

        [MonoPInvokeCallback(typeof(AdRequestExpiredCallback))]
        private static void didExpiredRequest(IntPtr ad)
        {
            if (hasAnyListener())
            {
                var request = new Request();

                if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
                {
                    iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestExpired(request);
                }
                if (iOSAdRequestBuilder<Bridge, Request>.auctionListener != null) 
                {
                    iOSAdRequestBuilder<Bridge, Request>.auctionListener.onRequestExpired(request);
                }
            }
        }

        private void SetAdRequestDelegate()
        {
            requestBuilderBridge.SetAdRequestDelegate(didLoadRequest, didFailRequest, didExpiredRequest);
        }

        private static bool hasAnyListener()
        {
            return iOSAdRequestBuilder<Bridge, Request>.auctionListener != null || iOSAdRequestBuilder<Bridge, Request>.requestListener != null;
        }
    }
}

namespace BidMachineAds.Unity.iOS
{
    [Serializable]
    internal class KeyValue<K, V>
    {
        public K Key;
        public V Value;

        public KeyValue(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    [Serializable]
    internal class KeyValueList<K,V>
    {
        public List<KeyValue<K, V>> items = new List<KeyValue<K, V>>();

        public KeyValueList(Dictionary<K, V> dict)
        {
            foreach (var kvp in dict)
            {
                items.Add(new KeyValue<K, V>(kvp.Key, kvp.Value));
            }
        }
    }
}
#endif