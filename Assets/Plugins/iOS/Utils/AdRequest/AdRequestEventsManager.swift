//
//  AdRequestEventsManager.swift
//  UnityFramework
//
//  Created by Dzmitry on 03/10/2024.
//

import Foundation
import BidMachine

final class AdRequestEventsManager: AdRequestsEventsHandlerProtocol {
    private(set) var adIsExpired: Bool = false

    private var requestSuccessClosure: CRequestSuccessCallback?
    private var requestFailureClosure: CRequestFailureCallback?
    private var requestExpiredClosure: CRequestExpiredCallback?
    
    init(
        onSuccess: @escaping CRequestSuccessCallback,
        onFailure: @escaping CRequestFailureCallback,
        onExpired: @escaping CRequestExpiredCallback
    ) {
        self.requestExpiredClosure = onExpired
        self.requestFailureClosure = onFailure
        self.requestSuccessClosure = onSuccess
    }
    
    func handle(_ event: AdRequestEvent) {
        switch event {
        case let .adLoaded(ad):
            notifyRequestSuccess(ad)

        case let .adLoadFailed(error):
            notifyRequestFailed(with: error)
            
        case let .adExpired(ad):
            adIsExpired = true
            notifyRequestExpired(ad)
        }
    }

    private func notifyRequestExpired(_ ad: BidMachineAdProtocol) {
        let adPtr = Unmanaged.passUnretained(ad).toOpaque()
        requestExpiredClosure?(adPtr)
    }

    private func notifyRequestFailed(with error: Error) {
        let nsError = error as NSError
        let errorPtr = Unmanaged.passUnretained(nsError).toOpaque()

        requestFailureClosure?(errorPtr)
    }
    
    private func notifyRequestSuccess(_ ad: BidMachineAdProtocol) {
        #warning("What is result?")
        let result = ad.auctionInfo.bidId
        let adPtr = Unmanaged.passUnretained(ad).toOpaque()
        let resultCString = result.cString

        requestSuccessClosure?(adPtr, resultCString)
    }
}
