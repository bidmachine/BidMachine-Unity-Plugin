#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerRequest : IBannerRequest
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject Client => javaObject;

        public AndroidBannerRequest(AndroidJavaObject javaObject)
        {
            this.javaObject = javaObject;
        }

        public BannerSize GetSize()
        {
            var size = javaObject.Call<AndroidJavaObject>("getSize").Call<string>("toString");
            return size switch
            {
                "Size_320x50" => BannerSize.Size_320x50,
                "Size_300x250" => BannerSize.Size_300x250,
                "Size_728x90" => BannerSize.Size_728x90,
                _ => BannerSize.Size_320x50 // Default case
            };
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
