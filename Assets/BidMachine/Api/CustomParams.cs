using System;
using System.Collections.Generic;

namespace BidMachineAds.Unity.Api
{
    [Serializable]
    public sealed class CustomParams
    {
        public Dictionary<string, string> Params { get; } = new Dictionary<string, string>();

        public CustomParams() { }

        public CustomParams(Dictionary<string, string> customParams)
        {
            Params = customParams;
        }

        public CustomParams AddParam(string key, string value)
        {
            Params[key] = value;
            return this;
        }
    }
}
