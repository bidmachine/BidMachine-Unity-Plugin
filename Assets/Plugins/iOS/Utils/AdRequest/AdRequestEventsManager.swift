//
//  AdRequestEventsManager.swift
//  UnityFramework
//
//  Created by Dzmitry on 03/10/2024.
//

import Foundation
import BidMachine

final class AdRequestEventsManager {
    enum Event {
        case adLoaded(_ ad: BidMachineAdProtocol)
        case adExpired(_ ad: BidMachineAdProtocol)
        case adLoadFailed(error: Error)
    }
    
    private(set) var adIsExpired: Bool = false

    private var requestSuccessClosure: RequestSuccessCallback?
    private var requestFailureClosure: RequestFailureCallback?
    private var requestExpiredClosure: RequestExpiredCallback?
    
    init(
        onSuccess: @escaping RequestSuccessCallback,
        onFailure: @escaping RequestFailureCallback,
        onExpired: @escaping RequestExpiredCallback
    ) {
        self.requestExpiredClosure = onExpired
        self.requestFailureClosure = onFailure
        self.requestSuccessClosure = onSuccess
    }
    
    func handle(_ event: Event) {
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
        #warning("Add logic")
        print(error.localizedDescription)
    }
    
    private func notifyRequestSuccess(_ ad: BidMachineAdProtocol) {
        #warning("What is result?")
        let result = ad.auctionInfo.bidId
        let adPtr = Unmanaged.passUnretained(ad).toOpaque()
        let resultCString = result.cString
        
        requestSuccessClosure?(adPtr, resultCString)
    }
}
