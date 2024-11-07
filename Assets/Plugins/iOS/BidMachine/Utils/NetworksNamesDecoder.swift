//
//  NetworksNamesDecoder.swift
//  UnityFramework
//
//  Created by Dzmitry on 15/10/2024.
//

import Foundation

struct NetworksNamesDecoder {
    static func decode(from string: String) -> [String] {
        let networksNames = string.components(separatedBy: ",")
        return networksNames
    }
}
