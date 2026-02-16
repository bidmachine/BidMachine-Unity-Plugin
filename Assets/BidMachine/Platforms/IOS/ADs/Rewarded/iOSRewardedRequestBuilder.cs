#if UNITY_IOS
using System;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequestBuilder : iOSAdRequestBuilder<RewardedRequestBuilderiOSUnityBridge, iOSRewardedRequest> {
        public iOSRewardedRequestBuilder() : base() { }

        public iOSRewardedRequestBuilder(AdPlacementConfig config) : base()
        {
            // Set ad content type from config
            if (config.AdContentType != AdContentType.All)
            {
                requestBuilderBridge.SetAdContentType(config.AdContentType.ToString());
            }

            // Set placement ID if provided
            if (!string.IsNullOrEmpty(config.PlacementId))
            {
                requestBuilderBridge.SetPlacementId(config.PlacementId);
            }

            // Set custom params if provided
            if (config.CustomParams != null)
            {
                string jsonString = JsonUtility.ToJson(config.CustomParams);
                requestBuilderBridge.SetCustomParams(jsonString);
            }
        }
    }
}
#endif