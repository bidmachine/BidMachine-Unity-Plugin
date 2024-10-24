#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedAd : IRewardedAd
    {
        private readonly AndroidJavaObject jObject;

        private readonly Func<AndroidJavaObject, IRewardedAd> adFactory;

        public AndroidRewardedAd()
            : this(
                new AndroidJavaObject(
                    AndroidConsts.RewardedAdClassName,
                    AndroidNativeConverter.GetActivity()
                )
            ) { }

        public AndroidRewardedAd(AndroidJavaObject javaObject)
        {
            jObject = javaObject;
            adFactory = delegate(AndroidJavaObject ad)
            {
                return new RewardedAd(new AndroidRewardedAd(ad));
            };
        }

        public void Show()
        {
            jObject.Call("show");
        }

        public bool CanShow()
        {
            return jObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            jObject.Call("destroy");
        }

        public void Load(IAdRequest request)
        {
            if (request == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>("load", ((AndroidRewardedRequest)request).JavaObject);
        }

        public void SetListener(IRewardedAdListener listener)
        {
            if (listener == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidRewardedAdListener(
                    AndroidConsts.RewardedListenerClassName,
                    listener,
                    adFactory
                )
            );
        }
    }
}
#endif
