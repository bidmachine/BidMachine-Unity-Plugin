# Step 1. Import SDK

1.1 Download BidMachine Unity Plugin that includes the newest Android and iOS BidMachine SDK with major improvements.

1.2 To import the BidMachine Unity plugin, double-click on the BidMachine-Unity-Plugin-1.1.2-01.09.2022.unitypackage , or go to Assets → Import Package → Custom Package . Keep all the files in the Importing Package window selected, and click Import .

# Step 2. Project configuration 

2.1 Android 

Requirements:
- Min Android SDK version - 21.

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
UserPermissions.Check(Permission.CoarseLocation));
UserPermissions.Check(Permission.FineLocation));
```

To request permissions you should use this method:<br> 
```c# 
 UserPermissions.Request();
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
BidMachine.Initialize("YOUR_SELLER_ID");
```
To enable logs use this method:
```c# 
BidMachine.SetLoggingEnabled(Boolean);
```
To enable test mode:
```c# 
BidMachine.SetTestMode(Boolean);
```
To Set your endpoint if required:
```c# 
BidMachine.SetEndpoint(Boolean);
```
To check if BidMachine SDK was initialized:
```c# 
BidMachine.IsInitialized();
```

# Request parameters

Global Parameters

Set default Targeting params:
```c# 
BidMachine.SetTargetingParams(TargetingParams);
```

Set consent config:
```c# 
BidMachine.SetConsentConfig(bool, string);
```

Set subject to GDPR:
```c# 
BidMachine.SetSubjectToGDPR(bool);
```

Set COPPA:
```c# 
BidMachine.SetCoppa(bool);
```

Set CCPA U.S. Privacy String:
```c# 
BidMachine.SetUSPrivacyString(string);
```

Sets publisher information:
```c# 
BidMachine.SetPublisher(Publisher);
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
BidMachine.SetTargetingParams(
    new TargetingParams
    {
        UserId = "UserId",
        gender = TargetingParams.Gender.Female,
        BirthdayYear = 1990,
        Keywords = new string[] { "keyword1", "keyword1" },
        DeviceLocation = new TargetingParams.Location
        {
            Provider = "GPS",
            Latitude = 43.9006,
            Longitude = 27.5590
        },
        Country = "Country",
        City = "City",
        Zip = "zip_code",
        StoreUrl = "https://play.google.com/store/apps/details?id=com.example.app",
        StoreCategory = "Category",
        StoreSubCategories = new string[] { "SubCategory1", "SubCategory2" },
        Framework = "unity",
        IsPaid = true,
        externalUserIds = new ExternalUserId[]
        {
            new() { SourceId = "ad_network_1", Value = "1" },
            new() { SourceId = "ad_network_2", Value = "2" }
        },
        BlockedDomains = new HashSet<string> { "domain1.com", "domain2.com" },
        BlockedCategories = new HashSet<string> { "category1", "category2" },
        BlockedApplications = new HashSet<string>
        {
            "com.unwanted.app1",
            "com.unwanted.app2"
        }
    }
);
```

# PRICE FLOOR PARAMETRS

Param | Type | Description
------------ | ------------- | -------------
Id | String | Unique floor identifier.
Price |	double | Floor price

Code example: 
```c#
priceFloorParams = new PriceFloorParams();
priceFloorParams.AddPriceFloor(Guid.NewGuid().ToString(), 0.01);
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
bannerView.SetListener(this);
```

To load banners:
```c#
bannerView.Load(bannerRequest);
```

To show banner or mrec:
```c#
bannerView?.Show(
    BidMachine.BANNER_VERTICAL_BOTTOM,
    BidMachine.BANNER_HORIZONTAL_CENTER,
    bannerView,
    size
);
```

To hide banner or mrec:
```c#
bannerView.Hide();
```

To destroy banner or mrec (you should destroy request each time before new request):
```c#
bannerView.Destroy();
```

Code example:
 ```c#
public class BidMachineController : MonoBehaviour, IAdRequestListener, IAdListener<IBannerView>
{
    BannerRequest bannerRequest = new BannerRequest.Builder()
        .SetSize(...) // Set BannerSize. Required
        .SetTargetingParams(...) // Set targeting params
        .SetPriceFloorParams(...) // Set price floor params
        .SetPlacementId(...) // Set placement id
        .SetLoadingTimeOut(10 * 1000) // Set loading timeout in milliseconds
        .SetListener(this) // Set request listener
        .Build();

    BannerView bannerView = new BannerView();
    bannerView.SetListener(this); // Set listener       
    bannerView.Load(bannerRequest); // Load ad

    public void onRequestExpired(IAdRequest request)
    {
        Debug.Log("BannerRequest: OnAdRequestExpired");
    }

    public void onRequestFailed(IAdRequest request, BMError error)
    {
        Debug.Log($"BannerRequest: OnAdRequestFailed");
    }

    public void onRequestSuccess(IAdRequest request, string auctionResult)
    {
        Debug.Log($"BannerRequest: OnAdRequestSuccess");
    }
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
interstitialAd.SetListener(this);
```

To check if Interstitial ad can show:
```c#
interstitialAd.CanShow();
```

To load interstitial:
```c#
interstitialAd.Load(interstitialRequest);
```

To show interstitial:
```c#
interstitialAd.Show();
```

To destroy interstitial (you should destroy request each time before new request):
```c#
interstitialAd.Destroy();
```

Example code: 
```c#
public class BidMachineController : MonoBehaviour, IInterstitialAdListener, IFullscreenAdListener<IFullscreenAd>
{
    InterstitialRequest interstitialRequest = new InterstitialRequest.Builder()
        .SetAdContentType(AdContentType.All)
        .SetTargetingParams(...) // Set targeting params
        .SetPriceFloorParams(...) // Set price floor params
        .SetPlacementId(...) // Set placement id
        .SetLoadingTimeOut(10 * 1000) // Set loading timeout in milliseconds
        .SetListener(this) // Set request listener
        .Build();

    InterstitialAd interstitialAd = new InterstitialAd();
    interstitialAd.setListener(this); // Set listener       
    interstitialAd.load(interstitialRequest); // Load ad
    
    public void onRequestExpired(IAdRequest request)
    {
        Debug.Log("InterstitialRequest: OnAdRequestExpired");
    }

    public void onRequestFailed(IAdRequest request, BMError error)
    {
        Debug.Log($"InterstitialRequest: OnAdRequestFailed");
    }

    public void onRequestSuccess(IAdRequest request, string auctionResult)
    {
        Debug.Log($"InterstitialRequest: OnAdRequestSuccess");
    }

    public void onAdClosed(IFullscreenAd ad, bool finished)
    {
        Debug.Log($"InterstitialAd: OnAdClosed");
    }

    public void onAdExpired(IFullscreenAd ad)
    {
        Debug.Log("InterstitialAd: OnAdExpired");
    }

    public void onAdImpression(IFullscreenAd ad)
    {
        Debug.Log("InterstitialAd: OnAdImpression");
    }

    public void onAdLoaded(IFullscreenAd ad)
    {
        Debug.Log("InterstitialAd: OnAdLoaded");
    }

    public void onAdLoadFailed(IFullscreenAd ad, BMError error)
    {
        Debug.Log($"InterstitialAd: OnAdLoadFailed");
    }

    public void onAdShowFailed(IFullscreenAd ad, BMError error)
    {
        Debug.Log($"InterstitialAd: OnAdShowFailed");
    }

    public void onAdShown(IFullscreenAd ad)
    {
        Debug.Log("InterstitialAd: OnAdShown");
    }
}
```
# REWARDED AD

To set Rewarded Ad listeners:
```c#
rewardedAd.SetListener(this);
```

To check if Rewarded ad can show:
```c#
rewardedAd.CanShow();
```

To load rewarded ad:
```c#
rewardedAd.Load(rewardedRequest);
```

To show rewarded ad:
```c#
rewardedAd.Show();
```

To destroy rewarded ad (you should destroy request each time before new request):
```c#
rewardedAd.Destroy();
```

Example code: 
```c#
public class BidMachineController : MonoBehaviour
{
    RewardedRequest rewardedRequest = new RewardedRequest.Builder()
        .SetTargetingParams(...) // Set targeting params
        .SetPriceFloorParams(...) // Set price floor params
        .SetPlacementId(...) // Set placement id
        .SetLoadingTimeOut(10 * 1000) // Set loading timeout in milliseconds
        .SetListener(this) // Set request listener
        .Build();

    RewardedAd rewardedAd = new RewardedAd();
    rewardedAd.setListener(this); // Set rewarded listener       
    rewardedAd.load(rewardedRequest); // Load rewarded ad
    
    public void onRequestExpired(IAdRequest request)
    {
        Debug.Log("RewardedRequest: OnAdRequestExpired");
    }

    public void onRequestFailed(IAdRequest request, BMError error)
    {
        Debug.Log($"RewardedRequest: OnAdRequestFailed");
    }

    public void onRequestSuccess(IAdRequest request, string auctionResult)
    {
        Debug.Log($"RewardedRequest: OnAdRequestSuccess");
    }

    public void onAdClosed(IFullscreenAd ad, bool finished)
    {
        Debug.Log($"RewardedAd: OnAdClosed");
    }

    public void onAdExpired(IFullscreenAd ad)
    {
        Debug.Log("RewardedAd: OnAdExpired");
    }

    public void onAdImpression(IFullscreenAd ad)
    {
        Debug.Log("RewardedAd: OnAdImpression");
    }

    public void onAdLoaded(IFullscreenAd ad)
    {
        Debug.Log("RewardedAd: OnAdLoaded");
    }

    public void onAdLoadFailed(IFullscreenAd ad, BMError error)
    {
        Debug.Log($"RewardedAd: OnAdLoadFailed");
    }

    public void onAdShowFailed(IFullscreenAd ad, BMError error)
    {
        Debug.Log($"RewardedAd: OnAdShowFailed");
    }

    public void onAdShown(IFullscreenAd ad)
    {
        Debug.Log("RewardedAd: OnAdShown");
    }
}
```
