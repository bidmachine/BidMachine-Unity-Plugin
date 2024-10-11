using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Dummy
{
    public class DummyBidMachine : IBidMachine
    {
        public void Initialize(string sellerId)
        {
            Debug.LogWarning($"Call to Initialize(string) {Const.DUMMY_MESSAGE}");
        }

        public bool IsInitialized()
        {
            Debug.LogWarning($"Call to IsInitialized() {Const.DUMMY_MESSAGE}");
            return false;
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            Debug.LogWarning($"Call to SetConsentConfig(bool, string) {Const.DUMMY_MESSAGE}");
        }

        public void SetCoppa(bool coppa)
        {
            Debug.LogWarning($"Call to SetCoppa(bool) {Const.DUMMY_MESSAGE}");
        }

        public void SetEndpoint(string url)
        {
            Debug.LogWarning($"Call to SetEndpoint(string) {Const.DUMMY_MESSAGE}");
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            Debug.LogWarning($"Call to SetGPP(string, int[]) {Const.DUMMY_MESSAGE}");
        }

        public void SetLoggingEnabled(bool logging)
        {
            Debug.LogWarning($"Call to SetLoggingEnabled(bool) {Const.DUMMY_MESSAGE}");
        }

        public void SetPublisher(Publisher publisher)
        {
            Debug.LogWarning($"Call to SetPublisher(Publisher) {Const.DUMMY_MESSAGE}");
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            Debug.LogWarning($"Call to SetSubjectToGDPR(bool) {Const.DUMMY_MESSAGE}");
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams) {Const.DUMMY_MESSAGE}");
        }

        public void SetTestMode(bool test)
        {
            Debug.LogWarning($"Call to SetTestMode(bool) {Const.DUMMY_MESSAGE}");
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            Debug.LogWarning($"Call to SetUSPrivacyString(string) {Const.DUMMY_MESSAGE}");
        }
    }

    internal class DummyBannerAd : IBannerView
    {
        public bool CanShow()
        {
            Debug.LogWarning($"Call to canShow() {Const.DUMMY_MESSAGE}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to destroy() {Const.DUMMY_MESSAGE}");
        }

        public void Hide()
        {
            Debug.LogWarning($"Call to Hide() {Const.DUMMY_MESSAGE}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to load(IBannerRequest) {Const.DUMMY_MESSAGE}");
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            Debug.LogWarning($"Call to setListener(IBannerListener) {Const.DUMMY_MESSAGE}");
        }

        public bool Show(int YAxis, int XAxis, IBannerView ad, BannerSize size)
        {
            Debug.LogWarning(
                $"Call to Show(int, int, IBannerAd, BannerSize) {Const.DUMMY_MESSAGE}"
            );
            return false;
        }
    }

    internal class DummyBannerRequestBuilder : IBannerRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to build() {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to setAdContentType(AdContentType) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to setBidPayload(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            Debug.LogWarning(
                $"Call to setListener(IAdRequestListener<IBannerRequest, string, BMError>) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to setLoadingTimeOut(int) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to setNetworks(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to setPlacementId(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning(
                $"Call to setPriceFloorParams(PriceFloorParams) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IBannerRequestBuilder setSize(BannerSize size)
        {
            Debug.LogWarning($"Call to SetSize(BannerSize) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams) {Const.DUMMY_MESSAGE}");
            return null;
        }
    }

    internal class DummyInterstitialAd : IInterstitialAd
    {
        public bool CanShow()
        {
            Debug.LogWarning($"Call to canShow() {Const.DUMMY_MESSAGE}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to destroy() {Const.DUMMY_MESSAGE}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to load(IInterstitialRequest) {Const.DUMMY_MESSAGE}");
        }

        public void SetListener(IInterstitialListener listener)
        {
            Debug.LogWarning($"Call to setListener(IInterstitialListener) {Const.DUMMY_MESSAGE}");
        }

        public void Show()
        {
            Debug.LogWarning($"Call to show() {Const.DUMMY_MESSAGE}");
        }
    }

    internal class DummyInterstitialRequestBuilder : IAdRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to build() {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to setAdContentType(AdContentType) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to setBidPayload(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            Debug.LogWarning(
                $"Call to setListener(IAdRequestListener<IInterstitialRequest, string, BMError>) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to setLoadingTimeOut(int) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to setNetworks(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to setPlacementId(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning(
                $"Call to setPriceFloorParams(PriceFloorParams) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams) {Const.DUMMY_MESSAGE}");
            return null;
        }
    }

    internal class DummyRewardedAd : IRewardedAd
    {
        public bool CanShow()
        {
            Debug.LogWarning($"Call to canShow() {Const.DUMMY_MESSAGE}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to destroy() {Const.DUMMY_MESSAGE}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to load(IRewardedRequest) {Const.DUMMY_MESSAGE}");
        }

        public void SetListener(IRewardedAdListener listener)
        {
            Debug.LogWarning($"Call to setListener(IRewardedlListener) {Const.DUMMY_MESSAGE}");
        }

        public void Show()
        {
            Debug.LogWarning($"Call to show() {Const.DUMMY_MESSAGE}");
        }
    }

    internal class DummyRewardedRequestBuilder : IAdRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to build() {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to setAdContentType(AdContentType) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to setBidPayload(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            Debug.LogWarning(
                $"Call to setListener(IAdRequestListener<IRewardedRequest, string, BMError>) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to setLoadingTimeOut(int) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to setNetworks(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to setPlacementId(string) {Const.DUMMY_MESSAGE}");
            return null;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning(
                $"Call to setPriceFloorParams(PriceFloorParams) {Const.DUMMY_MESSAGE}"
            );
            return null;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams) {Const.DUMMY_MESSAGE}");
            return null;
        }
    }

    internal class DummyUserPermissions : IUserPermissions
    {
        public bool Check(string permission)
        {
            Debug.LogWarning($"Call to CheckAndroidPermissions(string) {Const.DUMMY_MESSAGE}");
            return false;
        }

        public void Request()
        {
            Debug.LogWarning($"Call to RequestAndroidPermissions() {Const.DUMMY_MESSAGE}");
        }
    }

    class Const
    {
        internal const string DUMMY_MESSAGE =
            "on not supported platform. To test advertising, install your application on the Android/iOS device.";
    }
}
