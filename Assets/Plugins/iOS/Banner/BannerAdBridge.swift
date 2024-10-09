//
//  BannerAdBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import Foundation
import BidMachine

final class BannerAdBridge: AdBridge<BidMachineBanner> {
    private let presenter: BannerPresenterProtocol

    init(
        instance: BidMachineSdk,
        adPresenter: BannerPresenterProtocol
    ) {
        self.presenter = adPresenter
        super.init(instance: instance)
    }
    
    func show(in rect: CGRect) {
        presenter.showBanner(in: rect)
    }
    
    func hide() {
        presenter.hideBanner()
    }
}
