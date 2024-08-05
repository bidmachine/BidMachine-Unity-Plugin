#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedAd : AndroidFullscreenAd
    {
        public AndroidRewardedAd()
            : base(
                AndroidUtils.RewardedAdClassName,
                AndroidUtils.RewardedListenerClassName,
                delegate(AndroidJavaObject ad)
                {
                    return new RewardedAd(new AndroidRewardedAd(ad));
                },
                delegate(IAdRequest request)
                {
                    return ((AndroidRewardedRequest)request).JavaObject;
                }
            ) { }

        public AndroidRewardedAd(AndroidJavaObject javaObject)
            : base(
                javaObject,
                AndroidUtils.RewardedListenerClassName,
                delegate(AndroidJavaObject ad)
                {
                    return new RewardedAd(new AndroidRewardedAd(ad));
                },
                delegate(IAdRequest request)
                {
                    return ((AndroidRewardedRequest)request).JavaObject;
                }
            ) { }
    }
}
#endif
