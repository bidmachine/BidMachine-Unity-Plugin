#if UNITY_IOS
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBannerRequest : iOSAdRequest<BannerRequestiOSUnityBridge>, IBannerRequest {
        public iOSBannerRequest() : base() { }

        public BannerSize GetSize() 
        {
            return requestBridge.GetSize();
        }
    }
}
#endif