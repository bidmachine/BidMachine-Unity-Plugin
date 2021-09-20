#import "BidMachineNativeDelegate.h"

@implementation BidMachineNativeDelegate

BDMNativeAd *bdmNativeAd;

 
- (void)nativeAd:(nonnull BDMNativeAd *)nativeAd failedWithError:(nonnull NSError *)error {
    if (self.onAdLoadFailed) {
        bdmNativeAd = nativeAd;
        self.onAdLoadFailed(nativeAd, error);
    }
}
 
- (void)nativeAd:(nonnull BDMNativeAd *)nativeAd readyToPresentAd:(nonnull BDMAuctionInfo *)auctionInfo {
    if (self.onAdLoaded) {
        bdmNativeAd = nativeAd;
        self.onAdLoaded(nativeAd);
    }
}


- (void)didProduceImpression:(nonnull id<BDMAdEventProducer>)producer {
    if(self.onAdImpression){
        if (!bdmNativeAd) {
            bdmNativeAd = [BDMNativeAd new];
        }
        self.onAdImpression(bdmNativeAd);
    }
}

- (void)didProduceUserAction:(nonnull id<BDMAdEventProducer>)producer {
    
}

@end

