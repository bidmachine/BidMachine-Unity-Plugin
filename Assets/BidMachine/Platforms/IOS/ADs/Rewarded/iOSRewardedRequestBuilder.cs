#if UNITY_IOS
using System;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequestBuilder : iOSAdRequestBuilder<RewardedRequestBuilderiOSUnityBridge, iOSRewardedRequest> {
        public iOSRewardedRequestBuilder() : base() { }
    }
}
#endif