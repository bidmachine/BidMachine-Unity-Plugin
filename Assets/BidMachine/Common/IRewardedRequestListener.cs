using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IRewardedRequestListener
    {
        void onRewardedRequestSuccess(RewardedRequest request, string auctionResult);
        void onRewardedRequestFailed(RewardedRequest request, BMError error);
        void onRewardedRequestExpired(RewardedRequest request);
    }
}