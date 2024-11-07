//
//  Optional+Extensions.swift
//  UnityFramework
//
//  Created by Dzmitry on 03/10/2024.
//

import Foundation

extension Optional {
    func apply(_ block: (Wrapped) -> Void) {
        switch self {
        case let .some(wrapped):
            block(wrapped)
        case .none:
            return
        }
    }
}
