#if UNITY_IOS
using System;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS
{
    using CustomValuesList = KeyValueList<string, string>;

    [Serializable]
    internal class AuctionResultWrapper
    {
        public string DealID;
        public string DemandSource;
        public string CID;

        public CustomValuesList CustomParams;
        public CustomValuesList CustomExtras;

        public string CreativeID;
        public string BidID;
        public double Price;

        public AuctionResult ToAuctionResult()
        {
            var auctionResult = new AuctionResult
            {
                DealID = this.DealID,
                DemandSource = this.DemandSource,
                CID = this.CID,
                CustomExtras = new CustomExtras(),
                CreativeID = this.CreativeID,
                BidID = this.BidID,
                Price = this.Price,
                CustomParams = new CustomParams()
            };

            foreach (var kvp in CustomParams.items)
            {
                auctionResult.CustomParams.AddParam(kvp.Key, kvp.Value);
            }

            foreach (var kvp in CustomExtras.items)
            {
                auctionResult.CustomExtras.AddExtra(kvp.Key, kvp.Value);
            }

            return auctionResult;
        }
    }
}
#endif
