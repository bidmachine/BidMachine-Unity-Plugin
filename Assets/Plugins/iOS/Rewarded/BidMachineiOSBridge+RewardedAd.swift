//
//  RewardedRequestBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 01/10/2024.
//

import Foundation
import BidMachine

final class RewardedAdBridge {
    var canShowAd: Bool {
        loadedRewarded?.canShow == true
    }
    
    private var builder: RequestConfigBuider?
    private var loadedRewarded: BidMachineRewarded?
    private var adCallbacksHandler: BidMachineRewardedAdHandler?
    
    private let instance: BidMachineSdk
    
    init(instance: BidMachineSdk) {
        self.instance = instance

        guard let config = try? instance.requestConfiguration(.rewarded) else {
            fatalError()
        }
        self.builder = RequestConfigBuider(initial: config, format: .rewarded)
    }
    
    func destroy() {
        builder = nil
        loadedRewarded = nil
        adCallbacksHandler = nil
    }
    
    func load() {
        loadedRewarded?.loadAd()
    }
    
    func show() {
        loadedRewarded?.controller = UIApplication.unityRootViewController
        loadedRewarded?.presentAd()
    }

    func setCallbacks(
        onSuccess: @escaping RewardedCallbackSuccess,
        onFailure: @escaping RewardedCallbackFailure,
        onClose: @escaping RewardedCallbackClose
    ) {
        let handler = self.adCallbacksHandler ?? BidMachineRewardedAdHandler()
     
        setupHandlerCallbacks(
            handler,
            onSuccess: onSuccess,
            onFailure: onFailure,
            onClose: onClose
        )
    }
    
    func setTimeout(_ interval: TimeInterval) {
        builder?.setTimeout(interval)
    }
    
    func setPlacementID(_ id: String) {
        builder?.setPlacementID(id)
    }
    
    func setPriceFloorParams(_ id: String) {
//        builder?
    }
    
    func setBidPayload(_ payload: String) {
        builder?.setBidPayload(payload)
    }
    
    func buildRequest() {
        guard let configuration = builder?.build() else {
            return
        }
        instance.rewarded(configuration) { [weak self] ad, error in
            if let error {
                print(error.localizedDescription)
                return
            }
            guard let ad, let self else {
                return
            }
            self.loadedRewarded = ad
            ad.delegate = self.adCallbacksHandler
        }
    }
}

private extension RewardedAdBridge {
    func setupHandlerCallbacks(
        _ handler: BidMachineRewardedAdHandler,
        onSuccess: @escaping RewardedCallbackSuccess,
        onFailure: @escaping RewardedCallbackFailure,
        onClose: @escaping RewardedCallbackClose
    ) {
        handler.onAdLoaded = { rewarded, result in
            let rewardedAdPtr = Unmanaged.passUnretained(rewarded).toOpaque()
            let resultCString = strdup(result)
            
            onSuccess(rewardedAdPtr, resultCString)
            free(resultCString)
        }
    }
}
