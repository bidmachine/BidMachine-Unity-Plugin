#if PLATFORM_ANDROID
using System;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidAuctionRequestListener : AndroidCommonAdRequestListener<AuctionResult>
    {
        internal AndroidAuctionRequestListener(
            string className,
            ICommonAdRequestListener<IAdRequest, AuctionResult, BMError> listener,
            Func<AndroidJavaObject, IAdRequest> factory
        )
            : base(className, listener, factory) { }

        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(
                factory(request),
                AndroidUnityConverter.GetAuctionResultObject(auctionResult)
            );
        }
    }
}
#endif
