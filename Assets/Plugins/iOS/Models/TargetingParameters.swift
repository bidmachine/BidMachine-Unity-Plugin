//
//  TargetingParameters.swift
//  UnityFramework
//
//  Created by Dzmitry on 30/09/2024.
//

import Foundation

struct TargetingParameters {
    struct Location {
        let povider: String
        let latitude: Double
        let longitude: Double
    }
    
    struct ExternalUser {
        let sourceId: String
        let value: String
    }
    
    enum Gender: Int, Decodable {
        case female = 0
        case male
        case omitted
    }
    
    enum Framework: String, Decodable {
        case unity, native
    }

    let userId: String
    let gender: Gender
    let birthdayYear: Int
    let keywords: [String]
    let location: Location
    let country: String
    let city: String
    let zipCode: String
    let storeURL: String
    let storeCategory: String
    let storeSubCategories: [String]
    let framework: Framework
    let isPaid: Bool
    let externalUserIDs: [ExternalUser]
    let blockedDomains: [String]
    let blockedCategories: [String]
    let blockedApplications: [String]
}

extension TargetingParameters: Decodable {
    enum CodingKeys: String, CodingKey {
        case userId = "UserId"
        case gender = "Gender"
        case birthdayYear = "BirthdayYear"
        case keywords = "Keywords"
        case location = "DeviceLocation"
        case country = "Country"
        case city = "City"
        case zipCode = "Zip"
        case storeURL = "StoreUrl"
        case storeCategory = "StoreCategory"
        case storeSubCategories = "StoreSubCategories"
        case framework = "Framework"
        case isPaid = "IsPaid"
        case externalUserIDs = "externalUserIds"
        case blockedDomains = "BlockedDomains"
        case blockedCategories = "BlockedCategories"
        case blockedApplications = "BlockedApplications"
    }
}

extension TargetingParameters.Location: Decodable {
    enum CodingKeys: String, CodingKey {
        case povider = "Provider"
        case latitude = "Latitude"
        case longitude = "Longitude"
    }
}

extension TargetingParameters.ExternalUser: Decodable {
    enum CodingKeys: String, CodingKey {
        case sourceId = "SourceId"
        case value = "Value"
    }
}
