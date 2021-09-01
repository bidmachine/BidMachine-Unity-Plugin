using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidBannerListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        readonly IBannerListener listener;
        internal AndroidBannerListener(IBannerListener listener) : base("io.bidmachine.banner.BannerListener")
        {
            this.listener = listener;
        }

        void onAdLoaded(AndroidJavaObject ad)
        {
            var bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onBannerAdLoaded(bannerView);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            var bmError = new BMError
            {
                code = error.Call<int>("getCode"),
                message = error.Call<string>("getMessage")
            };
            listener.onBannerAdLoadFailed(new BannerView(new AndroidBannerView(ad)), bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            listener.onBannerAdShown(new BannerView(new AndroidBannerView(ad)));
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            listener.onBannerAdImpression(new BannerView(new AndroidBannerView(ad)));
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            listener.onBannerAdClicked(new BannerView(new AndroidBannerView(ad)));
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            listener.onBannerAdClicked(new BannerView(new AndroidBannerView(ad)));
        }
    }
#else
    {
        public AndroidBannerListener(IBannerListener bannerListener) { }
    }
#endif
}