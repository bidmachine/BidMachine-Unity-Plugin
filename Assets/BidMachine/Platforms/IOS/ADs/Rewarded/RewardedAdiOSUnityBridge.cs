using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public delegate void AdCallback(IntPtr ad);
    public delegate void AdFailureCallback(IntPtr ad, IntPtr error);
    public delegate void AdClosedCallback(IntPtr ad, bool finished);

    public class RewardedAdiOSUnityBridge : MonoBehaviour, IiOSFullscreenAdBridge
    {
         [DllImport("__Internal")]
         private static extern bool BidMachineRewardedCanShow();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedDestroy();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedShow();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedLoad();

         [DllImport("__Internal")]
        private static extern void BidMachineSetRewardedAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired,
            AdClosedCallback onClosed
        );

         public bool CanShow() 
         {
            return BidMachineRewardedCanShow();
         }

         public void Destroy() 
         {
            BidMachineRewardedDestroy();
         }

         public void Show() 
         {
            BidMachineRewardedShow();
         }

         public void Load() 
         {
            BidMachineRewardedLoad();
         }

         public void SetAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired,
            AdClosedCallback onClosed
         )
         {
            BidMachineSetRewardedAdDelegate(
               onLoad,
               onFailedToLoad,
               onPresent,
               onFailedToPresent,
               onImpression,
               onExpired,
               onClosed
            );
         }
    }
}