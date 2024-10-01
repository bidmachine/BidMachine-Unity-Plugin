//
//  BidMachineRewardedAdDelegate.swift
//  UnityFramework
//
//  Created by Dzmitry on 27/09/2024.
//

import Foundation
import BidMachine

final class BidMachineRewardedAdHandler: NSObject {
    typealias SuccessCallback = (BidMachineRewarded) -> Void
    typealias FailureCallback = (BidMachineRewarded?, Error?) -> Void
    typealias ClosedCallback = (BidMachineRewarded) -> Void
    
    private var rewarded: BidMachineRewarded?
    
    // MARK: - Success
    
    var onAdLoaded: SuccessCallback?
    var onAdShown: SuccessCallback?
    var onAdImpression: SuccessCallback?
    var onAdClicked: SuccessCallback?
    var onAdRewarded: SuccessCallback?
    
    // MARK: - Failure
    
    var onAdLoadFailed: FailureCallback?
    var onAdShowFailed: FailureCallback?
    
    // MARK: - Close

    var onAdClosed: ClosedCallback?
    var onAdExpired: ClosedCallback?
}

extension BidMachineRewardedAdHandler: BidMachineAdDelegate {
    func didFailPresentAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        guard let onAdShowFailed else {
            return
        }
        self.rewarded = ad as? BidMachineRewarded
        onAdShowFailed(rewarded, error)
    }
    
    func didFailLoadAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        guard let onAdLoadFailed else {
            return
        }
        self.rewarded = ad as? BidMachineRewarded
        onAdLoadFailed(rewarded, error)
    }

    func didLoadAd(_ ad: any BidMachine.BidMachineAdProtocol) {
        guard let onAdLoaded, let rewarded = ad as? BidMachineRewarded else {
            return
        }
        self.rewarded = rewarded
        onAdLoaded(rewarded)
    }
    
    func didDismissAd(_ ad: any BidMachineAdProtocol) {
        guard let onAdClosed, let rewarded = ad as? BidMachineRewarded  else {
            return
        }
        self.rewarded = rewarded
        onAdClosed(rewarded)
    }
    
    func didUserInteraction(_ ad: any BidMachineAdProtocol) {
    }
    
    func willPresentScreen(_ ad: any BidMachineAdProtocol) {
    }
    
    func didReceiveReward(_ ad: any BidMachineAdProtocol) {
    }
    
    func didTrackImpression(_ ad: any BidMachineAdProtocol) {
    }
}
