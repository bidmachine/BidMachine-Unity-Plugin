#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineInterstitialCallbacks) (BDMInterstitial *interstitial);
typedef void (*BidMachineInterstitialFailedCallback) (BDMInterstitial *interstitial, NSError *error);
typedef void (*BidMachineInterstitialClosedCallback) (BDMInterstitial *interstitial, bool finished);

@interface BidMachineInterstitialDelegate: NSObject <BDMInterstitialDelegate, BDMAdEventProducerDelegate>

@property (assign, nonatomic) BidMachineInterstitialCallbacks onAdLoaded;
@property (assign, nonatomic) BidMachineInterstitialFailedCallback onAdLoadFailed;
@property (assign, nonatomic) BidMachineInterstitialCallbacks onAdShown;
@property (assign, nonatomic) BidMachineInterstitialFailedCallback onAdShowFailed;
@property (assign, nonatomic) BidMachineInterstitialCallbacks onAdImpression;
@property (assign, nonatomic) BidMachineInterstitialCallbacks onAdClicked;
@property (assign, nonatomic) BidMachineInterstitialClosedCallback onAdClosed;
@property (assign, nonatomic) BidMachineInterstitialCallbacks onAdExpired;

@end




