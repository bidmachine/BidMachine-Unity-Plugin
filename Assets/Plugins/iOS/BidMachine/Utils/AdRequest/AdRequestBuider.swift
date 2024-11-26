//
//  AdRequestBuider.swift
//  UnityFramework
//
//  Created by Dzmitry on 02/10/2024.
//

import Foundation
import BidMachine

struct AdRequest {
    let format: PlacementFormat
    let payload: String?
    let placementId: String?
    let timeout: TimeInterval?
    let configurations: [BidMachineBiddingUnitConfiguration]
    let customParams: [String: String]
    let priceFloors: [PriceFloorParameter]
}

final class AdRequestBuider {
    private var format: PlacementFormat?
    private var payload: String?
    private var placementId: String?
    private var timeout: TimeInterval?
    private var networks: [String]?
    private var customParameters: [String: String]?
    private var priceFloors: [PriceFloorParameter]?

    func build() -> AdRequest {
        let adFormat = format ?? .unknown
        let configurations = networks?.map {
            BidMachineBiddingUnitConfiguration($0, adFormat)
        }

        return AdRequest(
            format: adFormat,
            payload: payload,
            placementId: placementId,
            timeout: timeout,
            configurations: configurations ?? [],
            customParams: customParameters ?? [:],
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
    
    func setPlacementFormat(_ format: PlacementFormat) {
        self.format = format
    }
    
    func setCustomParameters(_ parameters: [String: String]) {
        self.customParameters = parameters
    }
}
