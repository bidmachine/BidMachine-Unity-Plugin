using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedAd : IFullscreenAd {
        RewardedAdiOSUnityBridge bridge;

        private RewardedAdiOSUnityBridge Bridge() 
        {
            return bridge ??= new RewardedAdiOSUnityBridge();
        }

        public bool CanShow()
        {
            return Bridge().CanShow();
        }

        public void Destroy()
        {
            Bridge().Destroy();
        }

        public void Load(IAdRequest request)
        {
            Bridge().Load();
        }

        public void SetListener(IFullscreenAdListener<IFullscreenAd> listener)
        {

        }

        public void Show()
        {
            Bridge().Show();
        }
    }
}