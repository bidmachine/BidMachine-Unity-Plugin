using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequestBuilder : iOSAdRequestBuilder<RewardedRequestBuilderiOSUnityBridge, iOSRewardedRequest> {
        public iOSRewardedRequestBuilder() : base() { }
    }
}