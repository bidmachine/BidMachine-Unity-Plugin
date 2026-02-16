#if UNITY_IOS
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBannerRequestBuilder : iOSAdRequestBuilder<BannerAdRequestBuilderiOSUnityBridge, iOSBannerRequest>, IBannerRequestBuilder {
        public iOSBannerRequestBuilder() : base() { }

        public iOSBannerRequestBuilder(AdPlacementConfig config) : base()
        {
            // Set banner size from config
            if (config.BannerAdSize != null)
            {
                requestBuilderBridge.SetSize(config.BannerAdSize);
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

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            requestBuilderBridge.SetSize(size);
            return this;
        }

        public IAdRequestBuilder SetSize(BannerAdSize size)
        {
            requestBuilderBridge.SetSize(size);
            return this;
        }
    }
}
#endif