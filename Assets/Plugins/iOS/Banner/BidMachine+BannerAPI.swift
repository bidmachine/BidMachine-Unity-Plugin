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
public func bannerShow() {
    // FIXME: add size logic
    iOSUnityBridge.bannerBridge.show(in: .zero)
}

@_cdecl("BidMachineBannerHide")
public func bannerHide() {
    iOSUnityBridge.bannerBridge.hide()
}

@_cdecl("BidMachineSetBannerAdDelegate")
public func setBannerAdCallbacks(
    onLoad: @escaping CAdCallback,
    onFailedToLoad: @escaping CAdFailureCallback,
    onPresent: @escaping CAdCallback,
    onFailedToPresent: @escaping CAdFailureCallback,
    onImpression: @escaping CAdCallback,
    onExpired: @escaping CAdCallback
) {
    iOSUnityBridge.bannerBridge.setAdCallbacks(
        onLoad: onLoad,
        onFailedToLoad: onFailedToLoad,
        onPresent: onPresent,
        onFailedToPresent: onFailedToPresent,
        onImpression: onImpression,
        onExpired: onExpired,
        onClose: nil
    )
}

// MARK: - Builder

@_cdecl("BidMachineBannerSetPriceFloorParams")
public func bannerSetPriceFloorParams(jsonString: UnsafePointer<CChar>) {
    let paramsString = String(cString: jsonString)

    guard let data = paramsString.data(using: .utf8) else {
        return
    }
    do {
        let parametersList = try JSONDecoder().decode(
            PriceFloorParameters.self,
            from: data
        )
        iOSUnityBridge.bannerBridge.setPriceFloorParams(parametersList.items)
    } catch let error {
        print("Error parsing price floor params: \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineBannerSetPlacementId")
public func bannerSetPlacementID(_ id: UnsafePointer<CChar>?) {
    guard let id else {
        return
    }
    let idString = String(cString: id)
    iOSUnityBridge.bannerBridge.setPlacementID(idString)
}

@_cdecl("BidMachineBannerSetAdContentType")
public func bannerSetAdContentType(_ type: UnsafePointer<CChar>) {
    let adTypeString = String(cString: type)
    guard let contentType = UnityAdContentType(rawValue: adTypeString) else {
        return
    }
    // FIXME: add logic or log error (remove from banner interface??)
}

@_cdecl("BidMachineBannerSetBidPayload")
public func bannerSetBidPayload(_ payload: UnsafePointer<CChar>?) {
    guard let payload else {
        return
    }
    let payloadString = String(cString: payload)
    iOSUnityBridge.bannerBridge.setBidPayload(payloadString)
}

@_cdecl("BidMachineBannerSetLoadingTimeOut")
public func bannerSetLoadingTimeout(_ interval: Int) {
    let measurement = Measurement(value: Double(interval), unit: UnitDuration.milliseconds)
    let seconds = measurement.converted(to: .seconds).value

    iOSUnityBridge.bannerBridge.setTimeout(seconds)
}

@_cdecl("BidMachineBannerBuildRequest")
public func bannerBuildRequest() {
    iOSUnityBridge.bannerBridge.loadRequest()
}

@_cdecl("BidMachineBannerSetSize")
public func bannerSetSize(_ size: UnsafePointer<CChar>) {
    _ = String(cString: size)
    // FIXME: add logic
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
