using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidBannerRequestListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        private readonly IBannerRequestListener listener;

        internal AndroidBannerRequestListener(IBannerRequestListener listener) : base(
            "io.bidmachine.banner.BannerRequest$AdRequestListener")
        {
            this.listener = listener;
        }

        void onRequestSuccess(AndroidJavaObject androidBannerRequest, AndroidJavaObject androidAuctionResult)
        {
            var auctionResult = !string.IsNullOrEmpty(androidAuctionResult.Call<string>("toString"))
                ? androidAuctionResult.Call<string>("toString")
                : "null";
            listener.onBannerRequestSuccess(getBannerRequest(androidBannerRequest), auctionResult);
        }

        void onRequestFailed(AndroidJavaObject androidBannerRequest, AndroidJavaObject bmError)
        {
            var error = new BMError
            {
                code = bmError.Call<int>("getCode"),
                message = bmError.Call<string>("getMessage")
            };

            listener.onBannerRequestFailed(getBannerRequest(androidBannerRequest), error);
        }

        void onRequestExpired(AndroidJavaObject androidBannerRequest)
        {
            listener.onBannerRequestExpired(getBannerRequest(androidBannerRequest));
        }

        private static BannerRequest getBannerRequest(AndroidJavaObject androidJavaObject)
        {
            return new BannerRequest(new AndroidBannerRequest(androidJavaObject));
        }
    }
#else
    {
        public AndroidBannerRequestListener(IBannerRequestListener listener) { }
    }
#endif
}