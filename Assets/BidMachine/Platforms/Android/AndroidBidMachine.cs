#if PLATFORM_ANDROID
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;


namespace BidMachineAds.Unity.Android
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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
            getBidMachineClass().CallStatic("initialize", getActivity(), Helper.getJavaObject(sellerId));
        }

        public bool isInitialized()
        {
            return getBidMachineClass().CallStatic<bool>("isInitialized");
        }

        public void setEndpoint(string url)
        {
            getBidMachineClass().CallStatic("setEndpoint", Helper.getJavaObject(url));
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
            var androidTargetingParams = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBidMachineClass().CallStatic("setTargetingParams", androidTargetingParams.getJavaObject());
        }

        public void setConsentConfig(bool consent, string consentConfig)
        {
            getBidMachineClass().CallStatic("setConsentConfig", consent,
                Helper.getJavaObject(consentConfig));
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
            getBidMachineClass().CallStatic("setUSPrivacyString", Helper.getJavaObject(usPrivacyString));
        }

        public void setPublisher(Publisher publisher)
        {
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
            var arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
            var arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance",
                new AndroidJavaClass("java.lang.String"), keyWords.Length);
            for (var i = 0; i < keyWords.Length; i++)
            {
                arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", keyWords[i]));
            }

            JavaTargetingParametrs.Call<AndroidJavaObject>("setKeywords", arrayObject);
        }

        public void setCountry(string country)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setCountry", Helper.getJavaObject(country));
        }

        public void setCity(string city)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setCity", Helper.getJavaObject(city));
        }

        public void setZip(string zip)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setZip", Helper.getJavaObject(zip));
        }

        public void setStoreUrl(string storeUrl)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreUrl", Helper.getJavaObject(storeUrl));
        }

        public void setStoreCategory(string storeCategory)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreCategory", Helper.getJavaObject(storeCategory));
        }

        public void setStoreSubCategories(string[] storeSubCategories)
        {
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

        public void setFramework(string framework)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("setFramework", Helper.getJavaObject(framework));
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
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedApplication",
                Helper.getJavaObject(bundleOrPackage));
        }

        public void addBlockedAdvertiserIABCategory(string category)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserIABCategory",
                Helper.getJavaObject(category));
        }

        public void addBlockedAdvertiserDomain(string domain)
        {
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserDomain", Helper.getJavaObject(domain));
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
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

        public void setPriceFloor(double priceFloor)
        {
            JavaPriceFloorParams.Call<AndroidJavaObject>("addPriceFloor", priceFloor);
        }

        public void setPriceFloor(string uniqueFloorId, double priceFloor)
        {
            var androidJavaObjectUniqueFloorId = new AndroidJavaObject("java.lang.String", uniqueFloorId);
            JavaPriceFloorParams.Call<AndroidJavaObject>("addPriceFloor", androidJavaObjectUniqueFloorId, priceFloor);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AndroidBannerRequestBuilder : IBannerRequestBuilder
    {
        AndroidJavaObject bannerRequest;
        AndroidJavaObject bannerRequestBuilder;

        public AndroidJavaObject getBannerRequestBuilder()
        {
            if (bannerRequestBuilder == null)
            {
                bannerRequestBuilder = new AndroidJavaObject("io.bidmachine.banner.BannerRequest$Builder");
            }

            return bannerRequestBuilder;
        }

        public AndroidJavaObject getJavaObject()
        {
            return bannerRequestBuilder;
        }

        public void setSize(BannerRequestBuilder.Size size)
        {
            switch (size)
            {
                case BannerRequestBuilder.Size.Size_320_50:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_320_50"));
                    break;
                }
                case BannerRequestBuilder.Size.Size_300_250:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_300_250"));
                    break;
                }
                case BannerRequestBuilder.Size.Size_728_90:
                {
                    getBannerRequestBuilder().Call<AndroidJavaObject>("setSize",
                        new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>(
                            "Size_728_90"));
                    break;
                }
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            var androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBannerRequestBuilder().Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            var p = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getBannerRequestBuilder().Call<AndroidJavaObject>("setPriceFloorParams", p.getJavaObject());
        }

        public IBannerRequest build()
        {
            bannerRequest = new AndroidJavaObject("io.bidmachine.banner.BannerRequest");
            bannerRequest = getBannerRequestBuilder().Call<AndroidJavaObject>("build");
            return new AndroidBannerRequest(bannerRequest);
        }
    }

    public class AndroidInterstitialRequestBuilder : IInterstitialRequestBuilder
    {
        AndroidJavaObject interstitialRequestBuilder;
        AndroidJavaObject interstitialRequest;

        public AndroidJavaObject getInterstitialBuilder()
        {
            if (interstitialRequestBuilder == null)
            {
                interstitialRequestBuilder =
                    new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest$Builder");
            }

            return interstitialRequestBuilder;
        }

        public AndroidJavaObject getJavaObject()
        {
            return interstitialRequestBuilder;
        }

        public void setAdContentType(InterstitialRequestBuilder.ContentType contentType)
        {
            switch (contentType)
            {
                case InterstitialRequestBuilder.ContentType.All:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("All"));
                    break;
                }
                case InterstitialRequestBuilder.ContentType.Video:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Video"));
                    break;
                }
                case InterstitialRequestBuilder.ContentType.Static:
                {
                    getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType",
                        new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Static"));
                    break;
                }
            }
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            AndroidPriceFloorParams p = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getInterstitialBuilder().Call<AndroidJavaObject>("setPriceFloorParams", p.getJavaObject());
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            AndroidTargetingParams androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getInterstitialBuilder().Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public IInterstitialRequest build()
        {
            interstitialRequest = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest");
            interstitialRequest = getInterstitialBuilder().Call<AndroidJavaObject>("build");
            return new AndroidInterstitialRequest(interstitialRequest);
        }
    }

    public class AndroidRewardedRequestBuilder : IRewardedRequestBuilder
    {
        AndroidJavaObject rewardedRequest;
        AndroidJavaObject rewardedRequestBuilder;

        public AndroidJavaObject getRewardedRequestBuilder()
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
            AndroidPriceFloorParams p = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
            getRewardedRequestBuilder().Call<AndroidJavaObject>("setPriceFloorParams", p.getJavaObject());
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            AndroidTargetingParams androidTargeting =
                (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getRewardedRequestBuilder().Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public IRewardedRequest build()
        {
            rewardedRequest = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest");
            rewardedRequest = getRewardedRequestBuilder().Call<AndroidJavaObject>("build");
            return new AndroidRewardedRequest(rewardedRequest);
        }
    }

    public class AndroidBannerRequest : IBannerRequest
    {
        AndroidJavaObject bannerRequest;

        public AndroidBannerRequest(AndroidJavaObject bannerRequest)
        {
            this.bannerRequest = bannerRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return bannerRequest;
        }
    }

    public class AndroidInterstitialRequest : IInterstitialRequest
    {
        AndroidJavaObject interstitialRequest;

        public AndroidInterstitialRequest(AndroidJavaObject interstitialRequest)
        {
            this.interstitialRequest = interstitialRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return interstitialRequest;
        }
    }

    public class AndroidRewardedRequest : IRewardedRequest
    {
        AndroidJavaObject rewardedRequest;

        public AndroidRewardedRequest(AndroidJavaObject rewardedRequest)
        {
            this.rewardedRequest = rewardedRequest;
        }

        public AndroidJavaObject getJavaObject()
        {
            return rewardedRequest;
        }
    }

    public class AndroidBanner : IBanner
    {
        private AndroidJavaClass bidMachineBannerClass;
        private AndroidJavaObject bidMachineBannerInstatnce;
        private AndroidJavaObject javaBannerView;
        private AndroidJavaObject activity;

        public AndroidJavaObject getActivity()
        {
            if (activity == null)
            {
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            return activity;
        }

        public AndroidJavaObject getBidMachineBannerInstance()
        {
            if (bidMachineBannerInstatnce == null)
            {
                bidMachineBannerClass =
                    new AndroidJavaClass("com.bidmachine.bidmachineunity.BidMachineUnityBannerView");
                bidMachineBannerInstatnce = bidMachineBannerClass.CallStatic<AndroidJavaObject>("getInstance");
            }

            return bidMachineBannerInstatnce;
        }

        public void showBannerView(int YAxis, int XAxis, BannerView bannerView)
        {
            AndroidBannerView aBannerView = (AndroidBannerView)bannerView.GetBannerView();
            AndroidJavaObject jBannerView = aBannerView.getJavaObject();
            getBidMachineBannerInstance().Call("showAdView", getActivity(), YAxis, XAxis, jBannerView);
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

    public class AndroidBannerView : IBannerView
    {
        private AndroidJavaObject javaBannerView;

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
            AndroidBannerRequest aBannerRequest = (AndroidBannerRequest)bannerRequest.GetBannerRequest();
            AndroidJavaObject jBannerRequest = aBannerRequest.getJavaObject();
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

    public class AndroidInterstitialAd : IInterstitialAd
    {
        private AndroidJavaObject javaInrestitialAd;
        private AndroidJavaObject activity;

        public AndroidInterstitialAd()
        {
            javaInrestitialAd = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialAd", getActivity());
        }

        public AndroidInterstitialAd(AndroidJavaObject interstitialAd)
        {
            javaInrestitialAd = interstitialAd;
        }

        public AndroidJavaObject getActivity()
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
            AndroidInterstitialRequest aInterstitialRequest =
                (AndroidInterstitialRequest)interstitialRequest.GetInterstitialRequest();
            AndroidJavaObject jInterstitialRequest = aInterstitialRequest.getJavaObject();
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

    public class AndroidRewardedAd : IRewardedAd
    {
        private AndroidJavaObject javaRewardedAd;
        private AndroidJavaObject activity;

        public AndroidRewardedAd()
        {
            javaRewardedAd = new AndroidJavaObject("io.bidmachine.rewarded.RewardedAd", getActivity());
        }

        public AndroidRewardedAd(AndroidJavaObject rewardedAd)
        {
            javaRewardedAd = rewardedAd;
        }

        public AndroidJavaObject getActivity()
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
            AndroidRewardedRequest aRewardedRequest = (AndroidRewardedRequest)rewardedRequest.GetRewardedRequest();
            AndroidJavaObject jRewardedRequest = aRewardedRequest.getJavaObject();
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

            return value ?? null;
        }
    }
}
#endif