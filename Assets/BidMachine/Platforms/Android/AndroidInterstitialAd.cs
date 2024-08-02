#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialAd : IInterstitialAd
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidInterstitialAd()
        {
            javaObject = new AndroidJavaObject(
                AndroidUtils.InterstitialAdClassName,
                AndroidUtils.GetActivity()
            );
        }

        public AndroidInterstitialAd(AndroidJavaObject javaObject) => this.javaObject = javaObject;

        public void Show()
        {
            javaObject.Call("show");
        }

        public bool CanShow()
        {
            return javaObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            javaObject.Call("destroy");
        }

        public void Load(IInterstitialRequest request)
        {
            if (request == null)
            {
                return;
            }
            javaObject.Call<AndroidJavaObject>(
                "load",
                ((AndroidInterstitialRequest)request).JavaObject
            );
        }

        public void SetListener(IInterstitialListener listener)
        {
            if (listener == null)
            {
                return;
            }
            javaObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidFullscreenAdListener<IInterstitialAd, IInterstitialListener>(
                    AndroidUtils.InterstitialListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new InterstitialAd(new AndroidInterstitialAd(ad));
                    }
                )
            );
        }
    }
}
#endif
