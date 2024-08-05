using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class BannerView : IBannerView
    {
        private readonly IBannerView client;

        public IBannerView Client => client;

        public BannerView()
        {
            client = BidMachineClientFactory.GetBannerView();
        }

        public BannerView(IBannerView client)
        {
            this.client = client;
        }

        public bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size)
        {
            return client.Show(yAxis, xAxis, view, size);
        }

        public void Hide()
        {
            client.Hide();
        }

        public bool CanShow()
        {
            return client.CanShow();
        }

        public void Destroy()
        {
            client.Destroy();
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            client.Load(request);
        }
    }
}
