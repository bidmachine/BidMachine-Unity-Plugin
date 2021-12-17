using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidInterstitialRequestListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        private readonly IInterstitialRequestListener listener;

        internal AndroidInterstitialRequestListener(IInterstitialRequestListener listener) : base(
            "io.bidmachine.interstitial.InterstitialRequest$AdRequestListener")
        {
            this.listener = listener;
        }

        void onRequestSuccess(AndroidJavaObject androidInterstitialRequest, AndroidJavaObject androidAuctionResult)
        {
            var auctionResult = AuctionResultHelper.BuildAuctionResultString(androidAuctionResult);
            listener.onInterstitialRequestSuccess(getInterstitialRequest(androidInterstitialRequest), auctionResult);
        }

        void onRequestFailed(AndroidJavaObject androidInterstitialRequest, AndroidJavaObject bmError)
        {
            var error = new BMError
            {
                code = bmError.Call<int>("getCode"),
                message = bmError.Call<string>("getMessage")
            };

            listener.onInterstitialRequestFailed(getInterstitialRequest(androidInterstitialRequest), error);
        }

        void onRequestExpired(AndroidJavaObject androidInterstitialRequest)
        {
            listener.onInterstitialRequestExpired(getInterstitialRequest(androidInterstitialRequest));
        }

        private static InterstitialRequest getInterstitialRequest(AndroidJavaObject androidJavaObject)
        {
            return new InterstitialRequest(new AndroidInterstitialRequest(androidJavaObject));
        }
    }
#else
    {
        public AndroidInterstitialRequestListener(IInterstitialRequestListener listener) { }
    }
#endif
}