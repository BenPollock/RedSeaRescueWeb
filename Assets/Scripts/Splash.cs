using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public GUISkin customSkin = null;

	public void OnGUI() 
	{
		if(customSkin!= null)
			GUI.skin = customSkin;

		int buttonWidth = 100;
		int buttonHeight = 100;

		if (GUI.Button (new Rect (Screen.width -300, Screen.height - 100, 100, 100), "Play"))
		{
			Application.LoadLevel ("main");
		}
	}

}
