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
        public static extern string BidMachineBannerGetSize();

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
            return BannerSize.Size_300x250;
        }
    }
}