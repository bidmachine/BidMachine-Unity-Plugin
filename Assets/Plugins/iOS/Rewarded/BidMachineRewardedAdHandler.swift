//
//  BidMachineRewardedAdHandler.swift
//  UnityFramework
//
//  Created by Dzmitry on 27/09/2024.
//

import Foundation
import BidMachine

final class BidMachineRewardedAdHandler: NSObject {
    typealias SuccessCallback = (_ ad: BidMachineRewarded, _ auctionResult: String) -> Void
    typealias FailureCallback = (_ ad: BidMachineRewarded? ,_ error: Error?) -> Void
    typealias ClosedCallback = (_ ad: BidMachineRewarded) -> Void
    
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
        let rewarded = ad as? BidMachineRewarded
        onAdShowFailed(rewarded, error)
    }
    
    func didFailLoadAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        guard let onAdLoadFailed else {
            return
        }
        let rewarded = ad as? BidMachineRewarded
        onAdLoadFailed(rewarded, error)
    }

    func didLoadAd(_ ad: any BidMachine.BidMachineAdProtocol) {
        guard let onAdLoaded, let rewarded = ad as? BidMachineRewarded else {
            return
        }
        #warning("what is auction result")
        let bidId = rewarded.auctionInfo.bidId
        onAdLoaded(rewarded, bidId)
        
    }
    
    func didDismissAd(_ ad: any BidMachineAdProtocol) {
        guard let onAdClosed, let rewarded = ad as? BidMachineRewarded  else {
            return
        }
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

public typealias RewardedCallbackSuccess = @convention(c) (UnsafeMutableRawPointer, UnsafePointer<CChar>?) -> Void
public typealias RewardedCallbackFailure = @convention(c) (UnsafeMutableRawPointer?, UnsafePointer<CChar>) -> Void
public typealias RewardedCallbackClose = @convention(c) (UnsafeMutableRawPointer?, UnsafePointer<CChar>) -> Void
