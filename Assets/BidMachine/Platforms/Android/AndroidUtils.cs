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

        public static object GetPrimitive(object obj)
        {
            return obj switch
            {
                char => new AndroidJavaObject("java.lang.Character", obj),
                bool => new AndroidJavaObject("java.lang.Boolean", obj),
                int => new AndroidJavaObject("java.lang.Integer", obj),
                long => new AndroidJavaObject("java.lang.Long", obj),
                float => new AndroidJavaObject("java.lang.Float", obj),
                double => new AndroidJavaObject("java.lang.Double", obj),
                _ => obj,
            };
        }

        public static AndroidJavaObject GetArrayList<T>(T[] objs)
        {
            var jArrayList = new AndroidJavaObject("java.util.ArrayList");
            foreach (var obj in objs)
            {
                jArrayList.Call<bool>("add", GetPrimitive(obj));
            }
            return jArrayList;
        }

        public static Dictionary<string, string> GetCsDictionary(AndroidJavaObject jObj)
        {
            var mapClazz = new AndroidJavaObject("java.util.HashMap");
            var setClazz = new AndroidJavaObject("java.util.HashSet");

            IntPtr keySetMethod = AndroidJNIHelper.GetMethodID(
                mapClazz.GetRawClass(),
                "keySet",
                "()Ljava/util/Set;"
            );
            IntPtr set = AndroidJNI.CallObjectMethod(
                jObj.GetRawObject(),
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
                var v = AndroidJNI.CallStringMethod(
                    jObj.GetRawObject(),
                    getMethod,
                    new jvalue[] { new() { l = AndroidJNI.NewStringUTF(k) } }
                );
                dict.Add($"\"{k}\"", $"\"{v}\"");
            }

            return dict;
        }

        public static AndroidJavaObject GetBannerSize(BannerSize bannerSize)
        {
            return bannerSize switch
            {
                BannerSize.Size_300x250 => GetBannerSize("Size_300x250"),
                BannerSize.Size_728x90 => GetBannerSize("Size_728x90"),
                _ => GetBannerSize("Size_320x50"),
            };
        }

        public static BannerSize GetBannerSize(AndroidJavaObject jBannerSize)
        {
            var size = jBannerSize.Call<string>("toString");
            return size switch
            {
                "Size_320x50" => BannerSize.Size_320x50,
                "Size_300x250" => BannerSize.Size_300x250,
                "Size_728x90" => BannerSize.Size_728x90,
                _ => BannerSize.Size_320x50 // Default case
            };
        }

        private static AndroidJavaObject GetBannerSize(string sizeName)
        {
            var jcBannerSize = new AndroidJavaClass("io.bidmachine.banner.BannerSize");
            return jcBannerSize.GetStatic<AndroidJavaObject>(sizeName);
        }

        public static BMError GetError(AndroidJavaObject jObject)
        {
            return new BMError
            {
                Code = jObject.Call<int>("getCode"),
                Message = jObject.Call<string>("getMessage")
            };
        }

        public static AndroidJavaObject GetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.PriceFloorParams");

            if (priceFloorParams != null && priceFloorParams.PriceFloors != null)
            {
                foreach (KeyValuePair<string, double> priceFloor in priceFloorParams.PriceFloors)
                {
                    jObject.Call(
                        "addPriceFloor",
                        GetPrimitive(priceFloor.Key),
                        GetPrimitive(priceFloor.Value)
                    );
                }
            }

            return jObject;
        }

        public static AndroidJavaObject GetPublisher(Publisher publisher)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.Publisher$Builder");
            jObject.Call("setId", publisher.Id);
            jObject.Call("setName", publisher.Name);
            jObject.Call("setDomain", publisher.Domain);
            jObject.Call("addCategories", GetArrayList(publisher.Categories));

            return jObject.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetTargetingParams(TargetingParams targetingParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.TargetingParams");

            if (targetingParams != null)
            {
                jObject.Call("setUserId", targetingParams.UserId);

                var jcGender = new AndroidJavaClass("io.bidmachine.utils.Gender");
                var jGender = jcGender.GetStatic<AndroidJavaObject>(
                    targetingParams.UserId.ToString()
                );
                jObject.Call("setGender", jGender);

                jObject.Call("setBirthdayYear", GetPrimitive(targetingParams.BirthdayYear));
                jObject.Call("setKeywords", (object)targetingParams.Keywords);

                var location = targetingParams.DeviceLocation;
                if (location != null)
                {
                    var jLocation = new AndroidJavaObject(
                        "android.location.Location",
                        location.Provider
                    );
                    jLocation.Call("setLatitude", location.Latitude);
                    jLocation.Call("setLongitude", location.Longitude);
                    jObject.Call("setDeviceLocation", jLocation);
                }

                jObject.Call("setCountry", targetingParams.Country);

                jObject.Call("setCity", targetingParams.City);

                jObject.Call("setZip", targetingParams.Zip);

                jObject.Call("setStoreUrl", targetingParams.StoreUrl);

                jObject.Call("setStoreCategory", targetingParams.StoreCategory);

                jObject.Call("setStoreSubCategories", (object)targetingParams.StoreSubCategories);

                jObject.Call("setFramework", targetingParams.Framework);

                jObject.Call("setPaid", AndroidUtils.GetPrimitive(targetingParams.IsPaid));

                var externalUserIds = targetingParams.externalUserIds;
                if (externalUserIds != null)
                {
                    var jArrayList = new AndroidJavaObject("java.util.ArrayList");
                    foreach (var externalUserId in externalUserIds)
                    {
                        var jExternalUserId = new AndroidJavaObject(
                            "io.bidmachine.ExternalUserId",
                            externalUserId.SourceId,
                            externalUserId.Value
                        );
                        jArrayList.Call<bool>("add", jExternalUserId);
                    }
                    jObject.Call("setExternalUserIds", jArrayList);
                }

                var blockedApplications = targetingParams.BlockedApplications;
                if (blockedApplications != null)
                {
                    foreach (var app in blockedApplications)
                    {
                        jObject.Call("addBlockedApplication", app);
                    }
                }

                var blockedCategories = targetingParams.BlockedCategories;
                if (blockedCategories != null)
                {
                    foreach (var category in blockedCategories)
                    {
                        jObject.Call("addBlockedAdvertiserIABCategory", category);
                    }
                }

                var blockedDomains = targetingParams.BlockedDomains;
                if (blockedDomains != null)
                {
                    foreach (var domain in blockedDomains)
                    {
                        jObject.Call("addBlockedAdvertiserDomain", domain);
                    }
                }
            }

            return jObject;
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
            var jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
#endif
