#if PLATFORM_ANDROID
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialRequest : IInterstitialRequest
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidInterstitialRequest(AndroidJavaObject javaObject) =>
            this.javaObject = javaObject;

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
