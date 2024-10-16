using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBannerRequestBuilder : iOSAdRequestBuilder<BannerAdRequestBuilderiOSUnityBridge, iOSBannerRequest>, IBannerRequestBuilder {
        public iOSBannerRequestBuilder() : base() { }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            requestBuilderBridge.SetSize(size);
            return this;
        }
    }
}
