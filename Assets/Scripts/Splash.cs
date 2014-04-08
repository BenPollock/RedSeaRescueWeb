using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public GUISkin customSkin = null;
	public Texture play;
	public Texture about;
	public Texture credits;
	public Texture sound;
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
		if (GUI.Button  (new Rect((int) (Screen.width/ 20) * 17, (int)(Screen.height / 20), (int)(Screen.width / 20) * 2, (int)(Screen.height / 10)), sound)){
			if(AudioListener.pause)
				AudioListener.pause = false;
			else
				AudioListener.pause = true;
		}
	}

}
