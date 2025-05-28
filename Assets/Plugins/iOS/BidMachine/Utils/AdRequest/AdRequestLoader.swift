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
        case unableToGetPlacement
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
        do {
            let adRequest = try createAuctionRequest(from: request)
            bidMachine.ad(request: adRequest) { ad, error in
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
        } catch {
            callback(.failure(error))
        }
    }

    private func createAuctionRequest(from adRequest: AdRequest) throws (RequestError) -> BidMachineAuctionRequest {
        // FIXME: where is timeout setting now?
        
        let placement = try? bidMachine.placement(from: adRequest.format) { builder in
            adRequest.placementId.apply { builder.withPlacementId($0) }
            builder.withCustomParameters(adRequest.customParams)
        }
        
        guard let placement else {
            throw RequestError.unableToGetPlacement
        }
        
        let request = bidMachine.auctionRequest(placement: placement) { builder in
            adRequest.priceFloors.forEach {
                builder.appendPriceFloor($0.value, $0.key)
            }
            adRequest.payload.apply { builder.withPayload($0) }
            builder.withUnitConfigurations(adRequest.configurations)
        }
        return request
    }
}
