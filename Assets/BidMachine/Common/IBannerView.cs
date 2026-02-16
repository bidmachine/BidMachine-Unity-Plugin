using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IBannerView : IAd<IAdListener<IBannerView>>
    {
        [System.Obsolete("Show() with BannerSize is deprecated. Use BannerAdSize instead.")]
        bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size);

        bool Show(int yAxis, int xAxis, IBannerView view, BannerAdSize size);

        void Hide();

        BannerAdSize GetAdSize();
    }

    public interface IBannerRequest : IAdRequest
    {
        [System.Obsolete("GetSize() is deprecated. Use GetBannerAdSize() instead.")]
        BannerSize GetSize();

        BannerAdSize GetBannerAdSize();
    }

    public interface IBannerRequestBuilder : IAdRequestBuilder
    {
        [System.Obsolete("SetSize(BannerSize) is deprecated in SDK 3.5.0+. Use SetSize(BannerAdSize) instead.")]
        IAdRequestBuilder SetSize(BannerSize size);

        IAdRequestBuilder SetSize(BannerAdSize size);
    }
}
