//
//  BannerAdBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import Foundation
import BidMachine

final class BannerAdBridge: AdBridge<BidMachineBanner> {
    enum BannerSize: Int {
        case size_320x50 = 0
        case size_300x250
        case size_728x90
    }

    private let presenter: BannerPresenterProtocol
    private var size: BannerSize = .size_320x50

    init(
        instance: BidMachineSdk,
        adPresenter: BannerPresenterProtocol
    ) {
        self.presenter = adPresenter
        super.init(instance: instance, defaultPlacementFormat: .banner)
    }

    override func destroy() {
        hide()
        super.destroy()
    }
    
    override func didReceiveAd() {
        if let loadedAd {
            presenter.prepareForPresentation(loadedAd)
            loadedAd.loadAd()
        }
    }

    @discardableResult
    func show(with layout: AdLayout) -> Bool {
        presenter.present(with: layout, size: size.cgSize)
    }
    
    func hide() {
        presenter.hideBanner()
    }

    func setSize(_ size: BannerSize) {
        self.size = size
    }
}

private extension BannerAdBridge.BannerSize {
    var cgSize: CGSize {
        switch self {
        case .size_320x50: return CGSize(width: 320, height: 50)
        case .size_300x250: return CGSize(width: 300, height: 250)
        case .size_728x90: return CGSize(width: 728, height: 90)
        }
    }
}
