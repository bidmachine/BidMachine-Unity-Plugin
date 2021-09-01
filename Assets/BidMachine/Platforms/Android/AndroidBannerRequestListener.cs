using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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
            var bannerRequest = new BannerRequest(new AndroidBannerRequest(androidBannerRequest));
            //listener.onBannerRequestSuccess(bannerRequest, auctionResult);
        }

        void onRequestFailed(AndroidJavaObject androidBannerRequest, AndroidJavaObject bmError)
        {
            var bannerRequest = new BannerRequest(new AndroidBannerRequest(androidBannerRequest));
            var error = new BMError
            {
                code = bmError.Call<int>("getCode"),
                brief = bmError.Call<string>("getBrief"),
                message = bmError.Call<string>("getMessage")
            };

            listener.onBannerRequestFailed(bannerRequest, error);
        }

        void onRequestExpired(AndroidJavaObject androidBannerRequest)
        {
            var bannerRequest = new BannerRequest(new AndroidBannerRequest(androidBannerRequest));
            listener.onBannerRequestExpired(bannerRequest);
        }
    }
#else
    {
        public AndroidBannerRequestListener(IBannerRequestListener listener) { }
    }
#endif
}