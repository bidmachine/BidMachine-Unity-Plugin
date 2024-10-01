//
//  TargetingParameters+BidMachine.swift
//  UnityFramework
//
//  Created by Dzmitry on 30/09/2024.
//

import Foundation
import BidMachine
import CoreLocation

extension TargetingParameters.Gender {
    var asBMGender: BidMachine.UserGender {
        switch self {
        case .female: return .female
        case .male: return .male
        case .omitted: return .unknown
        }
    }
}

extension TargetingParameters.Location {
    var coordinates: CLLocation {
        #warning("check if location is valid")
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
