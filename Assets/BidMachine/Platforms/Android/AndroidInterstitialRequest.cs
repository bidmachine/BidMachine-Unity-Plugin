﻿#if PLATFORM_ANDROID
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidInterstitialRequest : IAdRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidInterstitialRequest(AndroidJavaObject javaObject) =>
            this.jObject = javaObject;

        public string GetAuctionResult()
        {
            return AndroidUtils.BuildAuctionResultString(
                jObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public bool IsDestroyed()
        {
            return jObject.Call<bool>("isDestroyed");
        }

        public bool IsExpired()
        {
            return jObject.Call<bool>("isExpired");
        }
    }
}
#endif