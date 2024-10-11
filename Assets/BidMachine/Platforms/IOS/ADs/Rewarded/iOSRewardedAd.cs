using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using AOT;

namespace BidMachineAds.Unity.iOS
{
    public class iOSRewardedAd : iOSAd<RewardedAdiOSUnityBridge>, IRewardedAd {
        private static IRewardedAdListener listener;
        public iOSRewardedAd() : base() { }

        public void Show()
        {
            adBridge.Show();
        }

        public void SetListener(IRewardedAdListener listener)
        {
            iOSRewardedAd.listener = listener;

            adBridge.SetLoadCallback(didLoadAd);
            adBridge.SetLoadFailedCallback(didFailLoadAd);
            adBridge.SetPresentCallback(didPresentAd);
            adBridge.SetPresentFailedCallback(didFailPresentAd);
            adBridge.SetImpressionCallback(didReceiveAdImpression);
            adBridge.SetExpiredCallback(didExpire);
            adBridge.SetClosedCallback(didClose);
            adBridge.SetRewardedCallback(didReward);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didLoadAd(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdLoaded(new iOSRewardedAd());
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
                listener.onAdLoadFailed(new iOSRewardedAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didPresentAd(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdShown(new iOSRewardedAd());
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
                listener.onAdShowFailed(new iOSRewardedAd(), bmError);
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didReceiveAdImpression(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdImpression(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void didExpire(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdExpired(new iOSRewardedAd());
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didClose(IntPtr ad, bool finished)
        {
            if (listener != null) 
            {
                listener.onAdClosed(new iOSRewardedAd(), finished);
            }
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void didReward(IntPtr ad)
        {
            if (listener != null) 
            {
                listener.onAdRewarded(new iOSRewardedAd());
            }
        }
    }
}