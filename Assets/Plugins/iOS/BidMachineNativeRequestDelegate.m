#import "BidMachineNativeRequestDelegate.h"

@implementation BidMachineNativeRequestDelegate : NSObject

- (void)request:(BDMRequest *)request failedWithError:(NSError *)error {
    if (self.onNativeRequestFailed) {
        self.onNativeRequestFailed((BDMNativeAdRequest *)request,error);
    }
}

- (void)request:(BDMRequest *)request completeWithInfo:(BDMAuctionInfo *)info {
    
    if(info){
        
        NSString *jsonString = @"";
        NSMutableDictionary *dictionary = [NSMutableDictionary new];
        
        dictionary[@"adDomains"] = info.adDomains;
        dictionary[@"bidID"] = info.bidID;
        dictionary[@"cID"] = info.cID;
        dictionary[@"creativeID"] = info.creativeID;
        dictionary[@"customParams"] = info.customParams;
        dictionary[@"dealID"] = info.dealID;
        dictionary[@"demandSource"] = info.demandSource;
        dictionary[@"price"] = info.price;
        
        NSError *error;
        NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
        
        if (data) {
            NSLog(@"%s: Data error: %@", __func__, error.localizedDescription);
        }
        
        if (data) {
            
            jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            
            if (self.onNativeRequestSuccess) {
                self.onNativeRequestSuccess((BDMNativeAdRequest *)request, jsonString.UTF8String);
            }
        }
        else
        {
            if (self.onNativeRequestSuccess) {
                self.onNativeRequestSuccess((BDMNativeAdRequest *)request, "empty");
            }
        }
    }
}

- (void)requestDidExpire:(BDMRequest *)request {
    if (self.onNativeRequestExpired) {
        self.onNativeRequestExpired((BDMNativeAdRequest *)request);
    }
}

@end

