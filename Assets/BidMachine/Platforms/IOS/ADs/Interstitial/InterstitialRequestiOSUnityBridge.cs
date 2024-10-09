using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class InterstitialRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        public static extern IntPtr BidMachineInterstitialGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        public static extern bool BidMachineInterstitialIsExpired();

        [DllImport("__Internal")]
        public static extern bool BidMachineInterstitialIsDestroyed();

        public string GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineInterstitialGetAuctionResultUnmanagedPointer();

            string result = Marshal.PtrToStringAuto(resultPtr);
            Marshal.FreeHGlobal(resultPtr);

            return result;
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