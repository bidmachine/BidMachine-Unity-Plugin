//
//  AdBridge.swift
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
    private let defaultPlacementFormat: PlacementFormat
    
    init(
        instance: BidMachineSdk,
        defaultPlacementFormat: PlacementFormat
    ) {
        self.instance = instance
        self.defaultPlacementFormat = defaultPlacementFormat
        self.requestLoader = AdRequestLoader(bidMachine: instance)
    }

    func destroy() {
        builder = nil
        loadedAd = nil
        adRequestEventsManager = nil
        adCallbacksHandler.reset()
    }
    
    func load() {
        //
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
    
    func setLoadCallback(_ callback: @escaping CAdCallback) {
        adCallbacksHandler.setLoadCallback(callback)
    }
    
    func setLoadFailedCallback(_ callback: @escaping CAdFailureCallback) {
        adCallbacksHandler.setLoadFailedCallback(callback)
    }
    
    func setPresentCallback(_ callback: @escaping CAdCallback) {
        adCallbacksHandler.setPresentCallback(callback)
    }
    
    func setPresentFailedCallback(_ callback: @escaping CAdFailureCallback) {
        adCallbacksHandler.setFailToPresentCallback(callback)
    }
    
    func setImpressionCallback(_ callback: @escaping CAdCallback) {
        adCallbacksHandler.setImpressionReceivedCallback(callback)
    }
    
    func setExpiredCallback(_ callback: @escaping CAdCallback) {
        adCallbacksHandler.setExpirationCallback(callback)
    }
    
    func setClosedCallback(_ callback: @escaping CAdClosedCallback) {
        adCallbacksHandler.setCloseCallback(callback)
    }
    
    func setRewardedCallback(_ callback: @escaping CAdCallback) {
        adCallbacksHandler.setRewardedCallback(callback)
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
    
    func setNetworks(_ networks: [String]) {
        getRequestBuilder().setNetworks(networks)
    }
    
    func loadRequest() {
        let adRequest = getRequestBuilder().build()

        requestLoader.load(request: adRequest) { [weak self] result in
            switch result {
            case let .success(ad):
                self?.handleAdRequestSuccess(ad)

            case let .failure(error):
                self?.adRequestEventsManager?.handle(.adLoadFailed(error: error))
            }
        }
    }
    
    func didReceiveAd() {
        loadedAd?.loadAd()
    }
    
    private func getRequestBuilder() -> AdRequestBuider {
        if let builder {
            return builder
        }
        let builder = AdRequestBuider()
        builder.setPlacementFormat(defaultPlacementFormat)
        self.builder = builder
        return builder
    }
    
    private func handleAdRequestSuccess(_ ad: Ad) {
        loadedAd = ad
        ad.delegate = adCallbacksHandler

        adRequestEventsManager?.handle(.adLoaded(ad))
        didReceiveAd()
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
