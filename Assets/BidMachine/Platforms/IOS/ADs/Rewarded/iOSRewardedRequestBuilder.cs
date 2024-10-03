using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequestBuilder : IAdRequestBuilder {

        private static IAdRequestListener requestListener;
        private RewardedRequestBuilderiOSUnityBridge bridge;

        private RewardedRequestBuilderiOSUnityBridge Bridge()
        {
            return bridge ??= new RewardedRequestBuilderiOSUnityBridge();
        }

        public IAdRequest Build()
        {
            Bridge().Build();
            return new iOSRewardedRequest();
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Bridge().SetBidPayload(bidPayload);
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Bridge().SetLoadingTimeOut(loadingTimeout);
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Bridge().SetNetworks(networks);
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Bridge().SetPlacementId(placementId);
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            KeyValueList list = new KeyValueList(priceFloorParams.PriceFloors);
            string jsonString = JsonUtility.ToJson(list);

            Bridge().SetPriceFloorParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            // Delete from interface?
            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            iOSRewardedRequestBuilder.requestListener = listener;

            Bridge().SetRewardedRequestDelegate(
                didLoadRequest, 
                didFailRequest,
                didExpiredRequest
            );
            return this;
        }

       [MonoPInvokeCallback(typeof(RewardedRequestSuccessCallback))]
        private static void didLoadRequest(IntPtr ad, string auctionResult)
        {
            if (iOSRewardedRequestBuilder.requestListener != null) 
            {
                iOSRewardedRequestBuilder.requestListener.onRequestSuccess(new iOSRewardedRequest(), auctionResult);
            }
        }

        [MonoPInvokeCallback(typeof(RewardedRequestFailedCallback))]
        private static void didFailRequest(IntPtr ad, IntPtr error)
        {
            if (iOSRewardedRequestBuilder.requestListener != null) 
            {
                // FIXME
            }
        }

        [MonoPInvokeCallback(typeof(RewardedRequestExpiredCallback))]
        private static void didExpiredRequest(IntPtr ad)
        {
            if (iOSRewardedRequestBuilder.requestListener != null) 
            {
                iOSRewardedRequestBuilder.requestListener.onRequestExpired(new iOSRewardedRequest(), auctionResult);
            }
        }
    }
}

namespace BidMachineAds.Unity.iOS
{
    [Serializable]
    public class KeyValue
    {
        public string key;
        public double value;

        public KeyValue(string key, double value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [Serializable]
    public class KeyValueList
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