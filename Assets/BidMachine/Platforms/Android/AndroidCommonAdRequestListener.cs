#if PLATFORM_ANDROID
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal abstract class AndroidCommonAdRequestListener<TAuctionResult>
        : AndroidJavaProxy,
            ICommonAdRequestListener<AndroidJavaObject, AndroidJavaObject, AndroidJavaObject>
    {
        protected readonly ICommonAdRequestListener<IAdRequest, TAuctionResult, BMError> listener;

        protected readonly Func<AndroidJavaObject, IAdRequest> factory;

        internal AndroidCommonAdRequestListener(
            string className,
            ICommonAdRequestListener<IAdRequest, TAuctionResult, BMError> listener,
            Func<AndroidJavaObject, IAdRequest> factory
        )
            : base(className)
        {
            this.listener = listener;
            this.factory = factory;
        }

        public void onRequestFailed(AndroidJavaObject request, AndroidJavaObject error)
        {
            listener.onRequestFailed(factory(request), AndroidUnityConverter.GetError(error));
        }

        public void onRequestExpired(AndroidJavaObject request)
        {
            listener.onRequestExpired(factory(request));
        }
    }
}
#endif
