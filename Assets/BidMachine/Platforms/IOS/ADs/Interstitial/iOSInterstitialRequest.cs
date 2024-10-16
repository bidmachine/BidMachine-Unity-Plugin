#if UNITY_IOS
using System;

namespace BidMachineAds.Unity.iOS
{
    public class iOSInterstitialRequest : iOSAdRequest<InterstitialRequestiOSUnityBridge> {
        public iOSInterstitialRequest() : base() { }
    }
}
#endif