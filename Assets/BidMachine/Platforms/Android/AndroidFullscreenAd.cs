#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Common;
using System;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidFullscreenAd : IFullscreenAd
    {
        private readonly AndroidJavaObject jObject;

        private readonly string listenerClassName;

        private readonly Func<AndroidJavaObject, IFullscreenAd> adFactory;

        private readonly Func<IAdRequest, AndroidJavaObject> requestProvider;

        public AndroidFullscreenAd(
            string className,
            string listenerClassName,
            Func<AndroidJavaObject, IFullscreenAd> adFactory,
            Func<IAdRequest, AndroidJavaObject> requestProvider
        )
        {
            jObject = new AndroidJavaObject(className, AndroidUtils.GetActivity());
            this.listenerClassName = listenerClassName;
            this.adFactory = adFactory;
            this.requestProvider = requestProvider;
        }

        public AndroidFullscreenAd(
            AndroidJavaObject javaObject,
            string listenerClassName,
            Func<AndroidJavaObject, IFullscreenAd> adFactory,
            Func<IAdRequest, AndroidJavaObject> requestProvider
        )
        {
            this.jObject = javaObject;
            this.listenerClassName = listenerClassName;
            this.adFactory = adFactory;
            this.requestProvider = requestProvider;
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

            jObject.Call<AndroidJavaObject>("load", requestProvider(request));
        }

        public void SetListener(IFullscreenAdListener<IFullscreenAd> listener)
        {
            if (listener == null)
            {
                return;
            }

            jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidFullscreenAdListener(listenerClassName, listener, adFactory)
            );
        }
    }
}
#endif
