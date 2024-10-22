using System;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class Publisher
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public string[] Categories { get; set; }
    }
}
