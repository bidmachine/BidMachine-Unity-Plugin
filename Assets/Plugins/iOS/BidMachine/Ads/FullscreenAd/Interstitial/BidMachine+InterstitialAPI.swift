//
//  BidMachine+InterstitialAPI.swift
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

// MARK: - Ad Callbacks

@_cdecl("BidMachineInterstitialSetLoadCallback")
public func setInterstitialLoadCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.interstitialBridge.setLoadCallback(callback)
}

@_cdecl("BidMachineInterstitialSetLoadFailedCallback")
public func setInterstitialLoadFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.interstitialBridge.setLoadFailedCallback(callback)
}

@_cdecl("BidMachineInterstitialSetPresentCallback")
public func setInterstitialPresentCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.interstitialBridge.setPresentCallback(callback)
}

@_cdecl("BidMachineInterstitialSetPresentFailedCallback")
public func setInterstitialPresentFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.interstitialBridge.setPresentFailedCallback(callback)
}

@_cdecl("BidMachineInterstitialSetImpressionCallback")
public func setInterstitialImpressionCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.interstitialBridge.setImpressionCallback(callback)
}

@_cdecl("BidMachineInterstitialSetExpiredCallback")
public func setInterstitialExpiredCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.interstitialBridge.setExpiredCallback(callback)
}

@_cdecl("BidMachineInterstitialSetClosedCallback")
public func setInterstitialClosedCallback(_ callback: @escaping CAdClosedCallback) {
    iOSUnityBridge.interstitialBridge.setClosedCallback(callback)
}

// MARK: - Builder

@_cdecl("BidMachineInterstitialSetPriceFloorParams")
public func interstitialSetPriceFloorParams(jsonString: UnsafePointer<CChar>) {
    RequestBuilderHelper.setPriceFloorParams(jsonString: jsonString, bridge: iOSUnityBridge.interstitialBridge)
}

@_cdecl("BidMachineInterstitialSetCustomParams")
public func interstitialSetCustomParams(jsonString: UnsafePointer<CChar>) {
    RequestBuilderHelper.setCustomParams(jsonString: jsonString, bridge: iOSUnityBridge.interstitialBridge)
}

@_cdecl("BidMachineInterstitialSetPlacementId")
public func interstitialSetPlacementID(_ id: UnsafePointer<CChar>) {
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
public func interstitialSetBidPayload(_ payload: UnsafePointer<CChar>) {
    let payloadString = String(cString: payload)
    iOSUnityBridge.interstitialBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineInterstitialSetNetworks")
public func interstitialSetNetworks(_ networks: UnsafePointer<CChar>) {
    RequestBuilderHelper.setNetworks(networks, bridge: iOSUnityBridge.interstitialBridge)
}

@_cdecl("BidMachineInterstitialSetLoadingTimeOut")
public func interstitialSetLoadingTimeout(_ interval: Int) {
    let seconds = MillisecondsConverter.toSeconds(interval)

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
    let result = iOSUnityBridge.interstitialBridge.auctionResult
    return result?.utf8UnmanagedPtrCopy
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
