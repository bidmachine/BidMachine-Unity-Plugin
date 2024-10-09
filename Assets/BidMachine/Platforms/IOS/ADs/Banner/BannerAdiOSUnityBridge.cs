using System.Runtime.InteropServices;
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class BannerAdiOSUnityBridge : MonoBehaviour, IiOSAdBridge
    {
         [DllImport("__Internal")]
         private static extern bool BidMachineBannerCanShow();

         [DllImport("__Internal")]
         private static extern void BidMachineBannerDestroy();

         [DllImport("__Internal")]
         private static extern bool BidMachineBannerShow();

         [DllImport("__Internal")]
         private static extern void BidMachineBannerLoad();

         [DllImport("__Internal")]
         private static extern void BidMachineBannerHide();

         [DllImport("__Internal")]
        private static extern void BidMachineSetBannerAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired
        );

        public bool CanShow() 
        {
            return BidMachineBannerCanShow();
        }

        public void Destroy() 
        {
            BidMachineBannerDestroy();
        }

        public bool Show(int YAxis, int XAxis, IBannerView ad, BannerSize size) 
        {
            return BidMachineBannerShow();
        }

        public void Hide()
        {
            BidMachineBannerHide();
        }

        public void Load() 
        {
            BidMachineBannerLoad();
        }

        public void SetAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired
        )
        {
            BidMachineSetBannerAdDelegate(
               onLoad,
               onFailedToLoad,
               onPresent,
               onFailedToPresent,
               onImpression,
               onExpired
            );
        }
    }
}