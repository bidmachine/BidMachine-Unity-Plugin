# Step 1. Import SDK

1.1 Download BidMachine Unity Plugin that includes the newest Android and iOS BidMachine SDK with major improvements.

1.2 To import the BidMachine Unity plugin, double-click on the BidMachine-Unity-Plugin-1.0.8-15.06.2022.unitypackage , or go to Assets → Import Package → Custom Package . Keep all the files in the Importing Package window selected, and click Import .

# Step 2. Project configuration 

2.1 Android 

Requirements:
- Min Android SDK version - 15 (4.0.3 - 4.0.4, Ice Cream Sandwich).

2.1.1 External Dependency Manager (Play Services Resolver)
BidMachine Unity Plugin includes External Dependency Manager package.  You need to complete these following steps to resolve BidMachine's dependencies:

- After the import BidMachine Unity Plugin, in the Unity editor select File → Build Settings → Android.
- Add flag Custom Gradle Template for Unity 2017.4 - Unity 2019.2 versions or Custom Main Gradle Template for Unity 2019.3 or higher (Build Settings → Player Settings → Publishing settings).
- Enable the setting "Patch mainTemplate.gradle" (Assets → External Dependency Manager → Android Resolver → Settings).
- Enable the setting "Use Jetifier" (Assets → External Dependency Manager → Android Resolver → Settings).
- Then run Assets → External Dependency Manager → Android Resolver and press Resolve or Force Resolve.
- As a result, the modules, that are required for the BidMachine SDK support, will be imported to project's mainTemplate.gradle file.

2.1.2 Request runtime permissions in Android Marshmallow (API 6.0+)

SDK can automatically track user device location in order to serve better ads. To make it work for Android 6.0, you should request "android.permission.ACCESS_COARSE_LOCATION" and "android.permission.ACCESS_FINE_LOCATION":

To check permission use a method:
```c# 
BidMachine.checkAndroidPermissions(Permission.CoarseLocation));
BidMachine.checkAndroidPermissions(Permission.FineLocation));
```

To request permissions you should use this method:<br> 
```c# 
 BidMachine.requestAndroidPermissions();
```

2.1.3 Multidex support

- If you are using Unity 2019.2 and versions below you need to add Multidex support to your project. Follow this guide to add Multidex.
- If you are using Unity 2019.3 or higher go to Player Settings → Publishing Settings → Other Settings and change Minimum API Level to 21 or higher.


2.2 iOS 

Requirements:
- iOS 10.0+
- Xcode: 13.3+

2.2.1 External Dependency Manager (Play Services Resolver)
BidMachine Unity Plugin includes External Dependency Manager package. You need to complete these following steps to resolve BidMachine's dependencies:

- After the import BidMachine Unity Plugin, in the Unity editor select File → Build Settings → iOS.
- During build a project the modules, that are required for the BidMachine SDK support, will be imported to your project. You can edit them or add other modules in the Assets → BidMachine → Editor → BidMachineDependencies.xml file.
- Open Assets → External Dependency Manager → IOS Resolver → Settings and make sure that options "Link frameworks statically", "Enable Swift Framework Support Workaround" are unchecked.

# Step 3. Integration

Initialize SDK, and set your SellerId. (To get your SELLER_ID, visit our website or contact the support.)
To initialize SDK and set your SellerId you should use this method:
```c# 
BidMachine.initialize("YOUR_SELLER_ID");
```
To enable logs use this method:
```c# 
BidMachine.setLoggingEnabled(Boolean);
```
To enable test mode:
```c# 
BidMachine.setTestMode(Boolean);
```
To Set your endpoint if required:
```c# 
BidMachine.setEndpoint(Boolean);
```
To check if BidMachine SDK was initialized:
```c# 
BidMachine.isInitialized();
```

# Request parameters

Global Parameters

Set default Targeting params:
```c# 
BidMachine.setTargetingParams(TargetingParams);
```

Set consent config:
```c# 
BidMachine.setConsentConfig(bool, string);
```

Set subject to GDPR:
```c# 
BidMachine.setSubjectToGDPR(bool);
```

Set COPPA:
```c# 
BidMachine.setCoppa(bool);
```

Set CCPA U.S. Privacy String:
```c# 
BidMachine.setUSPrivacyString(string);
```

Sets publisher information:
```c# 
BidMachine.setPublisher(Publisher);
```

# User Restriction Parameters 

Param | Type | Description
------------ | ------------- | -------------
GDPR Consent String | String | GDPR consent string (if applicable), indicating the compliance with the IAB standard [Consent String Format](https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/Consent%20string%20and%20vendor%20list%20formats%20v1.1%20Final.md) of the [Transparency and ConsentFramework](https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework) technical specifications.
Subject to GDPR | Boolean | Flag indicating if GDPR regulations apply [The General Data Protection Regulation (GDPR)](https://wikipedia.org/wiki/General_Data_Protection_Regulation) is a regulation of the European Union.
Coppa | Boolean | Flag indicating if COPPA regulations apply. [The Children's Online Privacy Protection Act (COPPA)](https://wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act) was established by the U.S. Federal Trade Commission.
US Privacy String | String | CCPA string if applicable, compliant with the IAB standard [CCPA String Format](https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md)


# TARGETING PARAMS

Param | Type | Description
------------ | ------------- | -------------
User Id | String | Vendor-specific ID for the user.
Gender | Enum |	Gender, one of the following: Female, Male, Omitted.
Year of Birth | Integer | Year of birth as a 4-digit integer (e.g. - 1990).
Keywords | String[] | List of keywords, interests, or intents (separated by comma if you use .xml).
Device Location| string, double, double | Location of the device. It may not be the location sent to the server, as it is compared to the current device location at the time, when it was received.
Country | String | Country of the user's domicile (i.e. not necessarily their current location).
City | String | City of the user's domicile (i.e. not necessarily their current location).
Zip | String | Zip of the user's domicile (i.e. not necessarily their current location).
Store Url |	String | App store URL for an installed app; for IQG 2.1 compliance.
Store Category | String | Sets App store category definitions (e.g. - "games").
Store Sub Category | String[] | Sets App Store Subcategory definitions. The array is always capped at 3 strings.
Framework Name | String | Sets app framework definitions.
Paid | Boolean | Determines, if the app version is free or paid version of the app.
External User Ids |	ExternalUserId[] | Set external user ID list.
Blocked Advertiser IAB Category | String[] | Block list of content categories by IDs.
Blocked Advertiser Domain |	String[] | Block list of advertisers by their domains (e.g., “example.com”).
Blocked Application | String[] | Block list of apps where ads are disallowed. These should be bundle or package names (e.g., “com.foo.mygame”) and should NOT be app store IDs (e.g., not iTunes store IDs).

Code Example:

```c#
 BidMachine.setTargetingParams(
             new TargetingParams()
                 .setUserId("UserId")
                 .setStoreId("StoreId")
                 .setGender(TargetingParams.Gender.Female)
                 .setBirthdayYear(1991)
                 .setKeyWords(new[] { "key_1, key_2" })
                 .setCountry("Country")
                 .setCity("City")
                 .setZip("zip")
                 .setStoreUrl("StoreUrl")
                 .setStoreCategory("StoreCategory")
                 .setStoreSubCategories(new[] { "sub_category_1", "sub_category_2" })
                 .setFramework("unity")
                 .setPaid(true)
                 .setDeviceLocation("", 22.0d, 22.0d)
                 .addBlockedApplication("BlockedApplication")
                 .addBlockedAdvertiserIABCategory("BlockedAdvertiserIABCategory")
                 .addBlockedAdvertiserDomain("BlockedAdvertiserDomain")
                 .setExternalUserIds(new[]
                     {
                         new ExternalUserId("sourceId_1", "1"),
                         new ExternalUserId("sourceId_2", "2")
                     }
                 ));
```


# PRICE FLOOR PARAMETRS

Param | Type | Description
------------ | ------------- | -------------
Id | String | Unique floor identifier.
Price |	double | Floor price

Code example: 
```c#
PriceFloorParams priceFloorParams = new PriceFloorParams();
priceFloorParams.addPriceFloor("123", 1.2d);
```

# Session Ad Parameters

Param | Type | Description
------------ | ------------- | -------------
Session Duration | Integer | The total duration of time a user has spent so far in a specific app session expressed in seconds.
Impression Count | Integer | The count of impressions for a specific placement type in a given app session.
Click Rate | Float | The percentage of clicks/impressions per user per placement type over a given number of impressions.
Is User Clicked On Last Ad | Boolean | A boolean value indicating if the user clicked on the last impression in a given session per placement type.
Completion Rate | Float | The percentage of successful completions/impressions per user per placement type for a given number of impressions. This only applies to Rewarded and Video placement types.

Code example: 
```c#
SessionAdParams sessionAdParams = new SessionAdParams()
            .setSessionDuration(123)
            .setImpressionCount(123)
            .setClickRate(1.2f)
            .setIsUserClickedOnLastAd(true)
            .setCompletionRate(1.3f)
            .setLastBundle("LastBundle")
            .setLastAdomain("LastAdomain");
```

# BANNER / MREC

BannerSize 

Type | Size | Description
------------ | ------------- | -------------
Size_320x50 | width: 320 height: 50 | Regular banner size.
Size_728x90 | width: 728 height: 90 | Banner size for tablets.
Size_300x250 | width: 300 height: 250 | MREC banner size. 


To set Banner ads events listener:
```c#
bannerView.setListener(this);
```

To load banners:
```c#
bannerView.load(bannerRequest);
```

To show banner or mrec:
```c#
bannerView.showBannerView(
            BidMachine.BANNER_VERTICAL_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerView, bannerRequest.getSize());
```

To destroy banner or mrec:
```c#
bannerView.destroy();
```

To hide banner or mrec:
```c#
bannerView.hideBannerView();
```

Code example:
 ```c#

public class BidMachineController : MonoBehaviour, IBannerRequestListener, IBannerListener {

     BannerRequest bannerRequest = new BannerRequestBuilder()
        .setSize(...) // Set BannerSize. Required
        .setTargetingParams(...) // Set TargatingParams instance
        .setPriceFloorParams(...) // Set price floor parameters
        .setSessionAdParams(...) // Set SessionAdParams instance
        .setPlacementId("placement") // Set placement id
        .setLoadingTimeOut(123) // Set loading timeout in milliseconds
        .setListener(this) // Set banner request listener
        .build();

    BannerView bannerView = new BannerView();
    bannerView.setListener(this); // Set banner listener       
    bannerView.load(bannerRequest); // Load banner ad

    #region BannerRequestListener
    public void onBannerRequestSuccess(BannerRequest request, string auctionResult) { 
        Debug.Log("BidMachineUnity: onBannerRequestSuccess"); }
    public void onBannerRequestFailed(BannerRequest request, BMError error) { 
        Debug.Log("BannerRequestListener - onBannerRequestFailed" + $" - {error.code} - {error.message}"); }
    public void onBannerRequestExpired(BannerRequest request) { 
        Debug.Log("BannerRequestListener - onBannerRequestExpired"); }
    #endregion

    #region BannerListener
    public void onBannerAdLoaded(BannerView ad) { 
        Debug.Log("BidMachineUnity: BannerView - onAdLoaded"); }
    public void onBannerAdLoadFailed(BannerView ad, BMError error) { 
        Debug.Log("BidMachineUnity: BannerView - onAdLoadFailed"); }
    public void onBannerAdShown(BannerView ad) {  
        Debug.Log("BidMachineUnity: BannerView - onAdShown");}
    public void onBannerAdImpression(BannerView ad) {
         Debug.Log("BidMachineUnity: BannerView - onAdImpression"); }
    public void onBannerAdClicked(BannerView ad) { 
        Debug.Log("BidMachineUnity: BannerView - onAdClicked"); }
    public void onBannerAdExpired(BannerView ad) { 
        Debug.Log("BidMachineUnity: BannerView - onAdExpired"); }
    #endregion

    }
 ```

# INTERSTITIAL AD

AdContentType 

By default AdContentType is AdContentType.All

Type  | Description
------------ | -------------
AdContentType.All | Flag to request both Video and Static ad content types.
AdContentType.Static | Flag to request Static ad content type only.
AdContentType.Video | Flag to request Video ad content type only.

To set Interstitial Ad listeners:
```c#
interstitialAd.setListener(this);
```

To check if Interstitial ad can show:
```c#
interstitialAd.canShow();
```

To load interstitial:
```c#
interstitialAd.load(interstitialRequest);
```

To show interstitial:
```c#
interstitialAd.show();
```

To destroy interstitial:
```c#
interstitialAd.destroy();
```

Example code: 
```c#
public class BidMachineController : MonoBehaviour, IInterstitialAdListener, IInterstitialRequestListener {

     InterstitialRequest interstitialRequest = new InterstitialRequestBuilder()
        .setAdContentType(AdContentType.All)
        .setTargetingParams(...) // Set TargatingParams instance
        .setPriceFloorParams(...) // Set price floor parameters
        .setSessionAdParams(...) // Set SessionAdParams instance
        .setPlacementId("placement") // Set placement id
        .setLoadingTimeOut(123) // Set loading timeout in milliseconds
        .setListener(this) // Set interstitial request listener
        .build();

    InterstitialAd interstitialAd = new InterstitialAd();
    interstitialAd.setListener(this); // Set interstitial listener       
    interstitialAd.load(interstitialRequest); // Load interstitial ad
    
    #region InterstitialRequestListener
    public void onInterstitialRequestSuccess(InterstitialRequest request, string auctionResult) {
        Debug.Log($"onInterstitialRequestSuccess");
    }

    public void onInterstitialRequestFailed(InterstitialRequest request, BMError error){
        Debug.Log($"onInterstitialRequestFailed" +
                  $" {error.code} - {error.message}");
    }
    public void onInterstitialRequestExpired(InterstitialRequest request) {
        Debug.Log($"onInterstitialRequestExpired");
    }
    #endregion


    #region InterstitialAd Callbacks

    public void onInterstitialAdLoaded(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoaded");
    }

    public void onInterstitialAdLoadFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdLoadFailed");
    }

    public void onInterstitialAdShown(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShown");
    }

    public void onInterstitialAdImpression(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdImpression");
    }

    public void onInterstitialAdClosed(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    public void onInterstitialAdExpired(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdExpired");
    }

    public void onInterstitialAdShowFailed(InterstitialAd ad, BMError error)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdShowFailed");
    }

    public void onInterstitialAdClosed(InterstitialAd ad, bool finished)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClosed");
    }

    public void onInterstitialAdClicked(InterstitialAd ad)
    {
        Debug.Log("BidMachineUnity: InterstitialAd - onAdClicked");
    }

    #endregion


    }
```
# REWARDED AD

To set Rewarded Ad listeners:
```c#
rewardedAd.setListener(this);
```

To check if Rewarded ad can show:
```c#
rewardedAd.canShow();
```

To load rewarded ad:
```c#
rewardedAd.load(interstitialRequest);
```

To show rewarded ad:
```c#
rewardedAd.show();
```

To destroy rewarded ad:
```c#
rewardedAd.destroy();
```

Example code: 
```c#
public class BidMachineController : MonoBehaviour, IRewardedAdListener, IRewardedRequestListener {

     RewardedRequest rewardedRequest = new InterstitialRequestBuilder()
        .setTargetingParams(...) // Set TargatingParams instance
        .setPriceFloorParams(...) // Set price floor parameters
        .setSessionAdParams(...) // Set SessionAdParams instance
        .setPlacementId("placement") // Set placement id
        .setLoadingTimeOut(123) // Set loading timeout in milliseconds
        .setListener(this) // Set rewarded request listener
        .build();

    RewardedAd rewardedAd = new RewardedAd();
    rewardedAd.setListener(this); // Set rewarded listener       
    rewardedAd.load(rewardedRequest); // Load rewarded ad
    
    #region RewardedAd Callbacks

    public void onRewardedAdLoaded(RewardedAd ad) {
        Debug.Log("BidMachineUnity: onRewardedAdLoaded");
    }

    public void onRewardedAdLoadFailed(RewardedAd ad, BMError error){
        Debug.Log("BidMachineUnity: onRewardedAdLoadFailed");
    }

    public void onRewardedAdShown(RewardedAd ad){
        Debug.Log("BidMachineUnity: onRewardedAdShown");
    }

    public void onRewardedAdImpression(RewardedAd ad) {
        Debug.Log("BidMachineUnity: onRewardedAdImpression");
    }

    public void onRewardedAdClicked(RewardedAd ad)  {
        Debug.Log("BidMachineUnity: onRewardedAdClicked");
    }

    public void onRewardedAdExpired(RewardedAd ad) {
        Debug.Log("BidMachineUnity: onRewardedAdExpired");
    }

    public void onRewardedAdShowFailed(RewardedAd ad, BMError error) {
        Debug.Log("BidMachineUnity: onRewardedAdShowFailed");
    }

    public void onRewardedAdClosed(RewardedAd ad, bool finished)  {
        Debug.Log("$BidMachineUnity: onRewardedAdClosed {finished}");
    }

    public void onRewardedAdRewarded(RewardedAd ad){
        Debug.Log("BidMachineUnity: onRewardedAdRewarded");
    }

    public void onRewardedAdClosed(RewardedAd ad)
    {
        Debug.Log("BidMachineUnity: onRewardedAdClosed");
    }

    #endregion

    #region RewardedRequestListener

    public void onRewardedRequestSuccess(RewardedRequest request, string auctionResult)
    {
        Debug.Log($"onRewardedRequestSuccess");
    }

    public void onRewardedRequestFailed(RewardedRequest request, BMError error)
    {
        Debug.Log($"onRewardedRequestFailed");
    }

    public void onRewardedRequestExpired(RewardedRequest request)
    {
         Debug.Log($"onRewardedRequestExpired");
    }

    #endregion


    }
```

# NATIVE AD

NativeAdParams 

Type  | Description
------------ | -------------
MediaAssetType.Icon | Only icon assets will be downloaded and displayed
MediaAssetType.Image | Only image assets will be downloaded and displayed

- Add RawImage to scene Canvas (should be active in hierarchy)
- Add these following UI elements to your RawImage as children:
  - Text nativeAdViewTitle
  - Text nativeAdViewDescription
  - Text nativeAdViewSponsored
  - RawImage nativeAdViewIcon
  - Text nativeAdViewRatting
  - RawImage nativeAdViewImage
  - Button callToAction
- Add NativeAdView.cs to your RawImage via Add component Button.
- Pass the following your native ad components to NativeAdView script: 
  - Text nativeAdViewTitle
  - Text nativeAdViewDescription
  - Text nativeAdViewSponsored
  - RawImage nativeAdViewIcon
  - Text nativeAdViewRatting
  - RawImage nativeAdViewImage
  - Button callToAction

To set Native Ad listeners:
```c#
nativeAd.setListener(this);
```

To load Native Ad:
```c#
nativeAd.load(nativeRequest);
```

To show Native Ad:
```c#
nativeAdView.setNativeAd(ad);
```

To destroy Native Ad:
```c#
nativeAd.destroy();
```

Example code: 
```c#
public class BidMachineController : MonoBehaviour, INativeAdListener, INativeRequestListener {

    [SerializeField] public NativeAdView nativeAdView;

    NativeAdParams nativeAdParams = new NativeAdParams();
        nativeAdParams.setMediaAssetTypes(NativeAdParams.MediaAssetType.Icon, NativeAdParams.MediaAssetType.Image);

     NativeRequest nativeRequest = new NativeRequestBuilder()
        .setMediaAssetTypes(nativeAdParams)
        .setTargetingParams(...) // Set TargatingParams instance
        .setPriceFloorParams(...) // Set price floor parameters
        .setSessionAdParams(...) // Set SessionAdParams instance
        .setPlacementId("placement") // Set placement id
        .setLoadingTimeOut(123) // Set loading timeout in milliseconds
        .setListener(this) // Set native request listener
        .build();

    NativeAd nativeAd = new NativeAd();
    nativeAd.setListener(this); // Set native listener       
    nativeAd.load(nativeRequest); // Load native ad
    
    #region NativeRequestListener

    public void onNativeRequestSuccess(NativeRequest request, string auctionResult)
    {
         Debug.Log($"onNativeRequestSuccess");
    }

    public void onNativeRequestFailed(NativeRequest request, BMError error)
    {
        Debug.Log($"onNativeRequestFailed");
    }

    public void onNativeRequestExpired(NativeRequest request)
    {
        Debug.Log($"onNativeRequestExpired");
    }

    #endregion

        #region NativeAdListener

    public void onNativeAdLoaded(NativeAd ad)
    {
        if (nativeAdView)
        {
            nativeAdView.setNativeAd(ad);
        }
    }

    public void onNativeAdLoadFailed(NativeAd ad, BMError error)
    {
        Debug.Log($"onNativeAdLoadFailed - {error.message} - {error.code} ");
    }

    public void onNativeAdShown(NativeAd ad)
    {
        Debug.Log("onNativeAdShown");
    }

    public void onNativeAdImpression(NativeAd ad)
    {
        Debug.Log("onNativeAdImpression");
    }

    public void onNativeAdClicked(NativeAd ad)
    {
        Debug.Log("onNativeAdClicked");
    }

    public void onNativeAdExpired(NativeAd ad)
    {
        Debug.Log("onNativeAdExpired");
    }

    #endregion

    }
```
