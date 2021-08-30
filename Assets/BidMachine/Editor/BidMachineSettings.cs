#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class BidMachineSettings : ScriptableObject
{
	[MenuItem("BidMachine/SDK Documentation")]
	public static void OpenDocumentation()
	{
		Application.OpenURL("https://docs.bidmachine.io/docs");
	}
}
#endif