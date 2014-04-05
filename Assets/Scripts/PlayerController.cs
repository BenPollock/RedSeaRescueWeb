using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public Boundary boundary;

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * speed;

		rigidbody2D.transform.position = new Vector2 (Mathf.Clamp (rigidbody2D.transform.position.x, boundary.xMin + 0.35f, boundary.xMax - 0.35f), Mathf.Clamp (rigidbody2D.transform.position.y, boundary.yMin, boundary.yMax));
	
	}

}
