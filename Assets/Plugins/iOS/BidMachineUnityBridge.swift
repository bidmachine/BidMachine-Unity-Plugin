//
//  BidMachineiOSBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 27/09/2024.
//

import Foundation
import BidMachine

let iOSUnityBridge = BidMachineUnityBridge(instance: .shared)

final class BidMachineUnityBridge {
    let rewardedBridge: RewardedAdBridge

    var isInitialized: Bool {
        instance.isInitialized
    }
    
    private let instance: BidMachineSdk
    
    init(instance: BidMachineSdk) {
        self.instance = instance
        self.rewardedBridge = RewardedAdBridge(instance: instance)
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
