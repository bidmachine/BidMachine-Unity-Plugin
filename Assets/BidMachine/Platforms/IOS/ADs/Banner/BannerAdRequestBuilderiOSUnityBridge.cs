using System.Runtime.InteropServices;
using System;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
 {
    public class BannerAdRequestBuilderiOSUnityBridge : MonoBehaviour, IiOSAdRequestBuilderBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineSetBannerRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
        );

        [DllImport("__Internal")]
        private static extern void BidMachineBannerBuildRequest();

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetPriceFloorParams(string jsonString);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetPlacementId(string placementId);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetBidPayload(string bidPayload);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetLoadingTimeOut(int loadingTimeout);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetNetworks(string networks);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetAdContentType(string type);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetSize(int size);

        public void Build()
        {
            BidMachineBannerBuildRequest();
        }

        public void SetPriceFloorParams(string jsonString)
        {
            BidMachineBannerSetPriceFloorParams(jsonString);
        }

        public void SetPlacementId(string placementId)
        {
            BidMachineBannerSetPlacementId(placementId);
        }

        public void SetBidPayload(string bidPayload)
        {
            BidMachineBannerSetBidPayload(bidPayload);
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            BidMachineBannerSetLoadingTimeOut(loadingTimeout);
        }

        public void SetNetworks(string networks)
        {
            BidMachineBannerSetNetworks(networks);
        }

        public void SetAdContentType(string contentType)
        {
            BidMachineBannerSetAdContentType(contentType);
        }

         public void SetAdRequestDelegate(
            AdRequestSuccessCallback onSuccess,
            AdRequestFailedCallback onFailed,
            AdRequestExpiredCallback onExpired
         )
         {
            BidMachineSetBannerRequestDelegate(
                onSuccess,
                onFailed,
                onExpired
            );
         }

         public void SetSize(BannerSize size) 
         {
            int raw = (int)size;
            BidMachineBannerSetSize(raw);
         }
    }
}