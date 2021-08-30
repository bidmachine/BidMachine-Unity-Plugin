#import "BidMachineRewardedDelegate.h"

@implementation BidMachineRewardedDelegate




- (void)rewarded:(nonnull BDMRewarded *)rewarded failedToPresentWithError:(nonnull NSError *)error {
    if(self.rewardedLoadFailed){
        self.rewardedLoadFailed(rewarded, error);
    }
}

- (void)rewarded:(nonnull BDMRewarded *)rewarded failedWithError:(nonnull NSError *)error {
    if(self.rewardedLoadFailed){
        self.rewardedLoadFailed(rewarded, error);
    }
}

- (void)rewardedReadyToPresent:(nonnull BDMRewarded *)rewarded {
    if(self.rewardedLoaded){
        self.rewardedLoaded(rewarded);
    }
}

- (void)rewardedDidDismiss:(nonnull BDMRewarded *)rewarded {
    if(self.rewardedClosed){
        self.rewardedClosed(rewarded);
    }
}

- (void)rewardedRecieveUserInteraction:(nonnull BDMRewarded *)rewarded {
    if(self.rewardedClicked){
        self.rewardedClicked(rewarded);
    }
}

- (void)rewardedWillPresent:(nonnull BDMRewarded *)rewarded {
    if(self.rewardedShown){
        self.rewardedShown(rewarded);
    }
}

- (void)rewardedFinishRewardAction:(nonnull BDMRewarded *)rewarded {
    if(self.rewardedFinished){
        self.rewardedFinished(rewarded);
    }
}

@end
