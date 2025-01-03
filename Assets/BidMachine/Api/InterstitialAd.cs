﻿using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class InterstitialAd : IInterstitialAd
    {
        private readonly IInterstitialAd client;

        public InterstitialAd()
        {
            client = BidMachineClientFactory.GetInterstitialAd();
        }

        public InterstitialAd(IInterstitialAd client)
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

        public void SetListener(IInterstitialAdListener listener)
        {
            client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            client.Load(request);
        }
    }
}
