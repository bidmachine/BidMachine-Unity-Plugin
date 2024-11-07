//
//  BidMachineAuctionResponseProtocol+JSONString.swift
//  UnityFramework
//
//  Created by Dzmitry on 07/10/2024.
//

import Foundation
import BidMachine

extension BidMachineAuctionResponseProtocol {
    var resultJsonString: String? {
        let dataDict = [
            "dealID": dealId as Any,
            "demandSource": demandSource,
            "cID": cId as Any,
            "customParams": customParams.asStringValues,
            "customExtras": customExtras.asStringValues,
            "creativeID": creativeId as Any,
            "bidID": bidId,
            "price": "\(price)"
        ]
        guard let jsonData = try? JSONSerialization.data(withJSONObject: dataDict) else {
            return nil
        }
        let jsonString = String(data: jsonData, encoding: .utf8)
        return jsonString
    }
}

private extension Dictionary where Key == String {
    var asStringValues: [Key: String] {
        return self.compactMapValues { ($0 as? CustomStringConvertible)?.description }
    }
}
