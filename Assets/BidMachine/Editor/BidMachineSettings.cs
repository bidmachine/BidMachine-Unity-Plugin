#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BidMachineAds.Unity.Editor.Utils
{
    public class BidMachineSettings : ScriptableObject
    {
        [MenuItem("BidMachine/SDK Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://docs.bidmachine.io/docs");
        }

        [MenuItem("BidMachine/Remove plugin")]
        public static void RemoveBidmachinePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}
#endif
