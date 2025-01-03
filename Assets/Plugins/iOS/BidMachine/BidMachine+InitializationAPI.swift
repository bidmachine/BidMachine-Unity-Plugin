//
//  BidMachine+InitializationAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 01/10/2024.
//

import Foundation

@_cdecl("BidMachineInitialize")
public func initialize(_ sellerId: UnsafePointer<CChar>) {
    let sourceId = String(cString: sellerId)
    iOSUnityBridge.initialize(with: sourceId)
}

@_cdecl("BidMachineIsInitialized")
public func isInitialized() -> Bool {
    return iOSUnityBridge.isInitialized
}

@_cdecl("BidMachineSetEndpoint")
public func setEndpoint(url: UnsafePointer<CChar>) {
    let urlString = String(cString: url)
    iOSUnityBridge.setEndpoint(url: urlString)
}

@_cdecl("BidMachineSetLoggingEnabled")
public func setLoggingEnabled(_ enabled: Bool) {
    iOSUnityBridge.setLoggingEnabled(enabled)
}

@_cdecl("BidMachineSetTestEnabled")
public func setTestEnabled(_ enabled: Bool) {
    iOSUnityBridge.setTestEnabled(enabled)
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
        iOSUnityBridge.setTargeting(parameters: parameters)
    } catch let error {
        print("[DEBUG]: error \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineSetConsentConfig")
public func setConsentConfig(_ configString: UnsafePointer<CChar>, consent: Bool) {
    let config = String(cString: configString)
    iOSUnityBridge.setConsentConfig(config, consent: consent)
}

@_cdecl("BidMachineSetSubjectToGDPR")
public func setSetSubjectToGDPR(_ flag: Bool) {
    iOSUnityBridge.setGDPRZone(flag)
}

@_cdecl("BidMachineSetCoppa")
public func setCoppa(_ flag: Bool) {
    iOSUnityBridge.setCoppa(flag)
}

@_cdecl("BidMachineSetUSPrivacyString")
public func setUSPrivacyText(_ string: UnsafePointer<CChar>) {
    let privacyText = String(cString: string)
    iOSUnityBridge.setUSPrivacy(privacyText)
}

@_cdecl("BidMachineSetGPP")
public func setGPP(gppString: UnsafePointer<CChar>, gppIds: UnsafePointer<UInt32>, length: UInt32) {
    let gppString = String(cString: gppString)
    let ids = Array(
        UnsafeBufferPointer(start: gppIds, count: Int(length))
    )
    iOSUnityBridge.setGPP(gppString, ids: ids)
}

@_cdecl("BidMachineSetPublisher")
public func setPublisher(_ json: UnsafePointer<CChar>) {
    let jsonString = String(cString: json)
    
    guard let data = jsonString.data(using: .utf8) else {
        return
    }
    do {
        let publisher = try JSONDecoder().decode(Publisher.self, from: data)
        iOSUnityBridge.setPublisher(publisher)
    } catch let error {
        print("Error parsing publisher: \(error)")
    }
}
