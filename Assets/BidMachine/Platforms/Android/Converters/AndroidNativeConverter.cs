#if PLATFORM_ANDROID
using System.Collections.Generic;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidNativeConverter
    {
        public static AndroidJavaObject GetActivity()
        {
            var jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public static object GetObject(object obj)
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
                jArrayList.Call<bool>("add", GetObject(obj));
            }
            return jArrayList;
        }

#pragma warning disable CS0618
        public static AndroidJavaObject GetBannerSize(BannerSize bannerSize)
        {
            return bannerSize switch
            {
                BannerSize.Size_300x250 => GetBannerSizeLegacy("Size_300x250"),
                BannerSize.Size_728x90 => GetBannerSizeLegacy("Size_728x90"),
                _ => GetBannerSizeLegacy("Size_320x50"),
            };
        }
#pragma warning restore CS0618

        private static AndroidJavaObject GetBannerSizeLegacy(string sizeName)
        {
            var jcBannerSize = new AndroidJavaClass("io.bidmachine.banner.BannerSize");
            return jcBannerSize.GetStatic<AndroidJavaObject>(sizeName);
        }

        public static AndroidJavaObject GetBannerAdSize(BannerAdSize bannerAdSize)
        {
            // Try io.bidmachine.BannerAdSize (root package) first
            AndroidJavaClass jcBannerAdSize;
            try
            {
                jcBannerAdSize = new AndroidJavaClass("io.bidmachine.BannerAdSize");
            }
            catch
            {
                // Fallback: If BannerAdSize doesn't exist, use legacy BannerSize
                // This handles backward compatibility with SDK versions that don't have BannerAdSize yet
                return GetBannerSize(bannerAdSize.ToLegacyBannerSize());
            }

            if (bannerAdSize.IsAdaptive)
            {
                return jcBannerAdSize.CallStatic<AndroidJavaObject>(
                    "adaptive",
                    GetObject(bannerAdSize.Width),
                    GetObject(bannerAdSize.Height)
                );
            }

            // Get static instances for standard sizes
            if (bannerAdSize.Width == 320 && bannerAdSize.Height == 50)
            {
                return jcBannerAdSize.GetStatic<AndroidJavaObject>("Banner");
            }
            else if (bannerAdSize.Width == 728 && bannerAdSize.Height == 90)
            {
                return jcBannerAdSize.GetStatic<AndroidJavaObject>("Leaderboard");
            }
            else if (bannerAdSize.Width == 300 && bannerAdSize.Height == 250)
            {
                return jcBannerAdSize.GetStatic<AndroidJavaObject>("MediumRectangle");
            }

            // Default to Banner
            return jcBannerAdSize.GetStatic<AndroidJavaObject>("Banner");
        }

        public static AndroidJavaObject GetAdPlacementConfig(AdPlacementConfig config)
        {
            AndroidJavaObject builder;

            switch (config.PlacementType)
            {
                case AdPlacementType.Banner:
                    var jcAdPlacementConfig = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                    builder = jcAdPlacementConfig.CallStatic<AndroidJavaObject>(
                        "bannerBuilder",
                        GetBannerAdSize(config.BannerAdSize)
                    );
                    break;

                case AdPlacementType.Interstitial:
                    var jcAdPlacementConfigInterstitial = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                    var jcAdContentType = new AndroidJavaClass("io.bidmachine.AdContentType");
                    var jAdContentType = jcAdContentType.GetStatic<AndroidJavaObject>(config.AdContentType.ToString());

                    builder = jcAdPlacementConfigInterstitial.CallStatic<AndroidJavaObject>(
                        "interstitialBuilder",
                        jAdContentType
                    );
                    break;

                case AdPlacementType.Rewarded:
                    var jcAdPlacementConfigRewarded = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                    var jcAdContentTypeRewarded = new AndroidJavaClass("io.bidmachine.AdContentType");
                    var jAdContentTypeRewarded = jcAdContentTypeRewarded.GetStatic<AndroidJavaObject>(config.AdContentType.ToString());

                    builder = jcAdPlacementConfigRewarded.CallStatic<AndroidJavaObject>(
                        "rewardedBuilder",
                        jAdContentTypeRewarded
                    );
                    break;

                default:
                    throw new System.ArgumentException($"Unknown placement type: {config.PlacementType}");
            }

            // Apply optional parameters
            if (!string.IsNullOrEmpty(config.PlacementId))
            {
                builder = builder.Call<AndroidJavaObject>("withPlacementId", GetObject(config.PlacementId));
            }

            if (config.CustomParams != null)
            {
                builder = builder.Call<AndroidJavaObject>("withCustomParams", GetCustomParams(config.CustomParams));
            }

            return builder.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.PriceFloorParams");

            if (priceFloorParams != null && priceFloorParams.PriceFloors != null)
            {
                foreach (KeyValuePair<string, double> priceFloor in priceFloorParams.PriceFloors)
                {
                    jObject.Call<AndroidJavaObject>(
                        "addPriceFloor",
                        GetObject(priceFloor.Key),
                        priceFloor.Value
                    );
                }
            }

            return jObject;
        }

        public static AndroidJavaObject GetCustomParams(CustomParams customParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.CustomParams");

            if (customParams != null && customParams.Params != null)
            {
                foreach (KeyValuePair<string, string> customParam in customParams.Params)
                {
                    jObject.Call<AndroidJavaObject>(
                        "addParam",
                        GetObject(customParam.Key),
                        customParam.Value
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
                    targetingParams.gender.ToString()
                );
                jObject.Call("setGender", jGender);

                jObject.Call("setBirthdayYear", GetObject(targetingParams.BirthdayYear));
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

                jObject.Call("setPaid", GetObject(targetingParams.IsPaid));

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
    }
}
#endif
