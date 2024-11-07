//
//  PriceFloorParamsDecoder.swift
//  UnityFramework
//
//  Created by Dzmitry on 15/10/2024.
//

import Foundation

struct PriceFloorParamsDecoder {
    enum Error: Swift.Error {
        case invalidJSONString(String)
        case underlying(Swift.Error)
    }

    static func decode(from jsonString: String) throws -> PriceFloorParameters {
        guard let data = jsonString.data(using: .utf8) else {
            throw Error.invalidJSONString(jsonString)
        }
        do {
            let parametersList = try JSONDecoder().decode(
                PriceFloorParameters.self,
                from: data
            )
            return parametersList
        } catch let error {
            throw Error.underlying(error)
        }
    }
}
