//
//  AdLayout.swift
//  UnityFramework
//
//  Created by Dzmitry on 09/10/2024.
//

import Foundation

struct AdLayout {
    let verticalPin: Vertical
    let horizontalPin: Horizontal
}

extension AdLayout {
    enum Vertical: Int {
        case center = 16
        case top = 48
        case bottom = 80
    }

    enum Horizontal: Int {
        case center = 1
        case left = 3
        case right = 5
    }
}
