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

static BDMSdkConfiguration *configuration;
static BDMUserRestrictions *internalRestrictions;
static id<BDMContextualProtocol> contextualData;

static BDMTargeting *targeting;
static BDMPriceFloor *priceFloor;
static BDMUserGender *userGender;

static BDMInterstitial *interstitial;
static BDMRewarded * rewarded;
static BDMBannerView * bannerView;

static BDMRewardedRequest *rewardedRequest;
static BDMInterstitialRequest *interstitialRequest;
static BDMBannerRequest *bannerRequest;

NSMutableSet *rewardedRequests;
NSMutableSet *interstitialRequests;
NSMutableSet *bannerViewRequests;

static BidMachineBannerRequestDelegate *BidMachineBannerRequestDelegateInstance;
static BidMachineInterstitialRequestDelegate * BidMachineInterstitialRequestDelegateInstance;
static BidMachineRewardedRequestDelegate *BidMachineRewardedRequestDelegateInstance;

static BidMachineInterstitialDelegate *BidMachineInterstitialDelegateInstance;
static BidMachineRewardedDelegate *BidMachineRewardedDelegateInstance;
static BidMachineBannerViewDelegate *BidMachineBannerViewDelegateInstance;

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

void PriceFloorAddPrifeFloor(const char *priceFloorId, double value){
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

void BidMachineInitialize(const char *sellerId) {
    
    [BDMSdk sharedSdk].restrictions.coppa = internalRestrictions.coppa;
    [BDMSdk sharedSdk].restrictions.subjectToGDPR = internalRestrictions.subjectToGDPR;
    [BDMSdk sharedSdk].restrictions.hasConsent = internalRestrictions.hasConsent;
    [BDMSdk sharedSdk].restrictions.consentString = internalRestrictions.consentString;
    [BDMSdk sharedSdk].restrictions.USPrivacyString = internalRestrictions.USPrivacyString;
    
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
    NSString *urlEndPoint =[NSString stringWithUTF8String:url];
    configuration.baseURL = [NSURL URLWithString:urlEndPoint];
}

void BidMachineSetLogging(BOOL logging){
    BDMSdk.sharedSdk.enableLogging = logging;
}

void BidMachineSetTestMode(BOOL testing){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.testMode = testing;
}

void BidMachineSetCoppa (BOOL coppa){
    if (!internalRestrictions) {
        internalRestrictions = [BDMUserRestrictions new];
    }
    internalRestrictions.coppa = coppa;
}

void BidMachineSetUSPrivacyString(const char *USPrivacyString){
    if (!internalRestrictions) {
        internalRestrictions = [BDMUserRestrictions new];
    }
    internalRestrictions.USPrivacyString =[NSString stringWithUTF8String:USPrivacyString];
}

void BidMachineSetPublisher(const char *id, const char *name, const char *domain, const char *categories){
    BDMSdk.sharedSdk.publisherInfo.publisherId = [NSString stringWithUTF8String:id];
    BDMSdk.sharedSdk.publisherInfo.publisherName =[NSString stringWithUTF8String:name];
    BDMSdk.sharedSdk.publisherInfo.publisherDomain =[NSString stringWithUTF8String:domain];
    NSString *nsCategories =[NSString stringWithUTF8String:categories];
    BDMSdk.sharedSdk.publisherInfo.publisherCategories = [nsCategories componentsSeparatedByString:@","];
}

void BidMachineSetGdprRequired(BOOL subjectToGDPR){
    if (!internalRestrictions) {
        internalRestrictions = [BDMUserRestrictions new];
    }
    internalRestrictions.subjectToGDPR = subjectToGDPR;
}

void BidMachineSetConsentString(BOOL consent, const char *consentString){
    if (!internalRestrictions) {
        internalRestrictions = [BDMUserRestrictions new];
    }
    internalRestrictions.hasConsent = consent;
    internalRestrictions.consentString = [NSString stringWithUTF8String:consentString];
}

void BidMachineSetTargeting (){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.targeting = targeting;
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



id<BDMContextualProtocol> GetSessionAdParams(){
    return contextualData;
}

void SetSessionDuration(int value){
    contextualData.sessionDuration = value;
}

void SetImpressionCount(int value){
    contextualData.impressions = value;
}

void SetClickRate(int value){
    contextualData.clickRate = value;
}

void SetLastAdomain(const char *value){
    contextualData.lastAdomain = [NSString stringWithUTF8String:value];
}

void SetCompletionRate(int value){
    contextualData.completionRate = value;
}

void SetLastClickForImpression(int value){
    contextualData.completionRate = value;
}

void SetLastBundle(const char *value){
    contextualData.lastBundle =[NSString stringWithUTF8String:value];
}






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

void SetBannerRequestDelegate(BannerRequestSuccessCallback onSuccess,
                              BannerRequestFailedCallback onFailed,
                              BannerRequestExpiredCallback onExpired){
    
    if (!BidMachineBannerRequestDelegateInstance) {
        BidMachineBannerRequestDelegateInstance = [BidMachineBannerRequestDelegate new];
    }
    
    BidMachineBannerRequestDelegateInstance.onBannerRequestSuccess = onSuccess;
    BidMachineBannerRequestDelegateInstance.onBannerRequestFailed = onFailed;
    BidMachineBannerRequestDelegateInstance.onBannerRequestExpired = onExpired;
    
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
    [bannerRequest performWithDelegate:BidMachineBannerRequestDelegateInstance];
}

void SetInterstitialRequestDelegate(InterstitialRequestSuccessCallback onSuccess,
                                    InterstitialRequestFailedCallback onFailed,
                                    InterstitialRequestExpiredCallback onExpired){
    
    if (!BidMachineInterstitialRequestDelegateInstance) {
        BidMachineInterstitialRequestDelegateInstance = [BidMachineInterstitialRequestDelegate new];
    }
    
    BidMachineInterstitialRequestDelegateInstance.onIntersittialRequestSuccess = onSuccess;
    
    BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestFailed = onFailed;
    
    BidMachineInterstitialRequestDelegateInstance.onInterstitialRequestExpired = onExpired;
    
    
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    
    [interstitialRequest performWithDelegate:BidMachineInterstitialRequestDelegateInstance];
}

void SetRewardedRequestDelegate(RewardedRequestSuccessCallback onSuccess,
                                RewardedRequestFailedCallback onFailed,
                                RewardedRequestExpiredCallback onExpired){
    
    if (!BidMachineRewardedRequestDelegateInstance) {
        BidMachineRewardedRequestDelegateInstance = [BidMachineRewardedRequestDelegate new];
    }
    
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestSuccess = onSuccess;
    
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestFailed = onFailed;
    
    BidMachineRewardedRequestDelegateInstance.onRewardedRequestExpired = onExpired;
    
    
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    
    [rewardedRequest performWithDelegate:BidMachineRewardedRequestDelegateInstance];
}

void InterstitialAdSetDelegate(BidMachineInterstitialCallbacks onAdLoaded,
                               BidMachineInterstitialFailedCallback onAdLoadFailed,
                               BidMachineInterstitialCallbacks onAdShown,
                               BidMachineInterstitialCallbacks onAdClicked,
                               BidMachineInterstitialCallbacks onAdClosed){
    if (!interstitial) {
        interstitial = [BDMInterstitial new];
    }
    if(!BidMachineInterstitialDelegateInstance){
        BidMachineInterstitialDelegateInstance = [BidMachineInterstitialDelegate new];
        BidMachineInterstitialDelegateInstance.interstitialLoaded = onAdLoaded;
        BidMachineInterstitialDelegateInstance.interstitialLoadFailed = onAdLoadFailed;
        BidMachineInterstitialDelegateInstance.interstitialShown = onAdShown;
        BidMachineInterstitialDelegateInstance.interstitialClicked = onAdClicked;
        BidMachineInterstitialDelegateInstance.interstitialClosed = onAdClosed;
        
    }
    interstitial.delegate = BidMachineInterstitialDelegateInstance;
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

BDMRewardedRequest * GetRewardedRequest(){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    return rewardedRequest;
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

void RewardedSetDelegate(BidMachineRewardedCallbacks onAdLoaded,
                         BidMachineRewardedFailedCallback onAdLoadFailed,
                         BidMachineRewardedCallbacks onAdClosed,
                         BidMachineRewardedCallbacks onAdClicked,
                         BidMachineRewardedCallbacks onAdShown){
    if (!rewarded) {
        rewarded = [BDMRewarded new];
    }
    
    if(!BidMachineRewardedDelegateInstance){
        BidMachineRewardedDelegateInstance = [BidMachineRewardedDelegate new];
        BidMachineRewardedDelegateInstance.rewardedLoaded = onAdLoaded;
        BidMachineRewardedDelegateInstance.rewardedLoadFailed = onAdLoadFailed;
        BidMachineRewardedDelegateInstance.rewardedShown = onAdShown;
        BidMachineRewardedDelegateInstance.rewardedClicked = onAdClicked;
        BidMachineRewardedDelegateInstance.rewardedClosed = onAdClosed;
    }
    interstitial.delegate = BidMachineInterstitialDelegateInstance;
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

BDMBannerRequest * GetBannerViewRequest(){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    
    return bannerRequest;
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

void BannerViewSetDelegate(BidMachineBannerViewCallbacks onAdLoaded,
                           BidMachineBannerViewFailedCallback onAdLoadFailed,
                           BidMachineBannerViewCallbacks onAdClicked){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    
    if(!BidMachineBannerViewDelegateInstance){
        BidMachineBannerViewDelegateInstance = [BidMachineBannerViewDelegate new];
        BidMachineBannerViewDelegateInstance.bannerViewLoaded = onAdLoaded;
        BidMachineBannerViewDelegateInstance.bannerViewLoadFailed = onAdLoadFailed;
        BidMachineBannerViewDelegateInstance.bannerViewClicked = onAdClicked;
    }
    bannerView.delegate = BidMachineBannerViewDelegateInstance;
}

void BannerViewShow(int YAxis, int XAxis){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    
    [RootViewController().view addSubview:bannerView];
    [bannerView setTranslatesAutoresizingMaskIntoConstraints:NO];
    
    switch (YAxis) {
        case 2:
            if (@available(iOS 11.0, *)) {
                [[[[bannerView safeAreaLayoutGuide] bottomAnchor] constraintEqualToAnchor:RootViewController().view.safeAreaLayoutGuide.bottomAnchor] setActive:YES];
            } else {
                [[[bannerView bottomAnchor] constraintEqualToAnchor:RootViewController().view.bottomAnchor] setActive:YES];
                break;
            }
            break;
        case 1:
            if (@available(iOS 11.0, *)) {
                [[[[bannerView safeAreaLayoutGuide] topAnchor]
                  constraintEqualToAnchor:RootViewController().view.safeAreaLayoutGuide.topAnchor] setActive:YES];
                break;
            } else {
                [[[bannerView topAnchor] constraintEqualToAnchor:RootViewController().view.topAnchor] setActive:YES];
                break;
            }
    }
    
    switch (XAxis) {
        case -2:
            [[[bannerView centerXAnchor] constraintEqualToAnchor:RootViewController().view.centerXAnchor] setActive:YES];
            break;
        case -3:
            [[[bannerView rightAnchor] constraintEqualToAnchor:RootViewController().view.rightAnchor] setActive:YES];
            break;
        case -4:
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

BDMBannerView * GetBannerView(){
    if (!bannerView) {
        bannerView = [BDMBannerView new];
    }
    return bannerView;
}
