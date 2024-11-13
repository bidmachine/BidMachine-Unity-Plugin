#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS {
    public class RewardedRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineRewardedGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsDestroyed();

        public AuctionResult GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineRewardedGetAuctionResultUnmanagedPointer();

            string resultString = Marshal.PtrToStringAuto(resultPtr);
            iOSPointersBridge.ReleasePointer(resultPtr);

            AuctionResultWrapper result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);

            return result.ToAuctionResult();
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