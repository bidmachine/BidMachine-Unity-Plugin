#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class BannerRequestiOSUnityBridge : MonoBehaviour, IiOSAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineBannerGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineBannerIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineBannerIsDestroyed();

        [DllImport("__Internal")]
        private static extern int BidMachineBannerGetSize();

        [DllImport("__Internal")]
        private static extern int BidMachineBannerGetAdSize();

        public string GetAuctionResult()
        {
            IntPtr resultPtr = BidMachineBannerGetAuctionResultUnmanagedPointer();
            string resultString = Marshal.PtrToStringAuto(resultPtr);
            iOSPointersBridge.ReleasePointer(resultPtr);
            return resultString;
        }

        public AuctionResult GetAuctionResultObject()
        {
            string resultString = GetAuctionResult();
            AuctionResultWrapper result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);
            return result.ToAuctionResult();
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

        public BannerAdSize GetBannerAdSize()
        {
            int rawValue = BidMachineBannerGetAdSize();

            return rawValue switch
            {
                0 => BannerAdSize.Banner,
                1 => BannerAdSize.MediumRectangle,
                2 => BannerAdSize.Leaderboard,
                _ => BannerAdSize.Banner // Default case
            };
        }
    }
}
#endif