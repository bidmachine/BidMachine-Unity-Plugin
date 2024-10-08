//
//  TargetingParameters+BidMachine.swift
//  UnityFramework
//
//  Created by Dzmitry on 30/09/2024.
//

import Foundation
import BidMachine
import CoreLocation

extension BidMachine.UserGender {
    init(_ targetingGender: TargetingParameters.Gender) {
        switch targetingGender {
        case .female: self = .female
        case .male: self = .male
        case .omitted: self = .unknown
        }
    }
}

extension TargetingParameters.Location {
    var coordinates: CLLocation {
        return CLLocation(latitude: latitude, longitude: longitude)
    }
}

extension TargetingParameters.Framework {
    var asBMFrameworkName: BidMachine.FrameworkName {
        switch self {
        case .unity: return .unity
        case .native: return .native
        }
    }
}
