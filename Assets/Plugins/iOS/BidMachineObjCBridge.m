#if defined(__has_include) && __has_include("UnityAppController.h")
#import "UnityAppController.h"
#else
#import "EmptyUnityAppController.h"
#endif

#import <UIKit/UIKit.h>
#import <BidMachine/BidMachine.h>
#import "BidMachineInterstitialDelegate.h"
#import "BidMachineRewardedDelegate.h"
#import "BidMachineBannerViewDelegate.h"
#import "BidMachineBannerRequestDelegate.h"
#import "BidMachineInterstitialRequestDelegate.h"
#import "BidMachineRewardedRequestDelegate.h"
#import "BidMachineSessionAdParams.h"
#import "BidMachineNativeRequestDelegate.h"
#import "BidMachineNativeDelegate.h"
#import "BidMachine/BDMNativeAd+TrackingEvents.h"

static BDMSdkConfiguration *configuration;
static BidMachineSessionAdParams *sessionAdParams;

static BDMTargeting *targeting;
static BDMPriceFloor *priceFloor;
static BDMUserGender *userGender;

static BDMInterstitial *interstitial;
static BDMRewarded *rewarded;
static BDMBannerView *bannerView;
static BDMNativeAd *native;


static BDMRewardedRequest *rewardedRequest;
static BDMInterstitialRequest *interstitialRequest;
static BDMBannerRequest *bannerRequest;
static BDMNativeAdRequest *nativeRequest;

static BidMachineBannerRequestDelegate *BidMachineBannerRequestDelegateInstance;
static BidMachineInterstitialRequestDelegate * BidMachineInterstitialRequestDelegateInstance;
static BidMachineRewardedRequestDelegate *BidMachineRewardedRequestDelegateInstance;
static BidMachineNativeRequestDelegate *BidMachineNativeRequestDelegateInstance;

static BidMachineInterstitialDelegate *BidMachineInterstitialDelegateInstance;
static BidMachineRewardedDelegate *BidMachineRewardedDelegateInstance;
static BidMachineBannerViewDelegate *BidMachineBannerViewDelegateInstance;
static BidMachineNativeDelegate *BidMachineNativeDelegateInstance;

static UIViewController* RootViewController() {
    return ((UnityAppController *)[UIApplication sharedApplication].delegate).rootViewController;
}

// TargetingParams

void TargetingSetUserId(const char *userId){
    if (targeting != nil){
        targeting.userId = [NSString stringWithUTF8String:userId];
    } else {
        printf("BidMachineObjCBrigde.m TargetingSetUserId() BDMTargeting object - nil");
    }
}

void TargetingSetGender(int gender){
    if (targeting != nil){
        switch ((int)gender) {
            case 1:
                targeting.gender = kBDMUserGenderMale;
                break;
            case 2:
                targeting.gender = kBDMUserGenderFemale;
                break;
            case 3:
                targeting.gender = kBDMUserGenderUnknown;
                break;
            default:
                targeting.gender = kBDMUserGenderUnknown;
                break;
        }
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetGender() BDMTargeting object - nil");
    }
}

void TargetingSetYearOfBirth(int yearOfBirth){
    if (targeting != nil){
        targeting.yearOfBirth = [NSNumber numberWithInt:yearOfBirth];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetYearOfBirth() BDMTargeting object - nil");
    }
}

void TargetingSetKeyWords(const char *keywords){
    if (targeting != nil){
        targeting.keywords =  [NSString stringWithUTF8String:keywords];
    } else {
        printf("BidMachineObjCBrigde.m TargetingSetKeyWords() BDMTargeting object - nil");
    }
}

void TargetingSetCountry(const char *country){
    if (targeting != nil){
        targeting.country = [NSString stringWithUTF8String:country];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetCountry() BDMTargeting object - nil");
    }
}

void TargetingSetCity(const char *city){
    if (targeting != nil){
        targeting.city = [NSString stringWithUTF8String:city];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetCity() BDMTargeting object - nil");
    }
}

void TargetingSetZip(const char *zip){
    if (targeting != nil){
        targeting.zip = [NSString stringWithUTF8String:zip];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetZip() BDMTargeting object - nil");
    }
}

void TargetingSetStoreUrl(const char *url){
    if (targeting != nil){
        targeting.storeURL = [[NSURL alloc] initWithString:[NSString stringWithUTF8String:url]];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetStoreUrl() BDMTargeting object - nil");
    }
}

void TargetingSetStoreCategory(const char *storeCategory){
    if (targeting != nil){
        targeting.storeCategory = [NSString stringWithUTF8String:storeCategory];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetStoreCategory() BDMTargeting object - nil");
    }
}

void TargetingSetStoreSubCategories(const char *storeSubCategories){
    if (targeting != nil){
        NSString *list = [NSString stringWithUTF8String:storeSubCategories];
        NSArray *blockedAppsArray = [list componentsSeparatedByString:@","];
        targeting.storeSubcategory = blockedAppsArray;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetStoreSubCategories() BDMTargeting object - nil");
    }
}

void TargetingSetFramework(const char *framework){
    if (targeting != nil){
        targeting.frameworkName = [NSString stringWithUTF8String:framework];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetFramework() BDMTargeting object - nil");
    }
}

void TargetingSetPaid(BOOL paid){
    if (targeting != nil){
        targeting.paid = paid;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetPaid() BDMTargeting object - nil");
    }
}

void TargetingSetDeviceLocation(double latitude, double longitude){
    if (targeting != nil){
        targeting.deviceLocation =  [[CLLocation alloc] initWithLatitude:latitude longitude:longitude];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetDeviceLocation() BDMTargeting object - nil");
    }
}

void TargetingSetExternalUserIds(const char *ExternalUserIds){
    if (targeting != nil){
        NSString *jsonString = [NSString stringWithUTF8String:ExternalUserIds];
        NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        NSError *error = nil;
        NSDictionary *json = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:&error];
        NSArray *jsonArray = json[@"Items"];
        NSMutableArray<BDMExternalUserId *> *externalUserIds = [NSMutableArray<BDMExternalUserId *> new];
        for (NSDictionary *externalUserIdsArray in jsonArray) {
            BDMExternalUserId *externalUser = [BDMExternalUserId new];
            NSString *sourceId = [externalUserIdsArray objectForKey:@"sourceId"];
            externalUser.sourceId = sourceId;
            NSString *value = [externalUserIdsArray objectForKey:@"value"];
            externalUser.value = value;
            [externalUserIds addObject:externalUser];
        }
        targeting.externalUserIds = externalUserIds;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetExternalUserIds() BDMTargeting object - nil");
    }
}

void TargetingSetBlockedApps(const char *blockedApps){
    if (targeting != nil){
        NSString *list = [NSString stringWithUTF8String:blockedApps];
        NSArray *blockedAppsArray = [list componentsSeparatedByString:@","];
        targeting.blockedApps = blockedAppsArray;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetBlockedApps() BDMTargeting object - nil");
    }
}

void TargetingSetBlockedCategories(const char *blockedCategories){
    if (targeting != nil){
        NSString *list = [NSString stringWithUTF8String:blockedCategories];
        NSArray *blockedCategoriesArray = [list componentsSeparatedByString:@","];
        targeting.blockedCategories = blockedCategoriesArray;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetBlockedCategories() BDMTargeting object - nil");
    }
}

void TargetingSetBlockedAdvertisers(const char *blockedAdvertisers){
    if (targeting != nil){
        NSString *list = [NSString stringWithUTF8String:blockedAdvertisers];
        NSArray *blockedAdvertisersArray = [list componentsSeparatedByString:@","];
        targeting.blockedAdvertisers = blockedAdvertisersArray;
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetBlockedAdvertisers() BDMTargeting object - nil");
    }
}

void TargetingSetStoreId(const char *storeId){
    if (targeting != nil){
        targeting.storeId = [NSString stringWithUTF8String:storeId];
    }else {
        printf("BidMachineObjCBrigde.m TargetingSetStoreId() BDMTargeting object - nil");
    }
}

BDMTargeting * GetTargeting(){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    return targeting;
}

//PriceFloor

void PriceFloorAddPriceFloor(const char *priceFloorId, double value){
    if (priceFloor != nil) {
        priceFloor.ID = [NSString stringWithUTF8String:priceFloorId];
        priceFloor.value = [[NSDecimalNumber alloc] initWithDouble:value];
    }else {
        printf("BidMachineObjCBrigde.m PriceFloorAddPriceFloor() BDMPriceFloor object - nil");
    }
}

BDMPriceFloor * GetPriceFloor(){
    if (!priceFloor) {
        priceFloor = [BDMPriceFloor new];
    }
    return priceFloor;
}

//BMError

int BidMachineGetErrorCode(NSError * error){
    return (int) error.code;
}

char * BidMachineGetErrorBrief(NSError * error){
    NSString *brief = error.localizedDescription;
    const char *cString = [brief UTF8String];
    char *cStringCopy = calloc([brief length]+1, 1);
    return strncpy(cStringCopy, cString, [brief length]);
}

char * BidMachineGetErrorMessage(NSError * error){
    NSString *message = error.localizedFailureReason;
    const char *cString = [message UTF8String];
    char *cStringCopy = calloc([message length]+1, 1);
    return strncpy(cStringCopy, cString, [message length]);
}

//SessionAdParams

BidMachineSessionAdParams * GetSessionAdParams(){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    return sessionAdParams;
}

void SetSessionDuration(int value){
    if (sessionAdParams != nil) {
        sessionAdParams.sessionDuration = value;
    }else {
        printf("BidMachineObjCBrigde.m SetSessionDuration() BidMachineSessionAdParams object - nil");
    }
}

void SetImpressionCount(int value){
    if (sessionAdParams != nil) {
        sessionAdParams.impressions = value;
    }else {
        printf("BidMachineObjCBrigde.m SetImpressionCount() BidMachineSessionAdParams object - nil");
    }
}

void SetClickRate(int value){
    if (sessionAdParams != nil) {
        sessionAdParams.clickRate = value;
    }else {
        printf("BidMachineObjCBrigde.m SetClickRate() BidMachineSessionAdParams object - nil");
    }
}

void SetLastAdomain(const char *value){
    if (sessionAdParams != nil) {
        sessionAdParams.lastAdomain = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m SetLastAdomain() BidMachineSessionAdParams object - nil");
    }
}

void SetCompletionRate(int value){
    if (sessionAdParams != nil) {
        sessionAdParams.completionRate = value;
    }else {
        printf("BidMachineObjCBrigde.m SetCompletionRate() BidMachineSessionAdParams object - nil");
    }
}

void SetLastClickForImpression(int value){
    if (sessionAdParams != nil) {
        sessionAdParams.impressions = value;
    }else {
        printf("BidMachineObjCBrigde.m SetLastClickForImpression() BidMachineSessionAdParams object - nil");
    }
}

void SetLastBundle(const char *value){
    if (sessionAdParams != nil) {
        sessionAdParams.lastBundle =[NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m SetLastBundle() BidMachineSessionAdParams object - nil");
    }
}

//BidMachine

void BidMachineInitialize(const char *sellerId) {
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    
    [BDMSdk.sharedSdk
     startSessionWithSellerID:[NSString stringWithUTF8String:sellerId]
     configuration:configuration
     completion:nil];
}

bool BidMachineIsInitialized(){
    return BDMSdk.sharedSdk.initialized;
}

void BidMachineSetEndpoint(const char *url){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.baseURL = [NSURL URLWithString:[NSString stringWithUTF8String:url]];
}

void BidMachineSetTestMode(BOOL testing){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.testMode = testing;
}

void BidMachineSetLogging(BOOL logging){
    BDMSdk.sharedSdk.enableLogging = logging;
}

void BidMachineSetCoppa (BOOL coppa){
    [BDMSdk sharedSdk].restrictions.coppa = coppa;
}

void BidMachineSetUSPrivacyString(const char *USPrivacyString){
    [BDMSdk sharedSdk].restrictions.USPrivacyString =[NSString stringWithUTF8String:USPrivacyString];
}

void BidMachineSetGdprRequired(BOOL subjectToGDPR){
    [BDMSdk sharedSdk].restrictions.subjectToGDPR = subjectToGDPR;
}

void BidMachineSetConsentString(BOOL consent, const char *consentString){
    [BDMSdk sharedSdk].restrictions.hasConsent = consent;
    [BDMSdk sharedSdk].restrictions.consentString = [NSString stringWithUTF8String:consentString];
}

void BidMachineSetPublisher(const char *id, const char *name, const char *domain, const char *categories){
    BDMSdk.sharedSdk.publisherInfo.publisherId = [NSString stringWithUTF8String:id];
    BDMSdk.sharedSdk.publisherInfo.publisherName =[NSString stringWithUTF8String:name];
    BDMSdk.sharedSdk.publisherInfo.publisherDomain =[NSString stringWithUTF8String:domain];
    NSString *nsCategories =[NSString stringWithUTF8String:categories];
    BDMSdk.sharedSdk.publisherInfo.publisherCategories = [nsCategories componentsSeparatedByString:@","];
}

void BidMachineSetTargeting (){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.targeting = targeting;
}

//Interstitial

void InterstitialSetSessionAdParams(id<BDMContextualProtocol> value){
    if (interstitialRequest != nil) {
        interstitialRequest.contextualData = value;
    }else {
        printf("BidMachineObjCBrigde.m InterstitialSetSessionAdParams() BDMInterstitialRequest object - nil");
    }
}

void InterstitialSetLoadingTimeOut(int value){
    if (interstitialRequest != nil) {
        interstitialRequest.timeout =  [NSNumber numberWithInt:value];
    }else {
        printf("BidMachineObjCBrigde.m InterstitialSetLoadingTimeOut() BDMInterstitialRequest object - nil");
    }
}

void InterstitialSetPlacementId(const char *value){
    if (interstitialRequest != nil) {
        interstitialRequest.placementId = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m InterstitialSetPlacementId() BDMInterstitialRequest object - nil");
    }
}

void InterstitialSetBidPayload(const char *value){
    if (interstitialRequest != nil) {
        interstitialRequest.bidPayload = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m InterstitialSetBidPayload() BDMInterstitialRequest object - nil");
    }
}

void InterstitialRequestSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (interstitialRequest != nil) {
        NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
        interstitialRequest.priceFloors = array;
    }else {
        printf("BidMachineObjCBrigde.m InterstitialRequestSetPriceFloor() BDMInterstitialRequest object - nil");
    }
}

void InterstitialRequestSetType(int type){
    if (interstitialRequest != nil) {
        switch (type) {
            case 0:
                interstitialRequest.type = BDMFullsreenAdTypeBanner;
                break;
            case 1:
                interstitialRequest.type = BDMFullscreenAdTypeVideo;
                break;
            case 2:
                interstitialRequest.type = BDMFullscreenAdTypeAll;
                break;
            default:
                interstitialRequest.type = BDMFullscreenAdTypeAll;
        }
    }else {
        printf("BidMachineObjCBrigde.m InterstitialRequestSetType() BDMInterstitialRequest object - nil");
    }
}

BDMInterstitialRequest * GetInterstitialRequest(){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    
    return interstitialRequest;
}

BOOL InterstitialAdCanShow(){
    if (interstitial != nil) {
        return interstitial.canShow;
    } else {
        printf("BidMachineObjCBrigde.m InterstitialAdCanShow() BDMInterstitial object - nil");
        return false;
    }
}

void InterstitialAdDestroy(){
    if (interstitial != nil) {
        [interstitial invalidate];
        interstitial = nil;
        if (interstitialRequest != nil) {
            interstitialRequest = nil;
        }
    }else {
        printf("BidMachineObjCBrigde.m InterstitialAdDestroy() BDMInterstitial object - nil");
    }
}

void InterstitialAdLoad( BDMInterstitialRequest *interstitialRequest){
    if (interstitial != nil) {
        [interstitial populateWithRequest:interstitialRequest];
    }else {
        printf("BidMachineObjCBrigde.m InterstitialAdLoad() BDMInterstitial object - nil");
    }
}

void SetInterstitialRequestDelegate(InterstitialRequestSuccessCallback onSuccess,
                                    InterstitialRequestFailedCallback onFailed,
                                    InterstitialRequestExpiredCallback onExpired){
    
    if (!BidMachineInterstitialRequestDelegateInstance) {
        BidMachineInterstitialRequestDelegateInstance = [BidMachineInterstitialRequestDelegate new];
    }
    
    if (interstitialRequest != nil) {
        BidMachineInterstitialRequestDelegateInstance.onIntersittialRequestSuccess = onSuccess;
        BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestFailed = onFailed;
        BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestExpired = onExpired;
        [interstitialRequest performWithDelegate:BidMachineInterstitialRequestDelegateInstance];
    }else {
        printf("BidMachineObjCBrigde.m SetInterstitialRequestDelegate() BDMInterstitialRequest object - nil");
    }
}

void InterstitialAdSetDelegate(BidMachineInterstitialCallback onAdLoaded,
                               BidMachineInterstitialFailedCallback onAdLoadFailed,
                               BidMachineInterstitialCallback onAdShown,
                               BidMachineInterstitialFailedCallback onAdShowFailed,
                               BidMachineInterstitialCallback onAdImpression,
                               BidMachineInterstitialCallback onAdClicked,
                               BidMachineInterstitialClosedCallback onAdClosed,
                               BidMachineInterstitialCallback onAdExpired){
    
    if(!BidMachineInterstitialDelegateInstance){
        BidMachineInterstitialDelegateInstance = [BidMachineInterstitialDelegate new];
    }
    
    if (interstitial != nil) {
        BidMachineInterstitialDelegateInstance.onAdLoaded = onAdLoaded;
        BidMachineInterstitialDelegateInstance.onAdLoadFailed = onAdLoadFailed;
        BidMachineInterstitialDelegateInstance.onAdShown =  onAdShown;
        BidMachineInterstitialDelegateInstance.onAdShowFailed = onAdShowFailed;
        BidMachineInterstitialDelegateInstance.onAdImpression = onAdImpression;
        BidMachineInterstitialDelegateInstance.onAdClicked = onAdClicked;
        BidMachineInterstitialDelegateInstance.onAdClosed = onAdClosed;
        BidMachineInterstitialDelegateInstance.onAdExpired = onAdExpired;
        interstitial.delegate = BidMachineInterstitialDelegateInstance;
        interstitial.producerDelegate = BidMachineInterstitialDelegateInstance;
    }else {
        printf("BidMachineObjCBrigde.m InterstitialAdSetDelegate() BDMInterstitial object - nil");
    }
}

void InterstitialAdShow(){
    if (interstitial != nil) {
        [interstitial presentFromRootViewController:RootViewController()];
    }else {
        printf("BidMachineObjCBrigde.m InterstitialAdShow() BDMInterstitial object - nil");
    }
}

BDMInterstitial * GetInterstitialAd(){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    return interstitial;
}

//Rewarded

void SetRewardedRequestDelegate(RewardedRequestSuccessCallback onSuccess,
                                RewardedRequestFailedCallback onFailed,
                                RewardedRequestExpiredCallback onExpired){
    
    if (!BidMachineRewardedRequestDelegateInstance) {
        BidMachineRewardedRequestDelegateInstance = [BidMachineRewardedRequestDelegate new];
    }
    
    if (rewardedRequest != nil) {
        BidMachineRewardedRequestDelegateInstance.onRewardedRequestSuccess = onSuccess;
        BidMachineRewardedRequestDelegateInstance.onRewardedRequestFailed = onFailed;
        BidMachineRewardedRequestDelegateInstance.onRewardedRequestExpired = onExpired;
        [rewardedRequest performWithDelegate:BidMachineRewardedRequestDelegateInstance];
    }else {
        printf("BidMachineObjCBrigde.m SetRewardedRequestDelegate() BDMRewardedRequest object - nil");
    }
}

char *GetRewardedAuctionResult(){
    if (rewardedRequest != nil) {
        if(rewardedRequest.info){
            
            NSString *jsonString = @"";
            NSMutableDictionary *dictionary = [NSMutableDictionary new];
            dictionary[@"adDomains"] = rewardedRequest.info.adDomains;
            dictionary[@"bidID"] = rewardedRequest.info.bidID;
            dictionary[@"cID"] = rewardedRequest.info.cID;
            dictionary[@"creativeID"] = rewardedRequest.info.creativeID;
            dictionary[@"customParams"] = rewardedRequest.info.customParams;
            dictionary[@"dealID"] = rewardedRequest.info.dealID;
            dictionary[@"demandSource"] = rewardedRequest.info.demandSource;
            dictionary[@"price"] = rewardedRequest.info.price;
            NSError *error;
            NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
            if (data) {
                NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
            }
            if (data) {
                
                jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
                const char *cString = [jsonString UTF8String];
                char *cStringCopy = calloc([jsonString length]+1, 1);
                return strncpy(cStringCopy, cString, [jsonString length]);
                
            }
            else
            {
                return "empty";
            }
        }else {
            return "empty";
        }
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetRewardedAuctionResult() BDMRewardedRequest object - nil");
    }
}

char *GetInterstitialAuctionResult(){
    if (interstitialRequest != nil) {
        if(interstitialRequest.info){
            
            NSString *jsonString = @"";
            NSMutableDictionary *dictionary = [NSMutableDictionary new];
            
            dictionary[@"adDomains"] = interstitialRequest.info.adDomains;
            dictionary[@"bidID"] = interstitialRequest.info.bidID;
            dictionary[@"cID"] = interstitialRequest.info.cID;
            dictionary[@"creativeID"] = interstitialRequest.info.creativeID;
            dictionary[@"customParams"] = interstitialRequest.info.customParams;
            dictionary[@"dealID"] = interstitialRequest.info.dealID;
            dictionary[@"demandSource"] = interstitialRequest.info.demandSource;
            dictionary[@"price"] = interstitialRequest.info.price;
            
            NSError *error;
            NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
            
            if (data) {
                NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
            }
            
            if (data) {
                
                jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
                const char *cString = [jsonString UTF8String];
                char *cStringCopy = calloc([jsonString length]+1, 1);
                return strncpy(cStringCopy, cString, [jsonString length]);
                
            }
            else
            {
                return "empty";
            }
        }else {
            return "empty";
        }
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetInterstitialAuctionResult() BDMInterstitialRequest object - nil");
    }
}

void RewardedSetDelegate(BidMachineRewardedCallback onAdLoaded,
                         BidMachineRewardedFailedCallback onAdLoadFailed,
                         BidMachineRewardedCallback onAdShown,
                         BidMachineRewardedFailedCallback onAdShowFailed,
                         BidMachineRewardedCallback onAdImpression,
                         BidMachineRewardedCallback onAdClicked,
                         BidMachineRewardedCallback onAdRewarded,
                         BidMachineRewardedClosedCallback onAdClosed,
                         BidMachineRewardedCallback onAdExpired){
    
    if(!BidMachineRewardedDelegateInstance){
        BidMachineRewardedDelegateInstance = [BidMachineRewardedDelegate new];
    }
    
    if (rewarded != nil) {
        BidMachineRewardedDelegateInstance.onAdLoaded = onAdLoaded;
        BidMachineRewardedDelegateInstance.onAdLoadFailed = onAdLoadFailed;
        BidMachineRewardedDelegateInstance.onAdShown = onAdShown;
        BidMachineRewardedDelegateInstance.onAdShowFailed = onAdShowFailed;
        BidMachineRewardedDelegateInstance.onAdImpression = onAdImpression;
        BidMachineRewardedDelegateInstance.onAdClicked = onAdClicked;
        BidMachineRewardedDelegateInstance.onAdRewarded = onAdRewarded;
        BidMachineRewardedDelegateInstance.onAdClosed = onAdClosed;
        BidMachineRewardedDelegateInstance.onAdExpired = onAdExpired;
        rewarded.delegate = BidMachineRewardedDelegateInstance;
        rewarded.producerDelegate = BidMachineRewardedDelegateInstance;
        
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetDelegate() BDMRewarded object - nil");
    }
}

void RewardedSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (rewardedRequest != nil) {
        NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
        rewardedRequest.priceFloors = array;
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetPriceFloor() BDMRewardedRequest object - nil");
    }
}

void RewardedSetBidPayload(const char *value){
    if (rewardedRequest != nil) {
        rewardedRequest.bidPayload = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetBidPayload() BDMRewardedRequest object - nil");
    }
}

void RewardedSetPlacementId(const char *value){
    if (rewardedRequest != nil) {
        rewardedRequest.placementId = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetPlacementId() BDMRewardedRequest object - nil");
    }
}

void RewardedSetLoadingTimeOut(int value){
    if (rewardedRequest != nil) {
        rewardedRequest.timeout = [NSNumber numberWithInt:value];
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetLoadingTimeOut() BDMRewardedRequest object - nil");
    }
}

void RewardedSetSessionAdParams(id<BDMContextualProtocol> value){
    if (rewardedRequest != nil) {
        rewardedRequest.contextualData = value;
    }else {
        printf("BidMachineObjCBrigde.m RewardedSetSessionAdParams() BDMRewardedRequest object - nil");
    }
}

BOOL RewardedCanShow(){
    if (rewarded != nil) {
        return rewarded.canShow;
    } else{
        return false;
        printf("BidMachineObjCBrigde.m RewardedCanShow() BDMRewarded object - nil");
    }
}

void RewardedAdDestroy(){
    if (rewarded != nil) {
        [rewarded invalidate];
        rewarded = nil;
        if(rewardedRequest!= nil){
            rewardedRequest = nil;
        }
    }
}

void RewardedLoad(BDMRewardedRequest *rewardedRequest){
    if (rewarded != nil) {
        [rewarded populateWithRequest:rewardedRequest];
    }else {
        printf("BidMachineObjCBrigde.m RewardedLoad() BDMRewarded object - nil");
    }
}

void RewardedShow(){
    if (rewarded != nil) {
        [rewarded presentFromRootViewController:RootViewController()];
    }else {
        printf("BidMachineObjCBrigde.m RewardedShow() BDMRewarded object - nil");
    }
}

BDMRewarded * GetRewarded(){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    return rewarded;
}

BDMRewardedRequest * GetRewardedRequest(){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    return rewardedRequest;
}

//Banner

void SetBannerRequestDelegate(BannerRequestSuccessCallback onSuccess,
                              BannerRequestFailedCallback onFailed,
                              BannerRequestExpiredCallback onExpired){
    
    if (!BidMachineBannerRequestDelegateInstance) {
        BidMachineBannerRequestDelegateInstance = [BidMachineBannerRequestDelegate new];
    }
    
    if (bannerRequest != nil) {
        bannerRequest = [BDMBannerRequest new];
        BidMachineBannerRequestDelegateInstance.onBannerRequestSuccess = onSuccess;
        BidMachineBannerRequestDelegateInstance.onBannerRequestFailed = onFailed;
        BidMachineBannerRequestDelegateInstance.onBannerRequestExpired = onExpired;
        [bannerRequest performWithDelegate:BidMachineBannerRequestDelegateInstance];
    }else {
        printf("BidMachineObjCBrigde.m SetBannerRequestDelegate() BDMBannerRequest object - nil");
    }
}

void BannerViewRequestSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (bannerRequest != nil) {
        NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
        bannerRequest.priceFloors = array;
    }else {
        printf("BidMachineObjCBrigde.m BannerViewRequestSetPriceFloor() BDMBannerRequest object - nil");
    }
}

void BannerViewSetSize(int type){
    if (bannerRequest != nil) {
        switch (type) {
            case 0:
                bannerRequest.adSize = BDMBannerAdSize320x50;
                
                break;
            case 1:
                bannerRequest.adSize = BDMBannerAdSize300x250;
                break;
            case 2:
                bannerRequest.adSize = BDMBannerAdSize728x90;
                break;
            default:
                bannerRequest.adSize = BDMBannerAdSize320x50;;
        }
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetSize() BDMBannerRequest object - nil");
    }
}

void BannerViewSetBidPayload(const char *value){
    if (bannerRequest != nil) {
        bannerRequest.bidPayload = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetBidPayload() BDMBannerRequest object - nil");
    }
}

void BannerViewSetPlacementId(const char *value){
    if (bannerRequest != nil) {
        bannerRequest.placementId = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetPlacementId() BDMBannerRequest object - nil");
    }
}

void BannerViewSetLoadingTimeOut(int value){
    if (bannerRequest != nil) {
        bannerRequest.timeout = [NSNumber numberWithInt:value];
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetLoadingTimeOut() BDMBannerRequest object - nil");
    }
}

void BannerViewSetSessionAdParams(id<BDMContextualProtocol> value){
    if (bannerRequest != nil) {
        bannerRequest.contextualData = value;
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetSessionAdParams() BDMBannerRequest object - nil");
    }
}

BOOL BannerViewAdCanShow(){
    if (bannerView != nil) {
        return bannerView.canShow;
    }else {
        return false;
        printf("BidMachineObjCBrigde.m BannerViewAdCanShow() BDMBannerView object - nil");
    }
}

void BannerViewDestroy(){
    if (bannerView != nil) {
        [bannerView removeFromSuperview];
        bannerView = nil;
        bannerRequest = nil;
    }else {
        printf("BidMachineObjCBrigde.m BannerViewDestroy() BDMBannerView object - nil");
    }
}

void BannerViewLoad(BDMInterstitialRequest *interstitialRequest){
    if (bannerView != nil) {
        [bannerView populateWithRequest:bannerRequest];
    }else {
        printf("BidMachineObjCBrigde.m BannerViewLoad() BDMBannerView object - nil");
    }
}

void BannerViewShow(int YAxis, int XAxis){
    if (bannerView != nil) {
        
        [RootViewController().view addSubview:bannerView];
        [bannerView setTranslatesAutoresizingMaskIntoConstraints:NO];
        
        switch (YAxis) {
            case 80:
                if (@available(iOS 11.0, *)) {
                    [[[[bannerView safeAreaLayoutGuide] bottomAnchor] constraintEqualToAnchor:RootViewController().view.safeAreaLayoutGuide.bottomAnchor] setActive:YES];
                } else {
                    [[[bannerView bottomAnchor] constraintEqualToAnchor:RootViewController().view.bottomAnchor] setActive:YES];
                    break;
                }
                break;
            case 48:
                if (@available(iOS 11.0, *)) {
                    [[[[bannerView safeAreaLayoutGuide] topAnchor]
                      constraintEqualToAnchor:RootViewController().view.safeAreaLayoutGuide.topAnchor] setActive:YES];
                    break;
                } else {
                    [[[bannerView topAnchor] constraintEqualToAnchor:RootViewController().view.topAnchor] setActive:YES];
                    break;
                }
            case 16:
                if (@available(iOS 11.0, *)) {
                    [[[[bannerView safeAreaLayoutGuide] topAnchor]
                      constraintEqualToAnchor:RootViewController().view.safeAreaLayoutGuide.centerYAnchor] setActive:YES];
                    break;
                } else {
                    [[[bannerView topAnchor] constraintEqualToAnchor:RootViewController().view.centerYAnchor] setActive:YES];
                    break;
                }
        }
        
        switch (XAxis) {
            case 1:
                [[[bannerView centerXAnchor] constraintEqualToAnchor:RootViewController().view.centerXAnchor] setActive:YES];
                break;
            case 3:
                [[[bannerView rightAnchor] constraintEqualToAnchor:RootViewController().view.rightAnchor] setActive:YES];
                break;
            case 5:
                [[[bannerView leftAnchor] constraintEqualToAnchor:RootViewController().view.leftAnchor] setActive:YES];
                break;
        }
        
        if (bannerRequest!= nil){
            if (bannerRequest.adSize == BDMBannerAdSize320x50) {
                [[[bannerView widthAnchor] constraintEqualToConstant:320] setActive:YES];
                [[[bannerView heightAnchor] constraintEqualToConstant:50] setActive:YES];
            } else if (bannerRequest.adSize == BDMBannerAdSize300x250){
                [[[bannerView widthAnchor] constraintEqualToConstant:300] setActive:YES];
                [[[bannerView heightAnchor] constraintEqualToConstant:250] setActive:YES];
            } else if (bannerRequest.adSize == BDMBannerAdSize728x90){
                [[[bannerView widthAnchor] constraintEqualToConstant:728] setActive:YES];
                [[[bannerView heightAnchor] constraintEqualToConstant:90] setActive:YES];
            }
        } else {
            [[[bannerView widthAnchor] constraintEqualToConstant:320] setActive:YES];
            [[[bannerView heightAnchor] constraintEqualToConstant:50] setActive:YES];
        }
    }else {
        printf("BidMachineObjCBrigde.m BannerViewShow() BDMBannerView object - nil");
    }
}

int GetBannerSize(){
    if (bannerRequest != nil) {
        switch (bannerRequest.adSize) {
            case BDMBannerAdSize320x50:
                return 0;
                
            case BDMBannerAdSize300x250:
                return 1;
                
            case BDMBannerAdSize728x90:
                return 2;
                
            default:
                return 0;
        }
        
    }else {
        return 0;
        printf("BidMachineObjCBrigde.m GetBannerSize() BDMBannerRequest object - nil");
    }
}


void BannerViewSetDelegate(BidMachineBannerCallback onAdLoaded,
                           BidMachineBannerFailedCallback onAdLoadFailed,
                           BidMachineBannerCallback onAdShown,
                           BidMachineBannerCallback onAdImpression,
                           BidMachineBannerCallback onAdClicked,
                           BidMachineBannerCallback onAdExpired){
    
    if(!BidMachineBannerViewDelegateInstance){
        BidMachineBannerViewDelegateInstance = [BidMachineBannerViewDelegate new];
    }
    
    if (bannerView != nil) {
        BidMachineBannerViewDelegateInstance.onAdLoaded = onAdLoaded;
        BidMachineBannerViewDelegateInstance.onAdLoadFailed = onAdLoadFailed;
        BidMachineBannerViewDelegateInstance.onAdShown = onAdShown;
        BidMachineBannerViewDelegateInstance.onAdImpression = onAdImpression;
        BidMachineBannerViewDelegateInstance.onAdClicked = onAdClicked;
        BidMachineBannerViewDelegateInstance.onAdExpired = onAdExpired;
        bannerView.delegate = BidMachineBannerViewDelegateInstance;
        bannerView.producerDelegate = BidMachineBannerViewDelegateInstance;
    }else {
        printf("BidMachineObjCBrigde.m BannerViewSetDelegate() BDMBannerRequest object - nil");
    }
}

char *GetBannerAuctionResult(){
    if (bannerRequest != nil) {
        if(bannerRequest.info){
            
            NSString *jsonString = @"";
            NSMutableDictionary *dictionary = [NSMutableDictionary new];
            
            dictionary[@"adDomains"] = bannerRequest.info.adDomains;
            dictionary[@"bidID"] = bannerRequest.info.bidID;
            dictionary[@"cID"] = bannerRequest.info.cID;
            dictionary[@"creativeID"] = bannerRequest.info.creativeID;
            dictionary[@"customParams"] = bannerRequest.info.customParams;
            dictionary[@"dealID"] = bannerRequest.info.dealID;
            dictionary[@"demandSource"] = bannerRequest.info.demandSource;
            dictionary[@"price"] = bannerRequest.info.price;
            
            NSError *error;
            NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
            
            if (data) {
                NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
            }
            
            if (data) {
                jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
                const char *cString = [jsonString UTF8String];
                char *cStringCopy = calloc([jsonString length]+1, 1);
                return strncpy(cStringCopy, cString, [jsonString length]);
                
            }
            else
            {
                return "empty";
            }
        }else {
            return "empty";
        }
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetBannerAuctionResult() BDMBannerRequest object - nil");
    }
}

BDMBannerRequest * GetBannerViewRequest(){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
    return bannerRequest;
}

BDMBannerView * GetBannerView(){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    return bannerView;
}


void SetNativeRequestDelegate(NativeRequestSuccessCallback onSuccess,
                              NativeRequestFailedCallback onFailed,
                              NativeRequestExpiredCallback onExpired){
    
    if (!BidMachineNativeRequestDelegateInstance) {
        BidMachineNativeRequestDelegateInstance = [BidMachineNativeRequestDelegate new];}
    
    if (nativeRequest != nil) {
        
        BidMachineNativeRequestDelegateInstance.onNativeRequestSuccess = onSuccess;
        BidMachineNativeRequestDelegateInstance.onNativeRequestFailed = onFailed;
        BidMachineNativeRequestDelegateInstance.onNativeRequestExpired = onExpired;
        
        [nativeRequest performWithDelegate:BidMachineNativeRequestDelegateInstance];
    }else {
        printf("BidMachineObjCBrigde.m GetBannerAuctionResult() BDMBannerRequest object - nil");
    }
}

void NativeAdSetDelegate(BidMachineNativeCallback onAdLoaded,
                         BidMachineNativeFailedCallback onAdLoadFailed,
                         BidMachineNativeCallback onAdShown,
                         BidMachineNativeCallback onAdClicked,
                         BidMachineNativeCallback onAdImpression,
                         BidMachineNativeCallback onAdExpired){
    
    if (native != nil) {
        
        if(!BidMachineNativeDelegateInstance){
            BidMachineNativeDelegateInstance = [BidMachineNativeDelegate new];
        }
        
        BidMachineNativeDelegateInstance.onAdLoaded = onAdLoaded;
        BidMachineNativeDelegateInstance.onAdLoadFailed = onAdLoadFailed;
        BidMachineNativeDelegateInstance.onAdShown = onAdShown;
        BidMachineNativeDelegateInstance.onAdImpression = onAdImpression;
        BidMachineNativeDelegateInstance.onAdClicked = onAdClicked;
        BidMachineNativeDelegateInstance.onAdExpired = onAdExpired;
        native.delegate = BidMachineNativeDelegateInstance;
        native.producerDelegate = BidMachineNativeDelegateInstance;
    }else {
        printf("BidMachineObjCBrigde.m NativeAdSetDelegate() BDMNativeAdRequest object - nil");
    }
}

void NativeSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (nativeRequest != nil) {
        NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
        nativeRequest.priceFloors = array;
    }else {
        printf("BidMachineObjCBrigde.m NativeSetPriceFloor() BDMNativeAdRequest object - nil");
    }
}

void NativeSetBidPayload(const char *value){
    if (nativeRequest != nil) {
        nativeRequest.bidPayload = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m NativeSetBidPayload() BDMNativeAdRequest object - nil");
    }
}

void NativeSetPlacementId(const char *value){
    if (nativeRequest != nil) {
        nativeRequest.placementId = [NSString stringWithUTF8String:value];
    }else {
        printf("BidMachineObjCBrigde.m NativeSetPlacementId() BDMNativeAdRequest object - nil");
    }
}

void NativeSetLoadingTimeOut(int value){
    if (nativeRequest != nil) {
        nativeRequest.timeout = [NSNumber numberWithInt:value];
    }else {
        printf("BidMachineObjCBrigde.m NativeSetLoadingTimeOut() BDMNativeAdRequest object - nil");
    }
}

void NativeSetSessionAdParams(id<BDMContextualProtocol> value){
    if (nativeRequest != nil) {
        nativeRequest.contextualData = value;
    }else {
        printf("BidMachineObjCBrigde.m NativeSetSessionAdParams() BDMNativeAdRequest object - nil");
    }
}

char * GetNativeTitle(){
    if (native != nil) {
        const char *cString = [native.title UTF8String];
        char *cStringCopy = calloc([native.title length]+1, 1);
        return strncpy(cStringCopy, cString, [native.title length]);
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeTitle() BDMNativeAd object - nil");
    }
}

char * GetNativeDescription(){
    if (native != nil) {
        const char *cString = [native.description UTF8String];
        char *cStringCopy = calloc([native.description length]+1, 1);
        return strncpy(cStringCopy, cString, [native.description length]);
    } else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeDescription() BDMNativeAd object - nil");
    }
}

char * GetNativeCallToAction(){
    if (native != nil) {
        const char *cString = [native.CTAText UTF8String];
        char *cStringCopy = calloc([native.CTAText length]+1, 1);
        return strncpy(cStringCopy, cString, [native.CTAText length]);
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeCallToAction() BDMNativeAd object - nil");
    }
}

float GetNativeRating(){
    if (native != nil) {
        float floatNativeStar = [native.starRating floatValue];
        return floatNativeStar;
    } else {
        return 0.0f;
        printf("BidMachineObjCBrigde.m GetNativeRating() BDMNativeAd object - nil");
    }
}

char * GetNativeImage(){
    if (native != nil) {
        const char *cString = [native.mainImageUrl UTF8String];
        char *cStringCopy = calloc([native.mainImageUrl length]+1, 1);
        return strncpy(cStringCopy, cString, [native.mainImageUrl length]);
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeImage() BDMNativeAd object - nil");
    }
}

char * GetNativeIcon(){
    if (native != nil) {
        const char *cString = [native.iconUrl UTF8String];
        char *cStringCopy = calloc([native.iconUrl length]+1, 1);
        return strncpy(cStringCopy, cString, [native.iconUrl length]);
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeIcon() BDMNativeAd object - nil");
    }
}

bool * NativeAdCanShow(){
    if (native != nil) {
        return native.canShow;
    } else {
        return false;
        printf("BidMachineObjCBrigde.m NativeAdCanShow() BDMNativeAd object - nil");
    }
}

void NativeAdDestroy(){
    if (native != nil) {
        [native invalidate];
        printf("DEBuGBM Destroy native ad %p \n", &native);
        printf("DEBuGBM Destroy native request %p \n", &nativeRequest);
        native = nil;
        
        if(nativeRequest != nil){
            nativeRequest = nil;
        }
    }else {
        printf("BidMachineObjCBrigde.m NativeAdDestroy() BDMNativeAd object - nil");
    }
}

void NativeAdLoad(){
    if (native != nil && nativeRequest != nil) {
        printf("DEBuGBM Load native ad %p \n", &native);
        printf("DEBuGBM Load native request %p \n", &nativeRequest);
        [native makeRequest:nativeRequest];
    }else {
        printf("BidMachineObjCBrigde.m NativeAdLoad() BDMNativeAd and BDMNativeAdRequest objects - nil");
    }
}

void NativeSetMediaAssetTypes(const char *value){
    if (nativeRequest != nil) {
        nativeRequest.type = BDMNativeAdTypeIconAndImage;
    }else {
        printf("BidMachineObjCBrigde.m NativeSetMediaAssetTypes() BDMNativeAd object - nil");
    }
}

char *GetNativeAuctionResult(){
    if (nativeRequest != nil) {
        if(nativeRequest.info){
            
            NSString *jsonString = @"";
            NSMutableDictionary *dictionary = [NSMutableDictionary new];
            
            dictionary[@"adDomains"] = nativeRequest.info.adDomains;
            dictionary[@"bidID"] = nativeRequest.info.bidID;
            dictionary[@"cID"] = nativeRequest.info.cID;
            dictionary[@"creativeID"] = nativeRequest.info.creativeID;
            dictionary[@"customParams"] = nativeRequest.info.customParams;
            dictionary[@"dealID"] = nativeRequest.info.dealID;
            dictionary[@"demandSource"] = nativeRequest.info.demandSource;
            dictionary[@"price"] = nativeRequest.info.price;
            
            NSError *error;
            NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
            
            if (data) {
                NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
            }
            
            if (data) {
                
                jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
                const char *cString = [jsonString UTF8String];
                char *cStringCopy = calloc([jsonString length]+1, 1);
                return strncpy(cStringCopy, cString, [jsonString length]);
                
            }
            else
            {
                return "empty";
            }
        }else {
            return "empty";
        }
    }else {
        return "";
        printf("BidMachineObjCBrigde.m GetNativeAuctionResult() BDMNativeAdRequest object - nil");
    }
}

void DispatchClick(){
    if (native != nil) {
        [native trackUserInteraction];
    }else {
        printf("BidMachineObjCBrigde.m DispatchClick() BDMNativeAd object - nil");
    }
}

void DispatchImpression(){
    if (native != nil) {
        [native trackContainerAdded];
        [native trackImpression];
        [native trackViewable];
    }else{
        printf("BidMachineObjCBrigde.m DispatchImpression() BDMNativeAd object - nil");
    }
}

BDMNativeAd * GetNativeAd(){
    if (native == nil) {
        native = [BDMNativeAd new];
    }
    return native;
}

BDMNativeAdRequest * GetNativeRequest(){
    if (nativeRequest == nil) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    
    return nativeRequest;
}