//
//  KeyValueBox.swift
//  UnityFramework
//
//  Created by Dzmitry on 04/10/2024.
//

import Foundation

struct KeyValueBox<K: Hashable & Codable, V: Codable> {
    let key: K
    let value: V
}

struct KeyValueList<K: Hashable & Codable, V: Codable> {
    let items: [KeyValueBox<K, V>]
}

extension KeyValueBox: Codable {
    enum CodingKeys: String, CodingKey {
        case key = "Key"
        case value = "Value"
    }
}

extension KeyValueList: Codable {}
