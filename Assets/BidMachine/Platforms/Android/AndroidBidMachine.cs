using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Android;
using UnityEngine.Android;


namespace BidMachineAds.Unity.Android
{

    public class AndroidBidMachine : IBidMachine
    {
        bool isShow;
        AndroidJavaClass JavaBidMachineClass;
        AndroidJavaClass JavaAppCompatClass;
        AndroidJavaObject userSettings;
        AndroidJavaObject activity;
        AndroidJavaObject popupWindow, resources, displayMetrics, window, decorView, attributes, rootView;

        public AndroidJavaClass getBidMachineClass()
        {
            if (JavaBidMachineClass == null)
            {
                JavaBidMachineClass = new AndroidJavaClass("io.bidmachine.BidMachine");
            }
            return JavaBidMachineClass;
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

        public AndroidJavaClass getAppCompatClass()
        {
            if (JavaAppCompatClass == null)
            {
                JavaAppCompatClass = new AndroidJavaClass("android.support.v4.app");
            }
            return JavaAppCompatClass;
        }

        public void initialize(string sellerId)
        {
            AndroidJavaObject androidJavaObjectSellerId = new AndroidJavaObject("java.lang.String", sellerId);
            getBidMachineClass().CallStatic("initialize", getActivity(), androidJavaObjectSellerId);

            Debug.Log("AppodealUnity. SDK Version: " + getBidMachineClass().GetStatic<int>("VERSION_CODE"));
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
            AndroidTargetingParams p = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBidMachineClass().CallStatic("setTargetingParams", p.getJavaObject());
        }

        public void setConsentConfig(bool consent, string consentConfig)
        {
            AndroidJavaObject androidJavaObjectConsentConsentConfig = new AndroidJavaObject("java.lang.String", consentConfig);
            getBidMachineClass().CallStatic("setConsentConfig", consent, androidJavaObjectConsentConsentConfig);
        }

        public void setSubjectToGDPR(bool subjectToGDPR)
        {
            AndroidJavaObject androidJavaObjectSubjectToGDPR = new AndroidJavaObject("java.lang.Boolean", subjectToGDPR);
            getBidMachineClass().CallStatic("setSubjectToGDPR", androidJavaObjectSubjectToGDPR);
        }

        public void setCoppa(bool coppa)
        {
            AndroidJavaObject androidJavaObjectCoppa = new AndroidJavaObject("java.lang.Boolean", coppa);
            getBidMachineClass().CallStatic("setCoppa", androidJavaObjectCoppa);
        }

        public bool checkAndroidPermissions(string permission)
        {
            bool flag = false;
            switch (permission){
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
            #if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.FineLocation);
            #endif
        }
    }

    public class AndroidTargetingParams : ITargetingParams
    {

        AndroidJavaObject JavaTargetingParametrs;

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
            AndroidJavaObject androidJavaObjectId = new AndroidJavaObject("java.lang.String", id);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setUserId", androidJavaObjectId);
        }

        public void setGender(TargetingParams.Gender gender)
        {
            switch (gender)
            {
                case TargetingParams.Gender.Omitted:
                    {
                        JavaTargetingParametrs.Call<AndroidJavaObject>("setGender", new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Omitted"));
                        break;
                    }
                case TargetingParams.Gender.Male:
                    {
                        JavaTargetingParametrs.Call<AndroidJavaObject>("setGender", new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Male"));
                        break;
                    }
                case TargetingParams.Gender.Female:
                    {
                        JavaTargetingParametrs.Call<AndroidJavaObject>("setGender", new AndroidJavaClass("io.bidmachine.utils.Gender").GetStatic<AndroidJavaObject>("Female"));
                        break;
                    }
            }
        }

        public void setBirthdayYear(int year)
        {
            AndroidJavaObject androidJavaObjectBirthdayYear = new AndroidJavaObject("java.lang.Integer", year);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setBirthdayYear", androidJavaObjectBirthdayYear);
        }

        public void setKeyWords(string[] keyWords)
        {
            AndroidJavaClass arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
            AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass("java.lang.String"), keyWords.Length);
            for (int i = 0; i < keyWords.Length; i++)
            {
                arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", keyWords[i]));
            }
            JavaTargetingParametrs.Call<AndroidJavaObject>("setKeywords", arrayObject);
        }

        
        public void setDeviceLocation(double latitude, double longitude)
        {

        }

        public void setCountry(string country)
        {
            AndroidJavaObject androidJavaObjectCountry = new AndroidJavaObject("java.lang.String", country);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setCountry", androidJavaObjectCountry);
        }

        public void setCity(string city)
        {
            AndroidJavaObject androidJavaObjectCity = new AndroidJavaObject("java.lang.String", city);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setCity", androidJavaObjectCity);
        }

        public void setZip(string zip)
        {
            AndroidJavaObject androidJavaObjectZip = new AndroidJavaObject("java.lang.String", zip);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setZip", androidJavaObjectZip);
        }

        public void setStoreUrl(string storeUrl)
        {
            AndroidJavaObject androidJavaObjectStoreUrl = new AndroidJavaObject("java.lang.String", storeUrl);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setStoreUrl", androidJavaObjectStoreUrl);
        }

        public void setPaid(bool paid)
        {
            AndroidJavaObject androidJavaObjectPaid = new AndroidJavaObject("java.lang.Boolean", paid);
            JavaTargetingParametrs.Call<AndroidJavaObject>("setPaid", androidJavaObjectPaid);
        }

        public void setBlockedAdvertiserIABCategories(string categories)
        {
            AndroidJavaObject aCategories = new AndroidJavaObject("java.lang.String", categories);
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserIABCategory", aCategories);
        }

        public void setBlockedAdvertiserDomain(string domains)
        {
            AndroidJavaObject aDomains = new AndroidJavaObject("java.lang.String", domains);
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedAdvertiserDomain", aDomains);
        }

        public void setBlockedApplication(string applications)
        {
            AndroidJavaObject aApplication = new AndroidJavaObject("java.lang.String", applications);
            JavaTargetingParametrs.Call<AndroidJavaObject>("addBlockedApplication", aApplication);
        }
    }

    public class AndroidPriceFloorParams : IPriceFloorParams
    {
        AndroidJavaObject JavaPriceFloorParams;

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
            AndroidJavaObject androidJavaObjectUniqueFloorId = new AndroidJavaObject("java.lang.String", uniqueFloorId);
            JavaPriceFloorParams.Call<AndroidJavaObject>("addPriceFloor", androidJavaObjectUniqueFloorId, priceFloor);
        }
    }

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
                        getBannerRequestBuilder().Call<AndroidJavaObject>("setSize", new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>("Size_320_50"));
                        break;
                    }
                case BannerRequestBuilder.Size.Size_300_250:
                    {
                        getBannerRequestBuilder().Call<AndroidJavaObject>("setSize", new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>("Size_300_250"));
                        break;
                    }
                case BannerRequestBuilder.Size.Size_728_90:
                    {
                        getBannerRequestBuilder().Call<AndroidJavaObject>("setSize", new AndroidJavaClass("io.bidmachine.banner.BannerSize").GetStatic<AndroidJavaObject>("Size_728_90"));
                        break;
                    }
            }
        }

        public void setTargetingParams(TargetingParams targetingParams)
        {
            AndroidTargetingParams androidTargeting = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getBannerRequestBuilder().Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public void setPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            AndroidPriceFloorParams p = (AndroidPriceFloorParams)priceFloorParams.GetNativePriceFloorParams();
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
            if(interstitialRequestBuilder == null)
            {
                interstitialRequestBuilder = new AndroidJavaObject("io.bidmachine.interstitial.InterstitialRequest$Builder");
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
                        getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType", new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("All"));
                        break;
                    }
                case InterstitialRequestBuilder.ContentType.Video:
                    {
                        getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType", new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Video"));
                        break;
                    }
                case InterstitialRequestBuilder.ContentType.Static:
                    {
                        getInterstitialBuilder().Call<AndroidJavaObject>("setAdContentType", new AndroidJavaClass("io.bidmachine.AdContentType").GetStatic<AndroidJavaObject>("Static"));
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
            AndroidTargetingParams androidTargeting = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
            getInterstitialBuilder().Call<AndroidJavaObject>("setTargetingParams", androidTargeting.getJavaObject());
        }

        public IInterstitialRequest build() {
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
            AndroidTargetingParams androidTargeting = (AndroidTargetingParams)targetingParams.GetNativeTargetingParamsClient();
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
                bidMachineBannerClass = new AndroidJavaClass("com.bidmachine.bidmachineunity.BidMachineUnityBannerView");
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
            AndroidInterstitialRequest aInterstitialRequest =(AndroidInterstitialRequest) interstitialRequest.GetInterstitialRequest();
            AndroidJavaObject jInterstitialRequest = aInterstitialRequest.getJavaObject();
            javaInrestitialAd.Call<AndroidJavaObject>("load", jInterstitialRequest); 
        }

        public void setListener(IInterstitialAdListener interstitialAdListener)
        {
            if (interstitialAdListener != null)
            {
                javaInrestitialAd.Call<AndroidJavaObject>("setListener", new AndroidInterstitialAdListener(interstitialAdListener));
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
                javaRewardedAd.Call<AndroidJavaObject>("setListener", new AndroidRewardedAdListener(rewardedAdListener));
            }
        }
    }
}