//
//  BidMachineRewardedAdHandler.swift
//  UnityFramework
//
//  Created by Dzmitry on 27/09/2024.
//

import Foundation
import BidMachine
import Combine

final class BidMachineRewardedAdHandler: NSObject {
    typealias SuccessCallback = (_ ad: BidMachineRewarded, _ auctionResult: String) -> Void
    typealias FailureCallback = (_ ad: BidMachineRewarded? ,_ error: Error?) -> Void
    typealias ClosedCallback = (_ ad: BidMachineRewarded) -> Void
    
    private let adRequestEventsManager: AdRequestEventsManager
    
    init(adRequestEventsManager: AdRequestEventsManager) {
        self.adRequestEventsManager = adRequestEventsManager
    }
    
    // MARK: - Success
    
    struct LoadResult {
        let ad: BidMachineRewarded
        let auctionResult: String
    }
    
    struct LoadError: Error {
        let ad: BidMachineRewarded?
        let error: Error
    }
    
    var loadingPublisher: AnyPublisher<LoadResult, LoadError> {
        loadSubject.eraseToAnyPublisher()
    }
    
    private let loadSubject = PassthroughSubject<LoadResult, LoadError>()
    
    var didLoadClosure: SuccessCallback?
    var didShownClosure: SuccessCallback?
    var didReceiveImpressionClosure: SuccessCallback?
    var didClickedClosure: SuccessCallback?
    var didRewardedClosure: SuccessCallback?
    
    // MARK: - Failure
    
    var didFailLoadClosure: FailureCallback?
    var didFailShowClosure: FailureCallback?
    
    // MARK: - Close

    var didClosedClosure: ClosedCallback?
    var didExpiredClosure: ClosedCallback?
}

extension BidMachineRewardedAdHandler: BidMachineAdDelegate {
    func didFailPresentAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        let rewarded = ad as? BidMachineRewarded
        didFailShowClosure?(rewarded, error)
    }
    
    func didFailLoadAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        let rewarded = ad as? BidMachineRewarded
        didFailLoadClosure?(rewarded, error)
        
        let error = LoadError(ad: rewarded, error: error)
        
        loadSubject.send(completion: .failure(error))
    }

    func didLoadAd(_ ad: any BidMachine.BidMachineAdProtocol) {
        guard let rewarded = ad as? BidMachineRewarded else {
            return
        }
        #warning("what is auction result?")
        let bidId = rewarded.auctionInfo.bidId
        let result = LoadResult(ad: rewarded, auctionResult: bidId)

        loadSubject.send(result)
    }
    
    func didDismissAd(_ ad: any BidMachineAdProtocol) {
        guard let rewarded = ad as? BidMachineRewarded  else {
            return
        }
        didClosedClosure?(rewarded)
    }
    
    func didUserInteraction(_ ad: any BidMachineAdProtocol) {
        guard let rewarded = ad as? BidMachineRewarded  else {
            return
        }
//        didReceiveImpressionClosure?(rewarded, "")
    }
    
    func willPresentScreen(_ ad: any BidMachineAdProtocol) {
    }
    
    func didReceiveReward(_ ad: any BidMachineAdProtocol) {
    }
    
    func didTrackImpression(_ ad: any BidMachineAdProtocol) {
    }
    
    func didExpired(_ ad: any BidMachineAdProtocol) {
        adRequestEventsManager.handle(.adExpired(ad))
    }
}
