using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAdRequestListener
#if UNITY_ANDROID
        : AndroidJavaProxy,
            ICommonAdRequestListener<AndroidJavaObject, AndroidJavaObject, AndroidJavaObject>
    {
        private readonly IAdRequestListener listener;

        private readonly Func<AndroidJavaObject, IAdRequest> factory;

        internal AndroidAdRequestListener(
            string className,
            IAdRequestListener listener,
            Func<AndroidJavaObject, IAdRequest> factory
        )
            : base(className)
        {
            this.listener = listener;
            this.factory = factory;
        }

        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(
                factory(request),
                AndroidUtils.BuildAuctionResultString(auctionResult)
            );
        }

        public void onRequestFailed(AndroidJavaObject request, AndroidJavaObject error)
        {
            listener.onRequestFailed(factory(request), AndroidUtils.GetError(error));
        }

        public void onRequestExpired(AndroidJavaObject request)
        {
            listener.onRequestExpired(factory(request));
        }
    }
#else
    {
        internal AndroidAdRequestListener() { }
    }
#endif
}
