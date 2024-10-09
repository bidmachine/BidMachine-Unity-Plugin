//
//  AdRequestsEventsHandlerProtocol.swift
//  UnityFramework
//
//  Created by Dzmitry on 08/10/2024.
//

import Foundation
import BidMachine

enum AdRequestEvent {
    case adLoaded(_ ad: BidMachineAdProtocol)
    case adExpired(_ ad: BidMachineAdProtocol)
    case adLoadFailed(error: Error)
}

protocol AdRequestsEventsHandlerProtocol {
    func handle(_ event: AdRequestEvent)
}
