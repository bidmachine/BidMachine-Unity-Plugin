using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IInterstitialRequestListener
    {
        void onInterstitialRequestSuccess(InterstitialRequest request, string auctionResult);
        void onInterstitialRequestFailed(InterstitialRequest request, BMError error);
        void onInterstitialRequestExpired(InterstitialRequest request);
    }
}