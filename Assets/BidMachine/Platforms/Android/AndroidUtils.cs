#if PLATFORM_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidUtils
    {
        // Banner Ad constants
        public const string BannerAdClassName = "io.bidmachine.banner.BannerView";
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

        public static BMError GetError(AndroidJavaObject obj)
        {
            return new BMError
            {
                Code = obj.Call<int>("getCode"),
                Message = obj.Call<string>("getMessage")
            };
        }

        public static object GetJavaPrimitiveObject(object value)
        {
            return value switch
            {
                char => new AndroidJavaObject("java.lang.Character", value),
                bool => new AndroidJavaObject("java.lang.Boolean", value),
                int => new AndroidJavaObject("java.lang.Integer", value),
                long => new AndroidJavaObject("java.lang.Long", value),
                float => new AndroidJavaObject("java.lang.Float", value),
                double => new AndroidJavaObject("java.lang.Double", value),
                _ => value,
            };
        }

        public static AndroidJavaObject GetJavaListObject<T>(T[] items)
        {
            AndroidJavaObject javaTypeArrayList = new AndroidJavaObject("java.util.ArrayList");
            foreach (T item in items)
            {
                javaTypeArrayList.Call<bool>("add", GetJavaPrimitiveObject(item));
            }
            return javaTypeArrayList;
        }

        public static Dictionary<string, string> GetCsDictionary(AndroidJavaObject obj)
        {
            var mapClazz = new AndroidJavaObject("java.util.HashMap");
            var setClazz = new AndroidJavaObject("java.util.HashSet");

            IntPtr keySetMethod = AndroidJNIHelper.GetMethodID(
                mapClazz.GetRawClass(),
                "keySet",
                "()Ljava/util/Set;"
            );
            IntPtr set = AndroidJNI.CallObjectMethod(
                obj.GetRawObject(),
                keySetMethod,
                new jvalue[] { }
            );
            IntPtr toArrayMethod = AndroidJNIHelper.GetMethodID(
                setClazz.GetRawClass(),
                "toArray",
                "()[Ljava/lang/Object;"
            );
            IntPtr array = AndroidJNI.CallObjectMethod(set, toArrayMethod, new jvalue[] { });

            var dict = new Dictionary<string, string>();
            var keys = AndroidJNIHelper.ConvertFromJNIArray<string[]>(array);

            IntPtr getMethod = AndroidJNIHelper.GetMethodID(
                mapClazz.GetRawClass(),
                "get",
                "(Ljava/lang/Object;)Ljava/lang/Object;"
            );

            foreach (var k in keys)
            {
                string v = AndroidJNI.CallStringMethod(
                    obj.GetRawObject(),
                    getMethod,
                    new jvalue[] { new jvalue() { l = AndroidJNI.NewStringUTF(k) } }
                );
                dict.Add($"\"{k}\"", $"\"{v}\"");
            }

            return dict;
        }

        public static string BuildAuctionResultString(AndroidJavaObject obj)
        {
            var customParamsAndroidJavaObject = obj.Call<AndroidJavaObject>("getCustomParams");
            var adDomainsAndroidJavaObject = obj.Call<AndroidJavaObject>("getAdDomains");

            string dealID = string.IsNullOrEmpty(obj.Call<string>("getDeal"))
                ? "null"
                : obj.Call<string>("getDeal").ToUpper();
            string demandSource = string.IsNullOrEmpty(obj.Call<string>("getDemandSource"))
                ? "null"
                : obj.Call<string>("getDemandSource");
            string cID = string.IsNullOrEmpty(obj.Call<string>("getCid"))
                ? "null"
                : obj.Call<string>("getCid");
            string customParams = string.Join(
                ",",
                GetCsDictionary(customParamsAndroidJavaObject)
                    .Select(pair =>
                        String.Format("{0}:{1}", pair.Key.ToString(), pair.Value.ToString())
                    )
                    .ToArray()
            );
            string adDomains = string.Join(
                ",",
                AndroidJNIHelper
                    .ConvertFromJNIArray<string[]>(adDomainsAndroidJavaObject.GetRawObject())
                    .ToList()
                    .Select(adDomain => $"\"{adDomain}\"")
                    .ToList()
            );
            string creativeID = string.IsNullOrEmpty(obj.Call<string>("getCreativeId"))
                ? "null"
                : obj.Call<string>("getCreativeId");
            string bidID = string.IsNullOrEmpty(obj.Call<string>("getId"))
                ? "null"
                : obj.Call<string>("getId");
            string price = string.IsNullOrEmpty(obj.Call<double>("getPrice").ToString())
                ? "null"
                : obj.Call<double>("getPrice").ToString();

            var auctionResult =
                $"{{\"dealID\":\"{dealID}\",\"demandSource\":\"{demandSource}\",\"cID\":\"{cID}\",\"customParams\":{{{customParams}}},\"adDomains\":[{adDomains}],\"creativeID\":\"{creativeID}\",\"bidID\":\"{bidID}\",\"price\":{price}}}";

            return auctionResult;
        }

        public static AndroidJavaObject GetActivity()
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
#endif
