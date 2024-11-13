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
        let customParameters = Helper.createKeyValueListCastingValues(from: customParams)
        let customExtras = Helper.createKeyValueListCastingValues(from: customExtras)
        
        let result = AuctionResultDTO(
            dealId: dealId,
            demandSource: demandSource,
            cID: cId,
            customParams: customParameters,
            customExtras: customExtras,
            creativeID: creativeId,
            bidID: bidId,
            price: price
        )

        let encoder = JSONEncoder()
        encoder.outputFormatting = .prettyPrinted

        guard let jsonData = try? encoder.encode(result) else {
            return nil
        }
        let jsonString = String(data: jsonData, encoding: .utf8)
        return jsonString
    }
}

private struct AuctionResultDTO: Encodable {
    let dealId: String?
    let demandSource: String
    let cID: String?
    let customParams: KeyValueList<String, String>
    let customExtras: KeyValueList<String, String>
    let creativeID: String?
    let bidID: String
    let price: Double
    
    enum CodingKeys: String, CodingKey {
        case dealId = "DealID"
        case demandSource = "DemandSource"
        case cID = "CID"
        case customParams = "CustomParams"
        case customExtras = "CustomExtras"
        case creativeID = "CreativeID"
        case bidID = "BidID"
        case price = "Price"
    }
    
    func encode(to encoder: any Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        try container.encode(self.dealId, forKey: .dealId)
        try container.encode(self.demandSource, forKey: .demandSource)
        try container.encode(self.cID, forKey: .cID)
        try container.encode(self.customParams, forKey: .customParams)
        try container.encode(self.customExtras, forKey: .customExtras)
        try container.encode(self.creativeID, forKey: .creativeID)
        try container.encode(self.bidID, forKey: .bidID)
        try container.encode(self.price, forKey: .price)
    }
}

private enum Helper {
    static func createKeyValueListCastingValues(from dictionary: [String: Any]) -> KeyValueList<String, String> {
        let items = dictionary.compactMap { (key, value) -> KeyValueBox<String, String>? in
            guard let casted = value as? String else {
                return nil
            }
            return KeyValueBox(key: key, value: casted)
        }
        return KeyValueList(items: items)
    }
}
