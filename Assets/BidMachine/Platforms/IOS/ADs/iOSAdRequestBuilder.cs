using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSAdRequestBuilderBridge {
        public void Build();

        public void SetPriceFloorParams(string jsonString);

        public void SetPlacementId(string placementId);

        public void SetBidPayload(string bidPayload);

        public void SetLoadingTimeOut(int loadingTimeout);

        public void SetNetworks(string networks);

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
        private readonly Bridge requestBuilderBridge;

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
            var contentTypeString = contentType.ToString();
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
            KeyValueList list = new KeyValueList(priceFloorParams.PriceFloors);
            string jsonString = JsonUtility.ToJson(list);

            requestBuilderBridge.SetPriceFloorParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            // Delete from interface?
            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            iOSAdRequestBuilder<Bridge, Request>.requestListener = listener;

            requestBuilderBridge.SetAdRequestDelegate(
                didLoadRequest, 
                didFailRequest,
                didExpiredRequest
            );
            return this;
        }

        [MonoPInvokeCallback(typeof(AdRequestSuccessCallback))]
        private static void didLoadRequest(IntPtr ad, IntPtr auctionResultUnamagedPointer)
        {
            string auctionString = Marshal.PtrToStringAuto(auctionResultUnamagedPointer);
            Marshal.FreeHGlobal(auctionResultUnamagedPointer);

            if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
            {
                iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestSuccess(
                    new Request(),
                    auctionString
                );
            }
        }

        [MonoPInvokeCallback(typeof(AdRequestFailedCallback))]
        private static void didFailRequest(IntPtr error)
        {
            if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestFailed(new Request(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdRequestExpiredCallback))]
        private static void didExpiredRequest(IntPtr ad)
        {
            if (iOSAdRequestBuilder<Bridge, Request>.requestListener != null) 
            {
                iOSAdRequestBuilder<Bridge, Request>.requestListener.onRequestExpired(new Request());
            }
        }
    }
}

namespace BidMachineAds.Unity.iOS
{
    [Serializable]
    internal class KeyValue
    {
        public string Key;
        public double Value;

        public KeyValue(string key, double value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    [Serializable]
    internal class KeyValueList
    {
        public List<KeyValue> items = new List<KeyValue>();

        public KeyValueList(Dictionary<string, double> dict)
        {
            foreach (var kvp in dict)
            {
                items.Add(new KeyValue(kvp.Key, kvp.Value));
            }
        }
    }
}