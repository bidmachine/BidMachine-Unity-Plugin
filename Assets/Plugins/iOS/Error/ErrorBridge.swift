//
//  ErrorBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 04/10/2024.
//

import Foundation

@_cdecl("BidMachineGetErrorCode")
public func getErrorCode(of error: NSError) -> Int {
    error.code
}

@_cdecl("BidMachineGetErrorBriefUnmanagedPointer")
public func getBriefDescription(of error: NSError) -> UnsafeMutablePointer<CChar>? {
    error.localizedDescription.utf8UnmanagedPtrCopy
}

@_cdecl("BidMachineGetErrorMessageUnmanagedPointer")
public func getErrorMessage(of error: NSError) -> UnsafeMutablePointer<CChar>? {
    error.localizedFailureReason?.utf8UnmanagedPtrCopy
}
