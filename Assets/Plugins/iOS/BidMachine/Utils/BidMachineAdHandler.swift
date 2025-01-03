//
//  BidMachineAdHandler.swift
//  UnityFramework
//
//  Created by Dzmitry on 04/10/2024.
//

import Foundation
import BidMachine

final class BidMachineAdHandler: NSObject {
    typealias AdCallback = (_ ad: BidMachineAdProtocol) -> Void
    typealias FailureCallback = (_ ad: BidMachineAdProtocol,_ error: Error?) -> Void
    typealias ClosedCallback = (_ ad: BidMachineAdProtocol, _ finished: Bool) -> Void
    
    var forceCloseFinished: Bool = false
    
    private var adRequestEventsManager: AdRequestsEventsHandlerProtocol?
    private var finished = false
    
    private var didLoadBridge: CAdCallbackBridge?
    private var didFailToLoadBridge: CAdFailureCallbackBridge?
    private var didPresentBridge: CAdCallbackBridge?
    private var didFailToPresentBridge: CAdFailureCallbackBridge?
    private var didReceiveImpressionBridge: CAdCallbackBridge?
    private var didExpireBridge: CAdCallbackBridge?
    private var didCloseBridge: CAdCallbackClosedBridge?
    private var didRewardBridge: CAdCallbackBridge?
    
    func reset() {
        adRequestEventsManager = nil
        
        didLoadBridge = nil
        didFailToLoadBridge = nil
        didPresentBridge = nil
        didFailToPresentBridge = nil
        didReceiveImpressionBridge = nil
        didExpireBridge = nil
        didCloseBridge = nil
        didRewardBridge = nil
    }
    
    func setRequestEventsHandler(_ handler: AdRequestsEventsHandlerProtocol) {
        self.adRequestEventsManager = handler
    }
    
    func setLoadCallback(_ closure: @escaping CAdCallback) {
        didLoadBridge = CAdCallbackBridge(cCallback: closure)
    }
    
    func setLoadFailedCallback(_ closure: @escaping CAdFailureCallback) {
        didFailToLoadBridge = CAdFailureCallbackBridge(cCallback: closure)
    }
    
    func setPresentCallback(_ closure: @escaping CAdCallback) {
        didPresentBridge = CAdCallbackBridge(cCallback: closure)
    }
    
    func setFailToPresentCallback(_ closure: @escaping CAdFailureCallback) {
        didFailToPresentBridge = CAdFailureCallbackBridge(cCallback: closure)
    }
    
    func setImpressionReceivedCallback(_ closure: @escaping CAdCallback) {
        didReceiveImpressionBridge = CAdCallbackBridge(cCallback: closure)
    }
    
    func setExpirationCallback(_ closure: @escaping CAdCallback) {
        didExpireBridge = CAdCallbackBridge(cCallback: closure)
    }
    
    func setCloseCallback(_ closure: @escaping CAdClosedCallback) {
        didCloseBridge = CAdCallbackClosedBridge(cCallback: closure)
    }
    
    func setRewardedCallback(_ closure: @escaping CAdCallback) {
        didRewardBridge = CAdCallbackBridge(cCallback: closure)
    }
}

extension BidMachineAdHandler: BidMachineAdDelegate {
    func didLoadAd(_ ad: any BidMachine.BidMachineAdProtocol) {
        didLoadBridge?.call(with: ad)
    }
    
    func didFailLoadAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        didFailToLoadBridge?.call(with: ad, error: error)
    }
    
    func didPresentAd(_ ad: any BidMachineAdProtocol) {
        didPresentBridge?.call(with: ad)
    }
    
    func didFailPresentAd(_ ad: any BidMachine.BidMachineAdProtocol, _ error: any Error) {
        didFailToPresentBridge?.call(with: ad, error: error)
    }
    
    func didTrackImpression(_ ad: any BidMachineAdProtocol) {
        didReceiveImpressionBridge?.call(with: ad)
    }
    
    func didExpired(_ ad: any BidMachineAdProtocol) {
        adRequestEventsManager?.handle(.adExpired(ad))
        didExpireBridge?.call(with: ad)
    }
    
    func didDismissAd(_ ad: any BidMachineAdProtocol) {
        let closeFinished = forceCloseFinished || finished

        didCloseBridge?.call(with: ad, finished: closeFinished)
    }
    
    func didReceiveReward(_ ad: any BidMachineAdProtocol) {
        finished = true
        didRewardBridge?.call(with: ad)
    }
}

private extension BidMachineAdHandler {
    final class CAdCallbackBridge {
        private let cCallback: CAdCallback
        
        init(cCallback: @escaping CAdCallback) {
            self.cCallback = cCallback
        }
        
        func call(with ad: BidMachineAdProtocol) {
            let adPtr = Unmanaged.passUnretained(ad).toOpaque()
            cCallback(adPtr)
        }
    }
    
    final class CAdFailureCallbackBridge {
        private let cCallback: CAdFailureCallback
        
        init(cCallback: @escaping CAdFailureCallback) {
            self.cCallback = cCallback
        }
        
        func call(with ad: BidMachineAdProtocol, error: Error) {
            let adPtr = Unmanaged.passUnretained(ad).toOpaque()
            let nsError = (error as NSError)
            let errorPtr = Unmanaged.passUnretained(nsError).toOpaque()

            cCallback(adPtr, errorPtr)
        }
    }
    
    final class CAdCallbackClosedBridge {
        private let cCallback: CAdClosedCallback
        
        init(cCallback: @escaping CAdClosedCallback) {
            self.cCallback = cCallback
        }
        
        func call(with ad: BidMachineAdProtocol, finished: Bool) {
            let adPtr = Unmanaged.passUnretained(ad).toOpaque()
            cCallback(adPtr, finished)
        }
    }
}
