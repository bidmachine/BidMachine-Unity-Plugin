using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdRequestListener<TAdRequest>
#if UNITY_ANDROID
        : AndroidJavaProxy,
            IAdRequestListener<AndroidJavaObject, AndroidJavaObject, AndroidJavaObject>
    {
        private readonly IAdRequestListener<TAdRequest, string, BMError> listener;

        private readonly Func<AndroidJavaObject, TAdRequest> factory;

        internal AndroidAdRequestListener(
            string className,
            IAdRequestListener<TAdRequest, string, BMError> listener,
            Func<AndroidJavaObject, TAdRequest> factory
        )
            : base(className)
        {
            this.listener = listener;
            this.factory = factory;
        }

        public void OnAdRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.OnAdRequestSuccess(
                factory(request),
                AndroidUtils.BuildAuctionResultString(auctionResult)
            );
        }

        public void OnAdRequestFailed(AndroidJavaObject request, AndroidJavaObject error)
        {
            listener.OnAdRequestFailed(factory(request), AndroidUtils.GetError(error));
        }

        public void OnAdRequestExpired(AndroidJavaObject request)
        {
            listener.OnAdRequestExpired(factory(request));
        }
    }
#else
    {
        public AndroidAdRequestListener(
            string className,
            IAdRequestListener<TAdRequest, string, BMError> listener,
            Func<AndroidJavaObject, TAdRequest> adRequestFactory
        )
    }
#endif
}
