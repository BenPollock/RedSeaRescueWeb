using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip newMusic;

	void Awake(){
		GameObject go = GameObject.Find ("Game Music");
		if (newMusic != go.audio.clip) {
						go.audio.clip = newMusic;
						go.audio.Play ();
		}
	}
}
