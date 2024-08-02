using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Dummy
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DummyBidMachine
        : IBidMachine,
            IBannerAd,
            IBannerRequestBuilder,
            IInterstitialAd,
            IInterstitialRequestBuilder,
            IRewardedAd,
            IRewardedRequestBuilder
    {
        public IRewardedRequest Build()
        {
            throw new System.NotImplementedException();
        }

        public bool CanShow()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckAndroidPermissions(string permission)
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(string sellerId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsInitialized()
        {
            throw new System.NotImplementedException();
        }

        public void Load(IRewardedRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void Load(IInterstitialRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void Load(IBannerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void RequestAndroidPermissions()
        {
            throw new System.NotImplementedException();
        }

        public void SetAdContentType(AdContentType contentType)
        {
            throw new System.NotImplementedException();
        }

        public void SetBidPayload(string bidPayload)
        {
            throw new System.NotImplementedException();
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoppa(bool coppa)
        {
            throw new System.NotImplementedException();
        }

        public void SetEndpoint(string url)
        {
            throw new System.NotImplementedException();
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IAdRequestListener<IRewardedRequest, string, BMError> listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IRewardedlListener listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IAdRequestListener<IInterstitialRequest, string, BMError> listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IInterstitialListener listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IAdRequestListener<IBannerRequest, string, BMError> listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetListener(IBannerListener listener)
        {
            throw new System.NotImplementedException();
        }

        public void SetLoadingTimeOut(int loadingTimeout)
        {
            throw new System.NotImplementedException();
        }

        public void SetLoggingEnabled(bool logging)
        {
            throw new System.NotImplementedException();
        }

        public void SetNetworks(string networks)
        {
            throw new System.NotImplementedException();
        }

        public void SetPlacementId(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            throw new System.NotImplementedException();
        }

        public void SetPublisher(Publisher publisher)
        {
            throw new System.NotImplementedException();
        }

        public void SetSessionAdParams(SessionAdParams sessionAdParams)
        {
            throw new System.NotImplementedException();
        }

        public void SetSize(BannerSize size)
        {
            throw new System.NotImplementedException();
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            throw new System.NotImplementedException();
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            throw new System.NotImplementedException();
        }

        public void SetTestMode(bool test)
        {
            throw new System.NotImplementedException();
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public bool Show(int YAxis, int XAxis, IBannerAd ad, BannerSize size)
        {
            throw new System.NotImplementedException();
        }

        IInterstitialRequest IAdRequestBuilder<IInterstitialRequest>.Build()
        {
            throw new System.NotImplementedException();
        }

        IBannerRequest IAdRequestBuilder<IBannerRequest>.Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
