#if UNITY_IOS
using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSAdRequestBridge {
        public AuctionResult GetAuctionResult();

        public bool IsDestroyed();

        public bool IsExpired();
    }

    public class iOSAdRequest<Bridge> : IAdRequest where Bridge : IiOSAdRequestBridge, new() {
        public readonly Bridge requestBridge;

        public iOSAdRequest() {
           requestBridge = new Bridge();
        }

        public AuctionResult GetAuctionResult()
        {
            AuctionResult auctionResult = requestBridge.GetAuctionResult();
           
            return auctionResult;
        }

        public bool IsDestroyed()
        {
            return requestBridge.IsDestroyed();
        }

        public bool IsExpired()
        {
            return requestBridge.IsExpired();
        }
    }
}
#endif