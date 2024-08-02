#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedRequest : IRewardedRequest
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidRewardedRequest(AndroidJavaObject javaObject)
        {
            this.javaObject = javaObject;
        }

        public string GetAuctionResult()
        {
            return AndroidUtils.BuildAuctionResultString(
                javaObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public bool IsDestroyed()
        {
            return javaObject.Call<bool>("isDestroyed");
        }

        public bool IsExpired()
        {
            return javaObject.Call<bool>("isExpired");
        }
    }
}
#endif
