using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBannerListener
#if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IBannerListener listener;
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
            var bannerView = new BannerView(new AndroidBannerView(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onBannerAdLoadFailed(bannerView, bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onBannerAdShown(bannerView);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onBannerAdImpression(bannerView);
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onBannerAdClicked(bannerView);
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onBannerAdClicked(bannerView);
        }
    }
#else
    {
        public AndroidBannerListener(IBannerListener bannerListener) { }
    }
#endif
}