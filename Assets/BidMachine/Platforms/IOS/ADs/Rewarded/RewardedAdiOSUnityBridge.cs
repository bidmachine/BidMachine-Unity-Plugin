using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class RewardedAdiOSUnityBridge : MonoBehaviour
    {
         [DllImport("__Internal")]
         private static extern bool BidMachineRewardedCanShow();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedDestroy();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedShow();

         [DllImport("__Internal")]
         private static extern void BidMachineRewardedLoad();

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
    }
}