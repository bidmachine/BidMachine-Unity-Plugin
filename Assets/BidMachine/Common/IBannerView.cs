using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IBannerView : IAd<IAdListener<IBannerView>>
    {
        bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size);

        void Hide();
    }

    public interface IBannerRequest : IAdRequest
    {
        BannerSize GetSize();
    }

    public interface IBannerRequestBuilder : IAdRequestBuilder
    {
        IAdRequestBuilder SetSize(BannerSize size);
    }
}
