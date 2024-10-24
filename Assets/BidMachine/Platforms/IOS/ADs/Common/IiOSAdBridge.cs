#if UNITY_IOS
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

        public void SetLoadCallback(AdCallback onLoad);

        public void SetLoadFailedCallback(AdFailureCallback onFailedToLoad);

        public void SetPresentCallback(AdCallback onPresent);

        public void SetPresentFailedCallback(AdFailureCallback onFailedToPresent);

        public void SetImpressionCallback(AdCallback onImpression);

        public void SetExpiredCallback(AdCallback onExpired);
    }

    public interface IiOSBannerAdBridge : IiOSAdBridge
    {
        public bool Show(int YAxis, int XAxis);

        public void Hide();
    }

    public interface IiOSFullscreenAdBridge : IiOSAdBridge
    {
        public void Show();

        public void SetClosedCallback(AdClosedCallback onExpired);
    }

    public interface IiOSRewardedAdBridge : IiOSFullscreenAdBridge 
    {
        public void SetRewardedCallback(AdCallback onRewarded);
    }
}
#endif