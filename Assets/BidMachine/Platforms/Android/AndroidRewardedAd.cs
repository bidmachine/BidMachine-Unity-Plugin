#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedAd : IRewardedAd
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidRewardedAd()
        {
            javaObject = new AndroidJavaObject(
                AndroidUtils.RewardedAdClassName,
                AndroidUtils.GetActivity()
            );
        }

        public AndroidRewardedAd(AndroidJavaObject javaObject) => this.javaObject = javaObject;

        public void Show()
        {
            javaObject.Call("show");
        }

        public bool CanShow()
        {
            return javaObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            javaObject.Call("destroy");
        }

        public void Load(IRewardedRequest request)
        {
            if (request == null)
            {
                return;
            }
            javaObject.Call<AndroidJavaObject>(
                "load",
                ((AndroidRewardedRequest)request).JavaObject
            );
        }

        public void SetListener(IRewardedlListener listener)
        {
            if (listener == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdListener<IRewardedAd, IRewardedlListener>(
                    AndroidUtils.RewardedListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new RewardedAd(new AndroidRewardedAd(ad));
                    }
                )
            );
        }
    }
}
#endif
