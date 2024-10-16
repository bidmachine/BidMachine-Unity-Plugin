using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface ICommonFullscreenAdListener<TAd, TAdError> : IAdListener<TAd>
    {
        void onAdClosed(TAd ad, bool finished) { }
    }

    public interface IFullscreenAdListener<TAd> : ICommonFullscreenAdListener<TAd, BMError> { }

    public interface IFullscreenRequest : IAdRequest
    {
        AdContentType GetAdContentType();
    }

    public interface IFullscreenAdRequestBuilder : IAdRequestBuilder
    {
        IAdRequestBuilder SetAdContentType(AdContentType contentType);
    }
}
