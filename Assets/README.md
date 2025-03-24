# BidMachine Unity Plugin

## Step 1. Import the SDK

1.1 Download the latest version of the BidMachine Unity Plugin, which includes the newest Android and iOS BidMachine SDK with significant improvements.

1.2 To import the BidMachine Unity plugin, either double-click on the `BidMachine-Unity-Plugin-3.2.1.unitypackage` file or navigate to `Assets → Import Package → Custom Package` in the Unity editor. Ensure all files are selected in the Importing Package window, then click `Import`.

## Step 2. Project configuration

### 2.1 Android

#### 2.1.1 Requirements

- **Min Android SDK version**: 21

#### 2.1.2 External Dependency Manager (Play Services Resolver)

The BidMachine Unity Plugin includes the External Dependency Manager package. Follow these steps to resolve BidMachine's dependencies:

1. **Import the BidMachine Unity Plugin**:
    - In the Unity editor, select `File → Build Settings → Android`.

2. **Configure Gradle Template**:
    - Add the flag `Custom Main Gradle Template` (Navigate to `Build Settings → Player Settings → Publishing settings`).

3. **Enable Required Settings**:
    - Enable the setting `Patch mainTemplate.gradle` (Navigate to `Assets → External Dependency Manager → Android Resolver → Settings`).
    - Enable the setting `Use Jetifier` (Navigate to `Assets → External Dependency Manager → Android Resolver → Settings`).

4. **Resolve Dependencies**:
    - Run `Assets → External Dependency Manager → Android Resolver` and press `Resolve` or `Force Resolve`.

As a result, the modules required for BidMachine SDK support will be imported into the project's `mainTemplate.gradle` file.

### 2.2 iOS

#### 2.2.1 Requirements

- **iOS Version**: 12.0+
The SDK is no longer compatible with iOS versions below 13. While projects with a minimum deployment target of iOS 12 will not encounter any compilation errors, the SDK will not execute any code on devices running iOS 12 or earlier.

- **Xcode Version**: 15.2+

#### 2.2.2 External Dependency Manager (Play Services Resolver)

The BidMachine Unity Plugin includes the External Dependency Manager package. Follow these steps to resolve BidMachine's dependencies:

1. **Import the BidMachine Unity Plugin**:
    - In the Unity editor, select `File → Build Settings → iOS`.

2. **Configure Dependencies**:
    - During the build process, the required modules for BidMachine SDK support will be imported into your project. You can edit or add other modules in the `Assets → BidMachine → Editor → BidMachineDependencies.xml` file.

3. **Adjust Settings**:
    - Open `Assets → External Dependency Manager → IOS Resolver → Settings`.
    - Ensure that the option "Enable Swift Framework Support Workaround" is unchecked.

## Step 3. Integration

To integrate the BidMachine SDK into your Unity project, follow these steps:

### Initialize the SDK

To initialize the SDK and set your Seller ID, use the following method. You can obtain your `SELLER_ID` by visiting our website or contacting support:

```csharp
BidMachine.Initialize("YOUR_SELLER_ID");
```

### Enable Logging

To enable logging for debugging purposes, use this method:

```csharp
BidMachine.SetLoggingEnabled(true);
```

### Enable Test Mode

To enable test mode, which allows you to test your integration without serving real ads, use this method:

```csharp
BidMachine.SetTestMode(true);
```

### Set Endpoint

If you need to set a custom endpoint, use this method:

```csharp
BidMachine.SetEndpoint("YOUR_ENDPOINT_URL");
```

### Check SDK Initialization

To check if the BidMachine SDK has been initialized, use this method:

```csharp
bool isInitialized = BidMachine.IsInitialized();
```

This will return `true` if the SDK is initialized, otherwise `false`.

By following these steps, you will successfully integrate the BidMachine SDK into your Unity project.

## Request Parameters

### User Restriction Parameters

| Parameter | Type | Description |
| - | - | - |
| Subject to GDPR | Boolean | Flag indicating if GDPR regulations apply. [The General Data Protection Regulation (GDPR)](https://wikipedia.org/wiki/General_Data_Protection_Regulation) is a regulation of the European Union. |
| Coppa | Boolean | Flag indicating if COPPA regulations apply. [The Children's Online Privacy Protection Act (COPPA)](https://wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act) was established by the U.S. Federal Trade Commission. |
| US Privacy String | String | CCPA string if applicable, compliant with the IAB standard. [CCPA String Format](https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md) |
| GPP String | String | GPP string if applicable, compliant with the IAB standard. [GPP String Format](https://github.com/InteractiveAdvertisingBureau/Global-Privacy-Platform/blob/main/Core/Consent%20String%20Specification.md#about-the-global-privacy-platform-string) |
| GPP Ids | List\<Integer\> | GPP ids |

#### Set Consent Configuration

To set the consent configuration, use the following method:

```csharp
BidMachine.SetConsentConfig(true, "consentString");
```

#### Set Subject to GDPR

To indicate if GDPR regulations should be applied, use the following method:

```csharp
BidMachine.SetSubjectToGDPR(true);
```

#### Set COPPA

To indicate if COPPA regulations should be applied, use the following method:

```csharp
BidMachine.SetCoppa(true);
```

#### Set CCPA U.S. Privacy String

To set the CCPA U.S. Privacy String, use the following method:

```csharp
BidMachine.SetUSPrivacyString("usPrivacyString");
```

#### Set Global Privacy Platform String

To set the Global Privacy Platform String and Ids, use the following method:

```csharp
BidMachine.SetGPP("gppString", new List<int> { 1, 2, 3 });
```

### Publisher

| Param  | Type   | Description      |
| :----- | :----- | :--------------- |
| Id     | String | Publisher ID     |
| Name   | String | Publisher name   |
| Domain | String | Publisher domain |

To set the publisher information, use the following method:

```csharp
Publisher publisher = new Publisher
{
    Id = "your_publisher_id",
    Name = "your_publisher_name",
    Domain = "your_publisher_domain"
};
BidMachine.SetPublisher(publisher);
```

### Targeting Parameters

| Param | Type | Description |
| - | - | - |
| User Id | String | Vendor-specific ID for the user. |
| Gender | Enum | Gender, one of the following: Female, Male, Omitted. |
| Year of Birth | Integer | Year of birth as a 4-digit integer (e.g. - 1990). |
| Keywords | String[] | List of keywords, interests, or intents (separated by comma if you use .xml). |
| Device Location | Location | Location of the device. It may not be the location sent to the server, as it is compared to the current device location at the time, when it was received. |
| Country | String | Country of the user's domicile (i.e. not necessarily their current location). |
| City | String | City of the user's domicile (i.e. not necessarily their current location). |
| Zip | String | Zip of the user's domicile (i.e. not necessarily their current location). |
| Store Url | String | App store URL for an installed app; for IQG 2.1 compliance. |
| Store Category | String | Sets App store category definitions (e.g. - "games"). |
| Store Sub Category | String[] | Sets App Store Subcategory definitions. The array is always capped at 3 strings. |
| Framework Name | String | Sets app framework definitions. |
| Paid | Boolean | Determines, if the app version is free or paid version of the app. |
| External User Ids | List<ExternalUserId> | Set external user ID list. |
| Blocked Advertiser IAB Category | String[] | Block list of content categories by IDs. |
| Blocked Advertiser Domain | String[] | Block list of advertisers by their domains (e.g., “example.com”). |
| Blocked Application | String[] | Block list of apps where ads are disallowed. These should be bundle or package names (e.g., “com.foo.mygame”) and should NOT be app store IDs (e.g., not iTunes store IDs). |

Code Example:

```csharp
TargetingParams targetingParams = new TargetingParams
{
    UserId = "UserId",
    StoreId = "StoreId",
    Gender = TargetingParams.Gender.Female,
    BirthdayYear = 1991,
    Keywords = new[] { "key_1", "key_2" },
    Country = "Country",
    City = "City",
    Zip = "zip",
    StoreUrl = "StoreUrl",
    StoreCategory = "StoreCategory",
    StoreSubCategories = new[] { "sub_category_1", "sub_category_2" },
    Framework = "unity",
    IsPaid = true,
    DeviceLocation = new TargetingParams.Location
    {
        Provider = "",
        Latitude = 22.0d,
        Longitude = 22.0d
    },
    BlockedApplications = new HashSet<string> { "BlockedApplication" },
    BlockedCategories = new HashSet<string> { "BlockedAdvertiserIABCategory" },
    BlockedDomains = new HashSet<string> { "BlockedAdvertiserDomain" },
    ExternalUserIds = new[]
    {
        new ExternalUserId { Source = "sourceId_1", Id = "1" },
        new ExternalUserId { Source = "sourceId_2", Id = "2" }
    }
};
BidMachine.SetTargetingParams(targetingParams);
```

### Price Floor Parameters

| Param | Type | Description |
| - | - | - |
| Id | String | Unique floor identifier. |
| Price | double | Floor price |

Code example:

```csharp
PriceFloorParams priceFloorParams = new PriceFloorParams();
priceFloorParams.AddPriceFloor("123", 1.2d);
```

### Custom Parameters

| Param | Type | Description |
| - | - | - |
| Key | String | Unique parameter identifier. |
| Value | String | Parameter value |

Code example:

```csharp
CustomParams customParams = new CustomParams();
customParams.AddParam("key", "value");
```

### Auction Result

| Id | String | Winner bid ID provided in the request. | "cc5bd14b-aaef-4037-b4f8-879913366e3c" |
| :- | :- | :- | :- |
| Demand Source | String | Winner advertising source name. | "BidMachine Test" |
| Price | double | Winner price expressed as CPM. | 0.023 |
| Deal Id | String | Id of Price Floor. | "d6f61bf9-11a8-4172-a77d-4b1ff85a727f" |
| Creative Id | String | Winner creative id. | "123.13579" |
| CID | String | Winner Campaign ID or other similar grouping of brand-related ads. | "123.13587" |
| Custom Params | Map<String, String> | Map that contains additional information about the response. | |
| Custom Extras | Map<String, String> | Client parameters of winner networks. |  |

You can get  `AuctionResult`  in two ways:

- Through  `IAdRequestListener`. Use  `AuctionResult`  from  `onRequestSuccess`  callback

```csharp
public  void  onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
{
// Use AuctionResult from onRequestSuccess callback
}
```

- Through getter. Each  `IAdRequest`  has an option to retrieve auction result information after it has been loaded.

```csharp
adRequest.getAuctionResult();
```

## Banner / MREC

BannerSize

| Type | Size | Description |
| - | - | - |
| Size_320x50 | width: 320 height: 50 | Regular banner size. |
| Size_728x90 | width: 728 height: 90 | Banner size for tablets. |
| Size_300x250 | width: 300 height: 250 | MREC banner size. |

To set up event listeners for Banner ads, follow these steps:

**Set the listener for banner view events**:

```csharp
private class BannerListener : IAdListener<IBannerView>
{
    // Implement the methods for banner events here
    ...
}

bannerView.SetListener(new BannerListener());
```

Load banner:

```csharp
bannerView.Load(bannerRequest);
```

Show banner or MREC:

```csharp
bannerView.Show(
            BidMachine.BANNER_VERTICAL_BOTTOM,
            BidMachine.BANNER_HORIZONTAL_CENTER,
            bannerView, 
            bannerRequest.getSize());
```

Hide banner or MREC:

```csharp
bannerView.Hide();
```

Destroy banner or MREC (you should destroy request each time before new request):

```csharp
bannerView.Destroy();
```

Code example:

 ```csharp
public class BidMachineController : MonoBehaviour
{
    IAdRequestListener bannerRequestListener = new BannerRequestListener();
    BannerRequest bannerRequest = new BannerRequestBuilder()
        .SetSize(...) // Set BannerSize. Required
        .SetTargetingParams(...) // Set targating params
        .SetPriceFloorParams(...) // Set price floor parameters
        .SetPlacementId("placement_id") // Set placement id
        .SetLoadingTimeOut(1000) // Set loading timeout in milliseconds
        .SetListener(bannerRequestListener) // Set banner request listener
        .Build();

    IAdListener<IBannerView> bannerListener = new BannerListener();

    BannerView bannerView = new BannerView();
    bannerView.SetListener(bannerListener); // Set banner listener       
    bannerView.Load(bannerRequest); // Load banner ad

    class BannerListener : IAdListener<IBannerView>
    {
        ...
    }

    class BannerRequestListener : IAdRequestListener
    {
        ...
    }
}
 ```

## Interstitial

AdContentType

By default AdContentType is AdContentType.All

| Type | Description |
| - | - |
| AdContentType.All | Flag to request both Video and Static ad content types. |
| AdContentType.Static | Flag to request Static ad content type only. |
| AdContentType.Video | Flag to request Video ad content type only. |

To set Interstitial Ad listeners:

```csharp
interstitialAd.SetListener(this);
```

To check if Interstitial ad can show:

```csharp
interstitialAd.CanShow();
```

To load interstitial:

```csharp
interstitialAd.Load(interstitialRequest);
```

To show interstitial:

```csharp
interstitialAd.Show();
```

To destroy interstitial (you should destroy request each time before new request):

```csharp
interstitialAd.Destroy();
```

Example code:

```csharp
public class BidMachineController : MonoBehaviour
{
    IAdRequestListener interstitialRequestListener = new InterstitialRequestListener();
    IAdRequest interstitialRequest = new InterstitialRequest.Builder()
        .SetAdContentType(...)
        .SetTargetingParams(...) // Set targating params
        .SetPriceFloorParams(...) // Set price floor parameters
        .SetPlacementId("placement_id") // Set placement id
        .SetLoadingTimeOut(1000) // Set loading timeout in milliseconds
        .SetListener(interstitialRequestListener) // Set request listener
        .Build();

    IInterstitialAdListener interstitialListener = new InterstitialListener();

    InterstitialAd interstitialAd = new InterstitialAd();
    interstitialAd.SetListener(interstitialListener);
    interstitialAd.Load(interstitialRequest);

    class InterstitialListener : IInterstitialAdListener
    {
        ...
    }

    class InterstitialRequestListener : IAdRequestListener
    {
        ...
    }
}
```

## Rewarded

To set Rewarded Ad listeners:

```csharp
rewardedAd.SetListener(this);
```

To check if Rewarded ad can show:

```csharp
rewardedAd.CanShow();
```

To load rewarded ad:

```csharp
rewardedAd.Load(rewardedRequest);
```

To show rewarded ad:

```csharp
rewardedAd.Show();
```

To destroy rewarded ad (you should destroy request each time before new request):

```csharp
rewardedAd.Destroy();
```

Example code:

```csharp
public class BidMachineController : MonoBehaviour
{
    IAdRequestListener rewardedRequestListener = new RewardedRequestListener();
    IAdRequest rewardedRequest = new RewardedRequest.Builder()
        .SetTargetingParams(...) // Set targating params
        .SetPriceFloorParams(...) // Set price floor parameters
        .SetPlacementId("placement_id") // Set placement id
        .SetLoadingTimeOut(1000) // Set loading timeout in milliseconds
        .SetListener(rewardedRequestListener) // Set request listener
        .Build();

    IRewardedAdListener rewardedListener = new RewardedAdListener();

    RewardedAd rewardedAd = new InterstitialAd();
    rewardedAd.SetListener(rewardedListener);
    rewardedAd.Load(rewardedRequest);

    class RewardedAdListener : IRewardedAdListener
    {
        ...
    }

    class RewardedRequestListener : IAdRequestListener
    {
        ...
    }
}
```

### Callbacks usage rules

**Run Callbacks in Main Unity Thread**

Callbacks in BidMachine plugin are executed in the main Android or iOS threads (not in the main Unity thread). What does it mean for you?
It’s not recommended to perform any UI changes (change colours, positions, sizes, texts and so on) directly in our callback functions.

So, how to react on BidMachine events to prevent multithreading problems? The simplest way is to use flags and Update() method of MonoBehaviour class:

```csharp
public class SomeClass : MonoBehaviour, IAdRequestListener
{
    bool requestFinished = false;
    string result;

    public void onRequestSuccess(IAdRequest request, string auctionResult)
    {
        result = auctionResult;

        // It's important to set flag to true only after all required parameters
        requestFinished = true;
    }

    // Update method always performs in the main Unity thread
    void Update()
    {
        if(requestFinished)
        {
            // Don't forget to set flag to false
            requestFinished = false;
            // Do something with result
        }
    }
}
```

Other, maybe more comfortable way is to use UnityMainThreadDispatcher. To use it:

Download script and prefab.
Import downloaded files to your project.
Add UnityMainThreadDispatcher.prefab to your scene (or to all scenes, where you want to make UI changes after BidMachine callbacks).
Use UnityMainThreadDispatcher.Instance().Enqueue()``` method to perform changes:

```csharp
public void onRequestSuccess(IAdRequest request, string auctionResult)
{
    UnityMainThreadDispatcher.Instance().Enqueue(()=> {
        Debug.Log($"BidMachine. Request success: {auctionResult}")
    });
}
```

And finally, the official way to send message to Unity Main thread is UnitySendMessage.
It’s platform dependent, so it’s required to make changes in Android native code and iOS native code.
