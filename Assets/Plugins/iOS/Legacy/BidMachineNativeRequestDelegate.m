// #import "BidMachineNativeRequestDelegate.h"

// @implementation BidMachineNativeRequestDelegate : NSObject

// - (void)request:(BDMRequest *)request failedWithError:(NSError *)error {
//     if (self.onNativeRequestFailed) {
//         self.onNativeRequestFailed((BDMNativeAdRequest *)request,error);
//     }
// }

// - (void)request:(BDMRequest *)request completeWithAd:(id<BDMAdProtocol>)adObject{
//     if(adObject.auctionInfo){
        
//         NSString *jsonString = @"";
//         NSMutableDictionary *dictionary = [NSMutableDictionary new];
        
//         dictionary[@"adDomains"] = adObject.auctionInfo.adDomains;
//         dictionary[@"bidID"] = adObject.auctionInfo.bidID;
//         dictionary[@"cID"] = adObject.auctionInfo.cID;
//         dictionary[@"creativeID"] = adObject.auctionInfo.creativeID;
//         dictionary[@"serverParams"] = adObject.auctionInfo.customParams;
//         dictionary[@"dealID"] = adObject.auctionInfo.dealID;
//         dictionary[@"demandSource"] = adObject.auctionInfo.demandSource;
//         dictionary[@"price"] = adObject.auctionInfo.price;
        
//         NSError *error;
//         NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
        
//         if (error) {
//             NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
//             self.onNativeRequestFailed((BDMNativeAdRequest *)request,error);
//             return;
//         }
        
//         if (data) {
            
//             jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            
//             if (self.onNativeRequestSuccess) {
//                 self.onNativeRequestSuccess((BDMNativeAdRequest *)request, jsonString.UTF8String);
//             } else {
//                 self.onNativeRequestFailed((BDMNativeAdRequest *)request,error);
//             }
//         }
//         else
//         {
//             self.onNativeRequestFailed((BDMNativeAdRequest *)request,error);
//         }
//     }
// }


// //- (void)request:(BDMRequest *)request didExpireAd:(id<BDMAdProtocol>)adObject{
// //    if (self.onNativeRequestExpired) {
// //        self.onNativeRequestExpired((BDMNativeAdRequest *)request);
// //    }
// //}


// @end

