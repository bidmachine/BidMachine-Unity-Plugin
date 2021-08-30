using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public class BidMachine
    {
        public static int BANNER_HORIZONTAL_SMART = -1;
        public static int BANNER_HORIZONTAL_CENTER = -2;
        public static int BANNER_HORIZONTAL_LEFT = -4;
        public static int BANNER_HORIZONTAL_RIGHT = -3;

        public static int BANNER_TOP = 1;
        public static int BANNER_BOTTOM = 2;

        private static IBidMachine client;
        private static IBidMachine getInstance()
        {
            if (client == null)
            {
                client = BidMachineClientFactory.GetBidMachine();
            }
            return client;
        }

        public static void initialize(string id)
        {
            getInstance().initialize(id);
        }

        public static void setLoggingEnabled(bool logging)
        {
            getInstance().setLoggingEnabled(logging);
        }

        public static void setTestMode(bool testMode)
        {
            getInstance().setTestMode(testMode);
        }

        public static bool checkAndroidPermissions(string permission)
        {
            return getInstance().checkAndroidPermissions(permission);
        }

        public static void setConsentConfig(bool consent, string consentConfig)
        {
            getInstance().setConsentConfig(consent, consentConfig);
        }

        public static void setSubjectToGDPR(bool subjectToGDPR)
        {
            getInstance().setSubjectToGDPR(subjectToGDPR);
        }

        public static void setCoppa(bool coppa)
        {
            getInstance().setCoppa(coppa);
        }

        public static void setTargetingParams(TargetingParams targetingParams)
        {
            getInstance().setTargetingParams(targetingParams);
        }

        public static void requestAndroidPermissions()
        {
            getInstance().requestAndroidPermissions();
        }
    }

    public class TargetingParams
    {

        public ITargetingParams nativeTargetingParams;
        public TargetingParams()
        {
            nativeTargetingParams = BidMachineClientFactory.GetTargetingParams();
        }

        public ITargetingParams GetNativeTargetingParamsClient()
        {
            return nativeTargetingParams;
        }

        public enum Gender
        {
            Female, Male, Omitted,
        }

        public TargetingParams setUserId(string id)
        {
            nativeTargetingParams.setUserId(id);
            return this;
        }

        public TargetingParams setGender(Gender gender)
        {
            nativeTargetingParams.setGender(gender);
            return this;
        }

        public TargetingParams setBirthdayYear(int year)
        {
            nativeTargetingParams.setBirthdayYear(year);
            return this;
        }

        public TargetingParams setKeyWords(string[] keyWords)
        {
            nativeTargetingParams.setKeyWords(keyWords);
            return this;
        }

        public TargetingParams setDeviceLocation(double latitude, double longitude)
        {
            nativeTargetingParams.setDeviceLocation(latitude,longitude);
            return this;
        }

        public TargetingParams setCountry(string country)
        {
            nativeTargetingParams.setCountry(country);
            return this;
        }

        public TargetingParams setCity(string city)
        {
            nativeTargetingParams.setCity(city);
            return this;
        }

        public TargetingParams setZip(string zip)
        {
            nativeTargetingParams.setZip(zip);
            return this;
        }

        public TargetingParams setStoreUrl(string storeUrl)
        {
            nativeTargetingParams.setStoreUrl(storeUrl);
            return this;
        }

        public TargetingParams setPaid(bool paid)
        {
            nativeTargetingParams.setPaid(paid);
            return this;
        }

        public TargetingParams setBlockedAdvertiserIABCategories(string categories)
        {
            nativeTargetingParams.setBlockedAdvertiserIABCategories(categories);
            return this;
        }

        public TargetingParams setBlockedAdvertiserDomain(string domains)
        {
            nativeTargetingParams.setBlockedAdvertiserDomain(domains);
            return this;
        }

        public TargetingParams setBlockedApplication(string applications)
        {
            nativeTargetingParams.setBlockedApplication(applications);
            return this;
        }
    }

    public class PriceFloorParams
    {
        public IPriceFloorParams nativePriceFloorParams;
        public PriceFloorParams()
        {
            nativePriceFloorParams = BidMachineClientFactory.GetPriceFloorParametrs();
        }

        public IPriceFloorParams GetNativePriceFloorParams()
        {
            return nativePriceFloorParams;
        }

        public PriceFloorParams setPriceFloor(double priceFloor)
        {
            nativePriceFloorParams.setPriceFloor(priceFloor);
            return this;
        }

        public PriceFloorParams setPriceFloor(string uniqueFloorId, double priceFloor)
        {
            nativePriceFloorParams.setPriceFloor(uniqueFloorId, priceFloor);
            return this;
        }
    }

    public class BMError
    {
        public int code;
        public string brief;
        public string message;
    }

    public class BannerRequestBuilder
    {
        public IBannerRequestBuilder nativeBannerRequestBuilder;
        public BannerRequestBuilder()
        {
            nativeBannerRequestBuilder = BidMachineClientFactory.GetBannerRequestBuilder();
        }

        public enum Size
        {
            Size_320_50, Size_300_250, Size_728_90,
        }

        public IBannerRequestBuilder GetBannerRequestBuilder()
        {
            return nativeBannerRequestBuilder;
        }

        public BannerRequestBuilder setTargetingParams(TargetingParams targetingParams)
        {
            nativeBannerRequestBuilder.setTargetingParams(targetingParams);
            return this;
        }

        public BannerRequestBuilder setSize(Size size)
        {
            nativeBannerRequestBuilder.setSize(size);
            return this;
        }

        public BannerRequestBuilder setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            nativeBannerRequestBuilder.setPriceFloorParams(priceFloorParams);
            return this;
        }

        public BannerRequest build()
        {
            return new BannerRequest(nativeBannerRequestBuilder.build());
        }
    }

    public class InterstitialRequestBuilder
    {
        public IInterstitialRequestBuilder nativeInterstitialRequestBuilder;
        public InterstitialRequestBuilder()
        {
            nativeInterstitialRequestBuilder = BidMachineClientFactory.GetIntertitialRequestBuilder();
        }

        public enum ContentType
        {
            All, Video, Static,
        }

        public IInterstitialRequestBuilder GetInterstitialRequestBuilder()
        {
            return nativeInterstitialRequestBuilder;
        }

        public InterstitialRequestBuilder setTargetingParams(TargetingParams targetingParams)
        {
            nativeInterstitialRequestBuilder.setTargetingParams(targetingParams);
            return this;
        }

        public InterstitialRequestBuilder setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            nativeInterstitialRequestBuilder.setPriceFloorParams(priceFloorParams);
            return this;
        }

        public InterstitialRequestBuilder setAdContentType(ContentType contentType)
        {
            nativeInterstitialRequestBuilder.setAdContentType(contentType);
            return this;
        }

        public InterstitialRequest build()
        {
            return new InterstitialRequest(nativeInterstitialRequestBuilder.build());
        }
    }

    public class RewardedRequestBuilder
    {
        public IRewardedRequestBuilder nativeRewardedRequestBuilder;
        public RewardedRequestBuilder()
        {
            nativeRewardedRequestBuilder = BidMachineClientFactory.GetRewardedRequestBuilder();
        }

        public IRewardedRequestBuilder GetRewardedRequestBuilder()
        {
            return nativeRewardedRequestBuilder;
        }

        public RewardedRequestBuilder setTargetingParams(TargetingParams targetingParams)
        {
            nativeRewardedRequestBuilder.setTargetingParams(targetingParams);
            return this;
        }

        public RewardedRequestBuilder setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            nativeRewardedRequestBuilder.setPriceFloorParams(priceFloorParams);
            return this;
        }

        public RewardedRequest build()
        {
            return new RewardedRequest(nativeRewardedRequestBuilder.build());
        }
    }

    public class BannerRequest
    {
        public IBannerRequest nativeBannerRequest;
        public BannerRequest(IBannerRequest bannerRequest)
        {
            nativeBannerRequest = bannerRequest;
        }

        public IBannerRequest GetBannerRequest()
        {
            return nativeBannerRequest;
        }
    }

    public class InterstitialRequest
    {
        public IInterstitialRequest nativeInterstitialRequest;
        public InterstitialRequest(IInterstitialRequest interstitialRequest)
        {
            nativeInterstitialRequest = interstitialRequest;
        }

        public IInterstitialRequest GetInterstitialRequest()
        {
            return nativeInterstitialRequest;
        }
    }

    public class RewardedRequest
    {
        public IRewardedRequest nativeRewardedRequest;
        public RewardedRequest(IRewardedRequest rewardedRequest)
        {
            nativeRewardedRequest = rewardedRequest;
        }

        public IRewardedRequest GetRewardedRequest()
        {
            return nativeRewardedRequest;
        }
    }

    public class Banner
    {
        public IBanner nativeBanner;
        public Banner()
        {
            nativeBanner = BidMachineClientFactory.GetAndroidBanner();
        }

        public Banner(IBanner banner)
        {
            nativeBanner = banner;
        }

        public IBanner GetBanner()
        {
            return nativeBanner;
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            nativeBanner.showBannerView(YAxis, XAxis, bannerView);
        }

        public void hideBannerView()
        {
            nativeBanner.hideBannerView();
        }

        public BannerView GetBannerView()
        {
            return new BannerView(nativeBanner.getBannerView());
        }
    }

    public class BannerView
    {
        public IBannerView nativeBannerView;
        public BannerView()
        {
            nativeBannerView = BidMachineClientFactory.GetAndroidBannerView();
        }

        public BannerView(IBannerView bannerView)
        {
            nativeBannerView = bannerView;
        }

        public IBannerView GetBannerView()
        {
            return nativeBannerView;
        }

        public void destroy()
        {
            nativeBannerView.destroy();
        }

        public void load(BannerRequest bannerRequest)
        {
            nativeBannerView.load(bannerRequest);
        }

        public void setListener(IBannerListener bannerViewListener)
        {
            nativeBannerView.setListener(bannerViewListener);
        }
    }

    public class InterstitialAd
    {
        public IInterstitialAd nativeInterstitialAd;
        public InterstitialAd()
        {
            nativeInterstitialAd = BidMachineClientFactory.GetInterstitialAd();
        }

        public InterstitialAd(IInterstitialAd interstitialAd)
        {
            nativeInterstitialAd = interstitialAd;
        }

        public IInterstitialAd GetInterstitialAdClient()
        {
            return nativeInterstitialAd;
        }

        public bool canShow()
        {
            return nativeInterstitialAd.canShow();
        }

        public void destroy()
        {
            nativeInterstitialAd.destroy();

        }

        public void show()
        {
            nativeInterstitialAd.show();

        }

        public void load(InterstitialRequest interstitialRequest)
        {
            nativeInterstitialAd.load(interstitialRequest);
        }

        public void setListener(IInterstitialAdListener interstitialAdListener)
        {
            nativeInterstitialAd.setListener(interstitialAdListener);
        }
    }

    public class RewardedAd
    {
        public IRewardedAd nativeRewardedAd;
        public RewardedAd()
        {
            nativeRewardedAd = BidMachineClientFactory.GetRewardedAd();
        }

        public RewardedAd(IRewardedAd rewardedAd)
        {
            nativeRewardedAd = rewardedAd;
        }

        public IRewardedAd GetInterstitialAdClient()
        {
            return nativeRewardedAd;
        }

        public bool canShow()
        {
            return nativeRewardedAd.canShow();
        }

        public void destroy()
        {
            nativeRewardedAd.destroy();
        }

        public void show()
        {
            nativeRewardedAd.show();
        }

        public void load(RewardedRequest rewardedRequest)
        {
            nativeRewardedAd.load(rewardedRequest);
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            nativeRewardedAd.setListener(rewardedAdListener);
        }
    }
}
