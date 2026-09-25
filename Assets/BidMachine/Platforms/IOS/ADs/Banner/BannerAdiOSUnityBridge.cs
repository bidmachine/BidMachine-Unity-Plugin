#if UNITY_IOS
using System.Runtime.InteropServices;
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.iOS {
    public class BannerAdiOSUnityBridge : MonoBehaviour, IiOSBannerAdBridge
    {
        [DllImport("__Internal")]
        private static extern bool BidMachineBannerCanShow();

        [DllImport("__Internal")]
        private static extern void BidMachineBannerDestroy();

        [DllImport("__Internal")]
        private static extern bool BidMachineBannerShow(int y, int x);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerLoad();

        [DllImport("__Internal")]
        private static extern void BidMachineBannerHide();

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetLoadCallback(AdCallback onLoad);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetLoadFailedCallback(AdFailureCallback onFailedToLoad);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetPresentCallback(AdCallback onPresent);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetPresentFailedCallback(AdFailureCallback onFailedToPresent);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetImpressionCallback(AdCallback onImpression);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetClickCallback(AdCallback onClick);

        [DllImport("__Internal")]
        private static extern void BidMachineBannerSetExpiredCallback(AdCallback onExpired);

        [DllImport("__Internal")]
        private static extern int BidMachineBannerGetAdSize();

        public bool CanShow() 
        {
            return BidMachineBannerCanShow();
        }

        public void Destroy() 
        {
            BidMachineBannerDestroy();
        }

        public bool Show(int YAxis, int XAxis) 
        {
            return BidMachineBannerShow(YAxis, XAxis);
        }

        public void Hide()
        {
            BidMachineBannerHide();
        }

        public void Load() 
        {
            BidMachineBannerLoad();
        }

        public void SetLoadCallback(AdCallback onLoad) {
            BidMachineBannerSetLoadCallback(onLoad);
        }

        public void SetLoadFailedCallback(AdFailureCallback onFailedToLoad) 
        {
            BidMachineBannerSetLoadFailedCallback(onFailedToLoad);
        }

        public void SetPresentCallback(AdCallback onPresent)
        {
            BidMachineBannerSetPresentCallback(onPresent);
        }

        public void SetPresentFailedCallback(AdFailureCallback onFailedToPresent)
        {
            BidMachineBannerSetPresentFailedCallback(onFailedToPresent);
        }

        public void SetImpressionCallback(AdCallback onImpression)
        {
            BidMachineBannerSetImpressionCallback(onImpression);
        }

        public void SetClickCallback(AdCallback onClick)
        {
            BidMachineBannerSetClickCallback(onClick);
        }

        public void SetExpiredCallback(AdCallback onExpired)
        {
            BidMachineBannerSetExpiredCallback(onExpired);
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
