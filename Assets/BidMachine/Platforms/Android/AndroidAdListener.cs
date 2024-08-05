using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdListener<TAd, TAdListener>
#if UNITY_ANDROID
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
            listener.onAdLoadFailed(adProvider(ad), AndroidUtils.GetError(error));
        }

        public void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            listener.onAdShowFailed(adProvider(ad), AndroidUtils.GetError(error));
        }

        public void onAdShown(AndroidJavaObject ad)
        {
            listener.onAdShown(adProvider(ad));
        }
    }
#else
    {
        public AndroidAdListener(
            string listenerClassName,
            IAdListener<TAdType, TRequestType, BMError> listener,
            Func<AndroidJavaObject, TAdType> adProvider
        ) { }
    }
#endif
}
