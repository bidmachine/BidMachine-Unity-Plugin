#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS
 {
    public class InterstitialRequestBuilderiOSUnityBridge : MonoBehaviour, IiOSAdRequestBuilderBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineSetInterstitialRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
        );

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialBuildRequest();

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetPriceFloorParams(string jsonString);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetCustomParams(string jsonString);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetPlacementId(string placementId);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetBidPayload(string bidPayload);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetLoadingTimeOut(int loadingTimeout);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetNetworks(string networks);

        [DllImport("__Internal")]
        private static extern void BidMachineInterstitialSetAdContentType(string type);

        public void Build()
        {
            BidMachineInterstitialBuildRequest();
        }

        public void SetPriceFloorParams(string jsonString)
        {
            BidMachineInterstitialSetPriceFloorParams(jsonString);
        }

        public void SetCustomParams(string jsonString)
        {
            BidMachineInterstitialSetCustomParams(jsonString);
        }

        public void SetPlacementId(string placementId)
        {
            BidMachineInterstitialSetPlacementId(placementId);
        }

        public void SetBidPayload(string bidPayload)
        {
            BidMachineInterstitialSetBidPayload(bidPayload);
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            BidMachineInterstitialSetLoadingTimeOut(loadingTimeout);
        }

        public void SetNetworks(string networks)
        {
            BidMachineInterstitialSetNetworks(networks);
        }

        public void SetAdContentType(string contentType)
        {
            BidMachineInterstitialSetAdContentType(contentType);
        }

         public void SetAdRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
         )
         {
            BidMachineSetInterstitialRequestDelegate(
                onSuccess,
                onFailed,
                onExpired
            );
         }
    }
}
#endif