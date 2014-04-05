using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {

	public float speed;

	
	
	// Update is called once per frame
	void Update () {
	
		renderer.material.mainTextureOffset = new Vector2(0f, Time.time * speed);
	}
}
