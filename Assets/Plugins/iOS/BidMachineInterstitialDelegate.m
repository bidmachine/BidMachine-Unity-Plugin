#import "BidMachineInterstitialDelegate.h"

@implementation BidMachineInterstitialDelegate

- (void)interstitial:(BDMInterstitial *)interstitial failedWithError:(NSError *)error {
    if(self.interstitialLoadFailed){
        self.interstitialLoadFailed(interstitial, error);
    }
}

- (void)interstitial:(BDMInterstitial *)interstitial failedToPresentWithError:(NSError *)error {
    if(self.interstitialLoadFailed){
        self.interstitialLoadFailed(interstitial, error);
    }
}

- (void)interstitialReadyToPresent:(nonnull BDMInterstitial *)interstitial {
    if(self.interstitialLoaded){
        self.interstitialLoaded(interstitial);
    }
}

- (void)interstitialDidDismiss:(nonnull BDMInterstitial *)interstitial {
    if(self.interstitialClosed){
        self.interstitialClosed(interstitial);
    }
}

- (void)interstitialRecieveUserInteraction:(nonnull BDMInterstitial *)interstitial {
    if(self.interstitialClicked){
        self.interstitialClicked(interstitial);
    }
}

- (void)interstitialWillPresent:(nonnull BDMInterstitial *)interstitial {
    if(self.interstitialShown){
        self.interstitialShown(interstitial);
    }
}

@end
