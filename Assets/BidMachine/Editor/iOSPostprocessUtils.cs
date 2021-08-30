using UnityEngine;
using System.IO;
using Unity.BidMachine.Xcode;


namespace BidMachineAds.Unity.Editor.iOS
{
    public class iOSPostprocessUtils
    {
        static string suffix = ".framework";
        static string absoluteProjPath;

        static string[] frameworkList = {"AdSupport", "StoreKit", "SafariServices", "SystemConfiguration", "CoreTelephony" };
        static string[] platformLibs = {"libxml2.2.dylib" };

        public static void PrepareProject(string buildPath)
        {
            Debug.Log("preparing your xcode project for BidMachine");
            string projPath = Path.Combine(buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
            absoluteProjPath = Path.GetFullPath(buildPath);
            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projPath));
            string target = project.TargetGuidByName("Unity-iPhone");

            AddProjectFrameworks(frameworkList, project, target, false);
            AddProjectLibs(platformLibs, project, target);
            project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");

            File.WriteAllText(projPath, project.WriteToString());
        }

        protected static void AddProjectFrameworks(string[] frameworks, PBXProject project, string target, bool weak)
        {
            foreach (string framework in frameworks)
            {
                if (!project.ContainsFramework(target, framework))
                {
                    project.AddFrameworkToProject(target, framework + suffix, weak);
                }
            }
        }

        protected static void AddProjectLibs(string[] libs, PBXProject project, string target)
        {
            foreach (string lib in libs)
            {
                string libGUID = project.AddFile("usr/lib/" + lib, "Libraries/" + lib, PBXSourceTree.Sdk);
                project.AddFileToBuild(target, libGUID);
            }
        }
    }
}
