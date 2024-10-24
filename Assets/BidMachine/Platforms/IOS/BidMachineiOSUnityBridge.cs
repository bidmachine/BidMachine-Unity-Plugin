#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine;

public class BidMachineiOSUnityBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void BidMachineInitialize(string sellerId);

    [DllImport("__Internal")]
    private static extern bool BidMachineIsInitialized();

    [DllImport("__Internal")]
    private static extern void BidMachineSetEndpoint(string url);

    [DllImport("__Internal")]
    private static extern void BidMachineSetLoggingEnabled(bool logging);

    [DllImport("__Internal")]
    private static extern void BidMachineSetTestEnabled(bool test);

    [DllImport("__Internal")]
    private static extern void BidMachineSetTargetingParams(string jsonString);

    [DllImport("__Internal")]
    private static extern void BidMachineSetConsentConfig(string consentConfig, bool consent);

    [DllImport("__Internal")]
    private static extern void BidMachineSetSubjectToGDPR(bool flag);

    [DllImport("__Internal")]
    private static extern void BidMachineSetCoppa(bool coppa);

    [DllImport("__Internal")]
    private static extern void BidMachineSetUSPrivacyString(string usPrivacyString);

    [DllImport("__Internal")]
    private static extern void BidMachineSetGPP(string gppString, int[] gppIds, int length);

    [DllImport("__Internal")]
    private static extern void BidMachineSetPublisher(string jsonString);

    public void Initialize(string sellerId)
    {
        BidMachineInitialize(sellerId);
    }

    public bool IsInitialized()
    {
        return BidMachineIsInitialized();
    }

    public void SetEndpoint(string url)
    {
        BidMachineSetEndpoint(url);
    }

    public void SetLoggingEnabled(bool logging)
    {
        BidMachineSetLoggingEnabled(logging);
    }

    public void SetTestMode(bool test)
    {
        BidMachineSetTestEnabled(test);
    }

    public void SetTargetingParams(string jsonString)
    {
        BidMachineSetTargetingParams(jsonString);
    }

    public void SetConsentConfig(bool consent, string consentConfig)
    {
        BidMachineSetConsentConfig(consentConfig, consent);
    }

    public void SetSubjectToGDPR(bool subjectToGDPR)
    {
        BidMachineSetSubjectToGDPR(subjectToGDPR);
    }

    public void SetCoppa(bool coppa)
    {
        BidMachineSetCoppa(coppa);
    }

    public void SetUSPrivacyString(string usPrivacyString)
    {
        BidMachineSetUSPrivacyString(usPrivacyString);
    }

    public void SetGPP(string gppString, int[] gppIds)
    {
        BidMachineSetGPP(gppString, gppIds, gppIds.Length);
    } 

    public void SetPublisher(string jsonString)
    {
        BidMachineSetPublisher(jsonString);
    }    
}
#endif
