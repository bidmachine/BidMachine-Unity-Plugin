using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class InterstitialAd : IFullscreenAd
    {
        private readonly IFullscreenAd client;

        public InterstitialAd()
        {
            client = BidMachineClientFactory.GetInterstitialAd();
        }

        public InterstitialAd(IFullscreenAd client)
        {
            this.client = client;
        }

        public void Show()
        {
            client.Show();
        }

        public bool CanShow()
        {
            return client.CanShow();
        }

        public void Destroy()
        {
            client.Destroy();
        }

        public void SetListener(IFullscreenAdListener<IFullscreenAd> listener)
        {
            client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            client.Load(request);
        }
    }
}
