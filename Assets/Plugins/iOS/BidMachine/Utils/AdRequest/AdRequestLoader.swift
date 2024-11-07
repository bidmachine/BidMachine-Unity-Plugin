//
//  AdRequestLoader.swift
//  UnityFramework
//
//  Created by Dzmitry on 03/10/2024.
//

import Foundation
import BidMachine

final class AdRequestLoader<T: BidMachineAdProtocol> {
    enum RequestError: Error {
        case noAd
        case unableToGetConfig
        case underlying(Error)
        case unableToCastToProvidedType
    }

    typealias Ad = T

    private let bidMachine: BidMachineSdk
    
    init(bidMachine: BidMachineSdk) {
        self.bidMachine = bidMachine
    }
    
    func load(
        request: AdRequest,
        callback: @escaping (Result<Ad, RequestError>) -> Void
    ) {
        guard let config = self.createConfiguration(for: request) else {
            callback(.failure(.unableToGetConfig))
            return
        }
        bidMachine.ad(config) { ad, error in
            if let error {
                callback(.failure(.underlying(error)))
                return
            }
            guard let ad else {
                callback(.failure(.noAd))
                return
            }
            guard let casted = ad as? Ad else {
                callback(.failure(.unableToCastToProvidedType))
                return
            }
            callback(.success(casted))
        }
    }

    private func createConfiguration(for adRequest: AdRequest) -> BidMachineRequestConfigurationProtocol? {
        guard let config = try? bidMachine.requestConfiguration(adRequest.format) else {
            return nil
        }
        config.populate { builder in
            builder.withUnitConfigurations(adRequest.configurations)

            adRequest.priceFloors.forEach {
                builder.appendPriceFloor($0.value, $0.key)
            }
            adRequest.payload.apply { builder.withPayload($0) }
            adRequest.placementId.apply { builder.withPlacementId($0) }
            adRequest.timeout.apply { builder.withTimeout($0) }
        }
        return config
    }
}
