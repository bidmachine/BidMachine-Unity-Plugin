using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class RewardedAd : IFullscreenAd
    {
        private readonly IFullscreenAd client;

        public RewardedAd()
        {
            this.client = BidMachineClientFactory.GetRewardedAd();
        }

        public RewardedAd(IFullscreenAd client)
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
