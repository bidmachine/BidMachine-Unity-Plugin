using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidFullscreenAdListener<TAd, TAdListener>
#if UNITY_ANDROID
        : AndroidAdListener<TAd, TAdListener>,
            ICloseableAdListener<AndroidJavaObject>
        where TAdListener : IAdListener<TAd, BMError>, IFullscreenAdListener<TAd, BMError>
    {
        internal AndroidFullscreenAdListener(
            string className,
            TAdListener listener,
            Func<AndroidJavaObject, TAd> factory
        )
            : base(className, listener, factory) { }

        public void OnAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.OnAdClosed(factory(ad), finished);
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
