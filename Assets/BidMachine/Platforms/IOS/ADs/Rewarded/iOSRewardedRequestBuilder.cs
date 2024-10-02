using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Collections.Generic;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequestBuilder : IAdRequestBuilder {
        public const string DUMMY_MESSAGE = "method doesn't support on iOS platform";

        RewardedRequestBuilderiOSUnityBridge bridge;

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

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
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
            return this;
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