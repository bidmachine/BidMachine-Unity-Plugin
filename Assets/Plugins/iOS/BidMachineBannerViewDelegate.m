#import "BidMachineBannerViewDelegate.h"

@implementation BidMachineBannerViewDelegate

- (void)bannerView:(nonnull BDMBannerView *)bannerView failedWithError:(nonnull NSError *)error {
    if(self.bannerViewLoadFailed){
        self.bannerViewLoadFailed(bannerView, error);
    }
}

- (void)bannerViewReadyToPresent:(nonnull BDMBannerView *)bannerView {
    if(self.bannerViewLoaded){
        self.bannerViewLoaded(bannerView);
    }
}

- (void)bannerViewRecieveUserInteraction:(nonnull BDMBannerView *)bannerView {
    if(self.bannerViewClicked){
        self.bannerViewClicked(bannerView);
    }
}
@end
