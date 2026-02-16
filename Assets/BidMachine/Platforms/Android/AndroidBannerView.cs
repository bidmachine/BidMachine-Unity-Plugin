#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBannerView : IBannerView
    {
        private readonly AndroidJavaObject jObject;

        private AndroidJavaClass jcBannerShowHelper;
        private AndroidJavaObject jBannerShowHelper;

        public AndroidBannerView()
        {
            jObject = new AndroidJavaObject(
                AndroidConsts.BannerViewClassName,
                AndroidNativeConverter.GetActivity()
            );
        }

        public AndroidBannerView(AndroidJavaObject javaObject) => this.jObject = javaObject;

#pragma warning disable CS0618
        public bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size)
        {
            var jSize = AndroidNativeConverter.GetBannerSize(size);
            var client = ((BannerView)view).Client;
            return GetBannerShowHelper()
                .Call<bool>(
                    "show",
                    AndroidNativeConverter.GetActivity(),
                    ((AndroidBannerView)client).jObject,
                    jSize,
                    xAxis,
                    yAxis
                );
        }
#pragma warning restore CS0618

        public bool Show(int yAxis, int xAxis, IBannerView view, BannerAdSize size)
        {
            var jSize = AndroidNativeConverter.GetBannerAdSize(size);
            var client = ((BannerView)view).Client;
            return GetBannerShowHelper()
                .Call<bool>(
                    "show",
                    AndroidNativeConverter.GetActivity(),
                    ((AndroidBannerView)client).jObject,
                    jSize,
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
            return jObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            jObject.Call("destroy");
        }

        public void Load(IAdRequest request)
        {
            jObject.Call<AndroidJavaObject>("load", ((AndroidBannerRequest)request).JavaObject);
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            if (listener == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdListener<IBannerView, IAdListener<IBannerView>>(
                    AndroidConsts.BannerListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new BannerView(new AndroidBannerView(ad));
                    }
                )
            );
        }

        public BannerAdSize GetAdSize()
        {
            var jAdSize = jObject.Call<AndroidJavaObject>("getAdSize");
            if (jAdSize == null)
            {
                return BannerAdSize.Banner; // Default
            }
            return AndroidUnityConverter.GetBannerAdSize(jAdSize);
        }

        private AndroidJavaObject GetBannerShowHelper()
        {
            jcBannerShowHelper ??= new AndroidJavaClass(
                "io.bidmachine.ads.extensions.unity.banner.BannerShowHelper"
            );
            jBannerShowHelper ??= jcBannerShowHelper.CallStatic<AndroidJavaObject>("get");

            return jBannerShowHelper;
        }
    }
}
#endif
