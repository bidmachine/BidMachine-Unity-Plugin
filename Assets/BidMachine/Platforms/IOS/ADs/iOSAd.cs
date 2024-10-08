using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public interface IiOSAdBridge {
        public bool CanShow();

        public void Destroy();

        public void Show();

        public void Load();

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

    public class iOSAd<Bridge> : IFullscreenAd where Bridge : IiOSAdBridge, new() {
        private static IFullscreenAdListener<IFullscreenAd> listener;
        private readonly Bridge adBridge;

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

        public void SetListener(IFullscreenAdListener<IFullscreenAd> listener)
        {
            iOSAd<Bridge>.listener = listener;

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
            if (iOSAd<Bridge>.listener != null) 
            {
                iOSAd<Bridge>.listener.onAdLoaded(new iOSAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailLoadAd(IntPtr ad, IntPtr error)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSAd<Bridge>.listener.onAdLoadFailed(new iOSAd<Bridge>(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                iOSAd<Bridge>.listener.onAdShown(new iOSAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailPresentAd(IntPtr ad, IntPtr error)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                iOSAd<Bridge>.listener.onAdShowFailed(new iOSAd<Bridge>(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                iOSAd<Bridge>.listener.onAdImpression(new iOSAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                iOSAd<Bridge>.listener.onAdExpired(new iOSAd<Bridge>());
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didClose(IntPtr ad, bool finished)
        {
            if (iOSAd<Bridge>.listener != null) 
            {
                iOSAd<Bridge>.listener.onAdClosed(new iOSAd<Bridge>(), finished);
            }
        }
    }
}
