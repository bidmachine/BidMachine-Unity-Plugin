//
//  RewardedRequestProtocol.swift
//  UnityFramework
//
//  Created by Dzmitry on 03/10/2024.
//

import Foundation

protocol RewardedRequestProtocol {
    var auctionResult: String? { get }
    var isExpired: Bool { get }
    var isDestroyed: Bool { get }
}
