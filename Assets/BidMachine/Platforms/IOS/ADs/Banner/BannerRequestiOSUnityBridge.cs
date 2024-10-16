using System.Runtime.InteropServices;
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class BannerRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        public static extern IntPtr BidMachineBannerGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        public static extern bool BidMachineBannerIsExpired();

        [DllImport("__Internal")]
        public static extern bool BidMachineBannerIsDestroyed();

        [DllImport("__Internal")]
        public static extern int BidMachineBannerGetSize();

        public string GetAuctionResult() 
        {
            IntPtr resultPtr = BidMachineBannerGetAuctionResultUnmanagedPointer();

            string result = Marshal.PtrToStringAuto(resultPtr);
            Marshal.FreeHGlobal(resultPtr);

            return result;
        }

        public bool IsExpired()
        {
           return BidMachineBannerIsExpired(); 
        }

        public bool IsDestroyed()
        {
           return BidMachineBannerIsDestroyed(); 
        }

        public BannerSize GetSize()
        {
            int rawValue = BidMachineBannerGetSize();

            return rawValue switch
            {
                0 => BannerSize.Size_320x50,
                1 => BannerSize.Size_300x250,
                2 => BannerSize.Size_728x90,
                _ => BannerSize.Size_320x50 // Default case
            };
        }
    }
}