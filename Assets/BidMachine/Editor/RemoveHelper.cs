#if UNITY_EDITOR
#pragma warning disable 0649
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace BidMachineAds.Unity.Editor.Utils
{
    [System.Serializable]
    [InitializeOnLoad]
    public class RemoveHelper
    {
        public static void RemovePlugin(bool isCleanBeforeUpdate = false)
        {
            if (
                EditorUtility.DisplayDialog(
                    "Remove BidMachine plugin",
                    "Are you sure you want to remove the BidMachine plugin from your project?",
                    "Yes",
                    "Cancel"
                )
            )
            {
                FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "BidMachine"));
                FileUtil.DeleteFileOrDirectory(
                    Path.Combine(Application.dataPath, "BidMachine" + ".meta")
                );

                new List<string>(Directory.GetFiles("Assets/Plugins/iOS")).ForEach(file =>
                {
                    Regex re = new Regex("bidmachine", RegexOptions.IgnoreCase);
                    if (re.IsMatch(file))
                        File.Delete(file);
                    File.Delete(file + ".meta");
                });
                new List<string>(Directory.GetFiles("Assets/Plugins/Android")).ForEach(file =>
                {
                    Regex re = new("bidmachine", RegexOptions.IgnoreCase);
                    if (re.IsMatch(file))
                        File.Delete(file);
                    File.Delete(file + ".meta");
                });

                Directory.Delete("Assets/Plugins/Android/bidmachine.androidlib", true);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
    }
}
#endif
