#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidBidMachine : IBidMachine
    {
        private AndroidJavaClass JavaBidMachineClass;

        private AndroidJavaClass getBidMachineClass()
        {
            return JavaBidMachineClass
                ?? (JavaBidMachineClass = new AndroidJavaClass("io.bidmachine.BidMachine"));
        }

        public void Initialize(string sellerId)
        {
            if (string.IsNullOrEmpty(sellerId))
            {
                return;
            }

            getBidMachineClass()
                .CallStatic(
                    "initialize",
                    AndroidUtils.GetActivity(),
                    AndroidUtils.GetJavaPrimitiveObject(sellerId)
                );
        }

        public bool IsInitialized()
        {
            return getBidMachineClass().CallStatic<bool>("isInitialized");
        }

        public void SetEndpoint(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            getBidMachineClass()
                .CallStatic("setEndpoint", AndroidUtils.GetJavaPrimitiveObject(url));
        }

        public void SetLoggingEnabled(bool logging)
        {
            getBidMachineClass().CallStatic("setLoggingEnabled", logging);
        }

        public void SetTestMode(bool testMode)
        {
            getBidMachineClass().CallStatic("setTestMode", testMode);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null)
            {
                return;
            }

            getBidMachineClass()
                .CallStatic(
                    "setTargetingParams",
                    new AndroidTargetingParams(targetingParams).JavaObject
                );
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            if (string.IsNullOrEmpty(consentConfig))
            {
                return;
            }

            getBidMachineClass()
                .CallStatic(
                    "setConsentConfig",
                    consent,
                    AndroidUtils.GetJavaPrimitiveObject(consentConfig)
                );
        }

        public void SetSubjectToGDPR(bool subjectToGDPR)
        {
            getBidMachineClass()
                .CallStatic("setSubjectToGDPR", AndroidUtils.GetJavaPrimitiveObject(subjectToGDPR));
        }

        public void SetCoppa(bool coppa)
        {
            getBidMachineClass().CallStatic("setCoppa", AndroidUtils.GetJavaPrimitiveObject(coppa));
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            if (string.IsNullOrEmpty(usPrivacyString))
            {
                return;
            }

            getBidMachineClass()
                .CallStatic(
                    "setUSPrivacyString",
                    AndroidUtils.GetJavaPrimitiveObject(usPrivacyString)
                );
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            if (string.IsNullOrEmpty(gppString))
            {
                return;
            }

            var clientGppIds = AndroidUtils.GetJavaListObject(gppIds);

            getBidMachineClass()
                .CallStatic("setGPP", AndroidUtils.GetJavaPrimitiveObject(gppString), clientGppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                return;
            }

            var publisherBuilder = new AndroidJavaObject("io.bidmachine.Publisher$Builder");
            publisherBuilder.Call<AndroidJavaObject>(
                "setId",
                AndroidUtils.GetJavaPrimitiveObject(publisher.Id)
            );
            publisherBuilder.Call<AndroidJavaObject>(
                "setName",
                AndroidUtils.GetJavaPrimitiveObject(publisher.Name)
            );
            publisherBuilder.Call<AndroidJavaObject>(
                "setDomain",
                AndroidUtils.GetJavaPrimitiveObject(publisher.Domain)
            );
            var categories = AndroidUtils.GetJavaListObject(publisher.Categories);
            publisherBuilder.Call<AndroidJavaObject>("addCategories", categories);
            var androidPublisher = publisherBuilder.Call<AndroidJavaObject>("build");

            getBidMachineClass().CallStatic("setPublisher", androidPublisher);
        }

        public bool CheckAndroidPermissions(string permission)
        {
            if (string.IsNullOrEmpty(permission))
            {
                return false;
            }

            var flag = false;
            switch (permission)
            {
                case Permission.CoarseLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
                case Permission.FineLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
            }

            return flag;
        }

        public void RequestAndroidPermissions()
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
}

#endif
