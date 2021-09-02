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

static BDMSdkConfiguration *configuration;
static BDMUserRestrictions *restrictions;
BDMTargeting *targeting;
BDMPriceFloor *priceFloor;
BDMUserGender *userGender;

BDMInterstitial *interstitial;
BDMRewarded * rewarded;
BDMBannerView * bannerView;

BDMRewardedRequest *rewardedRequest;
BDMInterstitialRequest *interstitialRequest;
BDMBannerRequest *bannerRequest;

NSMutableSet *rewardedRequests;
NSMutableSet *interstitialRequests;
NSMutableSet *bannerViewRequests;

static BidMachineInterstitialDelegate *BidMachineInterstitialDelegateInstance;
static BidMachineRewardedDelegate *BidMachineRewardedDelegateInstance;
static BidMachineBannerViewDelegate *BidMachineBannerViewDelegateInstance;

static UIViewController* RootViewController() {
    return ((UnityAppController *)[UIApplication sharedApplication].delegate).rootViewController;
}

void BidMachineInitialize(const char *sellerId) {
    //BDMSdk.sharedSdk.restrictions = restrictions;
    [BDMSdk.sharedSdk
     startSessionWithSellerID:[NSString stringWithUTF8String:sellerId]
     configuration:configuration
     completion:nil];
}

bool BidMachineIsInitialized(){
    return BDMSdk.sharedSdk.initialized;
}

void BidMachineSetEndpoint(const char *url){
    if (configuration == NULL) {
        configuration =[BDMSdkConfiguration new];
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
    if (!restrictions) {
        restrictions = [BDMUserRestrictions new];
    }
    restrictions.coppa = coppa;
}

void BidMachineSetUSPrivacyString(const char *USPrivacyString){
    if (!restrictions) {
        restrictions = [BDMUserRestrictions new];
    }
    restrictions.USPrivacyString =[NSString stringWithUTF8String:USPrivacyString];
}

void BidMachineSetPublisher(const char *id, const char *name, const char *domain, const char *categories){
    BDMSdk.sharedSdk.publisherInfo.publisherId = [NSString stringWithUTF8String:id];
    BDMSdk.sharedSdk.publisherInfo.publisherName =[NSString stringWithUTF8String:name];
    BDMSdk.sharedSdk.publisherInfo.publisherDomain =[NSString stringWithUTF8String:domain];
    NSString *nsCategories =[NSString stringWithUTF8String:categories];
    BDMSdk.sharedSdk.publisherInfo.publisherCategories = [nsCategories componentsSeparatedByString:@","];
}

void BidMachineSetGdprRequired(BOOL subjectToGDPR){
    if (!restrictions) {
        restrictions = [BDMUserRestrictions new];
    }
    restrictions.subjectToGDPR = subjectToGDPR;
}

void BidMachineSetConsentString(BOOL consent, const char *consentString){
    if (!restrictions) {
        restrictions = [BDMUserRestrictions new];
    }
    restrictions.hasConsent = consent;
    restrictions.consentString = [NSString stringWithUTF8String:consentString];
}

void BidMachineSetTargeting (){
    if (!configuration) {
        configuration = [BDMSdkConfiguration new];
    }
    configuration.targeting = targeting;
}

void TargetingSetUserId(const char *userId){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.userId = [NSString stringWithUTF8String:userId];
}

void TargetingSetGender(int *gender){
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
            break;
    }
    targeting.gender = kBDMUserGenderUnknown;
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

void TargetingSetBlockedCategories(const char *blockedCategories){
    NSString *list = [NSString stringWithUTF8String:blockedCategories];
    NSArray *blockedCategoriesArray = [list componentsSeparatedByString:@","];
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.blockedCategories = blockedCategoriesArray;
}

void TargetingSetBlockedAdvertisers(const char *blockedAdvertisers){
    NSString *list = [NSString stringWithUTF8String:blockedAdvertisers];
    NSArray *blockedAdvertisersArray = [list componentsSeparatedByString:@","];
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.blockedAdvertisers = blockedAdvertisersArray;
}

void TargetingSetBlockedApps(const char *blockedApps){
    NSString *list = [NSString stringWithUTF8String:blockedApps];
    NSArray *blockedAppsArray = [list componentsSeparatedByString:@","];
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.blockedApps = blockedAppsArray;
}

void TargetingSetDeviceLocation(double latitude, double longitude){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.deviceLocation =  [[CLLocation alloc] initWithLatitude:latitude longitude:longitude];
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

void TargetginSetStoreId(const char *storeId){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.storeId = [NSString stringWithUTF8String:storeId];
}

void TargetingSetPaid(BOOL paid){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    targeting.paid = paid;
}

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

BDMTargeting * GetTargeting(){
    if (!targeting){
        targeting = [BDMTargeting new];
    }
    return targeting;
}

void PriceFloorAddPrifeFloor(const char *priceFloorId, double value){
    if (!priceFloor) {
        priceFloor = [BDMPriceFloor new];
    }
    NSDecimalNumber *doubleDecimal = [[NSDecimalNumber alloc] initWithDouble:value];
    priceFloor.ID = [NSString stringWithUTF8String:priceFloorId];
    priceFloor.value = doubleDecimal;
}

BDMPriceFloor * GetPriceFloor(){
    if (!priceFloor) {
        priceFloor = [BDMPriceFloor new];
    }
    return priceFloor;
}

void InterstitialRequestSetTargeting(BDMTargeting *bdmTargeting){
    if (!interstitialRequest) {
        interstitialRequest = [BDMInterstitialRequest new];
    }
    //interstitialRequest.targeting = bdmTargeting;
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

void RewardedSetTargeting(BDMTargeting *bdmTargeting){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    
    
    rewardedRequest.targeting = bdmTargeting;
}

void RewardedSetPriceFlooor(BDMPriceFloor *bdmPriceFloor){
    if (!rewardedRequest) {
        rewardedRequest = [BDMRewardedRequest new];
    }
    NSArray<BDMPriceFloor *> *array = [[NSArray alloc] initWithObjects:bdmPriceFloor, nil];
    rewardedRequest.priceFloors = array;
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

void BannerViewRequestSetTargeting(BDMTargeting *bdmTargeting){
    if (!bannerRequest) {
        bannerRequest = [BDMBannerRequest new];
    }
    //bannerRequest.targeting = bdmTargeting;
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
            [[[bannerView bottomAnchor] constraintEqualToAnchor:RootViewController().view.bottomAnchor] setActive:YES];
            break;
        case 1:
            [[[bannerView topAnchor] constraintEqualToAnchor:RootViewController().view.topAnchor] setActive:YES];
            break;
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
