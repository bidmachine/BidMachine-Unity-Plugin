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
    iOSBridge.rewardedBridge.canShowAd
}

@_cdecl("BidMachineRewardedDestroy")
public func rewardedDestroy() {
    iOSBridge.rewardedBridge.destroy()
}

@_cdecl("BidMachineRewardedLoad")
public func rewardedLoad() {
    iOSBridge.rewardedBridge.load()
}

@_cdecl("BidMachineRewardedShow")
public func rewardedShow() {
    iOSBridge.rewardedBridge.show()
}

// MARK: - Builder

@_cdecl("BidMachineRewardedSetPriceFloorParams")
public func rewardedSetPriceFloorParams(jsonString: UnsafePointer<CChar>?) {
    guard let jsonString else {
        return
    }
    let paramsString = String(cString: jsonString)
    iOSBridge.rewardedBridge.setPriceFloorParams(paramsString)
}

@_cdecl("BidMachineRewardedSetPlacementId")
public func rewardedSetPlacementID(_ id: UnsafePointer<CChar>?) {
    guard let id else {
        return
    }
    let idString = String(cString: id)
    iOSBridge.rewardedBridge.setPlacementID(idString)
}

@_cdecl("BidMachineRewardedSetBidPayload")
public func rewardedSetBidPayload(_ payload: UnsafePointer<CChar>?) {
    guard let payload else {
        return
    }
    let payloadString = String(cString: payload)
    iOSBridge.rewardedBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineRewardedSetLoadingTimeOut")
public func rewardedSetLoadingTimeout(_ interval: Int) {
    #warning("ask about measurement here")
    iOSBridge.rewardedBridge.setTimeout(TimeInterval(interval))
}

@_cdecl("BidMachineRewardedBuildRequest")
public func rewardedBuildRequest() {
    iOSBridge.rewardedBridge.buildRequest()
}

@_cdecl("BidMachineSetRewardedRequestDelegate")
public func setRewardedCallbacks(
    onSuccess: @escaping RewardedCallbackSuccess,
    onFailure: @escaping RewardedCallbackFailure,
    onClose: @escaping RewardedCallbackClose
) {
    iOSBridge.rewardedBridge.setCallbacks(
        onSuccess: onSuccess,
        onFailure: onFailure,
        onClose: onClose
    )
}

// MARK: - Request

@_cdecl("BidMachineRewardedGetAuctionResult")
public func rewardedAuctionResult() {
//    iOSBridge.rewardedBridge
}

@_cdecl("BidMachineRewardedIsExpired")
public func rewardedIsExpired() -> Bool {
    return true
}

@_cdecl("BidMachineRewardedIsDestroyed")
public func rewardedIsDestroyed() -> Bool {
    return true
}

