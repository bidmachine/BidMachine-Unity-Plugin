using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSAdRequestBridge {
        public string GetAuctionResult();

        public bool IsDestroyed();

        public bool IsExpired();
    }

    public class iOSAdRequest<Bridge> : IAdRequest where Bridge : IiOSAdRequestBridge, new() {
        public readonly Bridge requestBridge;

        public iOSAdRequest() {
           requestBridge = new Bridge();
        }

        public string GetAuctionResult()
        {
            string auctionResult = requestBridge.GetAuctionResult();
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
            return requestBridge.IsDestroyed();
        }

        public bool IsExpired()
        {
            return requestBridge.IsExpired();
        }
    }
}