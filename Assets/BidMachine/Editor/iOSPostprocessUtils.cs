#if UNITY_IOS && UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using Debug = UnityEngine.Debug;

#pragma warning disable 618

namespace BidMachineAds.Unity.Editor.iOS
{
    public class iOSPostprocessUtils : MonoBehaviour
    {
        private const string suffix = ".framework";
        private const string minVersionToDisableBitcode = "14.0";

        [PostProcessBuild(41)]
        public static void UpdateInfoPlist(BuildTarget buildTarget, string buildPath)
        {
            var path = Path.Combine(buildPath, "Info.plist");

            AddNSAppTransportSecurity();
        }

        private static void AddKeyToPlist(string path, string key, string value)
        {
            var plist = new PlistDocument();
            plist.ReadFromFile(path);
            plist.root.SetString(key, value);
            File.WriteAllText(path, plist.WriteToString());
        }

        private static bool CheckContainsKey(string path, string key)
        {
            string contentString;
            using (var reader = new StreamReader(path))
            {
                contentString = reader.ReadToEnd();
                reader.Close();
            }

            return contentString.Contains(key);
        }

        private static void AddNSAppTransportSecurity()
        {
            if (!PlayerSettings.iOS.allowHTTPDownload)
            {
                PlayerSettings.iOS.allowHTTPDownload = true;
            }
        }

        private static void ReplaceInFile(
            string filePath, string searchText, string replaceText)
        {
            string contentString;
            using (var reader = new StreamReader(filePath))
            {
                contentString = reader.ReadToEnd();
                reader.Close();
            }

            contentString = Regex.Replace(contentString, searchText, replaceText);

            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(contentString);
                writer.Close();
            }
        }

        private static readonly string[] frameworkList =
        {
            "AdSupport",
            "AudioToolbox",
            "AVFoundation",
            "CFNetwork",
            "CoreFoundation",
            "CoreGraphics",
            "CoreImage",
            "CoreLocation",
            "CoreMedia",
            "CoreMotion",
            "CoreTelephony",
            "CoreText",
            "EventKitUI",
            "EventKit",
            "GLKit",
            "ImageIO",
            "JavaScriptCore",
            "MediaPlayer",
            "MessageUI",
            "MobileCoreServices",
            "QuartzCore",
            "SafariServices",
            "Security",
            "Social",
            "StoreKit",
            "SystemConfiguration",
            "Twitter",
            "UIKit",
            "VideoToolbox",
            "WatchConnectivity",
            "WebKit",
            "AppTrackingTransparency"
        };

        private static readonly string[] weakFrameworkList =
        {
            "CoreMotion",
            "WebKit",
            "Social"
        };

        private static readonly string[] platformLibs =
        {
            "libc++.dylib",
            "libz.dylib",
            "libsqlite3.dylib",
            "libxml2.2.dylib"
        };

        public static void PrepareProject(string buildPath)
        {
            Debug.Log("Preparing your Xcode project for additional frameworks");
            var projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();

            project.ReadFromString(File.ReadAllText(projectPath));

            var target = GetTargetNameOfProject(project);

            AddProjectFrameworks(frameworkList, project, target, false);
            AddProjectFrameworks(weakFrameworkList, project, target, true);
            AddProjectLibs(platformLibs, project, target);

            project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");

            var xcodeVersion = GetXcodeVersion();
            if (xcodeVersion == null || CompareVersions(xcodeVersion, minVersionToDisableBitcode) >= 0)
            {
                project.SetBuildProperty(project.ProjectGuid(), "ENABLE_BITCODE", "NO");
            }

            project.AddBuildProperty(target, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");
            project.AddBuildProperty(target, "LIBRARY_SEARCH_PATHS", "$(TOOLCHAIN_DIR)/usr/lib/swift/$(PLATFORM_NAME)");

#if UNITY_2019_3_OR_NEWER
                project.AddBuildProperty(target, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
#else
                project.AddBuildProperty(target, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
#endif

            project.AddBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
            project.SetBuildProperty(target, "SWIFT_VERSION", "5.5");

            File.WriteAllText(projectPath, project.WriteToString());
        }

        private static void AddProjectFrameworks(IEnumerable<string> frameworks, PBXProject project, string target,
            bool weak)
        {
            foreach (var framework in frameworks)
            {
                if (!project.ContainsFramework(target, framework))
                {
                    project.AddFrameworkToProject(target, framework + suffix, weak);
                }
            }
        }

        private static void AddProjectLibs(IEnumerable<string> libs, PBXProject project, string target)
        {
            foreach (var lib in libs)
            {
                var libGUID = project.AddFile("usr/lib/" + lib, "Libraries/" + lib, PBXSourceTree.Sdk);
                project.AddFileToBuild(target, libGUID);
            }
        }

        private static void CopyAndReplaceDirectory(string srcPath, string dstPath)
        {
            if (Directory.Exists(dstPath))
            {
                Directory.Delete(dstPath);
            }

            if (File.Exists(dstPath))
            {
                File.Delete(dstPath);
            }

            Directory.CreateDirectory(dstPath);

            foreach (var file in Directory.GetFiles(srcPath))
            {
                if (!file.Contains(".meta"))
                {
                    File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));
                }
            }

            foreach (var dir in Directory.GetDirectories(srcPath))
            {
                CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
            }
        }

        private static bool CheckiOSAttribute()
        {
            var adMobConfigPath = Path.Combine(Application.dataPath,
                "Appodeal/Editor/NetworkConfigs/GoogleAdMobDependencies.xml");

            XDocument config;
            try
            {
                config = XDocument.Load(adMobConfigPath);
            }
            catch (IOException exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }

            var elementConfigDependencies = config.Element("dependencies");
            if (elementConfigDependencies == null)
            {
                return false;
            }

            if (!elementConfigDependencies.HasElements)
            {
                return false;
            }

            var elementiOSPods = elementConfigDependencies.Element("iosPods");
            if (elementiOSPods == null)
            {
                return false;
            }

            if (!elementiOSPods.HasElements)
            {
                return false;
            }

            var elementiOSPod = elementiOSPods.Element("iosPod");
            if (elementiOSPod == null)
            {
                return false;
            }

            if (!elementiOSPod.HasAttributes)
            {
                return false;
            }

            var attributeElementiOSPod = elementiOSPod.Attribute("name");

            if (attributeElementiOSPod == null)
            {
                return false;
            }

            return attributeElementiOSPod.Value.Equals("APDGoogleAdMobAdapter");
        }

        private static string GetXcodeVersion()
        {
            string profilerOutput = null;
            try
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo("system_profiler", "SPDeveloperToolsDataType | grep \"Xcode:\"")
                    {
                        CreateNoWindow = false, RedirectStandardOutput = true, UseShellExecute = false
                    }
                };
                p.Start();
                p.WaitForExit();
                profilerOutput = p.StandardOutput.ReadToEnd();
                var re = new Regex(@"Xcode: (?<version>\d+(\.\d+)+)");
                var m = re.Match(profilerOutput);
                if (m.Success) profilerOutput = m.Groups["version"].Value;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            return profilerOutput;
        }

        private static string GetTargetNameOfProject(PBXProject project)
        {
#if UNITY_2019_3_OR_NEWER
                return project.GetUnityMainTargetGuid();
#else
                return project.TargetGuidByName("Unity-iPhone");
#endif
        }

        private static int CompareVersions(string v1, string v2)
        {
            var re = new Regex(@"\d+(\.\d+)+");
            var match1 = re.Match(v1);
            var match2 = re.Match(v2);
            return new Version(match1.ToString()).CompareTo(new Version(match2.ToString()));
        }
    }
}
#endif