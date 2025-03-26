#if UNITY_IOS && UNITY_EDITOR
using System.Diagnostics.CodeAnalysis;
using BidMachineAds.Unity.Editor.iOS;
using UnityEditor;

using UnityEditor.Callbacks;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class BidMachinePostProcess
{
    [PostProcessBuild(100)]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        if (target.ToString() == "iOS" || target.ToString() == "iPhone")
        {
            iOSPostprocessUtils.PrepareProject(path);
        }
    }
}
#endif