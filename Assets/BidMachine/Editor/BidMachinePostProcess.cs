#if UNITY_IPHONE
using UnityEditor;

using UnityEditor.Callbacks;

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