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

@_cdecl("BidMachineSetRewardedAdDelegate")
public func setRewardedAdCallbacks(
    onLoad: @escaping CAdCallback,
    onFailedToLoad: @escaping CAdFailureCallback,
    onPresent: @escaping CAdCallback,
    onFailedToPresent: @escaping CAdFailureCallback,
    onImpression: @escaping CAdCallback,
    onExpired: @escaping CAdCallback,
    onClose: @escaping CAdClosedCallback
) {
    iOSUnityBridge.rewardedBridge.setAdCallbacks(
        onLoad: onLoad,
        onFailedToLoad: onFailedToLoad,
        onPresent: onPresent,
        onFailedToPresent: onFailedToPresent,
        onImpression: onImpression,
        onExpired: onExpired,
        onClose: onClose
    )
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
public func rewardedSetPlacementID(_ id: UnsafePointer<CChar>?) {
    guard let id else {
        return
    }
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
public func rewardedSetBidPayload(_ payload: UnsafePointer<CChar>?) {
    guard let payload else {
        return
    }
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

private extension UnityAdContentType {
    var asRewardedPlacement: PlacementFormat {
        switch self {
        case .static: .rewardedStatic
        case .video: .rewardedVideo
        case .all: .rewarded
        }
    }
}
