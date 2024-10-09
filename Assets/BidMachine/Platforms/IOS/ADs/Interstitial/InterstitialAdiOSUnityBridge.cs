using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class InterstitialAdiOSUnityBridge : MonoBehaviour, IiOSAdBridge
    {
         [DllImport("__Internal")]
         private static extern bool BidMachineInterstitialCanShow();

         [DllImport("__Internal")]
         private static extern void BidMachineInterstitialDestroy();

         [DllImport("__Internal")]
         private static extern void BidMachineInterstitialShow();

         [DllImport("__Internal")]
         private static extern void BidMachineInterstitialLoad();

         [DllImport("__Internal")]
        private static extern void BidMachineSetInterstitialAdDelegate(
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
            return BidMachineInterstitialCanShow();
         }

         public void Destroy() 
         {
            BidMachineInterstitialDestroy();
         }

         public void Show() 
         {
            BidMachineInterstitialShow();
         }

         public void Load() 
         {
            BidMachineInterstitialLoad();
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
            BidMachineSetInterstitialAdDelegate(
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