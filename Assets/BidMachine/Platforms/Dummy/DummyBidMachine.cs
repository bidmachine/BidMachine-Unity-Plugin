using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Android;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Dummy
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DummyBidMachine : IBidMachine, ITargetingParams, IPriceFloorParams, IInterstitialRequestBuilder,
        IInterstitialAd, IInterstitialRequest,
        IRewardedRequestBuilder, IRewardedRequest, IRewardedAd, IBannerRequestBuilder,
        IBannerRequest, IBannerView, IBanner, ISessionAdParams, INativeAd, INativeRequestBuilder
    {
        
        
        private const string DUMMY_MESSAGE =
            "on not supported platform. To test advertising, install your application on the Android/iOS device.";

        public void setSize(BannerSize size)
        {
            Debug.LogWarning($"Call to setSize(BannerSize size) {DUMMY_MESSAGE}");
        }

        public void setListener(IBannerRequestListener bannerRequestListener)
        {
            Debug.LogWarning($"Call to setListener(IBannerRequestListener bannerRequestListener) {DUMMY_MESSAGE}");
        }

        public void setListener(IInterstitialRequestListener bannerRequestListener)
        {
            Debug.LogWarning(
                $"Call to setListener(IInterstitialRequestListener bannerRequestListener) {DUMMY_MESSAGE}");
        }

        public void setListener(IRewardedRequestListener rewardedRequestListener)
        {
            Debug.LogWarning($"Call to setListener(IRewardedRequestListener rewardedRequestListener) {DUMMY_MESSAGE}");
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            Debug.LogWarning($"Call to setSessionAdParams(SessionAdParams sessionAdParams) {DUMMY_MESSAGE}");
        }

        public void setLoadingTimeOut(int value)
        {
            Debug.LogWarning($"Call to setLoadingTimeOut(int value) {DUMMY_MESSAGE}");
        }

        public void setPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to setPlacementId(string placementId) {DUMMY_MESSAGE}");
        }

        public void setBidPayload(string bidPayLoad)
        {
            Debug.LogWarning($"Call to setBidPayload(string bidPayLoad) {DUMMY_MESSAGE}");
        }

        public void setNetworks(string networks)
        {
            Debug.LogWarning($"Call to setNetworks(string networks) {DUMMY_MESSAGE}");
        }

        public void setListener(INativeRequestListener bannerRequestListener)
        {
            Debug.LogWarning($"Call to setListener(INativeRequestListener bannerRequestListener) {DUMMY_MESSAGE}");
        }

        INativeRequest INativeRequestBuilder.build()
        {
            Debug.LogWarning($"Call to INativeRequestBuilder.build() {DUMMY_MESSAGE}");
            return null;
        }

        public string getTitle()
        {
            Debug.LogWarning($"Call to getTitle() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public string getDescription()
        {
            Debug.LogWarning($"Call to getDescription() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public string getCallToAction()
        {
            Debug.LogWarning($"Call to getCallToAction() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public string getImage(NativeAd nativeAd)
        {
            Debug.LogWarning($"Call to getImage() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public string getIcon(NativeAd nativeAd)
        {
            Debug.LogWarning($"Call to getIcon() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public float getRating()
        {
            Debug.LogWarning($"Call to getRating() {DUMMY_MESSAGE}");
            return 0.0f;
        }

        public bool canShow()
        {
            Debug.LogWarning($"Call to canShow() {DUMMY_MESSAGE}");
            return false;
        }

        public void load(NativeRequest nativeRequest)
        {
            Debug.LogWarning($"Call to load(NativeRequest nativeRequest) {DUMMY_MESSAGE}");
        }

        public void setUSPrivacyString(string usPrivacyString)
        {
            Debug.LogWarning($"Call to setUSPrivacyString(string usPrivacyString) {DUMMY_MESSAGE}");
        }

        public void setPublisher(Publisher publisher)
        {
            Debug.LogWarning($"Call to setPublisher(Publisher publisher) {DUMMY_MESSAGE}");
        }

        public bool checkAndroidPermissions(string permission)
        {
            Debug.LogWarning($"Call to checkAndroidPermissions(string permission) {DUMMY_MESSAGE}");
            return false;
        }

        public void destroy()
        {
            Debug.LogWarning($"Call to destroy() {DUMMY_MESSAGE}");
        }

        public void setListener(INativeAdListener nativeAdListener)
        {
            Debug.LogWarning($"Call to setListener(INativeAdListener nativeAdListener) {DUMMY_MESSAGE}");
        }

        public IBannerView getBannerView()
        {
            Debug.LogWarning($"Call to getBannerView() {DUMMY_MESSAGE}");
            return null;
        }

        public void hideBannerView()
        {
            Debug.LogWarning($"Call to hideBannerView() {DUMMY_MESSAGE}");
        }

        public void initialize(string sellerId)
        {
            Debug.LogWarning($"Call to initialize(string sellerId) {DUMMY_MESSAGE}");
        }

        public bool isInitialized()
        {
            Debug.LogWarning($"Call to isInitialized() {DUMMY_MESSAGE}");
            return false;
        }

        public void setEndpoint(string url)
        {
            Debug.LogWarning($"Call to setEndpoint(string url) {DUMMY_MESSAGE}");
        }

        public void load(InterstitialRequest interstitialRequest)
        {
            Debug.LogWarning($"Call to load(InterstitialRequest interstitialRequest) {DUMMY_MESSAGE}");
        }

        public void load(RewardedRequest rewardedRequest)
        {
            Debug.LogWarning($"Call to load(RewardedRequest rewardedRequest) {DUMMY_MESSAGE}");
        }

        public void load(BannerRequest bannerRequest)
        {
            Debug.LogWarning($"Call to load(BannerRequest bannerRequest) {DUMMY_MESSAGE}");
        }

        public void requestAndroidPermissions()
        {
            Debug.LogWarning($"Call to requestAndroidPermissions() {DUMMY_MESSAGE}");
        }

        public void setBirthdayYear(int year)
        {
            Debug.LogWarning($"Call to setBirthdayYear(int year) {DUMMY_MESSAGE}");
        }

        public void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            Debug.LogWarning(
                $"Call to setDeviceLocation(string providerName, double latitude, double longitude) {DUMMY_MESSAGE}");
        }

        public void setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            Debug.LogWarning($"Call to setExternalUserIds(ExternalUserId[] externalUserIdList) {DUMMY_MESSAGE}");
        }

        public void addBlockedApplication(string bundleOrPackage)
        {
            Debug.LogWarning($"Call to addBlockedApplication(string bundleOrPackage) {DUMMY_MESSAGE}");
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            Debug.LogWarning($"Call to addBlockedAdvertiserIABCategory(string category) {DUMMY_MESSAGE}");
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            Debug.LogWarning($"Call to addBlockedAdvertiserDomain(string domain) {DUMMY_MESSAGE}");
        }

        public void setCity(string city)
        {
            Debug.LogWarning($"Call to setCity(string city) {DUMMY_MESSAGE}");
        }

        public void setConsentConfig(bool consent, string consentConfig)
        {
            Debug.LogWarning($"Call to setConsentConfig(bool consent, string consentConfig) {DUMMY_MESSAGE}");
        }

        public void setCoppa(bool coppa)
        {
            Debug.LogWarning($"Call to setCoppa(bool coppa) {DUMMY_MESSAGE}");
        }

        public void setCountry(string country)
        {
            Debug.LogWarning($"Call to setCountry(string country) {DUMMY_MESSAGE}");
        }

        public void setGender(TargetingParams.Gender gender)
        {
            Debug.LogWarning($"Call to setGender(TargetingParams.Gender gender) {DUMMY_MESSAGE}");
        }

        public void setKeyWords(string[] keyWords)
        {
            Debug.LogWarning($"Call to setKeyWords(string[] keyWords) {DUMMY_MESSAGE}");
        }

        public void setListener(IInterstitialAdListener listener)
        {
            Debug.LogWarning($"Call to setListener(IInterstitialAdListener listener) {DUMMY_MESSAGE}");
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            Debug.LogWarning($"Call to setListener(IRewardedAdListener rewardedAdListener) {DUMMY_MESSAGE}");
        }

        public void setListener(IBannerListener bannerViewListener)
        {
            Debug.LogWarning($"Call to setListener(IBannerListener bannerViewListener) {DUMMY_MESSAGE}");
        }

        public void setLoggingEnabled(bool logging)
        {
            Debug.LogWarning($"Call to setLoggingEnabled(bool logging) {DUMMY_MESSAGE}");
        }

        public void setStoreSubCategories(string[] storeSubCategories)
        {
            Debug.LogWarning($"Call to setStoreSubCategories(string[] storeSubCategories) {DUMMY_MESSAGE}");
        }

        public void setStoreId(string storeId)
        {
            Debug.LogWarning($"Call to setStoreId(string storeId) {DUMMY_MESSAGE}");
        }

        public void setFramework(string framework)
        {
            Debug.LogWarning($"Call to setFramework(string framework) {DUMMY_MESSAGE}");
        }

        public void setPaid(bool paid)
        {
            Debug.LogWarning($"Call to setPaid(bool paid) {DUMMY_MESSAGE}");
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParameters)
        {
            Debug.LogWarning($"Call to setPriceFloorParams(PriceFloorParams priceFloorParameters) {DUMMY_MESSAGE}");
        }

        public void setStoreUrl(string storeUrl)
        {
            Debug.LogWarning($"Call to setStoreUrl(string storeUrl) {DUMMY_MESSAGE}");
        }

        public void setStoreCategory(string storeCategory)
        {
            Debug.LogWarning($"Call to setStoreCategory(string storeCategory) {DUMMY_MESSAGE}");
        }

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            Debug.LogWarning($"Call to setSubjectToGDPR(bool subjectToGDPR) {DUMMY_MESSAGE}");
        }

        public void setAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to setAdContentType(AdContentType contentType) {DUMMY_MESSAGE}");
        }

        public void setMediaAssetTypes(NativeAdParams nativeAdParams)
        {
            Debug.LogWarning($"Call to setMediaAssetTypes(NativeAdParams nativeAdParams) {DUMMY_MESSAGE}");
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to setTargetingParams(TargetingParams targetingParams) {DUMMY_MESSAGE}");
        }

        public void setTestMode(bool test)
        {
            Debug.LogWarning($"Call to setTestMode(bool test) {DUMMY_MESSAGE}");
        }

        public void setUserId(string id)
        {
            Debug.LogWarning($"Call to setUserId(string id) {DUMMY_MESSAGE}");
        }

        public void setZip(string zip)
        {
            Debug.LogWarning($"Call to setZip(string zip) {DUMMY_MESSAGE}");
        }

        public void show()
        {
            Debug.LogWarning($"Call to show() {DUMMY_MESSAGE}");
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            Debug.LogWarning($"Call to showBannerView(int YAxis, int XAxis, BannerView bannerView) {DUMMY_MESSAGE}");
        }

        IInterstitialRequest IInterstitialRequestBuilder.build()
        {
            Debug.LogWarning($"Call to IInterstitialRequestBuilder.build() {DUMMY_MESSAGE}");
            return null;
        }

        IBannerRequest IBannerRequestBuilder.build()
        {
            Debug.LogWarning($"Call to IBannerRequestBuilder.build() {DUMMY_MESSAGE}");
            return null;
        }

        IRewardedRequest IRewardedRequestBuilder.build()
        {
            Debug.LogWarning($"Call to IRewardedRequestBuilder.build() {DUMMY_MESSAGE}");
            return null;
        }

        public void addPriceFloor(double priceFloor)
        {
            Debug.LogWarning($"Call to addPriceFloor(double priceFloor) {DUMMY_MESSAGE}");
        }

        public void addPriceFloor(string uniqueFloorId, double priceFloor)
        {
            Debug.LogWarning($"Call to addPriceFloor(string uniqueFloorId, double priceFloor) {DUMMY_MESSAGE}");
        }

        public void setSessionDuration(int value)
        {
            Debug.LogWarning($"Call to setSessionDuration(int value) {DUMMY_MESSAGE}");
        }

        public void setImpressionCount(int value)
        {
            Debug.LogWarning($"Call to setImpressionCount(int value) {DUMMY_MESSAGE}");
        }

        public void setClickRate(float value)
        {
            Debug.LogWarning($"Call to setClickRate(float value) {DUMMY_MESSAGE}");
        }

        public void setLastAdomain(string value)
        {
            Debug.LogWarning($"Call to setLastAdomain(string value) {DUMMY_MESSAGE}");
        }

        public void setIsUserClickedOnLastAd(bool value)
        {
            Debug.LogWarning($"Call to setIsUserClickedOnLastAd(bool value) {DUMMY_MESSAGE}");
        }

        public void setCompletionRate(float value)
        {
            Debug.LogWarning($"Call to setCompletionRate(float value) {DUMMY_MESSAGE}");
        }

        public void setLastClickForImpression(int value)
        {
            Debug.LogWarning($"Call to setLastClickForImpression(int value) {DUMMY_MESSAGE}");
        }

        public void setLastBundle(string value)
        {
            Debug.LogWarning($"Call to setLastBundle(string value) {DUMMY_MESSAGE}");
        }

        public BannerSize? getSize()
        {
            Debug.LogWarning($"Call to getSize() {DUMMY_MESSAGE}");
            return null;
        }

        public string getAuctionResult()
        {
            Debug.LogWarning($"Call to getAuctionResult() {DUMMY_MESSAGE}");
            return string.Empty;
        }

        public bool isDestroyed()
        {
            Debug.LogWarning($"Call to isDestroyed() {DUMMY_MESSAGE}");
            return false;
        }

        public bool isExpired()
        {
            Debug.LogWarning($"Call to isExpired() {DUMMY_MESSAGE}");
            return false;
        }
    }
}