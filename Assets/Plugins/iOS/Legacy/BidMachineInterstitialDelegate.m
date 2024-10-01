// #import "BidMachineInterstitialDelegate.h"

// @implementation BidMachineInterstitialDelegate

// BDMInterstitial *bdmInterstitial;


// - (void)interstitial:(BDMInterstitial *)interstitial failedWithError:(NSError *)error {
//     if(self.onAdLoadFailed){
//         if(interstitial){
//             bdmInterstitial = interstitial;
//         }
//         self.onAdLoadFailed(interstitial, error);
//     }
// }

// - (void)interstitial:(BDMInterstitial *)interstitial failedToPresentWithError:(NSError *)error {
//     if(self.onAdShowFailed){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdShowFailed(interstitial, error);
//     }
// }

// - (void)interstitialReadyToPresent:(nonnull BDMInterstitial *)interstitial {
//     if(self.onAdLoaded){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdLoaded(interstitial);
//     }
// }

// - (void)interstitialDidDismiss:(nonnull BDMInterstitial *)interstitial {
//     if(self.onAdClosed){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdClosed(interstitial, true);
//     }
// }

// - (void)interstitialRecieveUserInteraction:(nonnull BDMInterstitial *)interstitial {
//     if(self.onAdClicked){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdClicked(interstitial);
//     }
// }

// - (void)interstitialWillPresent:(nonnull BDMInterstitial *)interstitial {
//     if(self.onAdShown){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdShown(interstitial);
//     }
// }

// - (void)interstitialDidExpire:(BDMInterstitial *)interstitial{
//     if(self.onAdExpired){
//         if (interstitial) {
//             bdmInterstitial = interstitial;
//         }
//         self.onAdExpired(interstitial);
//     }
// }

// - (void)didProduceImpression:(nonnull id<BDMAdEventProducer>)producer{
//     if (self.onAdImpression) {
//         if (!bdmInterstitial) {
//             bdmInterstitial = [BDMInterstitial new];
//         }
//         self.onAdImpression(bdmInterstitial);
//     }
// }

// - (void)didProduceUserAction:(nonnull id<BDMAdEventProducer>)producer {
    
// }

// @end
