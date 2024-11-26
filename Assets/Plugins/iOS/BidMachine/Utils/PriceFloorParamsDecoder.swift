//
//  PriceFloorParamsDecoder.swift
//  UnityFramework
//
//  Created by Dzmitry on 15/10/2024.
//

import Foundation

struct JSONStringDecoder {
    enum Error: Swift.Error {
        case invalidJSONString(String)
        case underlying(Swift.Error)
    }

    static func decode<T: Decodable >(from jsonString: String) throws -> T {
        guard let data = jsonString.data(using: .utf8) else {
            throw Error.invalidJSONString(jsonString)
        }
        do {
            let decoded = try JSONDecoder().decode(T.self, from: data)
            return decoded
        } catch let error {
            throw Error.underlying(error)
        }
    }
}
