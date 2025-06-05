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
                if let error = error {
                    callback(.failure(.underlying(error)))
                    return
                }
                guard let ad = ad else {
                    callback(.failure(.noAd))
                    return
                }
                guard let casted = ad as? Ad else {
                    callback(.failure(.unableToCastToProvidedType))
                    return
                }
                callback(.success(casted))
            }
        } catch let error as RequestError {
            callback(.failure(error))
        } catch {
            callback(.failure(.underlying(error)))
        }
    }
    
    private func createAuctionRequest(from adRequest: AdRequest) throws -> BidMachineAuctionRequest {
#warning("timeout setting is not available since 3.3.0")
        
        // Get placement safely
        guard let placement = try? bidMachine.placement(from: adRequest.format, builder: { builder in
            if let placementId = adRequest.placementId {
                builder.withPlacementId(placementId)
            }
            builder.withCustomParameters(adRequest.customParams)
        }) else {
            throw RequestError.unableToGetPlacement
        }
        
        // Create auction request
        let request = bidMachine.auctionRequest(placement: placement) { builder in
            adRequest.priceFloors.forEach {
                builder.appendPriceFloor($0.value, $0.key)
            }
            if let payload = adRequest.payload {
                builder.withPayload(payload)
            }
            builder.withUnitConfigurations(adRequest.configurations)
        }
        
        return request
    }
}
