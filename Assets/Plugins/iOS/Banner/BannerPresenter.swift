//
//  BannerPresenter.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import UIKit

protocol BannerPresenterProtocol {
    func showBanner(in rect: CGRect)
    func hideBanner()
}

final class BannerPresenter: BannerPresenterProtocol {
    private let viewController: UIViewController?

    init(viewController: UIViewController?) {
        self.viewController = viewController
    }

    func showBanner(in rect: CGRect) {
        // FIXME: add logic
    }
    
    func hideBanner() {
        // FIXME: add logic
    }
}
