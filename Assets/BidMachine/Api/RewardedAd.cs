using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class RewardedAd : IRewardedAd
    {
        private readonly IRewardedAd client;

        public RewardedAd()
        {
            this.client = BidMachineClientFactory.GetRewardedAd();
        }

        public RewardedAd(IRewardedAd client)
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

        public void SetListener(IRewardedlListener listener)
        {
            client.SetListener(listener);
        }

        public void Load(IRewardedRequest request)
        {
            client.Load(request);
        }
    }
}
