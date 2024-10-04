using System;
using System.Runtime.InteropServices;

namespace BidMachineAds.Unity.iOS
{
    public class iOSErrorBridge
    {
        [DllImport("__Internal")]
        public static extern int BidMachineGetErrorCode(IntPtr error);

        [DllImport("__Internal")]
        public static extern string BidMachineGetErrorMessage(IntPtr error);

        [DllImport("__Internal")]
        public static extern string BidMachineGetErrorBrief(IntPtr error);

        public static int GetErrorCode(IntPtr error)
        {
            return BidMachineGetErrorCode(error);
        }

        public static string GetErrorMessage(IntPtr error)
        {
            return BidMachineGetErrorMessage(error);
        }

        public static string GetErrorBrief(IntPtr error)
        {
            return BidMachineGetErrorBrief(error);
        }
    }
}