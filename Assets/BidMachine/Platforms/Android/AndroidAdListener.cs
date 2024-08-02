using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdListener<TAdType, TAdListener>
#if UNITY_ANDROID
        : AndroidJavaProxy,
            IAdListener<AndroidJavaObject, AndroidJavaObject>
        where TAdListener : IAdListener<TAdType, BMError>
    {
        protected readonly TAdListener listener;

        protected readonly Func<AndroidJavaObject, TAdType> factory;

        internal AndroidAdListener(
            string className,
            TAdListener listener,
            Func<AndroidJavaObject, TAdType> factory
        )
            : base(className)
        {
            this.listener = listener;
            this.factory = factory;
        }

        public void OnAdExpired(AndroidJavaObject ad)
        {
            listener.OnAdExpired(factory(ad));
        }

        public void OnAdImpression(AndroidJavaObject ad)
        {
            listener.OnAdImpression(factory(ad));
        }

        public void OnAdLoaded(AndroidJavaObject ad)
        {
            listener.OnAdLoaded(factory(ad));
        }

        public void OnAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            listener.OnAdLoadFailed(factory(ad), AndroidUtils.GetError(error));
        }

        public void OnAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            listener.OnAdShowFailed(factory(ad), AndroidUtils.GetError(error));
        }

        public void OnAdShown(AndroidJavaObject ad)
        {
            listener.OnAdShown(factory(ad));
        }
    }
#else
    {
        public AndroidAdListener(
            string listenerClassName,
            IAdListener<TAdType, TRequestType, BMError> listener,
            Func<AndroidJavaObject, TAdType> adFactory
        ) { }
    }
#endif
}
