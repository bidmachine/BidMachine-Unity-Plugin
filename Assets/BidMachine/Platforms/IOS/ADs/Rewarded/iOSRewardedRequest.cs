using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedRequest : IAdRequest {
        private RewardedRequestiOSUnityBridge bridge;

        public RewardedRequestiOSUnityBridge Bridge()
        {
            return bridge ??= new RewardedRequestiOSUnityBridge();
        }

        public string GetAuctionResult()
        {
            string auctionResult = Bridge().GetAuctionResult();
            if (string.IsNullOrEmpty(auctionResult))
            {
                return "unknown";
            }
            else 
            {
                return auctionResult;
            }
        }

        public bool IsDestroyed()
        {
            return Bridge().IsDestroyed();
        }

        public bool IsExpired()
        {
            return Bridge().IsExpired();
        }
    }
}