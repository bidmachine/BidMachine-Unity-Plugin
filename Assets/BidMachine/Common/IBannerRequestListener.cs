using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IBannerRequestListener
    {
        void onBannerRequestSuccess(BannerRequest request, string auctionResult);
        void onBannerRequestFailed(BannerRequest request, BMError error);
        void onBannerRequestExpired(BannerRequest request);
    }
}