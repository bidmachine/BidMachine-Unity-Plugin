//
//  MillisecondsConverter.swift
//  UnityFramework
//
//  Created by Dzmitry on 15/10/2024.
//

import Foundation

struct MillisecondsConverter {
    static func toSeconds(_ milliseconds: Int) -> Double {
        Double(milliseconds) / 1000
    }
}
