using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public GUISkin customSkin = null;
	public Texture play;
	public Texture about;
	public Texture credits;
	//public GUISkin test;

	public void OnGUI() 
	{
		GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 8) *4, Screen.width - 200, (int)(Screen.height / 8)), play))
		{
			Application.LoadLevel ("main");
		}
		if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 8) *3, Screen.width - 200, (int)(Screen.height / 8)), about))
		{
			Application.LoadLevel ("info");
		}
		if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 8) *2, Screen.width - 200, (int)(Screen.height / 8)), credits))
		{
			Application.LoadLevel ("about");
		}
	}

}
