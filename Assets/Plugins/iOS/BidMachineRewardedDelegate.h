#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineRewardedCallback) (BDMRewarded *rewarded);
typedef void (*BidMachineRewardedFailedCallback) (BDMRewarded *rewarded, NSError *error);
typedef void (*BidMachineRewardedClosedCallback) (BDMRewarded *rewarded);


@interface BidMachineRewardedDelegate: NSObject <BDMRewardedDelegate, BDMAdEventProducerDelegate>

@property (assign, nonatomic) BidMachineRewardedCallback onAdLoaded;
@property (assign, nonatomic) BidMachineRewardedFailedCallback onAdLoadFailed;
@property (assign, nonatomic) BidMachineRewardedCallback onAdShown;
@property (assign, nonatomic) BidMachineRewardedFailedCallback onAdShowFailed;
@property (assign, nonatomic) BidMachineRewardedCallback onAdImpression;
@property (assign, nonatomic) BidMachineRewardedCallback onAdClicked;
@property (assign, nonatomic) BidMachineRewardedCallback onAdRewarded;
@property (assign, nonatomic) BidMachineRewardedClosedCallback onAdClosed;
@property (assign, nonatomic) BidMachineRewardedClosedCallback onAdExpired;

@end
