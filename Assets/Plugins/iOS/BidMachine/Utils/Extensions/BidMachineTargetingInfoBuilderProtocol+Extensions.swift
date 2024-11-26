//
//  BidMachineTargetingInfoBuilderProtocol+Extensions.swift
//  UnityFramework
//
//  Created by Dzmitry on 08/10/2024.
//

import Foundation
import BidMachine

extension BidMachineTargetingInfoBuilderProtocol {
    @discardableResult
    func withTargetingParameters(_ parameters: TargetingParameters) -> Self {
        self.withUserId(parameters.userId)
        
        let userGender = BidMachine.UserGender(parameters.gender)
        self.withUserGender(userGender)
        
        let keywords = parameters.keywords.joined(separator: ",")
        self.withKeywords(keywords)

        self.withUserYOB(UInt32(parameters.birthdayYear))
        self.withUserLocation(parameters.location.coordinates)
        self.withCountry(parameters.country)
        self.withCity(parameters.city)
        self.withZip(parameters.zipCode)
        self.withStoreURL(parameters.storeURL)
        self.withStoreCategory(parameters.storeCategory)
        self.withStoreSubCategories(parameters.storeSubCategories)
        
        let frameworkName = BidMachine.FrameworkName(parameters.framework)
        self.withFrameworkName(frameworkName)

        parameters.externalUserIDs.forEach {
            self.appendExternalId($0.sourceId, $0.value)
        }

        self.withPaid(parameters.isPaid)
        self.withBlockedAdvertisers(parameters.blockedDomains)
        self.withBlockedCategories(parameters.blockedCategories)
        self.withBlockedApps(parameters.blockedApplications)

        return self
    }
}
