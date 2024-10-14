#if UNITY_IOS
using System;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
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
#endif