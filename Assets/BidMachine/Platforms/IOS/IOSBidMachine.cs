using System;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBidMachine : IBidMachine {
        public void Initialize(string sellerId)
        {
            BidMachineiOSUnityBridge.Initialize(sellerId);
        }

        public bool IsInitialized()
        {
            return BidMachineiOSUnityBridge.IsInitialized();
        }

        public void SetEndpoint(string url)
        {
            BidMachineiOSUnityBridge.SetEndpoint(url);
        }

        public void SetLoggingEnabled(bool logging)
        {
            BidMachineiOSUnityBridge.SetLoggingEnabled(logging);
        }

        public void SetTestMode(bool test)
        {
            BidMachineiOSUnityBridge.SetTestMode(test);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            iOSTargetingParameters parameters = iOSTargetingAdapter.Adapt(targetingParams);
            string jsonString = JsonUtility.ToJson(parameters);

            BidMachineiOSUnityBridge.SetTargetingParams(jsonString);
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            BidMachineiOSUnityBridge.SetConsentConfig(consent, consentConfig);
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            BidMachineiOSUnityBridge.SetSubjectToGDPR(subjectToGDPR);
        }

        public void SetCoppa(bool coppa)
        {
            BidMachineiOSUnityBridge.BidMachineSetCoppa(coppa);
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            BidMachineiOSUnityBridge.SetUSPrivacyString(usPrivacyString);
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            BidMachineiOSUnityBridge.SetGPP(gppString, gppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            iOSPublisher iOSPublisher = iOSPublisherAdapter.Adapt(publisher);
            string jsonString = JsonUtility.ToJson(iOSPublisher);
            BidMachineiOSUnityBridge.SetPublisher(jsonString);
        }
    }
}