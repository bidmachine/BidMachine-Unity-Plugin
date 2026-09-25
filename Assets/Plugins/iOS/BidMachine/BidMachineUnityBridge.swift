//
//  BidMachineUnityBridge.swift
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

    lazy var rewardedBridge = AdBridgeFactory.rewarded(
        bidmachine: instance,
        presenter: fullscreenAdPresenter
    )

    lazy var interstitialBridge = AdBridgeFactory.interstitial(
        bidmachine: instance,
        presenter: fullscreenAdPresenter
    )
    
    lazy var bannerBridge = AdBridgeFactory.banner(
        bidmachine: instance,
        presenter: bannerPresenter
    )

    private let instance: BidMachineSdk

    private let fullscreenAdPresenter: FullscreenAdPresenter
    private let bannerPresenter: BannerPresenter
    
    init(instance: BidMachineSdk) {
        self.instance = instance

        let rootViewController = UIApplication.unityRootViewController
        
        self.fullscreenAdPresenter = FullscreenAdPresenter(
            rootViewController: rootViewController
        )
        self.bannerPresenter = BannerPresenter(
            viewController: rootViewController
        )
    }
    
    func initialize(with sourceId: String) {
        instance.initializeSdk(sourceId)
    }

    func setEndpoint(url: String) {
        instance.installFeature(Constant.endpointKey, url)
    }
    
    func setLoggingEnabled(_ enabled: Bool) {
        instance.withLoggingMode(enabled)
    }
    
    func setTestEnabled(_ enabled: Bool) {
        instance.withTestMode(enabled)
    }
    
    func setTargeting(parameters: TargetingParameters) {
        instance.withTargetingParameters(parameters)
    }
    
    func setConsentConfig(_ configString: String, consent: Bool) {
        instance.withGDPRConsent(consent)
        // The SDK has no direct replacement for withGDPRConsentString yet
        instance.regulationInfo.populate {
            $0.withGDPRConsentString(configString)
        }
    }
    
    func setConsentStatus(_ consent: Bool) {
        instance.withGDPRConsent(consent)
    }
    
    func setGDPRZone(_ gdpr: Bool) {
        instance.withGDPRZone(gdpr)
    }
    
    func setCoppa(_ flag: Bool) {
        instance.withCOPPA(flag)
    }
    
    func setUSPrivacy(_ privacyText: String) {
        instance.withUSPrivacyString(privacyText)
    }
    
    func setGPP(_ gppString: String, ids: [UInt32]) {
        instance.withGPP(gppString, ids)
    }
    
    func setNonPersonalized(_ nonPersonalized: Bool) {
        instance.withNonPersonalized(nonPersonalized)
    }
    
    func setPublisher(_ publisher: Publisher) {
        instance
            .withPubCategories(publisher.categories)
            .withPubId(publisher.id)
            .withPubName(publisher.name)
            .withPubDomain(publisher.domain)
    }
}
