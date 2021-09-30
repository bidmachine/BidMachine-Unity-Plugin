# BM-unity-plugin
https://mega.nz/#!d2oVFQ5S!-r_aSPvPt5v77mLVhr9R6xRlHVwjgTc8dd4op4DbIkU
# BM-unity-plugin-resolver
https://mega.nz/#!MyoR0Sqb!Bu8Ec20jtRBaREqGme67Av25_wSEB-0rVVj4UjzRV2U



# Step 1. Import SDK

1.1 Download BidMachine Unity Plugin that includes the newest Android and iOS Appodeal SDK with major improvements.

1.2 To import the BidMachine Unity plugin, double-click on the Appodeal-Unity-Plugin-2.14.4-26.08.2021.unitypackage , or go to   Assets → Import Package → Custom Package . Keep all the files in the Importing Package window selected, and click Import .



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

<p>To check permission use a method:<br>
<code><strong>BidMachine.checkAndroidPermissions(Permission.CoarseLocation));<br>
BidMachine.checkAndroidPermissions(Permission.FineLocation));</strong></code></p>

<p>To request permissions you should use this method:<br> 
<code><strong>BidMachine.requestAndroidPermissions();</strong></code></p>

2.1.3 Multidex support

- If you are using Unity 2019.2 and versions below you need to add Multidex support to your project. Follow this guide to add Multidex.
- If you are using Unity 2019.3 or higher go to Player Settings → Publishing Settings → Other Settings and change Minimum API Level to 21 or higher.


2.2 iOS 

Requirements:
- iOS 10.0+
- Xcode: 12.0+

2.2.1 External Dependency Manager (Play Services Resolver)
BidMachine Unity Plugin includes External Dependency Manager package. You need to complete these following steps to resolve BidMachine's dependencies:

- After the import Appodeal Unity Plugin, in the Unity editor select File → Build Settings → iOS.
- During build a project the modules, that are required for the Appodeal SDK support, will be imported to your project. You can edit them or add other modules in the Assets → Appodeal → Editor → AppodealDependencies.xml file.

# Step 3. Integration

Initialize SDK, and set your SellerId. (To get your SELLER_ID, visit our website or contact the support.)

<p>To initialize SDK and set your SellerId you should use this method:<br>
<code><strong>BidMachine.initialize("YOUR_SELLER_ID");</strong></code></p>

<p>To enable logs use this method:<br>
<code><strong>BidMachine.setLoggingEnabled(Boolean);</strong></code></p>

<p>To enable test mode:<br>
<code><strong>BidMachine.setTestMode(Boolean);</strong></code></p>

<p>To Set your endpoint if required:<br>
<code><strong>BidMachine.setEndpoint(Boolean);</strong></code></p>

<p>To check if BidMachine SDK was initialized:<br>
<code><strong>BidMachine.isInitialized();</strong></code></p>


# Request parameters

Global Parameters

<p>Set default Targeting params:<br>
<code><strong>BidMachine.setTargetingParams(...):</strong></code></p>

<p>Set consent config:<br>
<code><strong>BidMachine.setConsentConfig(...):</strong></code></p>

<p>Set subject to GDPR:<br>
<code><strong>BidMachine.setSubjectToGDPR(...):</strong></code></p>

<p>Set COPPA:<br>
<code><strong>BidMachine.setCoppa(...):</strong></code></p>

<p>Set CCPA U.S. Privacy String:<br>
<code><strong>BidMachine.setUSPrivacyString(...):</strong></code></p>

<p>Sets publisher information:<br>
<code><strong>BidMachine.setPublisher(...):</strong></code></p>

# User Restriction Parameters 

Param | Type | Description
------------ | ------------- | -------------
GDPR Consent String | String | GDPR consent string (if applicable), indicating the compliance with the IAB standard [Consent String Format](https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/Consent%20string%20and%20vendor%20list%20formats%20v1.1%20Final.md) of the [Transparency and ConsentFramework](https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework) technical specifications.
Subject to GDPR | Boolean | 
Coppa | Boolean | 
US Privacy String | String | 


# TARGETING PARAMS
<p><code><strong>TargetingParams targetingParams = new TargetingParams();</strong></code><br>
<code><strong>targetingParams.setUserId("USER_ID");</strong></code><br>
<code><strong>targetingParams.setGender(TargetingParams.Gender.Male);</strong></code><br>
<code><strong>targetingParams.setBirthdayYear(1990);</strong></code><br>
<code><strong>targetingParams.setKeyWords(new string[] { "key_word_example", "key_word_example" });</strong></code><br>
<code><strong>targetingParams.setDeviceLocation(40.730610, 73.935242);</strong></code><br>
<code><strong>targetingParams.setCountry("USA");</strong></code><br>
<code><strong>targetingParams.setCity("New-York");</strong></code><br>
<code><strong>targetingParams.setZip("10001");</strong></code><br>
<code><strong>targetingParams.setStoreUrl("https://test.com");</strong></code><br>
<code><strong>targetingParams.setPaid(true);</strong></code><br>
<code><strong>targetingParams.setBlockedAdvertiserIABCategories("IAB26,AB25");</strong></code><br>
<code><strong>targetingParams.setBlockedAdvertiserDomain("example.com");</strong></code><br>
<code><strong>targetingParams.setBlockedApplication("com.test.mygame");</strong></code></p>

# USER RESTRICTION PARAMETRS
<p>To set consent config use a method:<br>
<code><strong>BidMachine.setConsentConfig(Boolean, String);</strong></code></p>

<p>To set subject to GDPR:<br>
<code><strong>BidMachine.setSubjectToGDPR(Boolean);</strong></code></p>
 
<p>To set coppa:<br>
<code><strong>BidMachine.setCoppa(Boolean);</strong></code></p>

# PRICE FLOOR PARAMETRS
<p><code><strong>PriceFloorParams priceFloorParams = new PriceFloorParams();</strong></code><br>
<code><strong>priceFloorParams.setPriceFloor("sample", 2.15);</strong></code></p>

# BANNER / MREC

<p>BannerRequestBuilder.Size.Size_320_50 - Regular banner size.<br>
 BannerRequestBuilder.Size.Size_300_250 - MREC banner size.<br>
 BannerRequestBuilder.Size.Size_728_90 - Banner size for tablets.</p>

<p><code><strong>BannerRequest bannerRequest = new BannerRequestBuilder()</strong></code><br>
<code><strong>            .setSize(BannerRequestBuilder.Size.Size_320_50)</strong></code><br>
<code><strong>            .setTargetingParams(TargetingParams targetingParams)</strong></code><br>
<code><strong>            .setPriceFloorParams(PriceFloorParams priceFloorParams)</strong></code><br>
<code><strong>            .build();</strong></code></p>

<p><code><strong>Banner banner = new Banner();</strong></code><br>
<code><strong>BannerView bannerView = banner.GetBannerView();</strong></code></p>

<p>To set Banner Ads events listener:<br>
<code><strong>bannerView.setListener(this);</strong></code></p>

<p>To load banners:<br>
<code><strong>bannerView.load(bannerRequest);</strong></code></p>

<p>To show banner or mrec:<br>
<code><strong>banner.showBannerView(BidMachine.BANNER_TOP, BidMachine.BANNER_HORIZONTAL_CENTER, bannerView);</strong></code></p>

<p>To destroy banner or mrec:<br>
<code><strong>bannerView.destroy();</strong></code><br>
<code><strong>banner.hideBannerView();</strong></code></p>

# INTERSTITIAL AD

<p>InterstitialRequestBuilder.ContentType.All - Flag to request both Video and Static ad content types.<br>
InterstitialRequestBuilder.ContentType.Video - Flag to request Video ad content type only.<br>
InterstitialRequestBuilder.ContentType.Static -	Flag to request Static ad content type only.</p>

<code><strong>InterstitialAd = new InterstitialAd();</strong></code>

<p><code><strong>InterstitialRequest interstitialRequest = new InterstitialRequestBuilder()</strong></code><br>
<code><strong>   .setAdContentType(InterstitialRequestBuilder.ContentType.All)</strong></code><br>
<code><strong>   .setTargetingParams(TargetingParams targetingParams)</strong></code><br>
<code><strong>   .setPriceFloorParams(PriceFloorParams priceFloorParams)</strong></code><br>
<code><strong>   .build();</strong></code></p>

<p>To set Interstitial Ad listeners:<br>
<code><strong>interstitialAd.setListener(this);</strong></code></p>

<p>To load interstitial:<br>
<code><strong>interstitialAd.load(interstitialRequest);</strong></code></p>

<p>To show interstitial:<br>
<code><strong>interstitialAd.show();</strong></code></p>

<p>To destroy interstitial:<br>
<code><strong>interstitialAd.destroy();</strong></code></p>


# REWARDED AD

<code><strong>RewardedAd rewardedAd = new RewardedAd();</strong></code>
        
<p><code><strong>RewardedRequest rewardedRequest = new RewardedRequestBuilder()</strong></code><br>
<code><strong>.setTargetingParams(TargetingParams targetingParams)</strong></code><br>
<code><strong>.setPriceFloorParams(PriceFloorParams priceFloorParams)</strong></code><br>
<code><strong>.build();</strong></code></p>
        
<p>To set rewarded listeners:<br>        
<code><strong>rewardedAd.setListener(this);</strong></code></p>

<p>To load rewarded ad:<br>
<code><strong>rewardedAd.load(rewardedRequest);</strong></code></p>

<p>To destroy rewarded ad:<br> 
<code><strong>rewardedAd.destroy();</strong></code></p>
