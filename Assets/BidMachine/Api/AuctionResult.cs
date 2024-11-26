using System;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class AuctionResult
    {
        public string DealID { get; set; }
        public string DemandSource { get; set; }
        public string CID { get; set; }

        public CustomParams CustomParams { get; set; }
        public CustomExtras CustomExtras { get; set; }

        public string CreativeID { get; set; }
        public string BidID { get; set; }
        public double Price { get; set; }
    }
}
