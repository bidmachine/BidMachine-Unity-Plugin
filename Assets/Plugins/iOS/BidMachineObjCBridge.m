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

NSMutableSet *rewardedRequests;
NSMutableSet *interstitialRequests;
NSMutableSet *bannerViewRequests;
NSMutableSet *nativeRequests;

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
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.userId = [NSString stringWithUTF8String:userId];
}

void TargetingSetGender(int gender){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
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
}

void TargetingSetYearOfBirth(int yearOfBirth){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.yearOfBirth = [NSNumber numberWithInt:yearOfBirth];
}

void TargetingSetKeyWords(const char *keywords){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.keywords =  [NSString stringWithUTF8String:keywords];
}

void TargetingSetCountry(const char *country){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.country = [NSString stringWithUTF8String:country];
}

void TargetingSetCity(const char *city){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.city = [NSString stringWithUTF8String:city];
}

void TargetingSetZip(const char *zip){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.zip = [NSString stringWithUTF8String:zip];
}

void TargetingSetStoreUrl(const char *url){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.storeURL = [[NSURL alloc] initWithString:[NSString stringWithUTF8String:url]];
}

void TargetingSetStoreCategory(const char *storeCategory){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.storeCategory = [NSString stringWithUTF8String:storeCategory];
}

void TargetingSetStoreSubCategories(const char *storeSubCategories){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    NSString *list = [NSString stringWithUTF8String:storeSubCategories];
    NSArray *blockedAppsArray = [list componentsSeparatedByString:@","];
    targeting.storeSubcategory = blockedAppsArray;
}

void TargetingSetFramework(const char *framework){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.frameworkName = [NSString stringWithUTF8String:framework];
}

void TargetingSetPaid(BOOL paid){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.paid = paid;
}

void TargetingSetDeviceLocation(double latitude, double longitude){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.deviceLocation =  [[CLLocation alloc] initWithLatitude:latitude longitude:longitude];
}

void TargetingSetExternalUserIds(const char *ExternalUserIds){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
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
}

void TargetingSetBlockedApps(const char *blockedApps){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    NSString *list = [NSString stringWithUTF8String:blockedApps];
    NSArray *blockedAppsArray = [list componentsSeparatedByString:@","];
    
    targeting.blockedApps = blockedAppsArray;
}

void TargetingSetBlockedCategories(const char *blockedCategories){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    NSString *list = [NSString stringWithUTF8String:blockedCategories];
    NSArray *blockedCategoriesArray = [list componentsSeparatedByString:@","];
    
    targeting.blockedCategories = blockedCategoriesArray;
}

void TargetingSetBlockedAdvertisers(const char *blockedAdvertisers){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    NSString *list = [NSString stringWithUTF8String:blockedAdvertisers];
    NSArray *blockedAdvertisersArray = [list componentsSeparatedByString:@","];
    
    targeting.blockedAdvertisers = blockedAdvertisersArray;
}

void TargetingSetStoreId(const char *storeId){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    
    targeting.storeId = [NSString stringWithUTF8String:storeId];
}

BDMTargeting * GetTargeting(){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    return targeting;
}

//PriceFloor

void PriceFloorAddPriceFloor(const char *priceFloorId, double value){
    if (!priceFloor) {
        priceFloor = [BDMPriceFloor new];
    }
    priceFloor.ID = [NSString stringWithUTF8String:priceFloorId];
    priceFloor.value = [[NSDecimalNumber alloc] initWithDouble:value];
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

id<BDMContextualProtocol> GetSessionAdParams(){
    return sessionAdParams;
}

void SetSessionDuration(int value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.sessionDuration = value;
}

void SetImpressionCount(int value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.impressions = value;
}

void SetClickRate(int value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.clickRate = value;
}

void SetLastAdomain(const char *value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.lastAdomain = [NSString stringWithUTF8String:value];
}

void SetCompletionRate(int value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.completionRate = value;
}

void SetLastClickForImpression(int value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.impressions = value;
}

void SetLastBundle(const char *value){
    if (!sessionAdParams) {
        sessionAdParams = [BidMachineSessionAdParams new];
    }
    
    sessionAdParams.lastBundle =[NSString stringWithUTF8String:value];
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
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    interstitialRequest.contextualData = value;
}

void InterstitialSetLoadingTimeOut(int value){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    interstitialRequest.timeout =  [NSNumber numberWithInt:value];
}

void InterstitialSetPlacementId(const char *value){
    interstitialRequest.placementId =[NSString stringWithUTF8String:value];
}

void InterstitialSetBidPayload(const char *value){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    interstitialRequest.bidPayload = [NSString stringWithUTF8String:value];
}

void InterstitialRequestSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
    interstitialRequest.priceFloors = array;
}

void InterstitialRequestSetType(int type){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
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
}

BDMInterstitialRequest * GetInterstitialRequest(){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    
    return interstitialRequest;
}

BOOL InterstitialAdCanShow(){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    return interstitial.canShow;
}

void InterstitialAdDestroy(){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    [interstitial invalidate];
    if(!interstitialRequests) interstitialRequests = [[NSMutableSet alloc ]init];
    if([interstitialRequests containsObject: interstitial]) [interstitialRequests removeObject:interstitial];
}

void InterstitialAdLoad( BDMInterstitialRequest *interstitialRequest){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    
    if(!interstitialRequests){
        interstitialRequests = [[NSMutableSet alloc ]init];
    }
    [interstitial populateWithRequest:interstitialRequest];
    [interstitialRequests addObject:interstitial];
}

void SetInterstitialRequestDelegate(InterstitialRequestSuccessCallback onSuccess,
                                    InterstitialRequestFailedCallback onFailed,
                                    InterstitialRequestExpiredCallback onExpired){
    
    if (!BidMachineInterstitialRequestDelegateInstance) {
        BidMachineInterstitialRequestDelegateInstance = [BidMachineInterstitialRequestDelegate new];
    }
    
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    
    BidMachineInterstitialRequestDelegateInstance.onIntersittialRequestSuccess = onSuccess;
    BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestFailed = onFailed;
    BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestExpired = onExpired;
    
    [interstitialRequest performWithDelegate:BidMachineInterstitialRequestDelegateInstance];
}

void InterstitialAdSetDelegate(BidMachineInterstitialCallback onAdLoaded,
                               BidMachineInterstitialFailedCallback onAdLoadFailed,
                               BidMachineInterstitialCallback onAdShown,
                               BidMachineInterstitialFailedCallback onAdShowFailed,
                               BidMachineInterstitialCallback onAdImpression,
                               BidMachineInterstitialCallback onAdClicked,
                               BidMachineInterstitialClosedCallback onAdClosed,
                               BidMachineInterstitialCallback onAdExpired){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    
    if(!BidMachineInterstitialDelegateInstance){
        BidMachineInterstitialDelegateInstance = [BidMachineInterstitialDelegate new];
    }
    
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
}

void InterstitialAdShow(){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    [interstitial presentFromRootViewController:RootViewController()];
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
    
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestSuccess = onSuccess;
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestFailed = onFailed;
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestExpired = onExpired;
    
    [rewardedRequest performWithDelegate:BidMachineRewardedRequestDelegateInstance];
}

char *GetRewardedAuctionResult(){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    
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
}

char *GetInterstitialAuctionResult(){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    
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
    
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    
    if(!BidMachineRewardedDelegateInstance){
        BidMachineRewardedDelegateInstance = [BidMachineRewardedDelegate new];
    }
    
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
}

void RewardedSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
    rewardedRequest.priceFloors = array;
}

void RewardedSetBidPayload(const char *value){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    rewardedRequest.bidPayload = [NSString stringWithUTF8String:value];
}

void RewardedSetPlacementId(const char *value){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    rewardedRequest.placementId = [NSString stringWithUTF8String:value];
}

void RewardedSetLoadingTimeOut(int value){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    rewardedRequest.timeout = [NSNumber numberWithInt:value];
}

void RewardedSetSessionAdParams(id<BDMContextualProtocol> value){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    rewardedRequest.contextualData = value;
}

BOOL RewardedCanShow(){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    return rewarded.canShow;
}

void RewardedAdDestroy(){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    
    [rewarded invalidate];
    
    if(!rewardedRequests) rewardedRequests = [[NSMutableSet alloc ]init];
    
    if([rewardedRequests containsObject: rewarded]) [rewardedRequests removeObject:rewarded];
    
}

void RewardedLoad(BDMRewardedRequest *rewardedRequest){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    if(!rewardedRequests){
        rewardedRequests = [[NSMutableSet alloc ]init];
    }
    [rewarded populateWithRequest:rewardedRequest];
    [rewardedRequests addObject:rewarded];
}

void RewardedShow(){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    [rewarded presentFromRootViewController:RootViewController()];
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
    
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
    BidMachineBannerRequestDelegateInstance.onBannerRequestSuccess = onSuccess;
    BidMachineBannerRequestDelegateInstance.onBannerRequestFailed = onFailed;
    BidMachineBannerRequestDelegateInstance.onBannerRequestExpired = onExpired;
    
    [bannerRequest performWithDelegate:BidMachineBannerRequestDelegateInstance];
}

void BannerViewRequestSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
    bannerRequest.priceFloors = array;
}

void BannerViewSetSize(int type){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
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
}

void BannerViewSetBidPayload(const char *value){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    bannerRequest.bidPayload = [NSString stringWithUTF8String:value];
}

void BannerViewSetPlacementId(const char *value){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    bannerRequest.placementId = [NSString stringWithUTF8String:value];
}

void BannerViewSetLoadingTimeOut(int value){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    bannerRequest.timeout = [NSNumber numberWithInt:value];
}

void BannerViewSetSessionAdParams(id<BDMContextualProtocol> value){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    bannerRequest.contextualData = value;
}

BOOL BannerViewAdCanShow(){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    return bannerView.canShow;
}

void BannerViewDestroy(){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    if(!bannerViewRequests) bannerViewRequests = [[NSMutableSet alloc ]init];
    if([bannerViewRequests containsObject: bannerView]) [bannerViewRequests removeObject:bannerView];
    [bannerView removeFromSuperview];
    bannerView = nil;
}

void BannerViewLoad(BDMInterstitialRequest *interstitialRequest){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    if(!bannerViewRequests){
        bannerViewRequests = [[NSMutableSet alloc ]init];
    }
    [bannerView populateWithRequest:bannerRequest];
    
    [bannerViewRequests addObject:bannerView];
}

void BannerViewShow(int YAxis, int XAxis){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    
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
}

int GetBannerSize(){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
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
}


void BannerViewSetDelegate(BidMachineBannerCallback onAdLoaded,
                           BidMachineBannerFailedCallback onAdLoadFailed,
                           BidMachineBannerCallback onAdShown,
                           BidMachineBannerCallback onAdImpression,
                           BidMachineBannerCallback onAdClicked,
                           BidMachineBannerCallback onAdExpired){
    
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    
    if(!BidMachineBannerViewDelegateInstance){
        BidMachineBannerViewDelegateInstance = [BidMachineBannerViewDelegate new];
    }
    
    BidMachineBannerViewDelegateInstance.onAdLoaded = onAdLoaded;
    BidMachineBannerViewDelegateInstance.onAdLoadFailed = onAdLoadFailed;
    BidMachineBannerViewDelegateInstance.onAdShown = onAdShown;
    BidMachineBannerViewDelegateInstance.onAdImpression = onAdImpression;
    BidMachineBannerViewDelegateInstance.onAdClicked = onAdClicked;
    BidMachineBannerViewDelegateInstance.onAdExpired = onAdExpired;
    
    bannerView.delegate = BidMachineBannerViewDelegateInstance;
    bannerView.producerDelegate = BidMachineBannerViewDelegateInstance;
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
        BidMachineNativeRequestDelegateInstance = [BidMachineNativeRequestDelegate new];
    }
    
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    
    BidMachineNativeRequestDelegateInstance.onNativeRequestSuccess = onSuccess;
    BidMachineNativeRequestDelegateInstance.onNativeRequestFailed = onFailed;
    BidMachineNativeRequestDelegateInstance.onNativeRequestExpired = onExpired;
    
    [nativeRequest performWithDelegate:BidMachineNativeRequestDelegateInstance];
    
}

void NativeAdSetDelegate(BidMachineNativeCallback onAdLoaded,
                         BidMachineNativeFailedCallback onAdLoadFailed,
                         BidMachineNativeCallback onAdShown,
                         BidMachineNativeCallback onAdClicked,
                         BidMachineNativeCallback onAdImpression,
                         BidMachineNativeCallback onAdExpired){
    
    if (!native) {
        native = [BDMNativeAd new];
    }
    
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
}

void NativeSetPriceFloor(BDMPriceFloor *bdmPriceFloor){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
    nativeRequest.priceFloors = array;
}

void NativeSetBidPayload(const char *value){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    nativeRequest.bidPayload = [NSString stringWithUTF8String:value];
}

void NativeSetPlacementId(const char *value){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    nativeRequest.placementId = [NSString stringWithUTF8String:value];
}

void NativeSetLoadingTimeOut(int value){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    nativeRequest.timeout = [NSNumber numberWithInt:value];
}

void NativeSetSessionAdParams(id<BDMContextualProtocol> value){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    nativeRequest.contextualData = value;
}

char * GetNativeTitle(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    const char *cString = [native.title UTF8String];
    char *cStringCopy = calloc([native.title length]+1, 1);
    return strncpy(cStringCopy, cString, [native.title length]);
    
}

char * GetNativeDescription(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    NSLog(@" Native description: %@", native.description);
  
    const char *cString = [native.description UTF8String];
    char *cStringCopy = calloc([native.description length]+1, 1);
    return strncpy(cStringCopy, cString, [native.description length]);
}

char * GetNativeCallToAction(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    const char *cString = [native.CTAText UTF8String];
    char *cStringCopy = calloc([native.CTAText length]+1, 1);
    return strncpy(cStringCopy, cString, [native.CTAText length]);
    
}

float GetNativeRating(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    float floatNativeStar = [native.starRating floatValue];
    return floatNativeStar;
}

char * GetNativeImage(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    const char *cString = [native.mainImageUrl UTF8String];
    char *cStringCopy = calloc([native.mainImageUrl length]+1, 1);
    return strncpy(cStringCopy, cString, [native.mainImageUrl length]);
}

char * GetNativeIcon(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    const char *cString = [native.iconUrl UTF8String];
    char *cStringCopy = calloc([native.iconUrl length]+1, 1);
    return strncpy(cStringCopy, cString, [native.iconUrl length]);
}

bool * NativeAdCanShow(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    return native.canShow;
}

void NativeAdDestroy(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    [native invalidate];
    if(!nativeRequests) nativeRequests = [[NSMutableSet alloc ]init];
    if([nativeRequests containsObject: native]) [nativeRequests removeObject:native];
}

void NativeAdLoad(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    if(!nativeRequests){
        nativeRequests = [[NSMutableSet alloc ]init];
    }
    
    [native makeRequest:nativeRequest];
    [nativeRequests addObject:native];
}

void NativeSetMediaAssetTypes(const char *value){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    
    nativeRequest.type = BDMNativeAdTypeIconAndImage;
}

char *GetNativeAuctionResult(){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    
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
}

char *GetBannerAuctionResult(){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
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
}

void DispatchClick(){
    if (!native) {
        native = [BDMNativeAd new];
    }

    [native trackUserInteraction];
}

void DispatchImpression(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    [native trackContainerAdded];
    [native trackImpression];
    [native trackViewable];
}

BDMNativeAd * GetNativeAd(){
    if (!native) {
        native = [BDMNativeAd new];
    }
    
    return native;
}

BDMNativeAdRequest * GetNativeRequest(){
    if (!nativeRequest) {
        nativeRequest = [BDMNativeAdRequest new];
    }
    
    return nativeRequest;
}

