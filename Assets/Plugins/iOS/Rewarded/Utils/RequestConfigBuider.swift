//
//  RequestConfigBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import Foundation
import BidMachine

struct RequestConfigBuider {
    private let config: BidMachineRequestConfigurationProtocol
    private let format: PlacementFormat

    init(
        initial: BidMachineRequestConfigurationProtocol,
        format: PlacementFormat
    ) {
        self.config = initial
        self.format = format
    }

    func build() -> BidMachineRequestConfigurationProtocol {
        config
    }
    
    func setTimeout(_ interval: TimeInterval) {
        config.populate {
            $0.withTimeout(interval)
        }
    }
    
    func setPlacementID(_ id: String) {
        config.populate {
            $0.withPlacementId(id)
        }
    }
    
    func setBidPayload(_ payload: String) {
        config.populate {
            $0.withPayload(payload)
        }
    }
    
    func setNetworks(_ networks: [String]) {
        let configurations = networks.map { BidMachineBiddingUnitConfiguration($0, format) }

        config.populate {
            $0.withUnitConfigurations(configurations)
        }
    }
}
