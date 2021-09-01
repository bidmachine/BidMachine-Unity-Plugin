using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidRewardedRequestListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        private readonly IRewardedRequestListener listener;

        internal AndroidRewardedRequestListener(IRewardedRequestListener listener) : base(
            "io.bidmachine.rewarded.RewardedRequest$AdRequestListener")
        {
            this.listener = listener;
        }

        void onRequestSuccess(AndroidJavaObject rewardedRequest, AndroidJavaObject androidAuctionResult)
        {
            var auctionResult = !string.IsNullOrEmpty(androidAuctionResult.Call<string>("toString"))
                ? androidAuctionResult.Call<string>("toString")
                : "null";
            listener.onRewardedRequestSuccess(getRewardedRequest(rewardedRequest), auctionResult);
        }

        void onRequestFailed(AndroidJavaObject rewardedRequest, AndroidJavaObject bmError)
        {
            var error = new BMError
            {
                code = bmError.Call<int>("getCode"),
                message = bmError.Call<string>("getMessage")
            };

            listener.onRewardedRequestFailed(getRewardedRequest(rewardedRequest), error);
        }

        void onRequestExpired(AndroidJavaObject rewardedRequest)
        {
            listener.onRewardedRequestExpired(getRewardedRequest(rewardedRequest));
        }

        private static RewardedRequest getRewardedRequest(AndroidJavaObject androidJavaObject)
        {
            return new RewardedRequest(new AndroidRewardedRequest(androidJavaObject));
        }
    }
#else
    {
        public AndroidRewardedRequestListener(IRewardedRequestListener listener) { }
    }
#endif
}