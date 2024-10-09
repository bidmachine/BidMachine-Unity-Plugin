//
//  FullscreenAdPresenter.swift
//  Unity-iPhone
//
//  Created by Dzmitry on 08/10/2024.
//

import UIKit
import BidMachine

protocol PresentingAd: BidMachineAdProtocol {
    func presentAd()
}

extension BidMachineRewarded: PresentingAd {}
extension BidMachineInterstitial: PresentingAd {}

final class FullscreenAdPresenter {
    private let viewController: UIViewController?

    init(rootViewController: UIViewController?) {
        self.viewController = rootViewController
    }
        
    func present(ad: PresentingAd) {
        ad.controller = viewController
        ad.presentAd()
    }
}