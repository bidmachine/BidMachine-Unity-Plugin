//
//  AdBridgeFactory.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import Foundation
import BidMachine

enum AdBridgeFactory {
    static func interstitial<Presenter: FullscreenAdPresenterProtocol>(
        bidmachine: BidMachineSdk,
        presenter: Presenter
    ) -> InterstitialAdBridge {
        let bridge = InterstitialAdBridge(
            instance: bidmachine,
            defaultPlacementFormat: .interstitial,
            adPresenter: presenter
        )
        bridge.forceMarkAdAsFinishedOnClose = true
        return bridge
    }
    
    static func rewarded<Presenter: FullscreenAdPresenterProtocol>(
        bidmachine: BidMachineSdk,
        presenter: Presenter
    ) -> RewardedAdBridge {
        let bridge = RewardedAdBridge(
            instance: bidmachine,
            defaultPlacementFormat: .rewarded,
            adPresenter: presenter
        )
        return bridge
    }
    
    static func banner(
        bidmachine: BidMachineSdk,
        presenter: BannerPresenterProtocol
    ) -> BannerAdBridge {
        let bridge = BannerAdBridge(
            instance: bidmachine,
            adPresenter: presenter
        )
        bridge.forceMarkAdAsFinishedOnClose = true
        return bridge
    }
}
