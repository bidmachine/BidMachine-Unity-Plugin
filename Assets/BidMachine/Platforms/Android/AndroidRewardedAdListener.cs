using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    public class AndroidRewardedAdListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        readonly IRewardedAdListener listener;

        internal AndroidRewardedAdListener(IRewardedAdListener listener) : base(
            "io.bidmachine.rewarded.RewardedListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            listener.onAdLoaded(new RewardedAd(new AndroidRewardedAd(ad)));
        }

        void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(new RewardedAd(new AndroidRewardedAd(ad)), finished);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onAdLoadFailed((new RewardedAd(new AndroidRewardedAd(ad))), bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            listener.onAdShown((new RewardedAd(new AndroidRewardedAd(ad))));
        }

        void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onAdShowFailed((new RewardedAd(new AndroidRewardedAd(ad))), bmError);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            listener.onAdImpression((new RewardedAd(new AndroidRewardedAd(ad))));
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            listener.onAdClicked((new RewardedAd(new AndroidRewardedAd(ad))));
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            listener.onAdExpired((new RewardedAd(new AndroidRewardedAd(ad))));
        }

        void onAdRewarded(AndroidJavaObject ad)
        {
            listener.onAdRewarded((new RewardedAd(new AndroidRewardedAd(ad))));
        }
    }
#else
    {
        public AndroidRewardedAdListener(IRewardedAdListener rewardedAdListener) { }
    }
#endif
}