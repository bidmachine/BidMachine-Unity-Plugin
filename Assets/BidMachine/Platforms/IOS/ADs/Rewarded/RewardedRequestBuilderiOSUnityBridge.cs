using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public delegate void RewardedRequestSuccessCallback(IntPtr ad, string auctionResult);
    public delegate void RewardedRequestFailedCallback(IntPtr ad, IntPtr error);
    public delegate void RewardedRequestExpiredCallback(IntPtr ad);

    public class RewardedRequestBuilderiOSUnityBridge : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void BidMachineSetRewardedRequestDelegate(
            RewardedRequestSuccessCallback onSuccess,
            RewardedRequestFailedCallback onFailed,
            RewardedRequestExpiredCallback onExpired
        );

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedBuildRequest();

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetPriceFloorParams(string jsonString);

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetPlacementId(string placementId);

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetBidPayload(string bidPayload);

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetLoadingTimeOut(int loadingTimeout);

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetNetworks(string networks);

        public void Build()
        {
            BidMachineRewardedBuildRequest();
        }

        public void SetPriceFloorParams(string jsonString)
        {
            BidMachineRewardedSetPriceFloorParams(jsonString);
        }

        public void SetPlacementId(string placementId)
        {
            BidMachineRewardedSetPlacementId(placementId);
        }

        public void SetBidPayload(string bidPayload)
        {
            BidMachineRewardedSetBidPayload(bidPayload);
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            BidMachineRewardedSetLoadingTimeOut(loadingTimeout);
        }

        public void SetNetworks(string networks)
        {
            BidMachineRewardedSetNetworks(networks);
        }
    }
}