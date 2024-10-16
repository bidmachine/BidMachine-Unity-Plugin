using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSInterstitialAd : iOSAd<InterstitialAdiOSUnityBridge>, IInterstitialAd {
        private static IInterstitialAdListener listener;
        public iOSInterstitialAd() : base() { }

        public void Show()
        {
            adBridge.Show();
        }

        public void SetListener(IInterstitialAdListener listener)
        {
            iOSInterstitialAd.listener = listener;

            adBridge.SetLoadCallback(didLoadAd);
            adBridge.SetLoadFailedCallback(didFailLoadAd);
            adBridge.SetPresentCallback(didPresentAd);
            adBridge.SetPresentFailedCallback(didFailPresentAd);
            adBridge.SetImpressionCallback(didReceiveAdImpression);
            adBridge.SetExpiredCallback(didExpire);
            adBridge.SetClosedCallback(didClose);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didLoadAd(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdLoaded(new iOSInterstitialAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailLoadAd(IntPtr ad, IntPtr error)
        {
            if (listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                listener.onAdLoadFailed(new iOSInterstitialAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdShown(new iOSInterstitialAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void didFailPresentAd(IntPtr ad, IntPtr error)
        {
            if (listener != null) 
            {
                var bmError = new BMError
                {
                    Code = iOSErrorBridge.GetErrorCode(error),
                    Message = iOSErrorBridge.GetErrorMessage(error)
                };
                listener.onAdShowFailed(new iOSInterstitialAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdImpression(new iOSInterstitialAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdExpired(new iOSInterstitialAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didClose(IntPtr ad, bool finished)
        {
            if (listener != null) 
            {
                listener.onAdClosed(new iOSInterstitialAd(), finished);
            }
        }
    }
}