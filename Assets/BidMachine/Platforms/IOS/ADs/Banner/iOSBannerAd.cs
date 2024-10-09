using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSBannerAdBridge : IiOSAdBridge
    {
        public bool Show(int YAxis, int XAxis);

        public void Hide();

        public void SetAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired
        );
    }

    public class iOSBannerAd : iOSAd<BannerAdiOSUnityBridge> , IBannerView
    {
        public iOSBannerAd() : base() { }

        private static IAdListener<IBannerView> listener;

        public void SetListener(IAdListener<IBannerView> listener) 
        {
            iOSBannerAd.listener = listener;

            adBridge.SetAdDelegate(
                didLoadAd,
                didFailLoadAd,
                didPresentAd,
                didFailPresentAd,
                didReceiveAdImpression,
                didExpire
            );
        }

        public bool Show(int YAxis, int XAxis, IBannerView ad, BannerSize size)
        {
            return adBridge.Show(YAxis, XAxis);
        }

        public void Hide()
        {
            adBridge.Hide();
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didLoadAd(IntPtr ad)
        {
            if (iOSBannerAd.listener != null) 
            {
                iOSBannerAd.listener.onAdLoaded(new iOSBannerAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailLoadAd(IntPtr ad, IntPtr error)
        {
            if (iOSBannerAd.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSBannerAd.listener.onAdLoadFailed(new iOSBannerAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (iOSBannerAd.listener != null) 
            {
                iOSBannerAd.listener.onAdShown(new iOSBannerAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailPresentAd(IntPtr ad, IntPtr error)
        {
            if (iOSBannerAd.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSBannerAd.listener.onAdShowFailed(new iOSBannerAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (iOSBannerAd.listener != null) 
            {
                iOSBannerAd.listener.onAdImpression(new iOSBannerAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (iOSBannerAd.listener != null) 
            {
                iOSBannerAd.listener.onAdExpired(new iOSBannerAd());
            }
        }
    }
}