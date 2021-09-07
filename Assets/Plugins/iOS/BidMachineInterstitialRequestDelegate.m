#import "BidMachineInterstitialRequestDelegate.h"

@implementation BidMachineInterstitialRequestDelegate : NSObject

- (void)request:(BDMRequest *)request failedWithError:(NSError *)error {
    if (self.onInterstitialRequestFailed) {
        self.onInterstitialRequestFailed((BDMInterstitialRequest *)request,error);
    }
}

- (void)request:(BDMRequest *)request completeWithInfo:(BDMAuctionInfo *)info {
    
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
    
    NSData *data = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:nil];
    if (data) {
        jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
    }
    const char *cString = [jsonString UTF8String];
    char *cStringCopy = calloc([jsonString length]+1, 1);
    
    char infoChar = strncpy(cStringCopy, cString, [jsonString length]);
    
    if (self.onIntersittialRequestSuccess) {
        self.onIntersittialRequestSuccess((BDMInterstitialRequest *)request, infoChar);
    }
}

- (void)requestDidExpire:(BDMRequest *)request {
    if (self.onInterstitialRequestExpired) {
        self.onInterstitialRequestExpired((BDMInterstitialRequest *)request);
    }
}

@end
