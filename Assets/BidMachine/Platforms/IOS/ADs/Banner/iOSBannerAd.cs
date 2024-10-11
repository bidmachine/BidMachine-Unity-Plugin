using UnityEngine;
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using System.Runtime.InteropServices;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBannerAd : iOSAd<BannerAdiOSUnityBridge>, IBannerView
    {
        private static IAdListener<IBannerView> listener;
        public iOSBannerAd() : base() { }

        public void SetListener(IAdListener<IBannerView> listener) 
        {
            iOSBannerAd.listener = listener;

            adBridge.SetLoadCallback(didLoadAd);
            adBridge.SetLoadFailedCallback(didFailLoadAd);
            adBridge.SetPresentCallback(didPresentAd);
            adBridge.SetPresentFailedCallback(didFailPresentAd);
            adBridge.SetImpressionCallback(didReceiveAdImpression);
            adBridge.SetExpiredCallback(didExpire);
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