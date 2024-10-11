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

// MARK: - Ad Callbacks

@_cdecl("BidMachineRewardedSetLoadCallback")
public func setRewardedLoadCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.rewardedBridge.setLoadCallback(callback)
}

@_cdecl("BidMachineRewardedSetLoadFailedCallback")
public func setRewardedLoadFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.rewardedBridge.setLoadFailedCallback(callback)
}

@_cdecl("BidMachineRewardedSetPresentCallback")
public func setRewardedPresentCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.rewardedBridge.setPresentCallback(callback)
}

@_cdecl("BidMachineRewardedSetPresentFailedCallback")
public func setRewardedPresentFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.rewardedBridge.setPresentFailedCallback(callback)
}

@_cdecl("BidMachineRewardedSetImpressionCallback")
public func setRewardedImpressionCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.rewardedBridge.setImpressionCallback(callback)
}

@_cdecl("BidMachineRewardedSetExpiredCallback")
public func setRewardedExpiredCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.rewardedBridge.setExpiredCallback(callback)
}

@_cdecl("BidMachineRewardedSetClosedCallback")
public func setRewardedClosedCallback(_ callback: @escaping CAdClosedCallback) {
    iOSUnityBridge.rewardedBridge.setClosedCallback(callback)
}

@_cdecl("BidMachineRewardedSetRewardedCallback")
public func setRewardedRewardCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.rewardedBridge.setRewardedCallback(callback)
}

// MARK: - Builder

typealias PriceFloorParameter = KeyValueBox<String, Double>
typealias PriceFloorParameters = KeyValueList<String, Double>

@_cdecl("BidMachineRewardedSetPriceFloorParams")
public func rewardedSetPriceFloorParams(jsonString: UnsafePointer<CChar>) {
    let paramsString = String(cString: jsonString)

    guard let data = paramsString.data(using: .utf8) else {
        return
    }
    do {
        let parametersList = try JSONDecoder().decode(
            PriceFloorParameters.self,
            from: data
        )
        iOSUnityBridge.rewardedBridge.setPriceFloorParams(parametersList.items)
    } catch let error {
        print("Error parsing price floor params: \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineRewardedSetPlacementId")
public func rewardedSetPlacementID(_ id: UnsafePointer<CChar>) {
    let idString = String(cString: id)
    iOSUnityBridge.rewardedBridge.setPlacementID(idString)
}

@_cdecl("BidMachineRewardedSetAdContentType")
public func rewardedSetAdContentType(_ type: UnsafePointer<CChar>) {
    let adTypeString = String(cString: type)
    guard let contentType = UnityAdContentType(rawValue: adTypeString) else {
        return
    }
    iOSUnityBridge.rewardedBridge.setPlacementFormat(
        contentType.asRewardedPlacement
    )
}

@_cdecl("BidMachineRewardedSetBidPayload")
public func rewardedSetBidPayload(_ payload: UnsafePointer<CChar>) {
    let payloadString = String(cString: payload)
    iOSUnityBridge.rewardedBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineRewardedSetLoadingTimeOut")
public func rewardedSetLoadingTimeout(_ interval: Int) {
    let measurement = Measurement(value: Double(interval), unit: UnitDuration.milliseconds)
    let seconds = measurement.converted(to: .seconds).value

    iOSUnityBridge.rewardedBridge.setTimeout(seconds)
}

@_cdecl("BidMachineRewardedBuildRequest")
public func rewardedBuildRequest() {
    iOSUnityBridge.rewardedBridge.loadRequest()
}

@_cdecl("BidMachineSetRewardedRequestDelegate")
public func setRewardedRequestCallbacks(
    onSuccess: @escaping CRequestSuccessCallback,
    onFailure: @escaping CRequestFailureCallback,
    onExpired: @escaping CRequestExpiredCallback
) {
    iOSUnityBridge.rewardedBridge.setRequestCallbacks(
        onSuccess: onSuccess,
        onFailure: onFailure,
        onExpired: onExpired
    )
}

// MARK: - Request

@_cdecl("BidMachineRewardedGetAuctionResultUnmanagedPointer")
public func rewardedAuctionResult() -> UnsafeMutablePointer<CChar>? {
    let result = iOSUnityBridge.rewardedBridge.auctionResult ?? "unknown"
    return result.utf8UnmanagedPtrCopy
}

@_cdecl("BidMachineRewardedIsExpired")
public func rewardedIsExpired() -> Bool {
    iOSUnityBridge.rewardedBridge.isExpired
}

@_cdecl("BidMachineRewardedIsDestroyed")
public func rewardedIsDestroyed() -> Bool {
    iOSUnityBridge.rewardedBridge.isDestroyed
}

private extension UnityAdContentType {
    var asRewardedPlacement: PlacementFormat {
        switch self {
        case .static: .rewardedStatic
        case .video: .rewardedVideo
        case .all: .rewarded
        }
    }
}
