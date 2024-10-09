//
//  FullscreenAdBridge.swift
//  Unity-iPhone
//
//  Created by Dzmitry on 08/10/2024.
//

import Foundation
import BidMachine

public typealias CRequestSuccessCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ resultUnmanagedPtr: UnsafeMutablePointer<CChar>?) -> Void
public typealias CRequestFailureCallback = @convention(c) (_ error: UnsafeMutableRawPointer) -> Void
public typealias CRequestExpiredCallback = @convention(c) (_ ad: UnsafeMutableRawPointer) -> Void

public typealias CAdCallback = @convention(c) (_ ad: UnsafeMutableRawPointer) -> Void
public typealias CAdFailureCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ error: UnsafeMutableRawPointer) -> Void
public typealias CAdClosedCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ finished: Bool) -> Void

class AdBridge<Ad: BidMachineAdProtocol> {
    var canShowAd: Bool {
        loadedAd?.canShow == true
    }
    
    var forceMarkAdAsFinishedOnClose: Bool {
        get { adCallbacksHandler.forceCloseFinished }
        set { adCallbacksHandler.forceCloseFinished = newValue }
    }
    
    private var builder: AdRequestBuider?
    private var adRequestEventsManager: AdRequestEventsManager?
    private(set) var loadedAd: Ad?

    private let instance: BidMachineSdk
    private let requestLoader: AdRequestLoader<Ad>
    private let adCallbacksHandler = BidMachineAdHandler()
    
    init(instance: BidMachineSdk) {
        self.instance = instance
        self.requestLoader = AdRequestLoader(bidMachine: instance)
    }

    func destroy() {
        builder = nil
        loadedAd = nil
        adRequestEventsManager = nil
        adCallbacksHandler.reset()
    }
    
    func load() {
        loadedAd?.loadAd()
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
        onClose: CAdClosedCallback?
    ) {
        adCallbacksHandler.setLoadCallback(onLoad)
        adCallbacksHandler.setLoadFailedCallback(onFailedToLoad)
        adCallbacksHandler.setPresentCallback(onPresent)
        adCallbacksHandler.setFailToPresentCallback(onFailedToPresent)
        adCallbacksHandler.setImpressionReceivedCallback(onImpression)
        adCallbacksHandler.setExpirationCallback(onExpired)
        
        if let onClose {
            adCallbacksHandler.setCloseCallback(onClose)
        }
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
    
    func setPlacementFormat(_ format: PlacementFormat) {
        getRequestBuilder().setPlacementFormat(format)
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
    
    private func handleAdRequestSuccess(_ ad: Ad) {
        ad.delegate = adCallbacksHandler
        loadedAd = ad

        adRequestEventsManager?.handle(.adLoaded(ad))
    }
}

extension AdBridge: AdRequestProtocol {
    var auctionResult: String? {
        return loadedAd?.auctionInfo.resultJsonString
    }

    var isExpired: Bool {
        adRequestEventsManager?.adIsExpired == true
    }

    var isDestroyed: Bool {
        loadedAd == nil
    }
}
