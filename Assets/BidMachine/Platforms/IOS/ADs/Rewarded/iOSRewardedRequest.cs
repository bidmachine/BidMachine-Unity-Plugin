#if UNITY_IOS
using System;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequest : iOSAdRequest<RewardedRequestiOSUnityBridge> {
        public iOSRewardedRequest() : base() { }
    }
}
#endif