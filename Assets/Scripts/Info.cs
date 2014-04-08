using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {
	public Texture quit;
	
	public void OnGUI() 
	{
		int buttonWidth = 100;
		int buttonHeight = 100;
		
		GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 8) *1, Screen.width - 200, (int)(Screen.height / 8)), quit))
		{
			Application.LoadLevel ("splash");
		}
	}
}
