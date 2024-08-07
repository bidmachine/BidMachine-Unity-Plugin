using System;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidFullscreenAdListener
#if UNITY_ANDROID
        : AndroidAdListener<IFullscreenAd, IFullscreenAdListener<IFullscreenAd>>,
            ICommonFullscreenAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidFullscreenAdListener(
            string className,
            IFullscreenAdListener<IFullscreenAd> listener,
            Func<AndroidJavaObject, IFullscreenAd> adProvider
        )
            : base(className, listener, adProvider) { }

        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }
    }
#else
    {
        internal AndroidFullscreenAdListener() { }
    }
#endif
}
