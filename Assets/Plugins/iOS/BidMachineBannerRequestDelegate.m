#import "BidMachineBannerRequestDelegate.h"

@implementation BidMachineBannerRequestDelegate

- (void)request:(BDMRequest *)request failedWithError:(NSError *)error {
    if (self.onBannerRequestFailed) {
        self.onBannerRequestFailed((BDMBannerRequest *)request,error);
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
            
            if (self.onBannerRequestSuccess) {
                self.onBannerRequestSuccess((BDMBannerRequest *)request, jsonString.UTF8String);
            }
        }
        else
        {
            if (self.onBannerRequestSuccess) {
                self.onBannerRequestSuccess((BDMBannerRequest *)request, "empty");
            }
        }
    }
}

- (void)requestDidExpire:(BDMRequest *)request {
    if (self.onBannerRequestExpired) {
        self.onBannerRequestExpired((BDMBannerRequest *)request);
    }
}

@end
