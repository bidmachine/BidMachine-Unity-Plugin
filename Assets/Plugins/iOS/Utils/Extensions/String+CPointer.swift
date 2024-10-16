//
//  String+CPointer.swift
//  UnityFramework
//
//  Created by Dzmitry on 07/10/2024.
//

import Foundation

extension String {
    var utf8UnmanagedPtrCopy: UnsafeMutablePointer<CChar>? {
        guard let utf8Pointer = (self as NSString).cString(using: String.Encoding.utf8.rawValue) else {
            return nil
        }
        let capacity = self.utf8.count + 1
        let ptr = UnsafeMutablePointer<CChar>.allocate(capacity: capacity)

        strncpy(ptr, utf8Pointer, capacity)
        
        return ptr
    }
}
