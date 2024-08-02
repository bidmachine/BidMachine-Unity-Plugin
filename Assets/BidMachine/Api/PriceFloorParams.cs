
using System;
using System.Collections.Generic;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class PriceFloorParams
    {
        public Dictionary<string, double> PriceFloors { get; } = new Dictionary<string, double>();

        public PriceFloorParams AddPriceFloor(double price)
        {
            AddPriceFloor(Guid.NewGuid().ToString(), price);
            return this;
        }

        public PriceFloorParams AddPriceFloor(string id, double price)
        {
            PriceFloors[id] = price;
            return this;
        }
    }
}