using System;
using System.Collections.Generic;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidTargetingParams
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidTargetingParams(TargetingParams targetingParams)
        {
            javaObject = new AndroidJavaObject("io.bidmachine.TargetingParams");

            SetUserId(targetingParams.UserId);
            SetGender(targetingParams.gender);
            SetBirthdayYear(targetingParams.BirthdayYear);
            SetKeywords(targetingParams.Keywords);
            SetDeviceLocation(targetingParams.DeviceLocation);
            SetCountry(targetingParams.Country);
            SetCity(targetingParams.City);
            SetZip(targetingParams.Zip);
            SetStoreUrl(targetingParams.StoreUrl);
            SetStoreCategory(targetingParams.StoreCategory);
            SetStoreSubCategories(targetingParams.StoreSubCategories);
            SetFramework(targetingParams.Framework);
            SetPaid(targetingParams.IsPaid);
            SetExternalUserIds(targetingParams.externalUserIds);

            if (targetingParams.BlockedApplications != null)
            {
                foreach (var app in targetingParams.BlockedApplications)
                {
                    AddBlockedApplication(app);
                }
            }

            if (targetingParams.BlockedCategories != null)
            {
                foreach (var category in targetingParams.BlockedCategories)
                {
                    AddBlockedAdvertiserIABCategory(category);
                }
            }

            if (targetingParams.BlockedDomains != null)
            {
                foreach (var domain in targetingParams.BlockedDomains)
                {
                    AddBlockedAdvertiserDomain(domain);
                }
            }
        }

        public AndroidJavaObject GetJavaObject()
        {
            return javaObject;
        }

        public AndroidTargetingParams SetUserId(string userId)
        {
            javaObject.Call<AndroidJavaObject>("setUserId", userId);
            return this;
        }

        public AndroidTargetingParams SetGender(TargetingParams.Gender gender)
        {
            AndroidJavaClass genderClass = new AndroidJavaClass("io.bidmachine.utils.Gender");
            AndroidJavaObject javaGender = genderClass.GetStatic<AndroidJavaObject>(
                gender.ToString()
            );
            javaObject.Call<AndroidJavaObject>("setGender", javaGender);
            return this;
        }

        public AndroidTargetingParams SetBirthdayYear(int birthdayYear)
        {
            javaObject.Call<AndroidJavaObject>(
                "setBirthdayYear",
                AndroidUtils.GetJavaPrimitiveObject(birthdayYear)
            );
            return this;
        }

        public AndroidTargetingParams SetKeywords(string[] keywords)
        {
            javaObject.Call<AndroidJavaObject>("setKeywords", (object)keywords);
            return this;
        }

        public AndroidTargetingParams SetDeviceLocation(TargetingParams.Location location)
        {
            if (location != null)
            {
                AndroidJavaObject javaLocation = new AndroidJavaObject(
                    "android.location.Location",
                    location.Provider
                );
                javaLocation.Call("setLatitude", location.Latitude);
                javaLocation.Call("setLongitude", location.Longitude);
                javaObject.Call<AndroidJavaObject>("setDeviceLocation", javaLocation);
            }
            return this;
        }

        public AndroidTargetingParams SetCountry(string country)
        {
            javaObject.Call<AndroidJavaObject>("setCountry", country);
            return this;
        }

        public AndroidTargetingParams SetCity(string city)
        {
            javaObject.Call<AndroidJavaObject>("setCity", city);
            return this;
        }

        public AndroidTargetingParams SetZip(string zip)
        {
            javaObject.Call<AndroidJavaObject>("setZip", zip);
            return this;
        }

        public AndroidTargetingParams SetStoreUrl(string url)
        {
            javaObject.Call<AndroidJavaObject>("setStoreUrl", url);
            return this;
        }

        public AndroidTargetingParams SetStoreCategory(string storeCategory)
        {
            javaObject.Call<AndroidJavaObject>("setStoreCategory", storeCategory);
            return this;
        }

        public AndroidTargetingParams SetStoreSubCategories(string[] storeSubCategories)
        {
            javaObject.Call<AndroidJavaObject>("setStoreSubCategories", (object)storeSubCategories);
            return this;
        }

        public AndroidTargetingParams SetFramework(string framework)
        {
            javaObject.Call<AndroidJavaObject>("setFramework", framework);
            return this;
        }

        public AndroidTargetingParams SetPaid(bool paid)
        {
            javaObject.Call<AndroidJavaObject>(
                "setPaid",
                AndroidUtils.GetJavaPrimitiveObject(paid)
            );
            return this;
        }

        public AndroidTargetingParams SetExternalUserIds(ExternalUserId[] externalUserIds)
        {
            if (externalUserIds != null)
            {
                AndroidJavaObject javaList = new AndroidJavaObject("java.util.ArrayList");
                foreach (var externalUserId in externalUserIds)
                {
                    AndroidJavaObject javaExternalUserId = new AndroidJavaObject(
                        "io.bidmachine.ExternalUserId",
                        externalUserId.SourceId,
                        externalUserId.Value
                    );
                    javaList.Call<bool>("add", javaExternalUserId);
                }
                javaObject.Call<AndroidJavaObject>("setExternalUserIds", javaList);
            }
            return this;
        }

        public AndroidTargetingParams AddBlockedApplication(string bundleOrPackage)
        {
            javaObject.Call<AndroidJavaObject>("addBlockedApplication", bundleOrPackage);
            return this;
        }

        public AndroidTargetingParams AddBlockedAdvertiserIABCategory(string category)
        {
            javaObject.Call<AndroidJavaObject>("addBlockedAdvertiserIABCategory", category);
            return this;
        }

        public AndroidTargetingParams AddBlockedAdvertiserDomain(string domain)
        {
            javaObject.Call<AndroidJavaObject>("addBlockedAdvertiserDomain", domain);
            return this;
        }
    }
}
