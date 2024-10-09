//
//  BidMachine+RewardedAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import Foundation
import BidMachine

// MARK: - Ad

@_cdecl("BidMachineInterstitialCanShow")
public func interstitialCanShow() -> Bool {
    iOSUnityBridge.interstitialBridge.canShowAd
}

@_cdecl("BidMachineInterstitialDestroy")
public func interstitialDestroy() {
    iOSUnityBridge.interstitialBridge.destroy()
}

@_cdecl("BidMachineInterstitialLoad")
public func interstitialLoad() {
    iOSUnityBridge.interstitialBridge.load()
}

@_cdecl("BidMachineInterstitialShow")
public func interstitialShow() {
    iOSUnityBridge.interstitialBridge.show()
}

@_cdecl("BidMachineSetInterstitialAdDelegate")
public func setInterstitialAdCallbacks(
    onLoad: @escaping CAdCallback,
    onFailedToLoad: @escaping CAdFailureCallback,
    onPresent: @escaping CAdCallback,
    onFailedToPresent: @escaping CAdFailureCallback,
    onImpression: @escaping CAdCallback,
    onExpired: @escaping CAdCallback,
    onClose: @escaping CAdClosedCallback
) {
    iOSUnityBridge.interstitialBridge.setAdCallbacks(
        onLoad: onLoad,
        onFailedToLoad: onFailedToLoad,
        onPresent: onPresent,
        onFailedToPresent: onFailedToPresent,
        onImpression: onImpression,
        onExpired: onExpired,
        onClose: onClose
    )
}
//
// MARK: - Builder

@_cdecl("BidMachineInterstitialSetPriceFloorParams")
public func interstitialSetPriceFloorParams(jsonString: UnsafePointer<CChar>) {
    let paramsString = String(cString: jsonString)

    guard let data = paramsString.data(using: .utf8) else {
        return
    }
    do {
        let parametersList = try JSONDecoder().decode(
            PriceFloorParameters.self,
            from: data
        )
        iOSUnityBridge.interstitialBridge.setPriceFloorParams(parametersList.items)
    } catch let error {
        print("Error parsing price floor params: \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineInterstitialSetPlacementId")
public func interstitialSetPlacementID(_ id: UnsafePointer<CChar>?) {
    guard let id else {
        return
    }
    let idString = String(cString: id)
    iOSUnityBridge.interstitialBridge.setPlacementID(idString)
}

@_cdecl("BidMachineInterstitialSetAdContentType")
public func interstitialSetAdContentType(_ type: UnsafePointer<CChar>) {
    let adTypeString = String(cString: type)
    guard let contentType = UnityAdContentType(rawValue: adTypeString) else {
        return
    }
    iOSUnityBridge.interstitialBridge.setPlacementFormat(
        contentType.asInterstitialPlacement
    )
}

@_cdecl("BidMachineInterstitialSetBidPayload")
public func interstitialSetBidPayload(_ payload: UnsafePointer<CChar>?) {
    guard let payload else {
        return
    }
    let payloadString = String(cString: payload)
    iOSUnityBridge.interstitialBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineInterstitialSetLoadingTimeOut")
public func interstitialSetLoadingTimeout(_ interval: Int) {
    let measurement = Measurement(value: Double(interval), unit: UnitDuration.milliseconds)
    let seconds = measurement.converted(to: .seconds).value

    iOSUnityBridge.interstitialBridge.setTimeout(seconds)
}

@_cdecl("BidMachineInterstitialBuildRequest")
public func interstitialBuildRequest() {
    iOSUnityBridge.interstitialBridge.loadRequest()
}

@_cdecl("BidMachineSetInterstitialRequestDelegate")
public func setInterstitialRequestCallbacks(
    onSuccess: @escaping CRequestSuccessCallback,
    onFailure: @escaping CRequestFailureCallback,
    onExpired: @escaping CRequestExpiredCallback
) {
    iOSUnityBridge.interstitialBridge.setRequestCallbacks(
        onSuccess: onSuccess,
        onFailure: onFailure,
        onExpired: onExpired
    )
}

// MARK: - Request

@_cdecl("BidMachineInterstitialGetAuctionResultUnmanagedPointer")
public func interstitialAuctionResult() -> UnsafeMutablePointer<CChar>? {
    let result = iOSUnityBridge.interstitialBridge.auctionResult ?? "unknown"
    return result.utf8UnmanagedPtrCopy
}

@_cdecl("BidMachineInterstitialIsExpired")
public func interstitialIsExpired() -> Bool {
    iOSUnityBridge.interstitialBridge.isExpired
}

@_cdecl("BidMachineInterstitialIsDestroyed")
public func interstitialIsDestroyed() -> Bool {
    iOSUnityBridge.interstitialBridge.isDestroyed
}

private extension UnityAdContentType {
    var asInterstitialPlacement: PlacementFormat {
        switch self {
        case .static: .interstitialStatic
        case .video: .interstitialVideo
        case .all: .interstitial
        }
    }
}
