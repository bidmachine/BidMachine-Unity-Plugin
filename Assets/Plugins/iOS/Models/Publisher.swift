//
//  Publisher.swift
//  UnityFramework
//
//  Created by Dzmitry on 30/09/2024.
//

import Foundation

struct Publisher {
    let id: String
    let name: String
    let domain: String
    let categories: [String]
}

extension Publisher: Decodable {
    enum CodingKeys: String, CodingKey {
        case id = "Id"
        case name = "Name"
        case domain = "Domain"
        case categories = "Categories"
    }
}
