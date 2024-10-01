//
//  BidMachineiOSBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 27/09/2024.
//

import Foundation
import BidMachine

private enum Constant {
    static let endpointKey = "overrideSessionUrl"
}

fileprivate let bridge = BidMachineUnityBridge(instance: .shared)

private final class BidMachineUnityBridge {
    private let rewardedHandler = BidMachineRewardedAdHandler()

    var isInitialized: Bool {
        instance.isInitialized
    }
    
    private let instance: BidMachineSdk
    
    init(instance: BidMachineSdk) {
        self.instance = instance
    }
    
    func initialize(with sourceId: String) {
        instance.initializeSdk(sourceId)
    }

    func setEndpoint(url: String) {
        instance.installFeature(Constant.endpointKey, url)
    }
    
    func setLoggingEnabled(_ enabled: Bool) {
        instance.populate {
            $0.withLoggingMode(enabled)
        }
    }
    
    func setTestEnabled(_ enabled: Bool) {
        instance.populate {
            $0.withTestMode(enabled)
        }
    }
    
    func setTargeting(parameters: TargetingParameters) {
        instance.targetingInfo.populate { info in
            info.withUserId(parameters.userId)
            info.withUserGender(parameters.gender.asBMGender)
            info.withUserYOB(UInt32(parameters.birthdayYear))
            info.withKeywords(parameters.keywords.first ?? "") // FIXME: how to handle array?
            info.withUserLocation(parameters.location.coordinates)
            info.withCountry(parameters.country)
            info.withCity(parameters.city)
            info.withZip(parameters.zipCode)
            info.withStoreURL(parameters.storeURL)
            info.withStoreCategory(parameters.storeCategory)
            info.withStoreSubCategories(parameters.storeSubCategories)
            info.withFrameworkName(parameters.framework.asBMFrameworkName) // FIXME: insert proper value here
            info.withPaid(parameters.isPaid)

            parameters.externalUserIDs.forEach {
                info.appendExternalId($0.sourceId, $0.value) // FIXME: insert proper value here
            }

            info.withBlockedAdvertisers(parameters.blockedDomains)
            info.withBlockedCategories(parameters.blockedCategories)
            info.withBlockedApps(parameters.blockedApplications)
        }
    }
    
    func setConsentConfig(_ configString: String, consent: Bool) {
        instance.regulationInfo.populate {
            $0.withGDPRConsent(consent)
            $0.withGDPRConsentString(configString)
        }
    }
    
    func setGDPRZone(_ gdpr: Bool) {
        instance.regulationInfo.populate {
            $0.withGDPRZone(gdpr)
        }
    }
    
    func setCoppa(_ flag: Bool) {
        instance.regulationInfo.populate {
            $0.withCOPPA(flag)
        }
    }
    
    func setUSPrivacy(_ privacyText: String) {
        instance.regulationInfo.populate {
            $0.withUSPrivacyString(privacyText)
        }
    }
    
    func setGPP(_ gppString: String, ids: [UInt32]) {
        instance.regulationInfo.populate {
            $0.withGPP(gppString, ids)
        }
    }
    
    func setPublisher(_ publisher: Publisher) {
        instance.publisherInfo.populate {
            $0.withCategories(publisher.categories)
            $0.withId(publisher.id)
            $0.withName(publisher.name)
            $0.withDomain(publisher.domain)
        }
    }
}

@_cdecl("BidMachineInitialize")
public func initialize(_ sellerId: UnsafePointer<CChar>?) {
    guard let sellerId else {
        return
    }
    let sourceId = String(cString: sellerId)
    bridge.initialize(with: sourceId)
}

@_cdecl("BidMachineIsInitialized")
public func isInitialized() -> Bool {
    return bridge.isInitialized
}

@_cdecl("BidMachineSetEndpoint")
public func setEndpoint(url: UnsafePointer<CChar>?) {
    guard let url else {
        return
    }
    let urlString = String(cString: url)
    bridge.setEndpoint(url: urlString)
}

@_cdecl("BidMachineSetLoggingEnabled")
public func setLoggingEnabled(_ enabled: Bool) {
    bridge.setLoggingEnabled(enabled)
}

@_cdecl("BidMachineSetTestEnabled")
public func setTestEnabled(_ enabled: Bool) {
    bridge.setTestEnabled(enabled)
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
        bridge.setTargeting(parameters: parameters)
    } catch let error {
        // FIXME: add error handling
        print("[DEBUG]: error \(error.localizedDescription)")
    }
}

@_cdecl("BidMachineSetConsentConfig")
public func setConsentConfig(_ configString: UnsafePointer<CChar>, consent: Bool) {
    let config = String(cString: configString)
    bridge.setConsentConfig(config, consent: consent)
}

@_cdecl("BidMachineSetSubjectToGDPR")
public func setSetSubjectToGDPR(_ flag: Bool) {
    bridge.setGDPRZone(flag)
}

@_cdecl("BidMachineSetCoppa")
public func setCoppa(_ flag: Bool) {
    bridge.setCoppa(flag)
}

@_cdecl("BidMachineSetUSPrivacyString")
public func setUSPrivacyText(_ string: UnsafePointer<CChar>?) {
    guard let string else {
        return
    }
    let privacyText = String(cString: string)
    bridge.setUSPrivacy(privacyText)
}

@_cdecl("BidMachineSetGPP")
public func setGPP(gppString: UnsafePointer<CChar>, gppIds: UnsafePointer<UInt32>, length: UInt32) {
    let gppString = String(cString: gppString)
    let ids = Array(
        UnsafeBufferPointer(start: gppIds, count: Int(length))
    )
    bridge.setGPP(gppString, ids: ids)
}

@_cdecl("BidMachineSetPublisher")
public func setPublisher(_ json: UnsafePointer<CChar>) {
    let jsonString = String(cString: json)
    
    guard let data = jsonString.data(using: .utf8) else {
        return
    }
    do {
        let publisher = try JSONDecoder().decode(Publisher.self, from: data)
        bridge.setPublisher(publisher)
    } catch let error {
        print("Error parsing publisher: \(error)")
    }
}