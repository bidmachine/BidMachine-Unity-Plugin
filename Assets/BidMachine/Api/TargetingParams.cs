using System;
using System.Collections.Generic;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class TargetingParams
    {
        public string UserId { get; set; }

        public Gender gender { get; set; }

        public int BirthdayYear { get; set; }

        public string[] Keywords { get; set; }

        public Location DeviceLocation { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string StoreUrl { get; set; }

        public string StoreCategory { get; set; }

        public string[] StoreSubCategories { get; set; }

        public string Framework { get; set; }

        public bool IsPaid { get; set; }

        public ExternalUserId[] externalUserIds { get; set; }

        public HashSet<string> BlockedDomains { get; set; }

        public HashSet<string> BlockedCategories { get; set; }

        public HashSet<string> BlockedApplications { get; set; }

        [Serializable]
        public sealed class Location
        {
            public string Provider { get; set; }

            public double Latitude { get; set; }

            public double Longitude { get; set; }
        }

        [Serializable]
        public enum Gender
        {
            Female,
            Male,
            Omitted,
        }
    }
}
