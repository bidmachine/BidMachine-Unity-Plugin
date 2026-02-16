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

#pragma warning disable CS0618
        public BannerSize GetSize()
        {
            return AndroidUnityConverter.GetBannerSize(jObject.Call<AndroidJavaObject>("getSize"));
        }
#pragma warning restore CS0618

        public BannerAdSize GetBannerAdSize()
        {
            return AndroidUnityConverter.GetBannerAdSize(jObject.Call<AndroidJavaObject>("getBannerAdSize"));
        }

        public string GetAuctionResult()
        {
            return AndroidUnityConverter.GetAuctionResult(
                jObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public AuctionResult GetAuctionResultObject()
        {
            return AndroidUnityConverter.GetAuctionResultObject(
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
