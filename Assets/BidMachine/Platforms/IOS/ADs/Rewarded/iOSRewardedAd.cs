using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedAd : IFullscreenAd {
        private static IFullscreenAdListener<IFullscreenAd> listener;
        private RewardedAdiOSUnityBridge bridge;

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
            iOSRewardedAd.listener = listener;

            Bridge().SetAdDelegate(
                didLoadAd,
                didFailLoadAd,
                didPresentAd,
                didFailPresentAd,
                didReceiveAdImpression,
                didExpire,
                didClose
            );
        }

        public void Show()
        {
            Bridge().Show();
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didLoadAd(IntPtr ad)
        {
            if (iOSRewardedAd.listener != null) 
            {
                iOSRewardedAd.listener.onAdLoaded(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailLoadAd(IntPtr ad, IntPtr error)
        {

        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (iOSRewardedAd.listener != null) 
            {
                iOSRewardedAd.listener.onAdShown(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailPresentAd(IntPtr ad, IntPtr error)
        {

        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (iOSRewardedAd.listener != null) 
            {
                iOSRewardedAd.listener.onAdShown(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (iOSRewardedAd.listener != null) 
            {
                iOSRewardedAd.listener.onAdExpired(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didClose(IntPtr ad, bool finished)
        {
            if (iOSRewardedAd.listener != null) 
            {
                iOSRewardedAd.listener.onAdClosed(new iOSRewardedAd(), finished);
            }
        }
    }
}