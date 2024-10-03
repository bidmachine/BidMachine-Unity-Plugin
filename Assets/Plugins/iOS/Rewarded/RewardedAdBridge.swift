//
//  RewardedRequestBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 01/10/2024.
//

import Foundation
import BidMachine

public typealias RequestSuccessCallback = @convention(c) (_ ad: UnsafeMutableRawPointer, _ result: UnsafePointer<CChar>?) -> Void
public typealias RequestFailureCallback = @convention(c) (_ ad: UnsafeMutableRawPointer?, _ error: UnsafePointer<CChar>) -> Void
public typealias RequestExpiredCallback = @convention(c) (_ ad: UnsafeMutableRawPointer) -> Void

final class RewardedAdBridge {
    var canShowAd: Bool {
        loadedRewarded?.canShow == true
    }
    
    private var builder: AdRequestBuider?
    private var adRequestEventsManager: AdRequestEventsManager?
    private var loadedRewarded: BidMachineRewarded?
    private var adCallbacksHandler: BidMachineRewardedAdHandler?
    
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
        adCallbacksHandler = nil
    }
    
    func load() {
        loadedRewarded?.loadAd()
    }
    
    func show() {
        loadedRewarded?.controller = UIApplication.unityRootViewController
        loadedRewarded?.presentAd()
    }

    func setRequestCallbacks(
        onSuccess: @escaping RequestSuccessCallback,
        onFailure: @escaping RequestFailureCallback,
        onExpired: @escaping RequestExpiredCallback
    ) {
        let adRequestEventsManager = AdRequestEventsManager(
            onSuccess: onSuccess,
            onFailure: onFailure,
            onExpired: onExpired
        )
        
        self.adCallbacksHandler = BidMachineRewardedAdHandler(
            adRequestEventsManager: adRequestEventsManager
        )
        self.adRequestEventsManager = adRequestEventsManager
    }
    
    func setTimeout(_ interval: TimeInterval) {
        getRequestBuilder().setTimeout(interval)
    }
    
    func setPlacementID(_ id: String) {
        getRequestBuilder().setPlacementID(id)
    }
    
    func setPriceFloorParams(_ id: String) {
        // FIXME: add logic
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
