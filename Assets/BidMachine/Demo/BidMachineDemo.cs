using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;


public class BidMachineDemo : MonoBehaviour, IInterstitialAdListener, IRewardedAdListener, IBannerListener
{

    int btnWidth, btnHeight, heightScale, widthScale, toggleSize;
    GUIStyle btnStyle;
    GUIStyle labelStyle;
    bool testingToggle;
    bool loggingToggle;
    Vector2 scrollPosition = new Vector2();

    TargetingParams targetingParams;
    PriceFloorParams priceFloorParams;

    InterstitialRequest interstitialRequest;
    InterstitialRequestBuilder interstitialRequestBuilder;
    InterstitialAd interstitialAd;

    RewardedRequest rewardedRequest;
    RewardedRequestBuilder rewardedRequestBuilder;
    RewardedAd rewardedAd;

    BannerRequest bannerRequest;
    BannerRequestBuilder BannerRequestBuilder;
    BannerView bannerView;
    Banner banner;

    void OnGUI()
    {
		InitStyles();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
		testingToggle = GUI.Toggle(new Rect(widthScale, heightScale + 1 * heightScale, (float)(toggleSize * 3), toggleSize), testingToggle, new GUIContent("Testing"));
		loggingToggle = GUI.Toggle(new Rect((float)(Screen.width / 1.57), heightScale + 1 * heightScale, (float)(toggleSize * 3), toggleSize), loggingToggle, new GUIContent("Logging"));

		if (GUI.Button(new Rect(widthScale, heightScale + 2 * heightScale, btnWidth, btnHeight), "INITIALIZE", btnStyle))
        {
            bidMachineInitialize();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 3 * heightScale, btnWidth, btnHeight), "LOAD INTERSTITIAL", btnStyle))
        {
            loadInterstitialAd();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 4 * heightScale, btnWidth, btnHeight), "SHOW INTERSTITIAL", btnStyle))
        {
            showInterstitialAd();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 5 * heightScale, btnWidth, btnHeight), "DESTROY INTERSTITIAL", btnStyle))
        {
            destroyInterstitial();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 6 * heightScale, btnWidth, btnHeight), "LOAD REWARDED", btnStyle))
        {
            loadRewardedAd();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 7 * heightScale, btnWidth, btnHeight), "SHOW REWARDED", btnStyle))
        {
            showRewardedAd();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 8 * heightScale, btnWidth, btnHeight), "DESTROY REWARDED", btnStyle))
        {
            destroyRewardedVideo();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 9 * heightScale, btnWidth, btnHeight), "LOAD BANNER", btnStyle))
        {
            loadBanner();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 10 * heightScale, btnWidth, btnHeight), "DESTROY BANNER", btnStyle))
        {
            destroyBanner();
        }

        if (GUI.Button(new Rect(widthScale, heightScale + 11 * heightScale, btnWidth, btnHeight), "CHECK PERMISSIONS", btnStyle))
        {
            Debug.Log("Permission: " + BidMachine.checkAndroidPermissions(Permission.CoarseLocation));
        }

        GUILayout.EndScrollView();
	}

    private void bidMachineInitialize()
    {
        targetingParams = new TargetingParams();
        priceFloorParams = new PriceFloorParams();

        /* Vendor-specific ID for the user. */
        targetingParams.setUserId("1");

        /* Gender, one of following: Female, Male, Omitted. */
        targetingParams.setGender(TargetingParams.Gender.Male);

        /* Year of birth as a 4-digit integer (e.g - 1990). */
        targetingParams.setBirthdayYear(1990);

        /* List of keywords, interests, or intents (separated by comma if you use .xml). */
        targetingParams.setKeyWords(new string[] { "games", "sport" });

        /* Location of the device. It might not be the location that was sent to the server,
         * as it is compared with the current device location at the time, when it was received. */
        targetingParams.setDeviceLocation(12.8888, 12.222);

        /* Country of the user's home base (i.e., not necessarily their current location). */
        targetingParams.setCountry("USA");

        /*City of the user's home base (i.e., not necessarily their current location).*/
        targetingParams.setCity("New-York");

        /* Zip of the user's home base (i.e., not necessarily their current location). */
        targetingParams.setZip("10001");

        /* App store URL for an installed app; for IQG 2.1 compliance. */
        targetingParams.setStoreUrl("https://test.com");

        /* Determines, if it is a free or paid version of the app. */
        targetingParams.setPaid(true);

        /* Block list of content categories using IDs. */
        targetingParams.setBlockedAdvertiserIABCategories("IAB26,AB25");

        /* Block list of advertisers by their domains (e.g., “example.com”). */
        targetingParams.setBlockedAdvertiserDomain("example.com");

        /* Block list of apps where ads are disallowed. These should be bundle or package names
         * (e.g., “com.foo.mygame”) and should NOT be app store IDs (e.g., not iTunes store IDs). */
        targetingParams.setBlockedApplication("com.foo.mygame");

        priceFloorParams = new PriceFloorParams();
        priceFloorParams.setPriceFloor("sample", 2.15);

        /* Enable logs */
        BidMachine.setLoggingEnabled(loggingToggle);

        /* Enable test mode */
        BidMachine.setTestMode(testingToggle);

        /* Initialize BidMachine */
        BidMachine.initialize("1");

        /* Set default Targeting params */
        BidMachine.setTargetingParams(targetingParams);

        /* Set consent config. GDPR consent string( if applicable), indicating the compliance to the IAB standard Consent String Format
         * of the Transparency and Consent Framework technical specifications. */
        BidMachine.setConsentConfig(true, "Something");

        /* Set Subject to GDPR. Flag indicating if GDPR regulations can be applied.
         * The General Data Protection Regulation (GDPR) is a regulation of the European Union.*/
        BidMachine.setSubjectToGDPR(true);

        /* Set Coppa. Flag indicating if COPPA regulations can be applied.
         * The Children's Online Privacy Protection Act (COPPA) was established by the U.S. Federal Trade Commission. */
        BidMachine.setCoppa(true);
    }

    private void loadInterstitialAd()
    {
        if(interstitialAd == null)
        {
            interstitialAd = new InterstitialAd();
        }

        if (interstitialRequest == null) {
            interstitialRequest = new InterstitialRequestBuilder()
            .setAdContentType(InterstitialRequestBuilder.ContentType.All)
            //.setTargetingParams(targetingParams)
            //.setPriceFloorParams(priceFloorParams)
            .build();
        }

        interstitialAd.setListener(this);
        interstitialAd.load(interstitialRequest);
    }

    private void loadRewardedAd()
    {
        if (rewardedAd == null)
        {
            rewardedAd = new RewardedAd();
        }

        if (rewardedRequest == null) {
            rewardedRequest = new RewardedRequestBuilder()
            //.setTargetingParams(targetingParams)
            //.setPriceFloorParams(priceFloorParams)
            .build();
        }
        
        rewardedAd.setListener(this);
        rewardedAd.load(rewardedRequest);
    }

    private void loadBanner()
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

    private void destroyBanner()
    {
        bannerView.destroy();
        banner.hideBannerView();
    }

    private void destroyRewardedVideo()
    {
        if (rewardedAd != null)
        {
            rewardedAd.destroy();
            rewardedAd = null;
            rewardedRequest = null;
        }
    }

    private void destroyInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.destroy();
            interstitialAd = null;
            interstitialRequest = null;
        }
    }

    private void showInterstitialAd()
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

    private void showRewardedAd()
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

    private void InitStyles()
	{
		if (btnStyle == null)
		{
			btnWidth = Screen.width - Screen.width / 5;
			btnHeight = Screen.height / 18;
			heightScale = Screen.height / 15;
			widthScale = (int)(Screen.width / 9.85);
			toggleSize = Screen.height / 25;

			btnStyle = new GUIStyle(GUI.skin.button);
            btnStyle.fontSize = btnHeight / 2;
            btnStyle.normal.textColor = Color.black;
            btnStyle.hover.textColor = Color.black;
            btnStyle.active.textColor = Color.black;
            btnStyle.focused.textColor = Color.black;

            btnStyle.active.background = MakeTexure(btnWidth, btnHeight, Color.grey);
            btnStyle.focused.background = MakeTexure(btnWidth, btnHeight, Color.grey);
            btnStyle.normal.background = MakeTexure(btnWidth, btnHeight, Color.white);
            btnStyle.hover.background = MakeTexure(btnWidth, btnHeight, Color.white);

			GUI.skin.toggle = btnStyle;

			labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontSize = btnStyle.fontSize;
			GUI.skin.label = labelStyle;
		}
	}

	private Texture2D MakeTexure(int width, int height, Color color)
	{
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; ++i)
		{
			pix[i] = color;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}

    /* InterstitialAd Callbacks */
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

    /* RewardedAd Callbacks */
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

    /* BannerView Callbacks */
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
}