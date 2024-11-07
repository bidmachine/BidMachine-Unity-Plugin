//
//  UIApplication+Extensions.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import UIKit

extension UIApplication {
    static var unityRootViewController: UIViewController? {
        return UIApplication.shared.unityRootController
    }

    var unityRootController: UIViewController? {
        (delegate as? UnityAppController)?.rootViewController
    }
}
