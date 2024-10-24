#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
   public class InterstitialAdiOSUnityBridge : MonoBehaviour, IiOSFullscreenAdBridge
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
      private static extern void BidMachineInterstitialSetLoadCallback(AdCallback onLoad);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetLoadFailedCallback(AdFailureCallback onFailedToLoad);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetPresentCallback(AdCallback onPresent);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetPresentFailedCallback(AdFailureCallback onFailedToPresent);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetImpressionCallback(AdCallback onImpression);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetExpiredCallback(AdCallback onExpired);

      [DllImport("__Internal")]
      private static extern void BidMachineInterstitialSetClosedCallback(AdClosedCallback onClosed);
   
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

      public void SetLoadCallback(AdCallback onLoad) {
         BidMachineInterstitialSetLoadCallback(onLoad);
      }

      public void SetLoadFailedCallback(AdFailureCallback onFailedToLoad) 
      {
         BidMachineInterstitialSetLoadFailedCallback(onFailedToLoad);
      }

      public void SetPresentCallback(AdCallback onPresent)
      {
         BidMachineInterstitialSetPresentCallback(onPresent);
      }

      public void SetPresentFailedCallback(AdFailureCallback onFailedToPresent)
      {
         BidMachineInterstitialSetPresentFailedCallback(onFailedToPresent);
      }

      public void SetImpressionCallback(AdCallback onImpression)
      {
         BidMachineInterstitialSetImpressionCallback(onImpression);
      }

      public void SetExpiredCallback(AdCallback onExpired)
      {
         BidMachineInterstitialSetExpiredCallback(onExpired);
      }

      public void SetClosedCallback(AdClosedCallback onClosed)
      {
         BidMachineInterstitialSetClosedCallback(onClosed);
      }
    }
}
#endif