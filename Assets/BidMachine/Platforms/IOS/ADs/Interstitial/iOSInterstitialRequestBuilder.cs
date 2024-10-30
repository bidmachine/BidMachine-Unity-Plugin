#if UNITY_IOS
using System;

namespace BidMachineAds.Unity.iOS
{
    public class iOSInterstitialRequestBuilder : iOSAdRequestBuilder<InterstitialRequestBuilderiOSUnityBridge, iOSInterstitialRequest> {
        public iOSInterstitialRequestBuilder() : base() { }
    }
}
#endif
