using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{

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
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onAdLoaded(bannerView);
        }

        void onAdLoadFailed(AndroidJavaObject ad, AndroidJavaObject error)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            BMError bmError = new BMError();
            bmError.code = error.Call<int>("getCode");
            bmError.brief = error.Call<string>("getBrief");
            bmError.message = error.Call<string>("getMessage");
            listener.onAdLoadFailed(bannerView, bmError);
        }

        void onAdShown(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onAdShown(bannerView);
        }

        void onAdImpression(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onAdImpression(bannerView);
        }

        void onAdClicked(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onAdClicked(bannerView);
        }

        void onAdExpired(AndroidJavaObject ad)
        {
            BannerView bannerView = new BannerView(new AndroidBannerView(ad));
            listener.onAdClicked(bannerView);
        }
    }
#else
    {
        public AndroidBannerListener(IBannerListener bannerListener) { }
    }
#endif
}