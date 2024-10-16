#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public delegate void AdRequestSuccessCallback(IntPtr ad, IntPtr auctionResultUnamagedPointer);
    public delegate void AdRequestFailedCallback(IntPtr error);
    public delegate void AdRequestExpiredCallback(IntPtr ad);

    public class RewardedRequestBuilderiOSUnityBridge : MonoBehaviour, IiOSAdRequestBuilderBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineSetRewardedRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
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

        [DllImport("__Internal")]
        private static extern void BidMachineRewardedSetAdContentType(string type);

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

        public void SetAdContentType(string contentType)
        {
            BidMachineRewardedSetAdContentType(contentType);
        }

         public void SetAdRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
         )
         {
            BidMachineSetRewardedRequestDelegate(
                onSuccess,
                onFailed,
                onExpired
            );
         }
    }
}
#endif