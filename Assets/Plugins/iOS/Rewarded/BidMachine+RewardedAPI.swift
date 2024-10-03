//
//  BidMachine+RewardedAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import Foundation
import BidMachine

// MARK: - Ad

@_cdecl("BidMachineRewardedCanShow")
public func rewardedCanShow() -> Bool {
    iOSUnityBridge.rewardedBridge.canShowAd
}

@_cdecl("BidMachineRewardedDestroy")
public func rewardedDestroy() {
    iOSUnityBridge.rewardedBridge.destroy()
}

@_cdecl("BidMachineRewardedLoad")
public func rewardedLoad() {
    iOSUnityBridge.rewardedBridge.load()
}

@_cdecl("BidMachineRewardedShow")
public func rewardedShow() {
    iOSUnityBridge.rewardedBridge.show()
}

// MARK: - Builder

@_cdecl("BidMachineRewardedSetPriceFloorParams")
public func rewardedSetPriceFloorParams(jsonString: UnsafePointer<CChar>?) {
    guard let jsonString else {
        return
    }
    let paramsString = String(cString: jsonString)
    iOSUnityBridge.rewardedBridge.setPriceFloorParams(paramsString)
}

@_cdecl("BidMachineRewardedSetPlacementId")
public func rewardedSetPlacementID(_ id: UnsafePointer<CChar>?) {
    guard let id else {
        return
    }
    let idString = String(cString: id)
    iOSUnityBridge.rewardedBridge.setPlacementID(idString)
}

@_cdecl("BidMachineRewardedSetBidPayload")
public func rewardedSetBidPayload(_ payload: UnsafePointer<CChar>?) {
    guard let payload else {
        return
    }
    let payloadString = String(cString: payload)
    iOSUnityBridge.rewardedBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineRewardedSetLoadingTimeOut")
public func rewardedSetLoadingTimeout(_ interval: Int) {
    #warning("ask about measurement here")
    iOSUnityBridge.rewardedBridge.setTimeout(TimeInterval(interval))
}

@_cdecl("BidMachineRewardedBuildRequest")
public func rewardedBuildRequest() {
    iOSUnityBridge.rewardedBridge.loadRequest()
}

@_cdecl("BidMachineSetRewardedRequestDelegate")
public func setRewardedRequestCallbacks(
    onSuccess: @escaping RequestSuccessCallback,
    onFailure: @escaping RequestFailureCallback,
    onExpired: @escaping RequestExpiredCallback
) {
    iOSUnityBridge.rewardedBridge.setRequestCallbacks(
        onSuccess: onSuccess,
        onFailure: onFailure,
        onExpired: onExpired
    )
}

// MARK: - Request

@_cdecl("BidMachineRewardedGetAuctionResult")
public func rewardedAuctionResult() -> UnsafePointer<CChar> {
    (iOSUnityBridge.rewardedBridge.auctionResult ?? "unknown").cString
}

@_cdecl("BidMachineRewardedIsExpired")
public func rewardedIsExpired() -> Bool {
    iOSUnityBridge.rewardedBridge.isExpired
}

@_cdecl("BidMachineRewardedIsDestroyed")
public func rewardedIsDestroyed() -> Bool {
    iOSUnityBridge.rewardedBridge.isDestroyed
}

extension String {
    var cString: UnsafePointer<CChar> {
        self.withCString { $0 }
    }
}
