using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IBannerAd : IAd<IBannerAd, IBannerListener, IBannerRequest>
    {
        bool Show(int YAxis, int XAxis, IBannerAd ad, BannerSize size);

        void Hide();
    }

    public interface IBannerListener : IAdListener<IBannerAd, BMError> { }

    public interface IBannerRequest : IAdRequest<IBannerRequest>
    {
        BannerSize GetSize();
    }

    public interface IBannerRequestBuilder : IAdRequestBuilder<IBannerRequest>
    {
        void SetSize(BannerSize size);
    }
}
