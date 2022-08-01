#pragma warning disable 0649
using System.IO;
using UnityEditor;
using System.Text.RegularExpressions;
using UnityEngine;
// using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// using System.Linq;

namespace BidMachine.Unity.Editor.Utils
{
    [System.Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]


    [InitializeOnLoad]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "RedundantJumpStatement")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class RemoveHelper
    {

        public static void RemovePlugin(bool isCleanBeforeUpdate = false)
        {
            if (EditorUtility.DisplayDialog("Remove BidMachine plugin",
                "Are you sure you want to remove the BidMachine plugin from your project?",
                "Yes",
                "Cancel"))
            {
                
                   FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "Plugins/Android"));
                    FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "Plugins/iOS"));
                    FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "BidMachine"));

                    FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "Plugins/Android" + ".meta"));
                    FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "Plugins/iOS" + ".meta"));
                    FileUtil.DeleteFileOrDirectory(Path.Combine(Application.dataPath, "BidMachine" + ".meta"));
                 
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                
            }
        }

    }
}