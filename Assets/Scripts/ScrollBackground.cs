using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {

	public float speed;
	public float scrollTime;
	
	void Start()
	{
		rigidbody2D.velocity = new Vector2 (0.0f, speed);
		//StartCoroutine (RefreshBackground ());
	}

	void Update()
	{
		//while (true) 
		//{
			//yield return new WaitForSeconds(scrollTime);
			Vector3 currentPos = rigidbody2D.transform.position;
			if(currentPos.y <= -0.15f){
				currentPos.x = 0.1161837f;
				currentPos.y = 0.9660175f;
				rigidbody2D.transform.position = currentPos;
			}

		//}
	}
}
