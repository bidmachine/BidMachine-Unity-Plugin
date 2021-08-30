using System;
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.UI;

public class BidMachineDemoController : MonoBehaviour, IInterstitialAdListener, IRewardedAdListener, IBannerListener
{
    
    [SerializeField] public Toggle tgTesting;
    [SerializeField] public Toggle tgLogging;
    
    TargetingParams targetingParams;
    PriceFloorParams priceFloorParams;

    InterstitialRequest interstitialRequest;
    InterstitialRequestBuilder interstitialRequestBuilder;
    InterstitialAd interstitialAd;

    RewardedRequest rewardedRequest;
    RewardedRequestBuilder rewardedRequestBuilder;
    RewardedAd rewardedAd;

    BannerRequest bannerRequest;
    BannerRequestBuilder bannerRequestBuilder;
    BannerView bannerView;
    Banner banner;

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;
    }

    public void BidMachineInitialize()
    {
        // targetingParams = new TargetingParams();
        // priceFloorParams = new PriceFloorParams();
        //
        // /* Vendor-specific ID for the user. */
        // targetingParams.setUserId("1");
        //
        // /* Gender, one of following: Female, Male, Omitted. */
        // targetingParams.setGender(TargetingParams.Gender.Male);
        //
        // /* Year of birth as a 4-digit integer (e.g - 1990). */
        // targetingParams.setBirthdayYear(1990);
        //
        // /* List of keywords, interests, or intents (separated by comma if you use .xml). */
        // targetingParams.setKeyWords(new[] { "games", "sport" });
        //
        // /* Location of the device. It might not be the location that was sent to the server,
        //  * as it is compared with the current device location at the time, when it was received. */
        // targetingParams.setDeviceLocation(12.8888, 12.222);
        //
        // /* Country of the user's home base (i.e., not necessarily their current location). */
        // targetingParams.setCountry("USA");
        //
        // /*City of the user's home base (i.e., not necessarily their current location).*/
        // targetingParams.setCity("New-York");
        //
        // /* Zip of the user's home base (i.e., not necessarily their current location). */
        // targetingParams.setZip("10001");
        //
        // /* App store URL for an installed app; for IQG 2.1 compliance. */
        // targetingParams.setStoreUrl("https://test.com");
        //
        // /* Determines, if it is a free or paid version of the app. */
        // targetingParams.setPaid(true);
        //
        // /* Block list of content categories using IDs. */
        // targetingParams.setBlockedAdvertiserIABCategories("IAB26,AB25");
        //
        // /* Block list of advertisers by their domains (e.g., “example.com”). */
        // targetingParams.setBlockedAdvertiserDomain("example.com");
        //
        // /* Block list of apps where ads are disallowed. These should be bundle or package names
        //  * (e.g., “com.foo.mygame”) and should NOT be app store IDs (e.g., not iTunes store IDs). */
        // targetingParams.setBlockedApplication("com.foo.mygame");
        //
        // priceFloorParams = new PriceFloorParams();
        // priceFloorParams.setPriceFloor("sample", 2.15);

        /* Enable logs */
        BidMachine.setLoggingEnabled(tgLogging.isOn);

        /* Enable test mode */
        BidMachine.setTestMode(tgTesting.isOn);

        /* Initialize BidMachine */
        BidMachine.initialize("1");

        // /* Set default Targeting params */
        // BidMachine.setTargetingParams(targetingParams);
        //
        // /* Set consent config. GDPR consent string( if applicable), indicating the compliance to the IAB standard Consent String Format
        //  * of the Transparency and Consent Framework technical specifications. */
        // BidMachine.setConsentConfig(true, "Something");
        //
        // /* Set Subject to GDPR. Flag indicating if GDPR regulations can be applied.
        //  * The General Data Protection Regulation (GDPR) is a regulation of the European Union.*/
        // BidMachine.setSubjectToGDPR(true);
        //
        // /* Set Coppa. Flag indicating if COPPA regulations can be applied.
        //  * The Children's Online Privacy Protection Act (COPPA) was established by the U.S. Federal Trade Commission. */
        // BidMachine.setCoppa(true);
    }

    public void LoadInterstitialAd()
    {
        if (interstitialAd == null)
        {
            interstitialAd = new InterstitialAd();
        }

        if (interstitialRequest == null)
        {
            interstitialRequest = new InterstitialRequestBuilder()
                .setAdContentType(InterstitialRequestBuilder.ContentType.All)
                //.setTargetingParams(targetingParams)
                //.setPriceFloorParams(priceFloorParams)
                .build();
        }

        interstitialAd.setListener(this);
        interstitialAd.load(interstitialRequest);
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd == null)
        {
            rewardedAd = new RewardedAd();
        }

        if (rewardedRequest == null)
        {
            rewardedRequest = new RewardedRequestBuilder()
                //.setTargetingParams(targetingParams)
                //.setPriceFloorParams(priceFloorParams)
                .build();
        }

        rewardedAd.setListener(this);
        rewardedAd.load(rewardedRequest);
    }

    public void LoadBanner()
    {
        bannerRequest = new BannerRequestBuilder()
            .setSize(BannerRequestBuilder.Size.Size_320_50)
            //.setTargetingParams(targetingParams)
            //.setPriceFloorParams(priceFloorParams)
            .build();

        banner = new Banner();
        bannerView = banner.GetBannerView();

        bannerView.setListener(this);
        bannerView.load(bannerRequest);
    }

    public void DestroyBanner()
    {
        bannerView.destroy();
        banner.hideBannerView();
    }

    public void DestroyRewardedVideo()
    {
        if (rewardedAd == null) return;
        rewardedAd.destroy();
        rewardedAd = null;
        rewardedRequest = null;
    }

    public void DestroyInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.destroy();
            interstitialAd = null;
            interstitialRequest = null;
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.canShow())
        {
            interstitialAd.show();
        }
        else
        {
            Debug.Log("canShow - InterstitialAd - false");
        }
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.canShow())
        {
            rewardedAd.show();
        }
        else
        {
            Debug.Log("canShow - RewardedAd - false");
        }
    }


    #region InterstitialAd Callbacks

    public void onAdLoaded(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoaded");
    }

    public void onAdLoadFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoadFailed");
    }

    public void onAdShown(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShown");
    }

    public void onAdImpression(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdImpression");
    }

    public void onAdClosed(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    public void onAdExpired(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdExpired");
    }

    public void onAdShowFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShowFailed");
    }

    public void onAdClosed(InterstitialAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClosed");
    }

    public void onAdClicked(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    #endregion

    #region RewardedAd Callbacks

    public void onAdLoaded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoaded");
    }

    public void onAdLoadFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdLoadFailed");
    }

    public void onAdShown(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShown");
    }

    public void onAdImpression(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdImpression");
    }

    public void onAdClicked(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClicked");
    }

    public void onAdExpired(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdExpired");
    }

    public void onAdShowFailed(RewardedAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdShowFailed");
    }

    public void onAdClosed(RewardedAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
        Debug.Log("BidMachineUnity: RewardedAd - isFinished " + finished);
    }

    public void onAdRewarded(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    public void onAdClosed(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: RewardedAd - onAdClosed");
    }

    #endregion

    #region BannerView Callbacks

    public void onAdLoaded(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoaded");
        banner.showBannerView(BidMachine.BANNER_TOP, BidMachine.BANNER_HORIZONTAL_CENTER, bannerView);
    }

    public void onAdLoadFailed(BannerView ad, BMError error)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdLoadFailed");
    }

    public void onAdShown(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdShown");
    }

    public void onAdImpression(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdImpression");
    }

    public void onAdClicked(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdClicked");
    }

    public void onAdExpired(BannerView ad)
    {
        Debug.Log("BidMachineUnity: BannerView - onAdExpired");
    }

    #endregion
}