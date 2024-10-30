#if UNITY_ANDROID
using System;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedAdListener
        : AndroidAdListener<IRewardedAd, IRewardedAdListener>,
            ICommonRewardedAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidRewardedAdListener(
            string className,
            IRewardedAdListener listener,
            Func<AndroidJavaObject, IRewardedAd> adProvider
        )
            : base(className, listener, adProvider) { }

        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }

        public void onAdRewarded(AndroidJavaObject ad)
        {
            listener.onAdRewarded(adProvider(ad));
        }
    }
}
#endif
