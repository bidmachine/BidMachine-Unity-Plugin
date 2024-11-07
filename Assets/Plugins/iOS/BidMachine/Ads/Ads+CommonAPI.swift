//
//  CommonAdsAPI.swift
//  UnityFramework
//
//  Created by Dzmitry on 07/11/2024.
//

import Foundation
import BidMachine

enum CommonAdsAPI {
    static func setCustomParams<Ad: BidMachineAdProtocol>(
        jsonString: UnsafePointer<CChar>,
        bridge: AdBridge<Ad>
    ) {
        let jsonString = String(cString: jsonString)
        
        do {
            let paramsList: KeyValueList<String, String> = try JSONStringDecoder.decode(
                from: jsonString
            )
            let dict = paramsList.items.reduce(into: [String: String]()) { accumulator, box in
                accumulator[box.key] = box.value
            }
            bridge.setCustomParams(dict)
        } catch let error {
            print("Error parsing price floor params: \(error.localizedDescription)")
        }
    }
    
    static func setPriceFloorParams<Ad: BidMachineAdProtocol>(
        jsonString: UnsafePointer<CChar>,
        bridge: AdBridge<Ad>
    ) {
        let jsonString = String(cString: jsonString)
        
        do {
            let parametersList: PriceFloorParameters = try JSONStringDecoder.decode(
                from: jsonString
            )
            bridge.setPriceFloorParams(parametersList.items)
        } catch let error {
            print("Error parsing price floor params: \(error.localizedDescription)")
        }
    }
    
    static func setNetworks<Ad: BidMachineAdProtocol>(
        _ networks: UnsafePointer<CChar>,
        bridge: AdBridge<Ad>
    ) {
        let networksString = String(cString: networks)
        let networksNames = NetworksNamesDecoder.decode(from: networksString)
        
        bridge.setNetworks(networksNames)
    }
}
