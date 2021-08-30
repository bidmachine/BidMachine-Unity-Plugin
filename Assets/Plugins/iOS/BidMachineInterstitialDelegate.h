#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineInterstitialCallbacks) (BDMInterstitial *interstitial);
typedef void (*BidMachineInterstitialFailedCallback) (BDMInterstitial *interstitial, NSError *error);

@interface BidMachineInterstitialDelegate: NSObject <BDMInterstitialDelegate>

@property (assign, nonatomic) BidMachineInterstitialCallbacks interstitialLoaded;
@property (assign, nonatomic) BidMachineInterstitialFailedCallback interstitialLoadFailed;
@property (assign, nonatomic) BidMachineInterstitialCallbacks interstitialShown;
@property (assign, nonatomic) BidMachineInterstitialCallbacks interstitialClosed;
@property (assign, nonatomic) BidMachineInterstitialCallbacks interstitialClicked;

@end
