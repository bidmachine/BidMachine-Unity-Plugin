using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
   public delegate void AdCallback(IntPtr ad);
   public delegate void AdFailureCallback(IntPtr ad, IntPtr error);
   public delegate void AdClosedCallback(IntPtr ad, bool finished);

   public class RewardedAdiOSUnityBridge : MonoBehaviour, IiOSRewardedAdBridge
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
      private static extern void BidMachineRewardedSetLoadCallback(AdCallback onLoad);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetLoadFailedCallback(AdFailureCallback onFailedToLoad);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetPresentCallback(AdCallback onPresent);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetPresentFailedCallback(AdFailureCallback onFailedToPresent);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetImpressionCallback(AdCallback onImpression);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetExpiredCallback(AdCallback onExpired);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetClosedCallback(AdClosedCallback onClosed);

      [DllImport("__Internal")]
      private static extern void BidMachineRewardedSetRewardedCallback(AdCallback onRewarded);
   
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

      public void SetLoadCallback(AdCallback onLoad) 
      {
         BidMachineRewardedSetLoadCallback(onLoad);
      }

      public void SetLoadFailedCallback(AdFailureCallback onFailedToLoad) 
      {
         BidMachineRewardedSetLoadFailedCallback(onFailedToLoad);
      }

      public void SetPresentCallback(AdCallback onPresent)
      {
         BidMachineRewardedSetPresentCallback(onPresent);
      }

      public void SetPresentFailedCallback(AdFailureCallback onFailedToPresent)
      {
         BidMachineRewardedSetPresentFailedCallback(onFailedToPresent);
      }

      public void SetImpressionCallback(AdCallback onImpression)
      {
         BidMachineRewardedSetImpressionCallback(onImpression);
      }

      public void SetExpiredCallback(AdCallback onExpired)
      {
         BidMachineRewardedSetExpiredCallback(onExpired);
      }

      public void SetClosedCallback(AdClosedCallback onClosed)
      {
         BidMachineRewardedSetClosedCallback(onClosed);
      }

      public void SetRewardedCallback(AdCallback onRewarded)
      {
         BidMachineRewardedSetRewardedCallback(onRewarded);
      }
    }
}