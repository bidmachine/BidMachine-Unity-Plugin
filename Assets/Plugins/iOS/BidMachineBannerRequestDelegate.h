#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BannerRequestSuccessCallback) (BDMBannerRequest *request, const char *auctionInfo);
typedef void (*BannerRequestFailedCallback) (BDMBannerRequest *request, NSError *error);
//typedef void (*BannerRequestExpiredCallback) (BDMBannerRequest *request);


@interface BidMachineBannerRequestDelegate: NSObject <BDMRequestDelegate>

@property (assign, nonatomic) BannerRequestSuccessCallback onBannerRequestSuccess;
@property (assign, nonatomic) BannerRequestFailedCallback onBannerRequestFailed;
//@property (assign, nonatomic) BannerRequestExpiredCallback onBannerRequestExpired;

@end
