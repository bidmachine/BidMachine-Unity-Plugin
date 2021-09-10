#if PLATFORM_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;


namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public class AndroidBidMachine : IBidMachine
    {
        private AndroidJavaClass JavaBidMachineClass;
        private AndroidJavaClass JavaAppCompatClass;
        private AndroidJavaObject userSettings;
        private AndroidJavaObject activity;
        private AndroidJavaObject popupWindow, resources, displayMetrics, window, decorView, attributes, rootView;

        private AndroidJavaClass getBidMachineClass()
        {
            return JavaBidMachineClass ?? (JavaBidMachineClass = new AndroidJavaClass("io.bidmachine.BidMachine"));
        }

        private AndroidJavaObject getActivity()
        {
            if (activity != null) return activity;
            var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        public void initialize(string sellerId)
        {
            if (!string.IsNullOrEmpty(sellerId))
            {
                getBidMachineClass().CallStatic("initialize", getActivity(), Helper.getJavaObject(sellerId));
            }
        }

        public bool isInitialized()
        {
            return getBidMachineClass().CallStatic<bool>("isInitialized");
        }

        public void setEndpoint(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                getBidMachineClass().CallStatic("setEndpoint", Helper.getJavaObject(url));
            }
        }

        public void setLoggingEnabled(bool logging)
        {
            getBidMachineClass().CallStatic("setLoggingEnabled", logging);
        }

        public void setTestMode(bool testMode)
        {
            getBidMachineClass().CallStatic("setTestMode", testMode);
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var androidTargetingParams = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBidMachineClass().CallStatic("setTargetingParams", androidTargetingParams.getJavaObject());
        }

        public void setConsentConfig(bool consent, string consentConfig)
        {
            if (!string.IsNullOrEmpty(consentConfig))
            {
                getBidMachineClass().CallStatic("setConsentConfig", consent,
                    Helper.getJavaObject(consentConfig));
            }
        }

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            getBidMachineClass().CallStatic("setSubjectToGDPR", Helper.getJavaObject(subjectToGDPR));
        }

        public void setCoppa(bool coppa)
        {
            getBidMachineClass().CallStatic("setCoppa", Helper.getJavaObject(coppa));
        }

        public void setUSPrivacyString(string usPrivacyString)
        {
            if (!string.IsNullOrEmpty(usPrivacyString))
            {
                getBidMachineClass().CallStatic("setUSPrivacyString", Helper.getJavaObject(usPrivacyString));
            }
        }

        public void setPublisher(Publisher publisher)
        {
            if (publisher == null) return;
            var publisherBuilder = new AndroidJavaObject("io.bidmachine.Publisher$Builder");
            publisherBuilder.Call<AndroidJavaObject>("setId", Helper.getJavaObject(publisher.ID));
            publisherBuilder.Call<AndroidJavaObject>("setName", Helper.getJavaObject(publisher.Name));
            publisherBuilder.Call<AndroidJavaObject>("setDomain", Helper.getJavaObject(publisher.Domain));

            var list = new AndroidJavaObject("java.util.ArrayList");
            foreach (var obj in publisher.Categories)
            {
                list.Call<bool>("add", Helper.getJavaObject(obj));
            }

            publisherBuilder.Call<AndroidJavaObject>("addCategories", list);

            var androidPublisher = publisherBuilder.Call<AndroidJavaObject>("build");

            getBidMachineClass().CallStatic("setPublisher", androidPublisher);
        }

        public bool checkAndroidPermissions(string permission)
        {
            var flag = false;
            if (string.IsNullOrEmpty(permission)) return false;
            switch (permission)
            {
                case Permission.CoarseLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
                case Permission.FineLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
            }

            return flag;
        }

        public void requestAndroidPermissions()
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidTargetingParams : ITargetingParams
    {
        private readonly AndroidJavaObject JavaTargetingParametrs;

        public AndroidTargetingParams()
        {
            JavaTargetingParametrs = new AndroidJavaObject("io.bidmachine.TargetingParams");
        }

        public AndroidJavaObject getJavaObject()
        {
            return JavaTargetingParametrs;
        }

        public void setUserId(string id)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setUserId", Helper.getJavaObject(id));
        }

        public void setGender(TargetingParams.Gender gender)
        {
            switch (gender)
            {
                case TargetingParams.Gender.Omitted:
                {
                    JavaTargetingParametrs.Call<AndroidJavaObject>("setGender",
                        new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Omitted"));
                    break;
                }
                case TargetingParams.Gender.Male:
                {
                    JavaTargetingParametrs.Call<AndroidJavaObject>("setGender",
                        new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Male"));
                    break;
                }
                case TargetingParams.Gender.Female:
                {
                    JavaTargetingParametrs.Call<AndroidJavaObject>("setGender",
                        new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Female"));
                    break;
                }
                default:
                    JavaTargetingParametrs.Call<AndroidJavaObject>("setGender",
                        new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Omitted"));
                    break;
            }
        }

        public void setBirthdayYear(int year)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setBirthdayYear", Helper.getJavaObject(year));
        }

        public void setKeyWords(string[] keyWords)
        {
            if (keyWords.Length > 0)
            {
                var arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
                var arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance",
                    new AndroidJavaClass("java.lang.String"), keyWords.Length);
                for (var i = 0; i < keyWords.Length; i++)
                {
                    arrayClass.CallStatic("set", arrayObject, i,
                        new AndroidJavaObject("java.lang.String", keyWords[i]));
                }

                JavaTargetingParametrs.Call<AndroidJavaObject>("setKeywords", arrayObject);
            }
        }

        public void setCountry(string country)
        {
            if (!string.IsNullOrEmpty(country))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setCountry", Helper.getJavaObject(country));
            }
        }

        public void setCity(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setCity", Helper.getJavaObject(city));
            }
        }

        public void setZip(string zip)
        {
            if (!string.IsNullOrEmpty(zip))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setZip", Helper.getJavaObject(zip));
            }
        }

        public void setStoreUrl(string storeUrl)
        {
            if (!string.IsNullOrEmpty(storeUrl))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreUrl", Helper.getJavaObject(storeUrl));
            }
        }

        public void setStoreCategory(string storeCategory)
        {
            if (!string.IsNullOrEmpty(storeCategory))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreCategory", Helper.getJavaObject(storeCategory));
            }
        }

        public void setStoreSubCategories(string[] storeSubCategories)
        {
            if (storeSubCategories.Length <= 0) return;
            var arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
            var arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance",
                new AndroidJavaClass("java.lang.String"), storeSubCategories.Length);
            for (var i = 0; i < storeSubCategories.Length; i++)
            {
                arrayClass.CallStatic("set", arrayObject, i,
                    new AndroidJavaObject("java.lang.String", storeSubCategories[i]));
            }

            JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreSubCategories", arrayObject);
        }

        public void setStoreId(string storeId)
        {
            Debug.Log("Not support on Android Platform");
        }

        public void setFramework(string framework)
        {
            if (!string.IsNullOrEmpty(framework))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("setFramework", Helper.getJavaObject(framework));
            }
        }

        public void setPaid(bool paid)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setPaid", Helper.getJavaObject(paid));
        }

        public void setDeviceLocation(string providerName, double latitude, double longitude)
        {
            var locationJavaObject =
                new AndroidJavaObject("android.location.Location", Helper.getJavaObject(providerName));

            locationJavaObject.Call("setLatitude", latitude);
            locationJavaObject.Call("setLongitude", longitude);

            JavaTargetingParametrs.Call<AndroidJavaObject>("setDeviceLocation", locationJavaObject);
        }

        public void setExternalUserIds(ExternalUserId[] externalUserIds)
        {
            if (externalUserIds.Length <= 0) return;
            var arrayList = new AndroidJavaObject("java.util.ArrayList");

            foreach (var externalUserId in externalUserIds)
            {
                arrayList.Call<bool>("add", new AndroidJavaObject("io.bidmachine.ExternalUserId",
                    Helper.getJavaObject(externalUserId.SourceId), Helper.getJavaObject(externalUserId.Value)));
            }

            JavaTargetingParametrs.Call<AndroidJavaObject>("setExternalUserIds", arrayList);
        }

        public void addBlockedApplication(string bundleOrPackage)
        {
            if (!string.IsNullOrEmpty(bundleOrPackage))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedApplication",
                    Helper.getJavaObject(bundleOrPackage));
            }
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserIABCategory",
                    Helper.getJavaObject(category));
            }
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            if (!string.IsNullOrEmpty(domain))
            {
                JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserDomain",
                    Helper.getJavaObject(domain));
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AndroidPriceFloorParams : IPriceFloorParams
    {
        private readonly AndroidJavaObject JavaPriceFloorParams;

        public AndroidPriceFloorParams()
        {
            JavaPriceFloorParams = new AndroidJavaObject("io.bidmachine.PriceFloorParams");
        }

        public AndroidJavaObject getJavaObject()
        {
            return JavaPriceFloorParams;
        }

        public void addPriceFloor(double priceFloor)
        {
            JavaPriceFloorParams.Call<AndroidJavaObject>("addPriceFloor", priceFloor);
        }

        public void addPriceFloor(string uniqueFloorId, double priceFloor)
        {
            if (!string.IsNullOrEmpty(uniqueFloorId))
            {
                JavaPriceFloorParams.Call<AndroidJavaObject>("addPriceFloor", Helper.getJavaObject(uniqueFloorId),
                    priceFloor);
            }
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AndroidSessionAdParams : ISessionAdParams
    {
        private readonly AndroidJavaObject javaSessionAdParams;

        public AndroidSessionAdParams()
        {
            javaSessionAdParams = new AndroidJavaObject("io.bidmachine.SessionAdParams");
        }

        public AndroidSessionAdParams(AndroidJavaObject sessionAdParams)
        {
            javaSessionAdParams = sessionAdParams;
        }

        public AndroidJavaObject GetAndroidSessionAdParams()
        {
            return javaSessionAdParams;
        }

        public void setSessionDuration(int value)
        {
            javaSessionAdParams.Call<AndroidJavaObject>("setSessionDuration", Helper.getJavaObject(value));
        }

        public void setImpressionCount(int value)
        {
            javaSessionAdParams.Call<AndroidJavaObject>("setImpressionCount", Helper.getJavaObject(value));
        }

        public void setClickRate(float value)
        {
            javaSessionAdParams.Call<AndroidJavaObject>("setClickRate", Helper.getJavaObject(value));
        }

        public void setLastAdomain(string value)
        {
            Debug.Log("Not support on Android Platform");
        }

        public void setIsUserClickedOnLastAd(bool value)
        {
            javaSessionAdParams.Call<AndroidJavaObject>("setIsUserClickedOnLastAd", Helper.getJavaObject(value));
        }

        public void setCompletionRate(float value)
        {
            javaSessionAdParams.Call<AndroidJavaObject>("setCompletionRate", Helper.getJavaObject(value));
        }

        public void setLastClickForImpression(int value)
        {
            Debug.Log("Not support on Android Platform");
        }

        public void setLastBundle(string value)
        {
            Debug.Log("Not support on Android Platform");
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBannerRequestBuilder : IBannerRequestBuilder
    {
        private AndroidJavaObject bannerRequest;
        private AndroidJavaObject bannerRequestBuilder;

        private AndroidJavaObject getBannerRequestBuilder()
        {
            return bannerRequestBuilder ??
                   (bannerRequestBuilder = new AndroidJavaObject("io.bidmachine.banner.BannerRequest$Builder"));
        }

        public AndroidJavaObject getJavaObject()
        {
            return bannerRequestBuilder;
        }

        public void setSize(BannerSize size)
        {
            switch (size)
            {
                case BannerSize.Size_320х50:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_320x50"));
                    break;
                }
                case BannerSize.Size_300х250:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_300x250"));
                    break;
                }
                case BannerSize.Size_728х90:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_728x90"));
                    break;
                }
                default:
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_320x50"));
                    break;
            }
        }

        public void setListener(IBannerRequestListener bannerRequestListener)
        {
            if (bannerRequestListener != null)
            {
                getBannerRequestBuilder()
                    .Call<AndroidJavaObject>("setListener", new AndroidBannerRequestListener(bannerRequestListener));
            }
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var androidSessionAdParams = (AndroidSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            getBannerRequestBuilder().Call<AndroidJavaObject>("setSessionAdParams",
                androidSessionAdParams.GetAndroidSessionAdParams());
        }

        public void setLoadingTimeOut(int value)
        {
            getBannerRequestBuilder().Call<AndroidJavaObject>("setLoadingTimeOut", value);
        }

        public void setPlacementId(string placementId)
        {
            if (!string.IsNullOrEmpty(placementId))
            {
                getBannerRequestBuilder()
                    .Call<AndroidJavaObject>("setPlacementId", Helper.getJavaObject(placementId));
            }
        }

        public void setBidPayload(string bidPayLoad)
        {
            if (!string.IsNullOrEmpty(bidPayLoad))
            {
                getBannerRequestBuilder()
                    .Call<AndroidJavaObject>("setBidPayload", Helper.getJavaObject(bidPayLoad));
            }
        }

        public void setNetworks(string networks)
        {
            if (!string.IsNullOrEmpty(networks))
            {
                getBannerRequestBuilder().Call<AndroidJavaObject>("setNetworks", Helper.getJavaObject(networks));
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBannerRequestBuilder()
                .Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null) return;
            var androidPriceFloorParams = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getBannerRequestBuilder()
                .Call<AndroidJavaObject>("setPriceFloorParams", androidPriceFloorParams.getJavaObject());
        }

        public IBannerRequest build()
        {
            bannerRequest = new AndroidJavaObject("io.bidmachine.banner.BannerRequest");
            bannerRequest = getBannerRequestBuilder().Call<AndroidJavaObject>("build");
            return new AndroidBannerRequest(bannerRequest);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        private AndroidJavaObject interstitialRequestBuilder;
        private AndroidJavaObject interstitialRequest;

        private AndroidJavaObject getInterstitialBuilder()
        {
            return interstitialRequestBuilder ?? (interstitialRequestBuilder =
                new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest$Builder"));
        }

        public AndroidJavaObject getJavaObject()
        {
            return interstitialRequestBuilder;
        }

        public void setAdContentType(AdContentType contentType)
        {
            switch (contentType)
            {
                case AdContentType.All:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("All"));
                    break;
                }
                case AdContentType.Video:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Video"));
                    break;
                }
                case AdContentType.Static:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Static"));
                    break;
                }
                default:
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("All"));
                    break;
            }
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null) return;
            var androidPriceFloorParams = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getInterstitialBuilder()
                .Call<AndroidJavaObject>("setPriceFloorParams", androidPriceFloorParams.getJavaObject());
        }

        public void setListener(IInterstitialRequestListener interstitialRequestListener)
        {
            if (interstitialRequestListener != null)
            {
                getInterstitialBuilder()
                    .Call<AndroidJavaObject>("setListener",
                        new AndroidInterstitialRequestListener(interstitialRequestListener));
            }
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var androidSessionAdParams = (AndroidSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            getInterstitialBuilder().Call<AndroidJavaObject>("setSessionAdParams",
                androidSessionAdParams.GetAndroidSessionAdParams());
        }

        public void setLoadingTimeOut(int value)
        {
            getInterstitialBuilder().Call<AndroidJavaObject>("setLoadingTimeOut", value);
        }

        public void setPlacementId(string placementId)
        {
            if (!string.IsNullOrEmpty(placementId))
            {
                getInterstitialBuilder()
                    .Call<AndroidJavaObject>("setPlacementId", Helper.getJavaObject(placementId));
            }
        }

        public void setBidPayload(string bidPayLoad)
        {
            if (!string.IsNullOrEmpty(bidPayLoad))
            {
                getInterstitialBuilder()
                    .Call<AndroidJavaObject>("setBidPayload", Helper.getJavaObject(bidPayLoad));
            }
        }

        public void setNetworks(string networks)
        {
            if (!string.IsNullOrEmpty(networks))
            {
                getInterstitialBuilder()
                    .Call<AndroidJavaObject>("setNetworks", Helper.getJavaObject(networks));
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getInterstitialBuilder()
                .Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public IInterstitialRequest build()
        {
            interstitialRequest = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest");
            interstitialRequest = getInterstitialBuilder().Call<AndroidJavaObject>("build");
            return new AndroidInterstitialRequest(interstitialRequest);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidRewardedRequestBuilder : IRewardedRequestBuilder
    {
        private AndroidJavaObject rewardedRequest;
        private AndroidJavaObject rewardedRequestBuilder;

        private AndroidJavaObject getRewardedRequestBuilder()
        {
            if (rewardedRequestBuilder == null)
            {
                rewardedRequestBuilder = new AndroidJavaObject("io.bidmachine.rewarded.RewardedRequest$Builder");
            }

            return rewardedRequestBuilder;
        }

        public AndroidJavaObject getJavaObject()
        {
            return rewardedRequestBuilder;
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null) return;
            var p = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getRewardedRequestBuilder().Call<AndroidJavaObject>("setPriceFloorParams", p.getJavaObject());
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;
            var androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getRewardedRequestBuilder()
                .Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public void setListener(IRewardedRequestListener rewardedRequestListener)
        {
            if (rewardedRequestListener != null)
            {
                getRewardedRequestBuilder()
                    .Call<AndroidJavaObject>("setListener",
                        new AndroidRewardedRequestListener(rewardedRequestListener));
            }
        }

        public void setSessionAdParams(SessionAdParams sessionAdParams)
        {
            if (sessionAdParams == null) return;
            var androidSessionAdParams = (AndroidSessionAdParams)sessionAdParams.GetNativeSessionAdParams();
            getRewardedRequestBuilder().Call<AndroidJavaObject>("setSessionAdParams",
                androidSessionAdParams.GetAndroidSessionAdParams());
        }

        public void setLoadingTimeOut(int value)
        {
            getRewardedRequestBuilder().Call<AndroidJavaObject>("setLoadingTimeOut", value);
        }

        public void setPlacementId(string placementId)
        {
            if (!string.IsNullOrEmpty(placementId))
            {
                getRewardedRequestBuilder()
                    .Call<AndroidJavaObject>("setPlacementId", Helper.getJavaObject(placementId));
            }
        }

        public void setBidPayload(string bidPayLoad)
        {
            if (!string.IsNullOrEmpty(bidPayLoad))
            {
                getRewardedRequestBuilder()
                    .Call<AndroidJavaObject>("setBidPayload", Helper.getJavaObject(bidPayLoad));
            }
        }

        public void setNetworks(string networks)
        {
            if (!string.IsNullOrEmpty(networks))
            {
                getRewardedRequestBuilder()
                    .Call<AndroidJavaObject>("setNetworks", Helper.getJavaObject(networks));
            }
        }

        public IRewardedRequest build()
        {
            rewardedRequest = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest");
            rewardedRequest = getRewardedRequestBuilder().Call<AndroidJavaObject>("build");
            return new AndroidRewardedRequest(rewardedRequest);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBannerRequest : IBannerRequest
    {
        private readonly AndroidJavaObject bannerRequest;

        public AndroidBannerRequest(AndroidJavaObject bannerRequest)
        {
            this.bannerRequest = bannerRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return bannerRequest;
        }

        public BannerSize getSize()
        {
            var bannerSize = BannerSize.Size_320х50;
            var banner = bannerRequest.Call<AndroidJavaObject>("getSize").Call<string>("toString");
            switch (banner)
            {
                case "Size_320x50":
                    bannerSize = BannerSize.Size_320х50;
                    break;
                case "Size_300x250":
                    bannerSize = BannerSize.Size_300х250;
                    break;
                case "Size_728x90":
                    bannerSize = BannerSize.Size_728х90;
                    break;
            }

            return bannerSize;
        }

        public string getAuctionResult()
        {
            var auctionResultAndroidJavaObject = bannerRequest.Call<AndroidJavaObject>("getAuctionResult");
            return !string.IsNullOrEmpty(auctionResultAndroidJavaObject.Call<string>("toString"))
                ? auctionResultAndroidJavaObject.Call<string>("toString")
                : "null";
        }

        public bool isDestroyed()
        {
            return bannerRequest.Call<bool>("isDestroyed");
        }

        public bool isExpired()
        {
            return bannerRequest.Call<bool>("isDestroyed");
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidInterstitialRequest : IInterstitialRequest
    {
        private readonly AndroidJavaObject interstitialRequest;

        public AndroidInterstitialRequest(AndroidJavaObject interstitialRequest)
        {
            this.interstitialRequest = interstitialRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return interstitialRequest;
        }
        
        public string getAuctionResult()
        {
            var auctionResultAndroidJavaObject = interstitialRequest.Call<AndroidJavaObject>("getAuctionResult");
            return !string.IsNullOrEmpty(auctionResultAndroidJavaObject.Call<string>("toString"))
                ? auctionResultAndroidJavaObject.Call<string>("toString")
                : "null";
        }

        public bool isDestroyed()
        {
            return interstitialRequest.Call<bool>("isDestroyed");
        }

        public bool isExpired()
        {
            return interstitialRequest.Call<bool>("isDestroyed");
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidRewardedRequest : IRewardedRequest
    {
        private readonly AndroidJavaObject rewardedRequest;

        public AndroidRewardedRequest(AndroidJavaObject rewardedRequest)
        {
            this.rewardedRequest = rewardedRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return rewardedRequest;
        }
        
        public string getAuctionResult()
        {
            var auctionResultAndroidJavaObject = rewardedRequest.Call<AndroidJavaObject>("getAuctionResult");
            return !string.IsNullOrEmpty(auctionResultAndroidJavaObject.Call<string>("toString"))
                ? auctionResultAndroidJavaObject.Call<string>("toString")
                : "null";
        }

        public bool isDestroyed()
        {
            return rewardedRequest.Call<bool>("isDestroyed");
        }

        public bool isExpired()
        {
            return rewardedRequest.Call<bool>("isDestroyed");
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBanner : IBanner
    {
        private AndroidJavaClass bidMachineBannerClass;
        private AndroidJavaObject bidMachineBannerInstance;
        private AndroidJavaObject javaBannerView;
        private AndroidJavaObject activity;
        private BannerView internalBannerView;

        private int internalYAxis;
        private int internalXAxis;

        private AndroidJavaObject getActivity()
        {
            if (activity != null) return activity;
            var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            return activity;
        }

        private AndroidJavaObject getBidMachineBannerInstance()
        {
            if (bidMachineBannerInstance != null) return bidMachineBannerInstance;
            bidMachineBannerClass =
                new AndroidJavaClass("com.bidmachine.bidmachineunity.BidMachineUnityBannerView");
            bidMachineBannerInstance = bidMachineBannerClass.CallStatic<AndroidJavaObject>("getInstance");

            return bidMachineBannerInstance;
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            internalBannerView = bannerView;
            internalXAxis = XAxis;
            internalYAxis = XAxis;
            getActivity().Call("runOnUiThread", new AndroidJavaRunnable(runOnUiThread));
        }

        private void runOnUiThread()
        {
            AndroidBannerView aBannerView = (AndroidBannerView)internalBannerView.GetBannerView();
            AndroidJavaObject jBannerView = aBannerView.getJavaObject();
            getBidMachineBannerInstance()
                .Call("showAdView", getActivity(), internalXAxis, internalYAxis, jBannerView);
        }

        public void hideBannerView()
        {
            getBidMachineBannerInstance().Call("hidePopUpWindow", getActivity());
        }

        public IBannerView getBannerView()
        {
            javaBannerView = new AndroidJavaObject("io.bidmachine.banner.BannerView", getActivity());
            javaBannerView = getBidMachineBannerInstance().Call<AndroidJavaObject>("getBannerView", getActivity());
            return new AndroidBannerView(javaBannerView);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBannerView : IBannerView
    {
        private readonly AndroidJavaObject javaBannerView;

        public AndroidBannerView(AndroidJavaObject bannerView)
        {
            javaBannerView = bannerView;
        }

        public AndroidJavaObject getJavaObject()
        {
            return javaBannerView;
        }

        public void destroy()
        {
            javaBannerView.Call("destroy");
        }

        public void load(BannerRequest bannerRequest)
        {
            var aBannerRequest = (AndroidBannerRequest)bannerRequest.GetBannerRequest();
            var jBannerRequest = aBannerRequest.getJavaObject();
            javaBannerView.Call<AndroidJavaObject>("load", jBannerRequest);
        }

        public void setListener(IBannerListener bannerListener)
        {
            if (bannerListener != null)
            {
                javaBannerView.Call<AndroidJavaObject>("setListener", new AndroidBannerListener(bannerListener));
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidInterstitialAd : IInterstitialAd
    {
        private readonly AndroidJavaObject javaInrestitialAd;
        private AndroidJavaObject activity;

        public AndroidInterstitialAd()
        {
            javaInrestitialAd = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialAd", getActivity());
        }

        public AndroidInterstitialAd(AndroidJavaObject interstitialAd)
        {
            javaInrestitialAd = interstitialAd;
        }

        private AndroidJavaObject getActivity()
        {
            if (activity != null) return activity;
            var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        public AndroidJavaObject getJavaObject()
        {
            return javaInrestitialAd;
        }

        public bool canShow()
        {
            return javaInrestitialAd.Call<bool>("canShow");
        }

        public void destroy()
        {
            javaInrestitialAd.Call("destroy");
        }

        public void show()
        {
            javaInrestitialAd.Call("show");
        }

        public void load(InterstitialRequest interstitialRequest)
        {
            var aInterstitialRequest =
                (AndroidInterstitialRequest)interstitialRequest.GetInterstitialRequest();
            var jInterstitialRequest = aInterstitialRequest.getJavaObject();
            javaInrestitialAd.Call<AndroidJavaObject>("load", jInterstitialRequest);
        }

        public void setListener(IInterstitialAdListener interstitialAdListener)
        {
            if (interstitialAdListener != null)
            {
                javaInrestitialAd.Call<AndroidJavaObject>("setListener",
                    new AndroidInterstitialAdListener(interstitialAdListener));
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidRewardedAd : IRewardedAd
    {
        private readonly AndroidJavaObject javaRewardedAd;
        private AndroidJavaObject activity;

        public AndroidRewardedAd()
        {
            javaRewardedAd = new AndroidJavaObject("io.bidmachine.rewarded.RewardedAd", getActivity());
        }

        public AndroidRewardedAd(AndroidJavaObject rewardedAd)
        {
            javaRewardedAd = rewardedAd;
        }

        private AndroidJavaObject getActivity()
        {
            if (activity == null)
            {
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            return activity;
        }

        public AndroidJavaObject getJavaObject()
        {
            return javaRewardedAd;
        }

        public bool canShow()
        {
            return javaRewardedAd.Call<bool>("canShow");
        }

        public void destroy()
        {
            javaRewardedAd.Call("destroy");
        }

        public void show()
        {
            javaRewardedAd.Call("show");
        }

        public void load(RewardedRequest rewardedRequest)
        {
            if (rewardedRequest == null) return;
            var aRewardedRequest = (AndroidRewardedRequest)rewardedRequest.GetRewardedRequest();
            var jRewardedRequest = aRewardedRequest.getJavaObject();
            javaRewardedAd.Call<AndroidJavaObject>("load", jRewardedRequest);
        }

        public void setListener(IRewardedAdListener rewardedAdListener)
        {
            if (rewardedAdListener != null)
            {
                javaRewardedAd.Call<AndroidJavaObject>("setListener",
                    new AndroidRewardedAdListener(rewardedAdListener));
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Helper
    {
        public static object getJavaObject(object value)
        {
            if (value is string)
            {
                return value;
            }

            if (value is char)
            {
                return new AndroidJavaObject("java.lang.Character", value);
            }

            if ((value is bool))
            {
                return new AndroidJavaObject("java.lang.Boolean", value);
            }

            if (value is int)
            {
                return new AndroidJavaObject("java.lang.Integer", value);
            }

            if (value is long)
            {
                return new AndroidJavaObject("java.lang.Long", value);
            }

            if (value is float)
            {
                return new AndroidJavaObject("java.lang.Float", value);
            }

            if (value is double)
            {
                return new AndroidJavaObject("java.lang.Double", value);
            }

            return value;
        }
    }
}
#endif