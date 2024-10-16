#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialAd : IInterstitialAd
    {
        private readonly AndroidJavaObject jObject;

        private readonly Func<AndroidJavaObject, IInterstitialAd> adFactory;

        public AndroidInterstitialAd()
            : this(
                new AndroidJavaObject(
                    AndroidConsts.InterstitialAdClassName,
                    AndroidNativeConverter.GetActivity()
                )
            ) { }

        public AndroidInterstitialAd(AndroidJavaObject javaObject)
        {
            jObject = javaObject;
            adFactory = delegate(AndroidJavaObject ad)
            {
                return new InterstitialAd(new AndroidInterstitialAd(ad));
            };
        }

        public void Show()
        {
            jObject.Call("show");
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
            if (request == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>(
                "load",
                ((AndroidInterstitialRequest)request).JavaObject
            );
        }

        public void SetListener(IInterstitialAdListener listener)
        {
            if (listener == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidInterstitialAdListener(
                    AndroidConsts.InterstitialListenerClassName,
                    listener,
                    adFactory
                )
            );
        }
    }
}
#endif
