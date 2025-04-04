#if PLATFORM_ANDROID
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    [Obsolete("Use AndroidAuctionRequestListener instead")]
    internal class AndroidAdRequestListener : AndroidCommonAdRequestListener<string>
    {
        internal AndroidAdRequestListener(
            string className,
            ICommonAdRequestListener<IAdRequest, string, BMError> listener,
            Func<AndroidJavaObject, IAdRequest> factory
        )
            : base(className, listener, factory) { }

        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(
                factory(request),
                AndroidUnityConverter.GetAuctionResult(auctionResult)
            );
        }
    }
}
#endif
