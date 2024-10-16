#if UNITY_IOS
using System;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;
using System.Collections.Generic;

namespace BidMachineAds.Unity.iOS
{
    [Serializable]
    public sealed class iOSLocation {
        public string Provider;
        public double Latitude;
        public double Longitude;
    }

    [Serializable]
    public sealed  class iOSExternalUserId {
        public string SourceId;
        public string Value;
    }

    [Serializable]
    public sealed class iOSTargetingParameters {
        public string UserId;
        public TargetingParams.Gender Gender;
        public int BirthdayYear;
        public string[] Keywords;
        public iOSLocation DeviceLocation;
        public string Country;
        public string City;
        public string Zip;
        public string StoreUrl;
        public string StoreCategory;
        public string[] StoreSubCategories;
        public string Framework;
        public bool IsPaid;

        public iOSExternalUserId[] externalUserIds;
        public string[] BlockedDomains;
        public string[] BlockedCategories;
        public string[] BlockedApplications;
    }

    public sealed class iOSTargetingAdapter 
    {
        public static iOSTargetingParameters Adapt(TargetingParams source)
        {
            iOSLocation deviceLocation = new iOSLocation
            {
                Provider = source.DeviceLocation.Provider,
                Latitude = source.DeviceLocation.Latitude,
                Longitude = source.DeviceLocation.Longitude,
            };

            iOSExternalUserId[] iOSExternalUsers = new iOSExternalUserId[source.externalUserIds.Length];
            for (int i = 0; i < source.externalUserIds.Length; i++)
            {
                iOSExternalUsers[i] = new iOSExternalUserId 
                {
                    SourceId = source.externalUserIds[i].SourceId,
                    Value = source.externalUserIds[i].Value,
                };
            }

            string[] blockedDomains = source.BlockedDomains.AsArray();
            string[] blockedCategories = source.BlockedCategories.AsArray();
            string[] blockedApplications = source.BlockedApplications.AsArray();

            iOSTargetingParameters target = new iOSTargetingParameters
            {
                UserId = source.UserId,
                Gender = source.gender,
                BirthdayYear = source.BirthdayYear,
                Keywords = source.Keywords,
                DeviceLocation = deviceLocation,
                Country = source.Country,
                City = source.City,
                Zip = source.Zip,
                StoreUrl = source.StoreUrl,
                StoreCategory = source.StoreCategory,
                StoreSubCategories = source.StoreSubCategories,
                Framework = source.Framework,
                externalUserIds = iOSExternalUsers,
                BlockedDomains = blockedDomains,
                BlockedCategories = blockedCategories,
                BlockedApplications = blockedApplications,
            };
            return target;
        }
    }
}

internal static class HashSetExtensions
{
    internal static T[] AsArray<T>(this HashSet<T> hashSet)
    {
        T[] array = new T[hashSet.Count];
        hashSet.CopyTo(array);
        return array;
    }
}
#endif
