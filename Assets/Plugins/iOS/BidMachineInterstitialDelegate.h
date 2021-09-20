#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineInterstitialCallback) (BDMInterstitial *interstitial);
typedef void (*BidMachineInterstitialFailedCallback) (BDMInterstitial *interstitial, NSError *error);
typedef void (*BidMachineInterstitialClosedCallback) (BDMInterstitial *interstitial, bool finished);

@interface BidMachineInterstitialDelegate: NSObject <BDMInterstitialDelegate, BDMAdEventProducerDelegate>

@property (assign, nonatomic) BidMachineInterstitialCallback onAdLoaded;
@property (assign, nonatomic) BidMachineInterstitialFailedCallback onAdLoadFailed;
@property (assign, nonatomic) BidMachineInterstitialCallback onAdShown;
@property (assign, nonatomic) BidMachineInterstitialFailedCallback onAdShowFailed;
@property (assign, nonatomic) BidMachineInterstitialCallback onAdImpression;
@property (assign, nonatomic) BidMachineInterstitialCallback onAdClicked;
@property (assign, nonatomic) BidMachineInterstitialClosedCallback onAdClosed;
@property (assign, nonatomic) BidMachineInterstitialCallback onAdExpired;

@end
