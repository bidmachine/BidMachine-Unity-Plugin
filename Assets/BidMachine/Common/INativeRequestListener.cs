using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface INativeRequestListener
    {
        void onNativeRequestSuccess(NativeRequest request, string auctionResult);
        void onNativeRequestFailed(NativeRequest request, BMError error);
        void onNativeRequestExpired(NativeRequest request);
    }
}