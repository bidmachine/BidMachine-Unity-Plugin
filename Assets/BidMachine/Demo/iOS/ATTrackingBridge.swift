//
//  ATTrackingBridge.swift
//  UnityFramework
//
//  App Tracking Transparency (ATT) Bridge for Unity Demo
//

import Foundation
#if canImport(AppTrackingTransparency)
import AppTrackingTransparency
#endif
#if canImport(AdSupport)
import AdSupport
#endif

/// Get the current ATT authorization status
/// Returns: 0 = Not Determined, 1 = Restricted, 2 = Denied, 3 = Authorized
@_cdecl("BidMachineGetATTrackingStatus")
public func getATTrackingStatus() -> Int {
    #if canImport(AppTrackingTransparency)
    if #available(iOS 14, *) {
        switch ATTrackingManager.trackingAuthorizationStatus {
        case .notDetermined:
            return 0
        case .restricted:
            return 1
        case .denied:
            return 2
        case .authorized:
            return 3
        @unknown default:
            return 0
        }
    } else {
        // iOS 13 and below - tracking is always authorized
        return 3
    }
    #else
    // iOS 13 and below - tracking is always authorized
    return 3
    #endif
}

/// Request ATT permission from the user
/// This will show the system ATT dialog
@_cdecl("BidMachineRequestATTracking")
public func requestATTracking() {
    #if canImport(AppTrackingTransparency)
    if #available(iOS 14, *) {
        ATTrackingManager.requestTrackingAuthorization { status in
            print("[BidMachine ATT] Authorization status: \(status.rawValue)")
        }
    } else {
        print("[BidMachine ATT] ATT not required on iOS 13 and below")
    }
    #else
    print("[BidMachine ATT] ATT not available on this iOS version")
    #endif
}
