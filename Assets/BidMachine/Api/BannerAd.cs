using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class BannerAd : IBannerAd
    {
        private readonly IBannerAd client;

        public BannerAd()
        {
            client = BidMachineClientFactory.GetBannerAd();
        }

        public BannerAd(IBannerAd client)
        {
            this.client = client;
        }

        public bool Show(int YAxis, int XAxis, IBannerAd ad, BannerSize size)
        {
            return client.Show(YAxis, XAxis, ad, size);
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

        public void SetListener(IBannerListener listener)
        {
            client.SetListener(listener);
        }

        public void Load(IBannerRequest request)
        {
            client.Load(request);
        }
    }
}
