//
//  BidMachine+InitializationAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 01/10/2024.
//

import Foundation

@_cdecl("BidMachineInitialize")
public func initialize(_ sellerId: UnsafePointer<CChar>?) {
    guard let sellerId else {
        return
    }
    let sourceId = String(cString: sellerId)
    iOSBridge.initialize(with: sourceId)
}

@_cdecl("BidMachineIsInitialized")
public func isInitialized() -> Bool {
    return iOSBridge.isInitialized
}

@_cdecl("BidMachineSetEndpoint")
public func setEndpoint(url: UnsafePointer<CChar>?) {
    guard let url else {
        return
    }
    let urlString = String(cString: url)
    iOSBridge.setEndpoint(url: urlString)
}

@_cdecl("BidMachineSetLoggingEnabled")
public func setLoggingEnabled(_ enabled: Bool) {
    iOSBridge.setLoggingEnabled(enabled)
}

@_cdecl("BidMachineSetTestEnabled")
public func setTestEnabled(_ enabled: Bool) {
    iOSBridge.setTestEnabled(enabled)
}

@_cdecl("BidMachineSetTargetingParams")
public func setTargetingParams(_ json: UnsafePointer<CChar>) {
    let jsonString = String(cString: json)
    
    guard let data = jsonString.data(using: .utf8) else {
        return
    }
    do {
        let parameters = try JSONDecoder().decode(
            TargetingParameters.self,
            from: data
        )
        iOSBridge.setTargeting(parameters: parameters)
    } catch let error {
        // FIXME: add error handling
        print("[DEBUG]: error \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineSetConsentConfig")
public func setConsentConfig(_ configString: UnsafePointer<CChar>, consent: Bool) {
    let config = String(cString: configString)
    iOSBridge.setConsentConfig(config, consent: consent)
}

@_cdecl("BidMachineSetSubjectToGDPR")
public func setSetSubjectToGDPR(_ flag: Bool) {
    iOSBridge.setGDPRZone(flag)
}

@_cdecl("BidMachineSetCoppa")
public func setCoppa(_ flag: Bool) {
    iOSBridge.setCoppa(flag)
}

@_cdecl("BidMachineSetUSPrivacyString")
public func setUSPrivacyText(_ string: UnsafePointer<CChar>?) {
    guard let string else {
        return
    }
    let privacyText = String(cString: string)
    iOSBridge.setUSPrivacy(privacyText)
}

@_cdecl("BidMachineSetGPP")
public func setGPP(gppString: UnsafePointer<CChar>, gppIds: UnsafePointer<UInt32>, length: UInt32) {
    let gppString = String(cString: gppString)
    let ids = Array(
        UnsafeBufferPointer(start: gppIds, count: Int(length))
    )
    iOSBridge.setGPP(gppString, ids: ids)
}

@_cdecl("BidMachineSetPublisher")
public func setPublisher(_ json: UnsafePointer<CChar>) {
    let jsonString = String(cString: json)
    
    guard let data = jsonString.data(using: .utf8) else {
        return
    }
    do {
        let publisher = try JSONDecoder().decode(Publisher.self, from: data)
        iOSBridge.setPublisher(publisher)
    } catch let error {
        print("Error parsing publisher: \(error)")
    }
}
