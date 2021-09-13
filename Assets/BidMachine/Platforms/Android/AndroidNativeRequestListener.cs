using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidNativeRequestListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        private readonly INativeRequestListener listener;

        internal AndroidNativeRequestListener(INativeRequestListener listener) : base(
            "io.bidmachine.nativead.NativeRequest$AdRequestListener")
        {
            this.listener = listener;
        }

        void onRequestSuccess(AndroidJavaObject androidNativeRequest, AndroidJavaObject androidAuctionResult)
        {
            var auctionResult = !string.IsNullOrEmpty(androidAuctionResult.Call<string>("toString"))
                ? androidAuctionResult.Call<string>("toString")
                : "null";
            listener.onNativeRequestSuccess(getNativeRequest(androidNativeRequest), auctionResult);
        }

        void onRequestFailed(AndroidJavaObject androidNativeRequest, AndroidJavaObject bmError)
        {
            var error = new BMError
            {
                code = bmError.Call<int>("getCode"),
                message = bmError.Call<string>("getMessage")
            };
            listener.onNativeRequestFailed(getNativeRequest(androidNativeRequest), error);
        }

        void onRequestExpired(AndroidJavaObject androidNativeRequest)
        {
            listener.onNativeRequestExpired(getNativeRequest(androidNativeRequest));
        }

        private static NativeRequest getNativeRequest(AndroidJavaObject androidJavaObject)
        {
            return new NativeRequest(new AndroidNativeRequest(androidJavaObject));
        }
    }
#else
    {
        public AndroidNativeRequestListener(INativeRequestListener listener) { }
    }
#endif
}