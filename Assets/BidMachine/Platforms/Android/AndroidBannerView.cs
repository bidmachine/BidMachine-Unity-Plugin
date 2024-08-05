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
                AndroidUtils.BannerViewClassName,
                AndroidUtils.GetActivity()
            );
        }

        public AndroidBannerView(AndroidJavaObject javaObject) => this.jObject = javaObject;

        public bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size)
        {
            var jSize = AndroidUtils.GetBannerSize(size);
            var client = ((BannerView)view).Client;
            return GetBannerShowHelper()
                .Call<bool>(
                    "show",
                    AndroidUtils.GetActivity(),
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
                    AndroidUtils.BannerListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new BannerView(new AndroidBannerView(ad));
                    }
                )
            );
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
