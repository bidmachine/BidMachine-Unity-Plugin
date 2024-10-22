//
//  BidMachine+BannerAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import Foundation
import BidMachine

// MARK: - Ad

@_cdecl("BidMachineBannerCanShow")
public func bannerCanShow() -> Bool {
    iOSUnityBridge.bannerBridge.canShowAd
}

@_cdecl("BidMachineBannerDestroy")
public func bannerDestroy() {
    iOSUnityBridge.bannerBridge.destroy()
}

@_cdecl("BidMachineBannerLoad")
public func bannerLoad() {
    iOSUnityBridge.bannerBridge.load()
}

@_cdecl("BidMachineBannerShow")
public func bannerShow(y: Int, x: Int) -> Bool {
    let vertical = AdLayout.Vertical(rawValue: y)
    if vertical == nil {
        print("[BidMachine plugin] ⚠️ Warning: Invalid vertical position: \(y). Check BidMachine Unity plugin README.md '# BANNER / MREC' section for valid values. Using bottom as default.")
    }

    let horizontal = AdLayout.Horizontal(rawValue: x)
    if horizontal == nil {
        print("[BidMachine plugin] ⚠️ Warning: Invalid vertical position: \(x). Check BidMachine Unity plugin README.md '# BANNER / MREC' section for valid values. Using center as default.")
    }
    
    let adLayout = AdLayout(
        verticalPin: vertical ?? .bottom,
        horizontalPin: horizontal ?? .center
    )
    let shown = iOSUnityBridge.bannerBridge.show(with: adLayout)

    return shown
}

@_cdecl("BidMachineBannerHide")
public func bannerHide() {
    iOSUnityBridge.bannerBridge.hide()
}

// MARK: - Ad Callbacks

@_cdecl("BidMachineBannerSetLoadCallback")
public func setBannerLoadCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.bannerBridge.setLoadCallback(callback)
}

@_cdecl("BidMachineBannerSetLoadFailedCallback")
public func setBannerLoadFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.bannerBridge.setLoadFailedCallback(callback)
}

@_cdecl("BidMachineBannerSetPresentCallback")
public func setBannerPresentCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.bannerBridge.setPresentCallback(callback)
}

@_cdecl("BidMachineBannerSetPresentFailedCallback")
public func setBannerPresentFailedCallback(_ callback: @escaping CAdFailureCallback) {
    iOSUnityBridge.bannerBridge.setPresentFailedCallback(callback)
}

@_cdecl("BidMachineBannerSetImpressionCallback")
public func setBannerImpressionCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.bannerBridge.setImpressionCallback(callback)
}

@_cdecl("BidMachineBannerSetExpiredCallback")
public func setBannerExpiredCallback(_ callback: @escaping CAdCallback) {
    iOSUnityBridge.bannerBridge.setExpiredCallback(callback)
}

// MARK: - Builder

@_cdecl("BidMachineBannerSetPriceFloorParams")
public func bannerSetPriceFloorParams(jsonString: UnsafePointer<CChar>) {
    let jsonString = String(cString: jsonString)

    do {
        let parametersList = try PriceFloorParamsDecoder.decode(from: jsonString)

        iOSUnityBridge.bannerBridge.setPriceFloorParams(parametersList.items)
    } catch let error {
        print("Error parsing price floor params: \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineBannerSetPlacementId")
public func bannerSetPlacementID(_ id: UnsafePointer<CChar>) {
    let idString = String(cString: id)
    iOSUnityBridge.bannerBridge.setPlacementID(idString)
}

@_cdecl("BidMachineBannerSetAdContentType")
public func bannerSetAdContentType(_ type: UnsafePointer<CChar>) {
    let adTypeString = String(cString: type)
    guard let _ = UnityAdContentType(rawValue: adTypeString) else {
        return
    }
    // Additional logic can go here if needed in the future
}

@_cdecl("BidMachineBannerSetBidPayload")
public func bannerSetBidPayload(_ payload: UnsafePointer<CChar>) {
    let payloadString = String(cString: payload)
    iOSUnityBridge.bannerBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineBannerSetNetworks")
public func bannerSetNetworks(_ networks: UnsafePointer<CChar>) {
    let networksString = String(cString: networks)
    let networksNames = NetworksNamesDecoder.decode(from: networksString)
    iOSUnityBridge.bannerBridge.setNetworks(networksNames)
}

@_cdecl("BidMachineBannerSetLoadingTimeOut")
public func bannerSetLoadingTimeout(_ interval: Int) {
    let seconds = MillisecondsConverter.toSeconds(interval)

    iOSUnityBridge.bannerBridge.setTimeout(seconds)
}

@_cdecl("BidMachineBannerBuildRequest")
public func bannerBuildRequest() {
    iOSUnityBridge.bannerBridge.loadRequest()
}

@_cdecl("BidMachineBannerSetSize")
public func bannerSetSize(_ size: Int) {
    guard let bannerSize = BannerAdBridge.BannerSize(rawValue: size) else {
        return
    }
    iOSUnityBridge.bannerBridge.setSize(bannerSize)
    iOSUnityBridge.bannerBridge.setPlacementFormat(bannerSize.placementFormat)
}

@_cdecl("BidMachineBannerGetSize")
public func bannerGetSize() -> Int {
    iOSUnityBridge.bannerBridge.bannerSize
}

@_cdecl("BidMachineSetBannerRequestDelegate")
public func setBannerRequestCallbacks(
    onSuccess: @escaping CRequestSuccessCallback,
    onFailure: @escaping CRequestFailureCallback,
    onExpired: @escaping CRequestExpiredCallback
) {
    iOSUnityBridge.bannerBridge.setRequestCallbacks(
        onSuccess: onSuccess,
        onFailure: onFailure,
        onExpired: onExpired
    )
}

private extension BannerAdBridge.BannerSize {
    var placementFormat: PlacementFormat {
        switch self {
        case .size_320x50:
            return .banner320x50
        case .size_300x250:
            return .banner300x250
        case .size_728x90:
            return .banner728x90
        }
    }
}
