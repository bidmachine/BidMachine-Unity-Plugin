#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BannerRequestSuccessCallback) (BDMRequest *request, const char *auctionInfo);
typedef void (*BannerRequestFailedCallback) (BDMRequest *request, NSError *error);
typedef void (*BannerRequestExpiredCallback) (BDMRequest *request);


@interface BidMachineBannerRequestDelegate: NSObject <BDMRequestDelegate>

@property (assign, nonatomic) BannerRequestSuccessCallback onBannerRequestSuccess;
@property (assign, nonatomic) BannerRequestFailedCallback onBannerRequestFailed;
@property (assign, nonatomic) BannerRequestExpiredCallback onBannerRequestExpired;

@end
