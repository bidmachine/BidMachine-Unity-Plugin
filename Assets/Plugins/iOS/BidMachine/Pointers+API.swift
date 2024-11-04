//
//  BidMachineUnityBridge.swift
//  UnityFramework
//
//  Created by Dzmitry on 04/10/2024.
//

@_cdecl("BidMachineReleasePointer")
public func BidMachineReleasePointer(_ ptr: UnsafeMutableRawPointer?) {
    ptr?.deallocate()
}
