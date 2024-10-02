using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class RewardedRequestiOSUnityBridge : MonoBehaviour
    {
        [DllImport("__Internal")]
        public static extern IntPtr BidMachineRewardedGetAuctionResult();

        [DllImport("__Internal")]
        public static extern bool BidMachineRewardedIsExpired();

        [DllImport("__Internal")]
        public static extern bool BidMachineRewardedIsDestroyed();

        public string GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineRewardedGetAuctionResult();
            return Marshal.PtrToStringAuto(resultPtr);
        }

        public bool IsExpired()
        {
           return BidMachineRewardedIsExpired(); 
        }

        public bool IsDestroyed()
        {
           return BidMachineRewardedIsDestroyed(); 
        }
    }
}