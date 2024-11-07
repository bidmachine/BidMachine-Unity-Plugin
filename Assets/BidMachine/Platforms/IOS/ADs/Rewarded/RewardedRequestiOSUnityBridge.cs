#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class RewardedRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineRewardedGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsDestroyed();

        public string GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineRewardedGetAuctionResultUnmanagedPointer();

            string result = Marshal.PtrToStringAuto(resultPtr);
            iOSPointersBridge.ReleasePointer(resultPtr);

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