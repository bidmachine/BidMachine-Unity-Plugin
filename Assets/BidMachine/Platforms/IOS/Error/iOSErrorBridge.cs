#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace BidMachineAds.Unity.iOS
{
    public class iOSErrorBridge
    {
        [DllImport("__Internal")]
        private static extern int BidMachineGetErrorCode(IntPtr error);

        [DllImport("__Internal")]
        private static extern IntPtr BidMachineGetErrorMessageUnmanagedPointer(IntPtr error);

        [DllImport("__Internal")]
        private static extern IntPtr BidMachineGetErrorBriefUnmanagedPointer(IntPtr error);

        public static int GetErrorCode(IntPtr error)
        {
            return BidMachineGetErrorCode(error);
        }

        public static string GetErrorMessage(IntPtr error)
        {
            IntPtr messagePtr = BidMachineGetErrorMessageUnmanagedPointer(error);

            string errorString = Marshal.PtrToStringAuto(messagePtr);
            Marshal.FreeHGlobal(messagePtr);

            return errorString;
        }

        public static string GetErrorBrief(IntPtr error)
        {
            IntPtr briefPtr = BidMachineGetErrorBriefUnmanagedPointer(error);

            string briefString = Marshal.PtrToStringAuto(briefPtr);
            Marshal.FreeHGlobal(briefPtr);

            return briefString;
        }
    }
}
#endif