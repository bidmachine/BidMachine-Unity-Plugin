using BidMachineAds.Unity.Api;

namespace BidMachineAds.Unity.Common
{
    public interface IBidMachine
    {
        void Initialize(string sellerId);

        bool IsInitialized();

        void SetEndpoint(string url);

        void SetLoggingEnabled(bool logging);

        void SetTestMode(bool test);

        void SetTargetingParams(TargetingParams targetingParams);

        void SetConsentConfig(bool consent, string consentConfig);

        void SetConsentStatus(bool consent);

        void SetSubjectToGDPR(bool subjectToGDPR);

        void SetCoppa(bool coppa);

        void SetUSPrivacyString(string usPrivacyString);

        void SetGPP(string gppString, int[] gppIds);

        void SetNonPersonalized(bool nonPersonalized);

        void SetPublisher(Publisher publisher);
    }
}
