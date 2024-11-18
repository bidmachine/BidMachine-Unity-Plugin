#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS {
    public class InterstitialRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineInterstitialGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineInterstitialIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineInterstitialIsDestroyed();

        public AuctionResult GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineInterstitialGetAuctionResultUnmanagedPointer();

            string resultString = Marshal.PtrToStringAuto(resultPtr);
            iOSPointersBridge.ReleasePointer(resultPtr);

            AuctionResultWrapper result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);

            return result.ToAuctionResult();
        }

        public bool IsExpired()
        {
           return BidMachineInterstitialIsExpired(); 
        }

        public bool IsDestroyed()
        {
           return BidMachineInterstitialIsDestroyed(); 
        }
    }
}
#endif