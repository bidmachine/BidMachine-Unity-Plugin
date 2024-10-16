#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class RewardedRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        public static extern IntPtr BidMachineRewardedGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        public static extern bool BidMachineRewardedIsExpired();

        [DllImport("__Internal")]
        public static extern bool BidMachineRewardedIsDestroyed();

        public string GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineRewardedGetAuctionResultUnmanagedPointer();

            string result = Marshal.PtrToStringAuto(resultPtr);
            Marshal.FreeHGlobal(resultPtr);

            return result;
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
#endif