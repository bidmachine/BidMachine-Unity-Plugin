#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidRewardedRequest : IAdRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidRewardedRequest(AndroidJavaObject javaObject)
        {
            this.jObject = javaObject;
        }

        public string GetAuctionResult()
        {
            return AndroidUtils.BuildAuctionResultString(
                jObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public bool IsDestroyed()
        {
            return jObject.Call<bool>("isDestroyed");
        }

        public bool IsExpired()
        {
            return jObject.Call<bool>("isExpired");
        }
    }
}
#endif
