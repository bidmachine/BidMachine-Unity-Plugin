#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using BidMachineAds.Unity.Common;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[InitializeOnLoad]
public class BidMachineSettings : ScriptableObject
{
	[MenuItem("BidMachine/SDK Documentation")]
	public static void OpenDocumentation()
	{
		string url = "https://wiki.appodeal.com/display/BID/BidMachine";
		Application.OpenURL(url);
	}
	
	[MenuItem("BidMachine/Appodeal Homepage")]
	public static void OpenAppodealHome()
	{
		string url = "http://www.appodeal.com";
		Application.OpenURL(url);
	}

}
#endif