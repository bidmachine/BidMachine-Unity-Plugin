using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSFullscreenAdBridge: IiOSAdBridge {
        public void Show();

        public void SetAdDelegate(
            AdCallback onLoad,
            AdFailureCallback onFailedToLoad,
            AdCallback onPresent,
            AdFailureCallback onFailedToPresent,
            AdCallback onImpression,
            AdCallback onExpired,
            AdClosedCallback onClosed
        );
    }

    public class iOSFullscreenAd<Bridge> : iOSAd<Bridge>, IFullscreenAd where Bridge : IiOSFullscreenAdBridge, new()
    {
        public iOSFullscreenAd() : base() { }

        private static IFullscreenAdListener<IFullscreenAd> listener;

        public void SetListener(IFullscreenAdListener<IFullscreenAd> listener)
        {
            iOSFullscreenAd<Bridge>.listener = listener;

            adBridge.SetAdDelegate(
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
            adBridge.Show();
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didLoadAd(IntPtr ad)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                iOSFullscreenAd<Bridge>.listener.onAdLoaded(new iOSFullscreenAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailLoadAd(IntPtr ad, IntPtr error)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSFullscreenAd<Bridge>.listener.onAdLoadFailed(new iOSFullscreenAd<Bridge>(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                iOSFullscreenAd<Bridge>.listener.onAdShown(new iOSFullscreenAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailPresentAd(IntPtr ad, IntPtr error)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSFullscreenAd<Bridge>.listener.onAdShowFailed(new iOSFullscreenAd<Bridge>(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                iOSFullscreenAd<Bridge>.listener.onAdImpression(new iOSFullscreenAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                iOSFullscreenAd<Bridge>.listener.onAdExpired(new iOSFullscreenAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didClose(IntPtr ad, bool finished)
        {
            if (iOSFullscreenAd<Bridge>.listener != null) 
            {
                iOSFullscreenAd<Bridge>.listener.onAdClosed(new iOSFullscreenAd<Bridge>(), finished);
            }
        }
    }
}