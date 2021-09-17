#import "BidMachineBannerViewDelegate.h"

@implementation BidMachineBannerViewDelegate

BDMBannerView *bdmBannerView;

- (void)bannerView:(nonnull BDMBannerView *)bannerView failedWithError:(nonnull NSError *)error {
    if(self.onAdLoadFailed){
        if (bannerView) {
            bdmBannerView = bannerView;
        }
        self.onAdLoadFailed(bannerView, error);
    }
}

- (void)bannerViewReadyToPresent:(nonnull BDMBannerView *)bannerView {
    if(self.onAdLoaded){
        if (bannerView) {
            bdmBannerView = bannerView;
        }
        self.onAdLoaded(bannerView);
    }
}

- (void)bannerViewRecieveUserInteraction:(nonnull BDMBannerView *)bannerView {
    if(self.onAdClicked){
        if (bannerView) {
            bdmBannerView = bannerView;
        }
        self.onAdClicked(bannerView);
    }
}

- (void)bannerViewDidExpire:(BDMBannerView *)bannerView{
    if(self.onAdExpired){
        if (bannerView) {
            bdmBannerView = bannerView;
        }
        self.onAdExpired(bannerView);
    }
}

- (void)didProduceImpression:(nonnull id<BDMAdEventProducer>)producer{
    if (self.onAdImpression) {
        if (!bdmBannerView) {
            bdmBannerView = [BDMBannerView new];
        }
        self.onAdImpression(bdmBannerView);
    }
}

- (void)didProduceUserAction:(nonnull id<BDMAdEventProducer>)producer {
    
}



@end
