using System.Runtime.InteropServices;
using UnityEngine;

public class BidMachineiOSUnityBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void BidMachineInitialize(string sellerId);

    [DllImport("__Internal")]
    public static extern bool BidMachineIsInitialized();

    [DllImport("__Internal")]
    public static extern void BidMachineSetEndpoint(string url);

    [DllImport("__Internal")]
    public static extern void BidMachineSetLoggingEnabled(bool logging);

    [DllImport("__Internal")]
    public static extern void BidMachineSetTestEnabled(bool test);

    [DllImport("__Internal")]
    public static extern void BidMachineSetTargetingParams(string jsonString);

    [DllImport("__Internal")]
    public static extern void BidMachineSetConsentConfig(string consentConfig, bool consent);

    [DllImport("__Internal")]
    public static extern void BidMachineSetSubjectToGDPR(bool flag);

    [DllImport("__Internal")]
    public static extern void BidMachineSetCoppa(bool coppa);

    [DllImport("__Internal")]
    public static extern void BidMachineSetUSPrivacyString(string usPrivacyString);

    [DllImport("__Internal")]
    public static extern void BidMachineSetGPP(string gppString, int[] gppIds, int length);

    [DllImport("__Internal")]
    public static extern void BidMachineSetPublisher(string jsonString);

    public static void Initialize(string sellerId)
    {
        BidMachineInitialize(sellerId);
    }

    public static bool IsInitialized()
    {
        return BidMachineIsInitialized();
    }

    public static void SetEndpoint(string url)
    {
        BidMachineSetEndpoint(url);
    }

    public static void SetLoggingEnabled(bool logging)
    {
        BidMachineSetLoggingEnabled(logging);
    }

    public static void SetTestMode(bool test)
    {
        BidMachineSetTestEnabled(test);
    }

    public static void SetTargetingParams(string jsonString)
    {
        BidMachineSetTargetingParams(jsonString);
    }

    public static void SetConsentConfig(bool consent, string consentConfig)
    {
        BidMachineSetConsentConfig(consentConfig, consent);
    }

    public static void SetSubjectToGDPR(bool subjectToGDPR)
    {
        BidMachineSetSubjectToGDPR(subjectToGDPR);
    }

    public static void SetCoppa(bool coppa)
    {
        BidMachineSetCoppa(coppa);
    }

    public static void SetUSPrivacyString(string usPrivacyString)
    {
        BidMachineSetUSPrivacyString(usPrivacyString);
    }

    public static void SetGPP(string gppString, int[] gppIds)
    {
        BidMachineSetGPP(gppString, gppIds, gppIds.Length);
    } 

    public static void SetPublisher(string jsonString)
    {
        BidMachineSetPublisher(jsonString);
    }    
}