using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface ICommonFullscreenAdListener<TAd, TAdError> : IAdListener<TAd>
    {
        void onAdClosed(TAd ad, bool finished) { }
    }

    public interface IFullscreenAdListener<TAd> : ICommonFullscreenAdListener<TAd, BMError> { }
}
