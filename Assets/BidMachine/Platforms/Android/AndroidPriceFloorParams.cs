using System.Collections.Generic;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidPriceFloorParams
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            javaObject = new AndroidJavaObject("io.bidmachine.PriceFloorParams");

            if (priceFloorParams != null && priceFloorParams.PriceFloors != null)
            {
                foreach (KeyValuePair<string, double> priceFloor in priceFloorParams.PriceFloors)
                {
                    AddPriceFloor(priceFloor.Key, priceFloor.Value);
                }
            }
        }

        public AndroidPriceFloorParams AddPriceFloor(double price)
        {
            javaObject.Call<AndroidJavaObject>(
                "addPriceFloor",
                AndroidUtils.GetJavaPrimitiveObject(price)
            );
            return this;
        }

        public AndroidPriceFloorParams AddPriceFloor(string id, double price)
        {
            javaObject.Call<AndroidJavaObject>(
                "addPriceFloor",
                AndroidUtils.GetJavaPrimitiveObject(id),
                AndroidUtils.GetJavaPrimitiveObject(price)
            );
            return this;
        }
    }
}
