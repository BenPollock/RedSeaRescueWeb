using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public GUISkin customSkin = null;
	public Texture play;
	//public GUISkin test;

	public void OnGUI() 
	{
	//	if(customSkin!= null)
		//	GUI.skin = customSkin;

		int buttonWidth = 100;
		int buttonHeight = 100;

		GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (Screen.width -300, Screen.height - 200, 250, 100), play))
		{
			Application.LoadLevel ("main");
		}
	}

}
