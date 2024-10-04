//
//  RewardedRequestBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 01/10/2024.
//

import Foundation
import BidMachine

public typealias CRequestSuccessCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ result: UnsafePointer<CChar>?) -> Void
public typealias CRequestFailureCallback = @convention(c) (_ error: UnsafeMutableRawPointer) -> Void
public typealias CRequestExpiredCallback = @convention(c) (_ ad: UnsafeMutableRawPointer) -> Void

public typealias CAdCallback = @convention(c) (_ ad: UnsafeMutableRawPointer) -> Void
public typealias CAdFailureCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ error: UnsafeMutableRawPointer) -> Void
public typealias CAdClosedCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ finished: Bool) -> Void

enum AdRequestEvent {
    case adLoaded(_ ad: BidMachineAdProtocol)
    case adExpired(_ ad: BidMachineAdProtocol)
    case adLoadFailed(error: Error)
}

protocol AdRequestsEventsHandlerProtocol {
    func handle(_ event: AdRequestEvent)
}

final class RewardedAdBridge {
    var canShowAd: Bool {
        loadedRewarded?.canShow == true
    }
    
    private var builder: AdRequestBuider?
    private var adRequestEventsManager: AdRequestEventsManager?
    private var loadedRewarded: BidMachineRewarded?
    private var adCallbacksHandler = BidMachineAdHandler()
    
    private let instance: BidMachineSdk
    private let requestLoader: AdRequestLoader<BidMachineRewarded>
    
    init(instance: BidMachineSdk) {
        self.instance = instance
        self.requestLoader = AdRequestLoader(bidMachine: instance)
    }

    func destroy() {
        builder = nil
        loadedRewarded = nil
        adRequestEventsManager = nil
        adCallbacksHandler.reset()
    }
    
    func load() {
        loadedRewarded?.loadAd()
    }
    
    func show() {
        loadedRewarded?.controller = UIApplication.unityRootViewController
        loadedRewarded?.presentAd()
    }

    func setRequestCallbacks(
        onSuccess: @escaping CRequestSuccessCallback,
        onFailure: @escaping CRequestFailureCallback,
        onExpired: @escaping CRequestExpiredCallback
    ) {
        let adRequestEventsManager = AdRequestEventsManager(
            onSuccess: onSuccess,
            onFailure: onFailure,
            onExpired: onExpired
        )
        self.adRequestEventsManager = adRequestEventsManager
        self.adCallbacksHandler.setRequestEventsHandler(adRequestEventsManager)
    }
    
    func setAdCallbacks(
        onLoad: @escaping CAdCallback,
        onFailedToLoad: @escaping CAdFailureCallback,
        onPresent: @escaping CAdCallback,
        onFailedToPresent: @escaping CAdFailureCallback,
        onImpression: @escaping CAdCallback,
        onExpired: @escaping CAdCallback,
        onClose: @escaping CAdClosedCallback
    ) {
        adCallbacksHandler.setLoadCallback(onLoad)
        adCallbacksHandler.setLoadFailedCallback(onFailedToLoad)
        adCallbacksHandler.setPresentCallback(onPresent)
        adCallbacksHandler.setFailToPresentCallback(onFailedToPresent)
        adCallbacksHandler.setImpressionReceivedCallback(onImpression)
        adCallbacksHandler.setExpirationCallback(onExpired)
        adCallbacksHandler.setCloseCallback(onClose)
    }
    
    func setTimeout(_ interval: TimeInterval) {
        getRequestBuilder().setTimeout(interval)
    }
    
    func setPlacementID(_ id: String) {
        getRequestBuilder().setPlacementID(id)
    }
    
    func setPriceFloorParams(_ parameters: [PriceFloorParameter]) {
        getRequestBuilder().setPriceFloorParameters(parameters)
    }
    
    func setBidPayload(_ payload: String) {
        getRequestBuilder().setBidPayload(payload)
    }
    
    func loadRequest() {
        let adRequest = getRequestBuilder().build()

        requestLoader.load(request: adRequest) { [weak self] result in
            switch result {
            case let .success(rewarded):
                self?.handleAdRequestSuccess(rewarded)

            case let .failure(error):
                self?.adRequestEventsManager?.handle(.adLoadFailed(error: error))
            }
        }
    }
    
    private func getRequestBuilder() -> AdRequestBuider {
        if let builder {
            return builder
        }
        let builder = AdRequestBuider()
        self.builder = builder
        return builder
    }
    
    private func handleAdRequestSuccess(_ ad: BidMachineRewarded) {
        ad.delegate = adCallbacksHandler
        loadedRewarded = ad

        adRequestEventsManager?.handle(.adLoaded(ad))
    }
}

extension RewardedAdBridge: RewardedRequestProtocol {
    var auctionResult: String? {
        #warning("what is auction result string?")
        return loadedRewarded?.auctionInfo.demandSource
    }

    var isExpired: Bool {
        adRequestEventsManager?.adIsExpired == true
    }
    var isDestroyed: Bool {
        loadedRewarded == nil
    }
}
