#import <Foundation/Foundation.h>
#import <BidMachine/BidMachine.h>

typedef void (*BidMachineRewardedCallbacks) (BDMRewarded *rewarded);
typedef void (*BidMachineRewardedFailedCallback) (BDMRewarded *rewarded, NSError *error);

@interface BidMachineRewardedDelegate: NSObject <BDMRewardedDelegate>

@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedLoaded;
@property (assign, nonatomic) BidMachineRewardedFailedCallback rewardedLoadFailed;
@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedShown;
@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedImpression;
@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedClicked;
@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedClosed;
@property (assign, nonatomic) BidMachineRewardedCallbacks rewardedFinished;

@end
