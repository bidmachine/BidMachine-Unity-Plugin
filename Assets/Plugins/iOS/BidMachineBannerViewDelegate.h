#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineBannerViewCallbacks) (BDMBannerView *nativeAd);
typedef void (*BidMachineBannerViewFailedCallback) (BDMBannerView *nativeAd, NSError *error);

@interface BidMachineBannerViewDelegate: NSObject <BDMBannerDelegate>

@property (assign, nonatomic) BidMachineBannerViewCallbacks bannerViewLoaded;
@property (assign, nonatomic) BidMachineBannerViewFailedCallback bannerViewLoadFailed;
@property (assign, nonatomic) BidMachineBannerViewCallbacks bannerViewClicked;

@end
