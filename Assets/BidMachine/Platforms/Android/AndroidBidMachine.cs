#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBidMachine : IBidMachine
    {
        private AndroidJavaClass jClass;

        private AndroidJavaClass GetBidMachineClass()
        {
            return jClass ??= new AndroidJavaClass("io.bidmachine.BidMachine");
        }

        public void Initialize(string sellerId)
        {
            if (string.IsNullOrEmpty(sellerId))
            {
                return;
            }

            GetBidMachineClass()
                .CallStatic("initialize", AndroidNativeConverter.GetActivity(), sellerId);
        }

        public bool IsInitialized()
        {
            return GetBidMachineClass().CallStatic<bool>("isInitialized");
        }

        public void SetEndpoint(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            GetBidMachineClass().CallStatic("setEndpoint", url);
        }

        public void SetLoggingEnabled(bool logging)
        {
            GetBidMachineClass().CallStatic("setLoggingEnabled", logging);
        }

        public void SetTestMode(bool testMode)
        {
            GetBidMachineClass().CallStatic("setTestMode", testMode);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null)
            {
                return;
            }

            GetBidMachineClass()
                .CallStatic(
                    "setTargetingParams",
                    AndroidNativeConverter.GetTargetingParams(targetingParams)
                );
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            if (string.IsNullOrEmpty(consentConfig))
            {
                return;
            }

            GetBidMachineClass().CallStatic("setConsentConfig", consent, consentConfig);
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            GetBidMachineClass()
                .CallStatic("setSubjectToGDPR", AndroidNativeConverter.GetObject(subjectToGDPR));
        }

        public void SetCoppa(bool coppa)
        {
            GetBidMachineClass().CallStatic("setCoppa", AndroidNativeConverter.GetObject(coppa));
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            if (string.IsNullOrEmpty(usPrivacyString))
            {
                return;
            }

            GetBidMachineClass().CallStatic("setUSPrivacyString", usPrivacyString);
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            if (string.IsNullOrEmpty(gppString))
            {
                return;
            }

            var clientGppIds = AndroidNativeConverter.GetArrayList(gppIds);

            GetBidMachineClass().CallStatic("setGPP", gppString, clientGppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                return;
            }

            GetBidMachineClass()
                .CallStatic("setPublisher", AndroidNativeConverter.GetPublisher(publisher));
        }
    }
}
#endif
