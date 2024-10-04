//
//  RequestConfigBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import Foundation
import BidMachine

struct AdRequest {
    var format: PlacementFormat
    var payload: String?
    var placementId: String?
    var timeout: TimeInterval?
    var configurations: [BidMachineBiddingUnitConfiguration]
    var priceFloors: [PriceFloorParameter]
}

final class AdRequestBuider {
    private var format: PlacementFormat?
    private var payload: String?
    private var placementId: String?
    private var timeout: TimeInterval?
    private var networks: [String]?
    private var priceFloors: [PriceFloorParameter]?

    func build() -> AdRequest {
        let adFormat = format ?? .rewarded
        let configurations = networks?.map { BidMachineBiddingUnitConfiguration($0, adFormat) }

        return AdRequest(
            format: adFormat,
            payload: payload,
            placementId: placementId,
            timeout: timeout,
            configurations: configurations ?? [],
            priceFloors: priceFloors ?? []
        )
    }
    
    func setTimeout(_ interval: TimeInterval) {
        timeout = interval
    }
    
    func setPlacementID(_ id: String) {
        placementId = id
    }
    
    func setBidPayload(_ payload: String) {
        self.payload = payload
    }
    
    func setNetworks(_ networks: [String]) {
        self.networks = networks
    }
    
    func setPriceFloorParameters(_ parameters: [PriceFloorParameter]) {
        self.priceFloors = parameters
    }
}
