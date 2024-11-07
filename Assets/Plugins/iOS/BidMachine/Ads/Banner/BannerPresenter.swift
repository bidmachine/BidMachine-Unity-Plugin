//
//  BannerPresenter.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import UIKit
import BidMachine

protocol BannerPresenterProtocol {
    func prepareForPresentation(_ banner: BidMachineBanner)

    @discardableResult
    func present(with layout: AdLayout, size: CGSize) -> Bool

    func hideBanner()
}

final class BannerPresenter: BannerPresenterProtocol {
    private let viewController: UIViewController?
    private var banner: BidMachineBanner?

    init(viewController: UIViewController?) {
        self.viewController = viewController
    }
    
    func prepareForPresentation(_ banner: BidMachineBanner) {
        banner.controller = viewController
        self.banner = banner
    }

    @discardableResult
    func present(with layout: AdLayout, size: CGSize) -> Bool {
        guard let banner, let parentView = viewController?.view else {
            return false
        }

        parentView.addSubview(banner)
        banner.translatesAutoresizingMaskIntoConstraints = false
        
        let horizontalConstraint = switch layout.horizontalPin {
        case .center:
            banner.centerXAnchor.constraint(equalTo: parentView.centerXAnchor)
        case .left:
            banner.leftAnchor.constraint(equalTo: parentView.leftAnchor)
        case .right:
            banner.rightAnchor.constraint(equalTo: parentView.centerXAnchor)
        }
        
        let verticalConstraint = switch layout.verticalPin {
        case .top:
            banner.topAnchor.constraint(equalTo: parentView.safeAreaLayoutGuide.topAnchor)
        case .center:
            banner.centerYAnchor.constraint(equalTo: parentView.centerYAnchor)
        case .bottom:
            banner.bottomAnchor.constraint(equalTo: parentView.safeAreaLayoutGuide.bottomAnchor)
        }
 
        NSLayoutConstraint.activate([
            banner.widthAnchor.constraint(equalToConstant: size.width),
            banner.heightAnchor.constraint(equalToConstant: size.height),
            horizontalConstraint,
            verticalConstraint
        ])
        return true
    }
    
    func hideBanner() {
        banner?.removeFromSuperview()
        banner = nil
    }
}
