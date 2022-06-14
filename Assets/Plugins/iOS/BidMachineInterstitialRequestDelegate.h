#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*InterstitialRequestSuccessCallback) (BDMInterstitialRequest *request, const char *auctionInfo);
typedef void (*InterstitialRequestFailedCallback) (BDMInterstitialRequest *request, NSError *error);
//typedef void (*InterstitialRequestExpiredCallback) (BDMInterstitialRequest *request);


@interface BidMachineInterstitialRequestDelegate: NSObject <BDMRequestDelegate>

@property (assign, nonatomic) InterstitialRequestSuccessCallback onIntersittialRequestSuccess;
@property (assign, nonatomic) InterstitialRequestFailedCallback onInterstitialRequestFailed;
//@property (assign, nonatomic) InterstitialRequestExpiredCallback onInterstitialRequestExpired;

@end
