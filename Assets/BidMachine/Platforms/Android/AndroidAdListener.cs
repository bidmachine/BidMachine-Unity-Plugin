#if PLATFORM_ANDROID
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdListener<TAd, TAdListener>
        : AndroidJavaProxy,
            ICommonAdListener<AndroidJavaObject, AndroidJavaObject>
        where TAdListener : ICommonAdListener<TAd, BMError>
    {
        protected readonly TAdListener listener;

        protected readonly Func<AndroidJavaObject, TAd> adProvider;

        internal AndroidAdListener(
            string className,
            TAdListener listener,
            Func<AndroidJavaObject, TAd> adProvider
        )
            : base(className)
        {
            this.listener = listener;
            this.adProvider = adProvider;
        }

        public void onAdExpired(AndroidJavaObject ad)
        {
            listener.onAdExpired(adProvider(ad));
        }

        public void onAdImpression(AndroidJavaObject ad)
        {
            listener.onAdImpression(adProvider(ad));
        }

        public void onAdLoaded(AndroidJavaObject ad)
        {
            listener.onAdLoaded(adProvider(ad));
        }

        public void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            listener.onAdLoadFailed(adProvider(ad), AndroidUnityConverter.GetError(error));
        }

        public void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            listener.onAdShowFailed(adProvider(ad), AndroidUnityConverter.GetError(error));
        }

        public void onAdShown(AndroidJavaObject ad)
        {
            listener.onAdShown(adProvider(ad));
        }
    }
}
#endif
