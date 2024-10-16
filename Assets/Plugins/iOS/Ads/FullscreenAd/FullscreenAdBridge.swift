//
//  FullscreenAdBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 08/10/2024.
//

import Foundation
import BidMachine

final class FullscreenAdBridge<T: PresentingAd>: AdBridge<T> {
    private let adPresenter: FullscreenAdPresenterProtocol
    
    init(
        instance: BidMachineSdk,
        defaultPlacementFormat: PlacementFormat,
        adPresenter: FullscreenAdPresenterProtocol
    ) {
        self.adPresenter = adPresenter

        super.init(
            instance: instance,
            defaultPlacementFormat: defaultPlacementFormat
        )
    }

    func show() {
        guard let loadedAd else {
            return
        }
        adPresenter.present(ad: loadedAd)
    }
}
