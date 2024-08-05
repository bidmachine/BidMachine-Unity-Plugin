#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerRequest : IBannerRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidBannerRequest(AndroidJavaObject jObject)
        {
            this.jObject = jObject;
        }

        public BannerSize GetSize()
        {
            return AndroidUtils.GetBannerSize(jObject.Call<AndroidJavaObject>("getSize"));
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
