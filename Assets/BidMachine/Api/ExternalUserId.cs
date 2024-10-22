using System;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class ExternalUserId
    {
        public string SourceId { get; set; }

        public string Value { get; set; }
    }
}
