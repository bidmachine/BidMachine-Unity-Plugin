//
//  KeyValueContainer.swift
//  UnityFramework
//
//  Created by Dzmitry on 04/10/2024.
//

import Foundation

struct KeyValueBox<K: Hashable & Decodable, V: Decodable> {
    let key: K
    let value: V
}

struct KeyValueList<K: Hashable & Decodable, V: Decodable> {
    let items: [KeyValueBox<K, V>]
}

extension KeyValueBox: Decodable {
    enum CodingKeys: String, CodingKey {
        case key = "Key"
        case value = "Value"
    }
}

extension KeyValueList: Decodable {}
