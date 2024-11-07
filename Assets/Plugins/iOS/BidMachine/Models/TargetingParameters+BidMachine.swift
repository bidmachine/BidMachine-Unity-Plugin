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

extension BidMachine.FrameworkName {
    init(_ targetingFramework: TargetingParameters.Framework) {
        switch targetingFramework {
        case .native: self = .native
        case .unity: self = .unity
        }
    }
}

extension TargetingParameters.Location {
    var coordinates: CLLocation {
        return CLLocation(latitude: latitude, longitude: longitude)
    }
}
