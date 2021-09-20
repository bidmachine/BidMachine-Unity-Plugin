#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineNativeCallback) (BDMNativeAd *nativeAd);
typedef void (*BidMachineNativeFailedCallback) (BDMNativeAd *nativeAd, NSError *error);

@interface BidMachineNativeDelegate: NSObject <BDMNativeAdDelegate, BDMAdEventProducerDelegate>

@property (assign, nonatomic) BidMachineNativeCallback onAdLoaded;
@property (assign, nonatomic) BidMachineNativeFailedCallback onAdLoadFailed;
@property (assign, nonatomic) BidMachineNativeCallback onAdShown;
@property (assign, nonatomic) BidMachineNativeCallback onAdImpression;
@property (assign, nonatomic) BidMachineNativeCallback onAdClicked;
@property (assign, nonatomic) BidMachineNativeCallback onAdExpired;

@end
