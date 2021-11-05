#import "BidMachineRewardedDelegate.h"

@implementation BidMachineRewardedDelegate

BDMRewarded *bdmRewarded;

- (void)rewarded:(nonnull BDMRewarded *)rewarded failedToPresentWithError:(nonnull NSError *)error {
    if(self.onAdShowFailed){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdShowFailed(rewarded, error);
    }
}

- (void)rewarded:(nonnull BDMRewarded *)rewarded failedWithError:(nonnull NSError *)error {
    if(self.onAdLoadFailed){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdLoadFailed(rewarded, error);
    }
}

- (void)rewardedReadyToPresent:(nonnull BDMRewarded *)rewarded {
    if(self.onAdLoaded){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdLoaded(rewarded);
    }
}

- (void)rewardedDidDismiss:(nonnull BDMRewarded *)rewarded {
    if(self.onAdClosed){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdClosed(rewarded);
    }
}

- (void)rewardedRecieveUserInteraction:(nonnull BDMRewarded *)rewarded {
    if(self.onAdClicked){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdClicked(rewarded);
    }
}

- (void)rewardedWillPresent:(nonnull BDMRewarded *)rewarded {
    if(self.onAdShown){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdShown(rewarded);
    }
}

- (void)rewardedFinishRewardAction:(nonnull BDMRewarded *)rewarded {
    if(self.onAdRewarded){
        if (rewarded) {
            bdmRewarded = rewarded;
        }
        self.onAdRewarded(rewarded);
    }
}

- (void)didProduceImpression:(nonnull id<BDMAdEventProducer>)producer {
    if(self.onAdImpression){
        if (!bdmRewarded) {
            bdmRewarded = [BDMRewarded new];
        }
        self.onAdImpression(bdmRewarded);
    }
}

- (void)didProduceUserAction:(nonnull id<BDMAdEventProducer>)producer {
    
}

@end
