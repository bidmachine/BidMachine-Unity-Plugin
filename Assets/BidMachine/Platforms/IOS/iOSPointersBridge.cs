#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace BidMachineAds.Unity.iOS
{
    public class iOSPointersBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineReleasePointer(IntPtr ptr);

        public static void ReleasePointer(IntPtr ptr)
        {
            BidMachineReleasePointer(ptr);
        }
    }
}
#endif
