#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*RewardedRequestSuccessCallback) (BDMRewardedRequest *request, const char *auctionInfo);
typedef void (*RewardedRequestFailedCallback) (BDMRewardedRequest *request, NSError *error);
typedef void (*RewardedRequestExpiredCallback) (BDMRewardedRequest *request);


@interface BidMachineRewardedRequestDelegate: NSObject <BDMRequestDelegate>

@property (assign, nonatomic) RewardedRequestSuccessCallback onRewardedRequestSuccess;
@property (assign, nonatomic) RewardedRequestFailedCallback onRewardedRequestFailed;
@property (assign, nonatomic) RewardedRequestExpiredCallback onRewardedRequestExpired;

@end
