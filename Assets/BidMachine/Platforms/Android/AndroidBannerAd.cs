#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerAd : IBannerAd
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidBannerAd()
        {
            javaObject = new AndroidJavaObject(
                AndroidUtils.BannerAdClassName,
                AndroidUtils.GetActivity()
            );
        }

        public AndroidBannerAd(AndroidJavaObject javaObject) => this.javaObject = javaObject;

        public bool Show(int yAxis, int xAxis, IBannerAd bannerView, BannerSize bannerSize)
        {
            AndroidJavaObject jBannerSize;
            switch (bannerSize)
            {
                case BannerSize.Size_320x50:
                {
                    jBannerSize = new AndroidJavaClass(
                        "io.bidmachine.banner.BannerSize"
                    ).GetStatic<AndroidJavaObject>("Size_320x50");
                    break;
                }
                case BannerSize.Size_300x250:
                {
                    jBannerSize = new AndroidJavaClass(
                        "io.bidmachine.banner.BannerSize"
                    ).GetStatic<AndroidJavaObject>("Size_300x250");
                    break;
                }
                case BannerSize.Size_728x90:
                {
                    jBannerSize = new AndroidJavaClass(
                        "io.bidmachine.banner.BannerSize"
                    ).GetStatic<AndroidJavaObject>("Size_728x90");
                    break;
                }
                default:
                    jBannerSize = new AndroidJavaClass(
                        "io.bidmachine.banner.BannerSize"
                    ).GetStatic<AndroidJavaObject>("Size_320x50");
                    break;
            }

            return GetBannerShowHelper()
                .Call<bool>(
                    "show",
                    AndroidUtils.GetActivity(),
                    ((AndroidBannerAd)bannerView).javaObject,
                    jBannerSize,
                    xAxis,
                    yAxis
                );
        }

        public void Hide()
        {
            GetBannerShowHelper().Call("hide");
        }

        public bool CanShow()
        {
            return javaObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            javaObject.Call("destroy");
        }

        public void Load(IBannerRequest request)
        {
            javaObject.Call<AndroidJavaObject>("load", ((AndroidBannerRequest)request).Client);
        }

        public void SetListener(IBannerListener listener)
        {
            if (listener == null)
            {
                return;
            }

            javaObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdListener<IBannerAd, IBannerListener>(
                    AndroidUtils.BannerListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new BannerAd(new AndroidBannerAd(ad));
                    }
                )
            );
        }

        private AndroidJavaObject GetBannerShowHelper()
        {
            var bannerShowHelperClass = new AndroidJavaClass(
                "io.bidmachine.ads.extensions.unity.banner.BannerShowHelper"
            );
            return bannerShowHelperClass.CallStatic<AndroidJavaObject>("get");
        }
    }
}

#endif
