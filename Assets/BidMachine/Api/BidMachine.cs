using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Api
{
    public sealed class BidMachine
    {
        public static int BANNER_HORIZONTAL_CENTER = 1;
        public static int BANNER_HORIZONTAL_LEFT = 3;
        public static int BANNER_HORIZONTAL_RIGHT = 5;

        public static int BANNER_VERTICAL_CENTER = 16;
        public static int BANNER_VERTICAL_TOP = 48;
        public static int BANNER_VERTICAL_BOTTOM = 80;

        private static IBidMachine client;

        private static IBidMachine GetInstance()
        {
            return client ??= BidMachineClientFactory.GetBidMachine();
        }

        public static string BIDMACHINE_UNITY_PLUGIN_VERSION = "3.1.1";

        /// <summary>
        /// Initializes BidMachine SDK.
        /// See <see cref="BidMachine.Initialize"/> for resulting triggered event.
        /// <param name="sellerId">Your Seller Id.</param>
        /// </summary>
        public static void Initialize(string id)
        {
            GetInstance().Initialize(id);
        }

        /// <summary>
        /// Checks if BidMachine SDK was initialized.
        /// See <see cref="BidMachine.IsInitialized"/> for resulting triggered event.
        /// @return {@code true} if BidMachine SDK was already initialized.
        /// </summary>
        public static bool IsInitialized()
        {
            return GetInstance().IsInitialized();
        }

        /// <summary>
        /// Sets BidMachine SDK endpoint.
        /// See <see cref="BidMachine.SetEndpoint"/> for resulting triggered event.
        /// <param name="url">BidMachine endpoint URL.</param>
        /// </summary>
        public static void SetEndpoint(string url)
        {
            GetInstance().SetEndpoint(url);
        }

        /// <summary>
        /// Sets BidMachine SDK logs enabled.
        /// See <see cref="BidMachine.SetLoggingEnabled"/> for resulting triggered event.
        /// @param enabled If {@code true} SDK will print all information about ad requests.
        /// </summary>
        public static void SetLoggingEnabled(bool enabled)
        {
            GetInstance().SetLoggingEnabled(enabled);
        }

        /// <summary>
        /// Initializes BidMachine SDK.
        /// See <see cref="BidMachine.SetTestMode"/> for resulting triggered event.
        /// @param testMode If {@code true} SDK will run in test mode.
        /// </summary>
        public static void SetTestMode(bool testMode)
        {
            GetInstance().SetTestMode(testMode);
        }

        /// <summary>
        /// Sets default {@link TargetingParams} for all ad requests.
        /// See <see cref="BidMachine.SetSubjectToGDPR"/> for resulting triggered event.
        /// <param name="targetingParams">TargetingParams object.</param>
        /// </summary>
        public static void SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.Log("BidMachine setTargetingParams");
            GetInstance().SetTargetingParams(targetingParams);
        }

        /// <summary>
        /// Sets consent config.
        /// See <see cref="BidMachine.SetConsentConfig"/> for resulting triggered event.
        /// <param name="hasConsent">User has given consent to the processing of personal data relating to him or her. https://www.eugdpr.org/.</param>
        /// <param name="consentString">GDPR consent string if applicable, complying with the comply with the IAB standard
        //                      <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/Consent%20string%20and%20vendor%20list%20formats%20v1.1%20Final.md">Consent String Format</a>
        //                      in the <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework">Transparency and Consent Framework</a> technical specifications.</param>
        /// </summary>
        public static void SetConsentConfig(bool consent, string consentString)
        {
            GetInstance().SetConsentConfig(consent, consentString);
        }

        /// <summary>
        /// Sets subject to GDPR.
        /// See <see cref="BidMachine.SetSubjectToGDPR"/> for resulting triggered event.
        /// <param name="subject">Flag indicating if GDPR regulations should be applied. <a href="https://wikipedia.org/wiki/General_Data_Protection_Regulation">The General Data Protection Regulation (GDPR)</a> is a regulation of the European Union.</param>
        /// </summary>
        public static void SetSubjectToGDPR(bool subject)
        {
            GetInstance().SetSubjectToGDPR(subject);
        }

        /// <summary>
        /// Sets subject to GDPR.
        /// See <see cref="BidMachine.SetCoppa"/> for resulting triggered event.
        /// <param name="coppa">Flag indicating if COPPA regulations should be applied. <a href="https://wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act">The Children's Online Privacy Protection Act (COPPA)</a> was established by the U.S. Federal Trade Commission..</param>
        /// </summary>
        public static void SetCoppa(bool coppa)
        {
            GetInstance().SetCoppa(coppa);
        }

        /// <summary>
        /// Sets US Privacy string.
        /// See <see cref="BidMachine.SetUSPrivacyString"/> for resulting triggered event.
        /// <param name="usPrivacyString">usPrivacyString CCPA string if applicable, complying with the comply with the IAB standard
        //                        <a href="https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md">CCPA String Format</a>.
        /// </summary>
        public static void SetUSPrivacyString(string usPrivacyString)
        {
            GetInstance().SetUSPrivacyString(usPrivacyString);
        }

        /// <summary>
        /// Sets GPP, if applicable, complying with the comply with the IAB standard <a href="https://github.com/InteractiveAdvertisingBureau/Global-Privacy-Platform/blob/main/Core/Consent%20String%20Specification.md">GPP String Format</a>.
        /// See <see cref="BidMachine.SetGPP"/> for resulting triggered event.
        /// <param name="gppString">GPP string
        /// <param name="gppIds">GPP Ids
        /// </summary>
        void SetGPP(string gppString, int[] gppIds)
        {
            GetInstance().SetGPP(gppString, gppIds);
        }

        /// <summary>
        /// Sets publisher information.
        /// See <see cref="BidMachine.SetPublisher"/> for resulting triggered event.
        /// <param name="publisher">Publisher object which contains all information about publisher.</param>
        /// </summary>
        public static void SetPublisher(Publisher publisher)
        {
            GetInstance().SetPublisher(publisher);
        }
    }
}
