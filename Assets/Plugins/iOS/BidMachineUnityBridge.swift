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
    var isInitialized: Bool {
        instance.isInitialized
    }

    lazy var rewardedBridge = RewardedAdBridge(
        instance: instance,
        presenter: fullscreenAdPresenter
    )

    lazy var interstitialBridge = {
        let bridge = InterstitialAdBridge(
            instance: instance,
            presenter: fullscreenAdPresenter
        )
        bridge.forceMarkAdAsFinishedOnClose = true
        return bridge
    }()

    private let instance: BidMachineSdk
    private let fullscreenAdPresenter: FullscreenAdPresenter
    
    init(instance: BidMachineSdk) {
        self.instance = instance
        self.fullscreenAdPresenter = FullscreenAdPresenter(
            rootViewController: UIApplication.unityRootViewController
        )
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
            info.withTargetingParameters(parameters)
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
