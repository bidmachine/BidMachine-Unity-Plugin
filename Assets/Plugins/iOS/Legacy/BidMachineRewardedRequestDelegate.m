// #import "BidMachineRewardedRequestDelegate.h"

// @implementation BidMachineRewardedRequestDelegate : NSObject

// - (void)request:(BDMRequest *)request failedWithError:(NSError *)error {
//     if (self.onRewardedRequestFailed) {
//         self.onRewardedRequestFailed((BDMRewardedRequest *)request,error);
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
//             self.onRewardedRequestFailed((BDMRewardedRequest *)request,error);
//             return;
//         }
        
//         if (data) {
            
//             jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            
//             if (self.onRewardedRequestSuccess) {
//                 self.onRewardedRequestSuccess((BDMRewardedRequest *)request, jsonString.UTF8String);
//             } else {
//                 self.onRewardedRequestFailed((BDMRewardedRequest *)request,error);
//             }
//         }
//         else
//         {
//             self.onRewardedRequestFailed((BDMRewardedRequest *)request,error);
//         }
//     }
// }


// //- (void)request:(BDMRequest *)request didExpireAd:(id<BDMAdProtocol>)adObject{
// //    if (self.onRewardedRequestExpired) {
// //        self.onRewardedRequestExpired((BDMRewardedRequest *)request);
// //    }
// //}


// @end
