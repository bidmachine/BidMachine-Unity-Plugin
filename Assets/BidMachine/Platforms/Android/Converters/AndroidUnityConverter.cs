#if PLATFORM_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidUnityConverter
    {
        public static Dictionary<string, string> GetDictionary(AndroidJavaObject jObject)
        {
            var jcMap = new AndroidJavaObject("java.util.HashMap");
            var jcSet = new AndroidJavaObject("java.util.HashSet");

            IntPtr jptrKeySet = AndroidJNIHelper.GetMethodID(
                jcMap.GetRawClass(),
                "keySet",
                "()Ljava/util/Set;"
            );
            IntPtr jptrSet = AndroidJNI.CallObjectMethod(
                jObject.GetRawObject(),
                jptrKeySet,
                new jvalue[] { }
            );
            IntPtr jptrToArray = AndroidJNIHelper.GetMethodID(
                jcSet.GetRawClass(),
                "toArray",
                "()[Ljava/lang/Object;"
            );
            IntPtr jptrArray = AndroidJNI.CallObjectMethod(jptrSet, jptrToArray, new jvalue[] { });

            var dict = new Dictionary<string, string>();
            var keys = AndroidJNIHelper.ConvertFromJNIArray<string[]>(jptrArray);

            IntPtr jptrGet = AndroidJNIHelper.GetMethodID(
                jcMap.GetRawClass(),
                "get",
                "(Ljava/lang/Object;)Ljava/lang/Object;"
            );

            foreach (var key in keys)
            {
                var value = AndroidJNI.CallStringMethod(
                    jObject.GetRawObject(),
                    jptrGet,
                    new jvalue[] { new() { l = AndroidJNI.NewStringUTF(key) } }
                );
                dict.Add($"\"{key}\"", $"\"{value}\"");
            }

            return dict;
        }

        public static BannerSize GetBannerSize(AndroidJavaObject jBannerSize)
        {
            var size = jBannerSize.Call<string>("toString");
            return size switch
            {
                "Size_320x50" => BannerSize.Size_320x50,
                "Size_300x250" => BannerSize.Size_300x250,
                "Size_728x90" => BannerSize.Size_728x90,
                _ =>
                    BannerSize.Size_320x50 // Default case
                ,
            };
        }

        public static BMError GetError(AndroidJavaObject jObject)
        {
            return new BMError
            {
                Code = jObject.Call<int>("getCode"),
                Message = jObject.Call<string>("getMessage"),
            };
        }

        [Obsolete("Use GetAuctionResultObject(AndroidJavaObject) instead")]
        public static string GetAuctionResult(AndroidJavaObject jObject)
        {
            var jCustomParams = jObject.Call<AndroidJavaObject>("getCustomParams");
            var jAdDomains = jObject.Call<AndroidJavaObject>("getAdDomains");

            string deal = string.IsNullOrEmpty(jObject.Call<string>("getDeal"))
                ? "null"
                : jObject.Call<string>("getDeal").ToUpper();
            string demandSource = string.IsNullOrEmpty(jObject.Call<string>("getDemandSource"))
                ? "null"
                : jObject.Call<string>("getDemandSource");
            string cid = string.IsNullOrEmpty(jObject.Call<string>("getCid"))
                ? "null"
                : jObject.Call<string>("getCid");
            string customParams = string.Join(
                ",",
                GetDictionary(jCustomParams)
                    .Select(pair =>
                        string.Format("{0}:{1}", pair.Key.ToString(), pair.Value.ToString())
                    )
                    .ToArray()
            );
            string adDomains = string.Join(
                ",",
                AndroidJNIHelper
                    .ConvertFromJNIArray<string[]>(jAdDomains.GetRawObject())
                    .ToList()
                    .Select(adDomain => $"\"{adDomain}\"")
                    .ToList()
            );
            string creativeId = string.IsNullOrEmpty(jObject.Call<string>("getCreativeId"))
                ? "null"
                : jObject.Call<string>("getCreativeId");
            string id = string.IsNullOrEmpty(jObject.Call<string>("getId"))
                ? "null"
                : jObject.Call<string>("getId");
            string price = string.IsNullOrEmpty(jObject.Call<double>("getPrice").ToString())
                ? "null"
                : jObject.Call<double>("getPrice").ToString();

            return $"{{\"dealID\":\"{deal}\",\"demandSource\":\"{demandSource}\",\"cID\":\"{cid}\",\"customParams\":{{{customParams}}},\"adDomains\":[{adDomains}],\"creativeID\":\"{creativeId}\",\"bidID\":\"{id}\",\"price\":{price}}}";
        }

        public static AuctionResult GetAuctionResultObject(AndroidJavaObject jObject)
        {
            return new AuctionResult
            {
                BidID = jObject.Call<string>("getId"),
                DemandSource = jObject.Call<string>("getDemandSource"),
                Price = jObject.Call<double>("getPrice"),
                DealID = jObject.Call<string>("getDeal"),
                CreativeID = jObject.Call<string>("getCreativeId"),
                CID = jObject.Call<string>("getCid"),
                CustomParams = new CustomParams(GetDictionary(jObject.Call<AndroidJavaObject>("getCustomParams"))),
                CustomExtras = new CustomExtras(GetDictionary(jObject.Call<AndroidJavaObject>("getNetworkParams"))),
            };
        }
    }
}
#endif
