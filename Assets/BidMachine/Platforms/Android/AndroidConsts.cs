#if PLATFORM_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidConsts
    {
        // Banner Ad constants
        public const string BannerViewClassName = "io.bidmachine.banner.BannerView";
        public const string BannerListenerClassName = "io.bidmachine.banner.BannerListener";
        public const string BannerRequestBuilderClassName =
            "io.bidmachine.banner.BannerRequest$Builder";
        public const string BannerRequestListenerClassName =
            "io.bidmachine.banner.BannerRequest$AdRequestListener";

        // Interstitial Ad constants
        public const string InterstitialAdClassName = "io.bidmachine.interstitial.InterstitialAd";
        public const string InterstitialListenerClassName =
            "io.bidmachine.interstitial.InterstitialListener";
        public const string InterstitialRequestBuilderClassName =
            "io.bidmachine.interstitial.InterstitialRequest$Builder";
        public const string InterstitialRequestListenerClassName =
            "io.bidmachine.interstitial.InterstitialRequest$AdRequestListener";

        // Rewarded Ad constants
        public const string RewardedAdClassName = "io.bidmachine.rewarded.RewardedAd";
        public const string RewardedListenerClassName = "io.bidmachine.rewarded.RewardedListener";
        public const string RewardedRequestBuilderClassName =
            "io.bidmachine.rewarded.RewardedRequest$Builder";
        public const string RewardedRequestListenerClassName =
            "io.bidmachine.rewarded.RewardedRequest$AdRequestListener";
    }
}
#endif
