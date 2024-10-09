using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSAdBridge 
    {
        public bool CanShow();

        public void Destroy();

        public void Load();
    }

    public class iOSAd<Bridge> where Bridge : IiOSAdBridge, new() {
        public readonly Bridge adBridge;

        public iOSAd() {
           adBridge = new Bridge();
        }
        
        public bool CanShow()
        {
            return adBridge.CanShow();
        }

        public void Destroy()
        {
            adBridge.Destroy();
        }

        public void Load(IAdRequest request)
        {
            adBridge.Load();
        }
    }
}
