using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
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
            return client ?? (client = BidMachineClientFactory.GetBidMachine());
        }
        
        /// <summary>
        /// Initializes BidMachine SDK.
        /// See <see cref="BidMachine.initialize"/> for resulting triggered event.
        /// <param name="sellerId">Your Seller Id.</param>
        /// </summary>
        public static void initialize(string id)
        {
            getInstance().initialize(id);
        }
        
        /// <summary>
        /// Checks if BidMachine SDK was initialized.
        /// See <see cref="BidMachine.isInitialized"/> for resulting triggered event.
        /// @return {@code true} if BidMachine SDK was already initialized.
        /// </summary>
        public static bool isInitialized()
        {
            return getInstance().isInitialized();
        }

        /// <summary>
        /// Sets BidMachine SDK endpoint.
        /// See <see cref="BidMachine.setEndpoint"/> for resulting triggered event.
        /// <param name="url">BidMachine endpoint URL.</param>
        /// </summary>
        public static void setEndpoint(string url)
        {
            getInstance().setEndpoint(url);
        }

        /// <summary>
        /// Sets BidMachine SDK logs enabled.
        /// See <see cref="BidMachine.setLoggingEnabled"/> for resulting triggered event.
        /// @param enabled If {@code true} SDK will print all information about ad requests.
        /// </summary>      
        public static void setLoggingEnabled(bool enabled)
        {
            getInstance().setLoggingEnabled(enabled);
        }
        
        /// <summary>
        /// Initializes BidMachine SDK.
        /// See <see cref="BidMachine.setTestMode"/> for resulting triggered event.
        /// @param testMode If {@code true} SDK will run in test mode.
        /// </summary> 
        public static void setTestMode(bool testMode)
        {
            getInstance().setTestMode(testMode);
        }
        
        /// <summary>
        /// Sets default {@link TargetingParams} for all ad requests.
        /// See <see cref="BidMachine.setSubjectToGDPR"/> for resulting triggered event.
        /// <param name="targetingParams">TargetingParams object.</param>
        /// </summary>
        public static void setTargetingParams(TargetingParams targetingParams)
        {
            getInstance().setTargetingParams(targetingParams);
        }

        /// <summary>
        /// Sets consent config.
        /// See <see cref="BidMachine.setConsentConfig"/> for resulting triggered event.
        /// <param name="hasConsent">User has given consent to the processing of personal data relating to him or her. https://www.eugdpr.org/.</param>
        /// <param name="consentString">GDPR consent string if applicable, complying with the comply with the IAB standard
        //                      <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/Consent%20string%20and%20vendor%20list%20formats%20v1.1%20Final.md">Consent String Format</a>
        //                      in the <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework">Transparency and Consent Framework</a> technical specifications.</param>
        /// </summary>
        public static void setConsentConfig(bool consent, string consentString)
        {
            getInstance().setConsentConfig(consent, consentString);
        }
        
        /// <summary>
        /// Sets subject to GDPR.
        /// See <see cref="BidMachine.setSubjectToGDPR"/> for resulting triggered event.
        /// <param name="subject">Flag indicating if GDPR regulations should be applied. <a href="https://wikipedia.org/wiki/General_Data_Protection_Regulation">The General Data Protection Regulation (GDPR)</a> is a regulation of the European Union.</param>
        /// </summary>
        public static void setSubjectToGDPR(bool subject)
        {
            getInstance().setSubjectToGDPR(subject);
        }

        /// <summary>
        /// Sets subject to GDPR.
        /// See <see cref="BidMachine.setCoppa"/> for resulting triggered event.
        /// <param name="coppa">Flag indicating if COPPA regulations should be applied. <a href="https://wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act">The Children's Online Privacy Protection Act (COPPA)</a> was established by the U.S. Federal Trade Commission..</param>
        /// </summary>
        public static void setCoppa(bool coppa)
        {
            getInstance().setCoppa(coppa);
        }

        /// <summary>
        /// Sets US Privacy string.
        /// See <see cref="BidMachine.setUSPrivacyString"/> for resulting triggered event.
        /// <param name="usPrivacyString">usPrivacyString CCPA string if applicable, complying with the comply with the IAB standard
        //                        <a href="https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md">CCPA String Format</a>.
        /// </summary>
        public static void setUSPrivacyString(string usPrivacyString)
        {
            getInstance().setUSPrivacyString(usPrivacyString);
        }
        
        /// <summary>
        /// Check CoarseLocation and FineLocation permission.
        /// See <see cref="BidMachine.checkAndroidPermissions"/> for resulting triggered event.
        /// <param name="permission">Permission.CoarseLocation or Permission.CoarseLocation</param>
        /// </summary> 
        public static bool checkAndroidPermissions(string permission)
        {
            return getInstance().checkAndroidPermissions(permission);
        }
        
        /// <summary>
        /// Request CoarseLocation and FineLocation permissions.
        /// See <see cref="BidMachine.requestAndroidPermissions"/> for resulting triggered event.
        /// </summary> 
        public static void requestAndroidPermissions()
        {
            getInstance().requestAndroidPermissions();
        }
        
        /// <summary>
        /// Sets publisher information.
        /// See <see cref="BidMachine.setPublisher"/> for resulting triggered event.
        /// <param name="publisher">Publisher object which contains all information about publisher.</param>
        /// </summary> 
        public static void setPublisher(Publisher publisher)
        {
            getInstance().setPublisher(publisher);
        }
    }

    public class ExternalUserId
    {
        public ExternalUserId(string sourceId, string value)
        {
            this.SourceId = sourceId;
            this.Value = value;
        }

        public string SourceId { get; }

        public string Value { get; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Publisher
    {
        private  string id;
        private  string name;
        private  string domain;
        private  string[] categories;

        public Publisher(string id, string name, string domain, string[] categories)
        {
            this.id = id;
            this.name = name;
            this.domain = domain;
            this.categories = categories;
        }

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Domain
        {
            get => domain;
            set => domain = value;
        }

        public string[] Categories
        {
            get => categories;
            set => categories = value;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TargetingParams
    {
        private readonly ITargetingParams nativeTargetingParams;

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
            Female,
            Male,
            Omitted,
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

        public TargetingParams setStoreCategory(string storeCategory)
        {
            nativeTargetingParams.setStoreCategory(storeCategory);
            return this;
        }
        
        public TargetingParams setStoreSubCategories(string[] storeSubCategories)
        {
            nativeTargetingParams.setStoreSubCategories(storeSubCategories);
            return this;
        }
        
        public TargetingParams setFramework(string framework)
        {
            nativeTargetingParams.setFramework(framework);
            return this;
        }
        
        public TargetingParams setPaid(bool paid)
        {
            nativeTargetingParams.setPaid(paid);
            return this;
        }

        public TargetingParams setDeviceLocation(string providerName, double latitude, double longitude)
        {
            nativeTargetingParams.setDeviceLocation(providerName,latitude,longitude);
            return this;
        }

        public TargetingParams setExternalUserIds(ExternalUserId[] externalUserIdList)
        {
            nativeTargetingParams.setExternalUserIds(externalUserIdList);
            return this;
        }

        public TargetingParams addBlockedApplication(string bundleOrPackage)
        {
            nativeTargetingParams.addBlockedApplication(bundleOrPackage);
            return this;
        }

        public TargetingParams addBlockedAdvertiserIABCategory(string category)
        {
            nativeTargetingParams.addBlockedAdvertiserIABCategory(category);
            return this;
        }

        public TargetingParams addBlockedAdvertiserDomain(string domain)
        {
            nativeTargetingParams.addBlockedAdvertiserDomain(domain);
            return this;
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PriceFloorParams
    {
        private readonly IPriceFloorParams nativePriceFloorParams;

        public PriceFloorParams()
        {
            nativePriceFloorParams = BidMachineClientFactory.GetPriceFloorParametrs();
        }

        public IPriceFloorParams GetNativePriceFloorParams()
        {
            return nativePriceFloorParams;
        }

        public PriceFloorParams addPriceFloor(double priceFloor)
        {
            nativePriceFloorParams.addPriceFloor(priceFloor);
            return this;
        }

        public PriceFloorParams addPriceFloor(string uniqueFloorId, double priceFloor)
        {
            nativePriceFloorParams.addPriceFloor(uniqueFloorId, priceFloor);
            return this;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SessionAdParams
    {
        private readonly ISessionAdParams nativeSessionAdParams;

        public SessionAdParams()
        {
            nativeSessionAdParams = BidMachineClientFactory.GetSessionAdParams();
        }
        
        public ISessionAdParams GetNativeSessionAdParams()
        {
            return nativeSessionAdParams;
        }

        public SessionAdParams setSessionDuration(int value)
        {
            nativeSessionAdParams.setSessionDuration(value);
            return this;
        }
        
        public SessionAdParams setImpressionCount(int value)
        {
            nativeSessionAdParams.setImpressionCount(value);
            return this;
        }
        
        public SessionAdParams setClickRate(float value)
        {
            nativeSessionAdParams.setClickRate(value);
            return this;
        }
        
        public SessionAdParams setIsUserClickedOnLastAd(bool value)
        {
            nativeSessionAdParams.setIsUserClickedOnLastAd(value);
            return this;
        }
     
        public SessionAdParams setCompletionRate(float value)
        {
            nativeSessionAdParams.setCompletionRate(value);
            return this;
        }
       
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BMError
    {
        public int code;
        public string brief;
        public string message;
    }

    public class BannerRequestBuilder
    {
        private readonly IBannerRequestBuilder nativeBannerRequestBuilder;

        public BannerRequestBuilder()
        {
            nativeBannerRequestBuilder = BidMachineClientFactory.GetBannerRequestBuilder();
        }

        public enum Size
        {
            Size_320_50,
            Size_300_250,
            Size_728_90,
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class InterstitialRequestBuilder
    {
        private readonly IInterstitialRequestBuilder nativeInterstitialRequestBuilder;

        public InterstitialRequestBuilder()
        {
            nativeInterstitialRequestBuilder = BidMachineClientFactory.GetIntertitialRequestBuilder();
        }

        public enum ContentType
        {
            All,
            Video,
            Static,
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RewardedRequestBuilder
    {
        private readonly IRewardedRequestBuilder nativeRewardedRequestBuilder;

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
        private readonly IBannerRequest nativeBannerRequest;

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
        private readonly IInterstitialRequest nativeInterstitialRequest;

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
        private readonly IRewardedRequest nativeRewardedRequest;

        public RewardedRequest(IRewardedRequest rewardedRequest)
        {
            nativeRewardedRequest = rewardedRequest;
        }

        public IRewardedRequest GetRewardedRequest()
        {
            return nativeRewardedRequest;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Banner
    {
        private readonly IBanner nativeBanner;

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BannerView
    {
        private readonly IBannerView nativeBannerView;

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class InterstitialAd
    {
        private readonly IInterstitialAd nativeInterstitialAd;

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RewardedAd
    {
        private readonly IRewardedAd nativeRewardedAd;

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