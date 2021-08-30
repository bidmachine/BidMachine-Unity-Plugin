using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    public class AndroidRewardedAdListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IRewardedAdListener listener;
        internal AndroidRewardedAdListener(IRewardedAdListener listener) : base("io.bidmachine.rewarded.RewardedListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdLoaded(rewardedAd);
        }

        void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdClosed(rewardedAd, finished);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onAdLoadFailed(rewardedAd, bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdShown(rewardedAd);
        }

        void onAdShowFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onAdShowFailed(rewardedAd, bmError);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdImpression(rewardedAd);
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdClicked(rewardedAd);
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdExpired(rewardedAd);
        }

        void onAdRewarded(AndroidJavaObject ad)
        {
            RewardedAd rewardedAd = new RewardedAd(new AndroidRewardedAd(ad));
            listener.onAdRewarded(rewardedAd);
        }
    }
#else
    {
        public AndroidRewardedAdListener(IRewardedAdListener rewardedAdListener) { }
    }
#endif
}
