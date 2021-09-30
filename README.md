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
<code><strong>BidMachine.setTargetingParams(TargetingParams):</strong></code></p>

<p>Set consent config:<br>
<code><strong>BidMachine.setConsentConfig(bool, string):</strong></code></p>

<p>Set subject to GDPR:<br>
<code><strong>BidMachine.setSubjectToGDPR(bool):</strong></code></p>

<p>Set COPPA:<br>
<code><strong>BidMachine.setCoppa(bool):</strong></code></p>

<p>Set CCPA U.S. Privacy String:<br>
<code><strong>BidMachine.setUSPrivacyString(string):</strong></code></p>

<p>Sets publisher information:<br>
<code><strong>BidMachine.setPublisher(Publisher):</strong></code></p>

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
