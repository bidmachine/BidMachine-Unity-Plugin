#if UNITY_IOS
using System;
using UnityEngine;
using BidMachineAds.Unity.Common;
using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.iOS
{
    public class iOSBidMachine : IBidMachine {
        private BidMachineiOSUnityBridge bridge;

        private BidMachineiOSUnityBridge Bridge()
        {
            return bridge ??= new BidMachineiOSUnityBridge();
        }

        public void Initialize(string sellerId)
        {
            Bridge().Initialize(sellerId);
        }

        public bool IsInitialized()
        {
            return Bridge().IsInitialized();
        }

        public void SetEndpoint(string url)
        {
            Bridge().SetEndpoint(url);
        }

        public void SetLoggingEnabled(bool logging)
        {
            Bridge().SetLoggingEnabled(logging);
        }

        public void SetTestMode(bool test)
        {
            Bridge().SetTestMode(test);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            iOSTargetingParameters parameters = iOSTargetingAdapter.Adapt(targetingParams);
            string jsonString = JsonUtility.ToJson(parameters);

            Bridge().SetTargetingParams(jsonString);
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            Bridge().SetConsentConfig(consent, consentConfig);
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            Bridge().SetSubjectToGDPR(subjectToGDPR);
        }

        public void SetCoppa(bool coppa)
        {
            Bridge().SetCoppa(coppa);
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            Bridge().SetUSPrivacyString(usPrivacyString);
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            Bridge().SetGPP(gppString, gppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            iOSPublisher iOSPublisher = iOSPublisherAdapter.Adapt(publisher);
            string jsonString = JsonUtility.ToJson(iOSPublisher);
            Bridge().SetPublisher(jsonString);
        }
    }
#endif
